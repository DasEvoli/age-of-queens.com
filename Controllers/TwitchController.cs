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
        string ClientId = null;
        string ClientSecret = null;
        TwitchViewModel.Json.AccessToken AccessToken = null;
        readonly string BaseTokenUrl = "https://id.twitch.tv/oauth2/token";
        readonly string BaseApiUrl = "https://api.twitch.tv/helix";
        readonly IHttpClientFactory HttpClientFactory;

        public TwitchController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            ClientId = configuration["TWITCH_CLIENT_ID"];
            ClientSecret = configuration["TWITCH_CLIENT_SECRET"];
            HttpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = HttpClientFactory.CreateClient();
            TwitchViewModel model = new TwitchViewModel();
            UpdateAccessToken();    // TODO: We could make the loading much faster if we generate the AccessToken only when it's expired and store it
            if (AccessToken != null) 
            {
                model.Team = await GetTwitchTeam("ageofqueens", AccessToken.Value);
                // Can be executed parallel. They depend on Twitch Team data.
                Task[] tasks= new Task[2];
                tasks[0] = Task.Run(async() => model.StreamsList = await GetStreams(model.Team.TeamMemberList, AccessToken.Value));
                tasks[1] = Task.Run(async() => model.UserList = await GetUsers(model.Team.TeamMemberList, AccessToken.Value));
                Task.WaitAll(tasks);
            }
            return View(model);
        }

        public void UpdateAccessToken()
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
                    AccessToken = JsonConvert.DeserializeObject<TwitchViewModel.Json.AccessToken>(responseString);
                }
            }
            catch (Exception e)
            {
                Log.Write(e);
            }
        }

        public async Task<TwitchViewModel.Json.Team> GetTwitchTeam(string team, string accessToken)
        {
            TwitchViewModel.Json.Team twitchTeam = null;
            try
            {
                string url = $"{BaseApiUrl}/teams?name={team}";
                HttpClient httpClient = HttpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
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

        public async Task<List<TwitchViewModel.Json.Stream>> GetStreams(List<TwitchViewModel.Json.TeamMember> members, string accessToken)
        {
            List<TwitchViewModel.Json.Stream> streams = null;
            try
            {
                string idQuery = "user_id=" + members[0].UserId;
                for (int i = 1; i < members.Count; i++) idQuery += "&user_id=" + members[i].UserId;
                string url = $"{BaseApiUrl}/streams?{idQuery}";

                HttpClient httpClient = HttpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
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

        public async Task<List<TwitchViewModel.Json.User>> GetUsers(List<TwitchViewModel.Json.TeamMember> members, string accessToken)
        {
            List<TwitchViewModel.Json.User> users = null;
            try
            {
                string idQuery = "id=" + members[0].UserId;
                for (int i = 1; i < members.Count; i++) idQuery += "&id=" + members[i].UserId;
                string url = $"{BaseApiUrl}/users?{idQuery}";

                HttpClient httpClient = HttpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
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

    }
}
