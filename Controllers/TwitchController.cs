using System;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Ageofqueenscom.Models;

namespace Ageofqueenscom.Controllers
{
    public class TwitchController : Controller
    {
        private IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private string _accessToken;
        private readonly string _baseTokenUrl = "https://id.twitch.tv/oauth2/token";
        private readonly string _baseApiUrl = "https://api.twitch.tv/helix";
        private readonly string _validationUrl = "https://id.twitch.tv/oauth2/validate";

        public TwitchController(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<TwitchController> logger)
        {
            _configuration = configuration;  // Needs to be initialized so we can set values in the configuration file.
            _logger = logger;
            _clientId = configuration["TWITCH_CLIENT_ID"];
            _clientSecret = configuration["TWITCH_CLIENT_SECRET"];
            _accessToken = configuration["TWITCH_ACCESS_TOKEN"];
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            TwitchViewModel model = new TwitchViewModel();
            if(!IsAccessTokenValid(_accessToken)){
                if(!UpdateAccessToken()) View(model);
            }
            model.Team = await GetTeam("ageofqueens");
            if(model.Team != null)
            {
                Task[] tasks= new Task[2];  // Can be executed parallel. They depend on Twitch Team.
                tasks[0] = Task.Run(async() => model.StreamsList = await GetStreams(model.Team.TeamMemberList));
                tasks[1] = Task.Run(async() => model.UserList = await GetUsers(model.Team.TeamMemberList));
                Task.WaitAll(tasks);
            }
            return View(model);
        }

        // Gets the Twitch Team. Teams are a way to unite and connect streamers together on Twitch.
        public async Task<TwitchViewModel.Json.Team> GetTeam(string teamName)
        {
            TwitchViewModel.Json.Team team = null;
            try
            {
                string url = $"{_baseApiUrl}/teams?name={teamName}";
                using HttpClient httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                httpClient.DefaultRequestHeaders.Add("Client-Id", _clientId);
                HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
                if(response.IsSuccessStatusCode){
                    HttpContent content = response.Content;
                    StreamReader reader = new StreamReader(await content.ReadAsStreamAsync());
                    string responseString = await reader.ReadToEndAsync();
                    TwitchViewModel.Json.TeamList teamList = JsonConvert.DeserializeObject<TwitchViewModel.Json.TeamList>(responseString);
                    team = teamList.Data[0];
                }
                return team;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }
        }

        // Gets all online streams from the Twitch Team.
        public async Task<List<TwitchViewModel.Json.Stream>> GetStreams(List<TwitchViewModel.Json.TeamMember> members)
        {
            List<TwitchViewModel.Json.Stream> streams = null;
            try
            {
                string idQuery = "user_id=" + members[0].UserId;
                for (int i = 1; i < members.Count; i++) idQuery += "&user_id=" + members[i].UserId;
                string url = $"{_baseApiUrl}/streams?{idQuery}";

                using HttpClient httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                httpClient.DefaultRequestHeaders.Add("Client-Id", _clientId);
                HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
                if(response.IsSuccessStatusCode){
                    HttpContent content = response.Content;
                    StreamReader reader = new StreamReader(await content.ReadAsStreamAsync());
                    string responseString = await reader.ReadToEndAsync();
                    streams = JsonConvert.DeserializeObject<TwitchViewModel.Json.StreamList>(responseString).Data;
                }
                return streams;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }
        }

        // Gets all users from the Twitch Team.
        public async Task<List<TwitchViewModel.Json.User>> GetUsers(List<TwitchViewModel.Json.TeamMember> members)
        {
            List<TwitchViewModel.Json.User> users = null;
            try
            {
                string idQuery = "id=" + members[0].UserId;
                for (int i = 1; i < members.Count; i++) idQuery += "&id=" + members[i].UserId;
                string url = $"{_baseApiUrl}/users?{idQuery}";

                using HttpClient httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
                httpClient.DefaultRequestHeaders.Add("Client-Id", _clientId);
                HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
                if(response.IsSuccessStatusCode){
                    HttpContent content = response.Content;
                    StreamReader reader = new StreamReader(await content.ReadAsStreamAsync());
                    string responseString = await reader.ReadToEndAsync();
                    users = JsonConvert.DeserializeObject<TwitchViewModel.Json.UserList>(responseString).Data;
                }
                return users;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }
        }

        public bool IsAccessTokenValid(string accessToken)
        {
            try
            {
                using HttpClient httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                HttpResponseMessage response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, _validationUrl));
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return false;
            }
        }

        // Updates the AccessToken in appsettings.json and initializes accesstoken member variable.
        public bool UpdateAccessToken()
        {
            try
            {  
                string url = $"{_baseTokenUrl}?client_id={_clientId}&client_secret={_clientSecret}&grant_type=client_credentials";
                using HttpClient httpClient = _httpClientFactory.CreateClient();
                HttpResponseMessage response = httpClient.Send(new HttpRequestMessage(HttpMethod.Post, url));
                if(response.IsSuccessStatusCode){
                    HttpContent content = response.Content;
                    StreamReader reader = new StreamReader(content.ReadAsStream());
                    string responseString = reader.ReadToEnd();
                    _accessToken = JsonConvert.DeserializeObject<TwitchViewModel.Json.AccessToken>(responseString).Value;
                    _configuration["TWITCH_ACCESS_TOKEN"] = _accessToken;
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return false;
            }
        }
    }
}
