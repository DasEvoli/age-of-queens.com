using System;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using ageofqueenscom.Code;
using ageofqueenscom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ageofqueenscom.Controllers
{
    public class TwitchController : Controller
    {
        public IConfiguration Configuration;
        public string ClientId = null;
        public string ClientSecret = null;
        public string AccessToken = null;
        public readonly string BaseTokenUrl = "https://id.twitch.tv/oauth2/token";
        public readonly string BaseApiUrl = "https://api.twitch.tv/helix";
        public readonly string ValidationUrl = "https://id.twitch.tv/oauth2/validate";
        public readonly IHttpClientFactory HttpClientFactory;

        public TwitchController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            Configuration = configuration;
            ClientId = configuration["TWITCH_CLIENT_ID"];
            ClientSecret = configuration["TWITCH_CLIENT_SECRET"];
            AccessToken = configuration["TWITCH_ACCESS_TOKEN"];
            HttpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            TwitchViewModel model = new TwitchViewModel();
            if(!IsAccessTokenValid(AccessToken)){
                if(!UpdateAccessToken()) View(model);           // TODO: Return an error message
            }
            model.Team = await GetTwitchTeam("ageofqueens");
            // Can be executed parallel. They depend on Twitch Team data.
            Task[] tasks= new Task[2];
            tasks[0] = Task.Run(async() => model.StreamsList = await GetStreams(model.Team.TeamMemberList));
            tasks[1] = Task.Run(async() => model.UserList = await GetUsers(model.Team.TeamMemberList));
            Task.WaitAll(tasks);
            return View(model);
        }

        public async Task<TwitchViewModel.Json.Team> GetTwitchTeam(string team)
        {
            TwitchViewModel.Json.Team twitchTeam = null;
            try
            {
                string url = $"{BaseApiUrl}/teams?name={team}";
                HttpClient httpClient = HttpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {AccessToken}");
                httpClient.DefaultRequestHeaders.Add("Client-Id", ClientId);
                HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
                if(response.IsSuccessStatusCode){
                    HttpContent content = response.Content;
                    StreamReader reader = new StreamReader(await content.ReadAsStreamAsync());
                    string responseString = await reader.ReadToEndAsync();
                    TwitchViewModel.Json.TeamList twitchTeamList = JsonConvert.DeserializeObject<TwitchViewModel.Json.TeamList>(responseString);
                    twitchTeam = twitchTeamList.Data[0];
                }
                return twitchTeam;
            }
            catch (Exception e)
            {
                Log.Write(e);
                return null;
            }
        }

        public async Task<List<TwitchViewModel.Json.Stream>> GetStreams(List<TwitchViewModel.Json.TeamMember> members)
        {
            List<TwitchViewModel.Json.Stream> streams = null;
            try
            {
                string idQuery = "user_id=" + members[0].UserId;
                for (int i = 1; i < members.Count; i++) idQuery += "&user_id=" + members[i].UserId;
                string url = $"{BaseApiUrl}/streams?{idQuery}";

                HttpClient httpClient = HttpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {AccessToken}");
                httpClient.DefaultRequestHeaders.Add("Client-Id", ClientId);
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
                Log.Write(e);
                return null;
            }
        }

        public async Task<List<TwitchViewModel.Json.User>> GetUsers(List<TwitchViewModel.Json.TeamMember> members)
        {
            List<TwitchViewModel.Json.User> users = null;
            try
            {
                string idQuery = "id=" + members[0].UserId;
                for (int i = 1; i < members.Count; i++) idQuery += "&id=" + members[i].UserId;
                string url = $"{BaseApiUrl}/users?{idQuery}";

                HttpClient httpClient = HttpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {AccessToken}");
                httpClient.DefaultRequestHeaders.Add("Client-Id", ClientId);
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
                Log.Write(e);
                return null;
            }
        }

        public bool IsAccessTokenValid(string accessToken)
        {
            try
            {
                HttpClient httpClient = HttpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                HttpResponseMessage response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ValidationUrl));
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Log.Write(e);
                return false;
            }
        }

        public bool UpdateAccessToken()
        {
            try
            {  
                string url = BaseTokenUrl + "?client_id=" + ClientId + "&client_secret=" + ClientSecret + "&grant_type=" + "client_credentials";
                HttpClient httpClient = HttpClientFactory.CreateClient();
                HttpResponseMessage response = httpClient.Send(new HttpRequestMessage(HttpMethod.Post, url));
                if(response.IsSuccessStatusCode){
                    HttpContent content = response.Content;
                    StreamReader reader = new StreamReader(content.ReadAsStream());
                    string responseString = reader.ReadToEnd();
                    AccessToken = JsonConvert.DeserializeObject<TwitchViewModel.Json.AccessToken>(responseString).Value;
                    Configuration["TWITCH_ACCESS_TOKEN"] = AccessToken;
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                Log.Write(e);
                return false;
            }
        }
    }
}
