using System;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using ageofqueenscom.Code.JsonClasses;
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
        TwitchAccessToken AccessToken = null;   // TODO: Make one AccessToken Class or even Interface TODO: Also: Test if Token is invalid. If it is, generate a new one and store it in somewhere
        readonly string BaseTokenUrl = "https://id.twitch.tv/oauth2/token";
        readonly string BaseApiUrl = "https://api.twitch.tv/helix";
        readonly IHttpClientFactory HttpClientFactory;

        public TwitchController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            ClientId = configuration["TWITCH_CLIENT_ID"];
            ClientSecret = configuration["TWITCH_CLIENT_SECRET"];
            HttpClientFactory = httpClientFactory;
            UpdateAccessToken();
        }
        public IActionResult Index()
        {
            //TODO: Create httpclient async so that the site does not lag when trying to open it.
            HttpClient httpClient = HttpClientFactory.CreateClient();
            TwitchViewModel model = new TwitchViewModel();
            if (AccessToken != null) // TODO: Maybe show that no access token was generated
            {
                model.TwitchTeam = GetTwitchTeam("ageofqueens", AccessToken);
                model.TwitchStreamsList = GetStreams(model.TwitchTeam.TwitchTeamMemberList, AccessToken);
                model.TwitchUserList = GetUsers(model.TwitchTeam.TwitchTeamMemberList, AccessToken); // TODO: Refactor name
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
                    AccessToken = JsonConvert.DeserializeObject<TwitchAccessToken>(responseString);
                }
            }
            catch (Exception e)
            {
                Log.Write(e);
            }
        }
        public TwitchTeam GetTwitchTeam(string team, TwitchAccessToken accessToken) // TODO: Find better way accessing token
        {
            TwitchTeam twitchTeam = null;
            try
            {
                string url = $"{BaseApiUrl}/teams?name={team}";
                HttpClient httpClient = HttpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken.AccessToken}");
                httpClient.DefaultRequestHeaders.Add("Client-Id", ClientId);
                HttpResponseMessage response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, url));
                if(response.IsSuccessStatusCode){
                    HttpContent content = response.Content;
                    StreamReader reader = new StreamReader(content.ReadAsStream());
                    string responseString = reader.ReadToEnd();
                    TwitchTeamList twitchTeamList = JsonConvert.DeserializeObject<TwitchTeamList>(responseString);
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
        public List<TwitchStream> GetStreams(List<TwitchTeamMember> members, TwitchAccessToken accessToken)
        {
            List<TwitchStream> streams = null;
            try
            {
                string idQuery = "user_id=" + members[0].UserId;
                for (int i = 1; i < members.Count; i++) idQuery += "&user_id=" + members[i].UserId;
                string url = $"{BaseApiUrl}/streams?{idQuery}";

                HttpClient httpClient = HttpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken.AccessToken}");
                httpClient.DefaultRequestHeaders.Add("Client-Id", ClientId);
                HttpResponseMessage response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, url));
                if(response.IsSuccessStatusCode){
                    HttpContent content = response.Content;
                    StreamReader reader = new StreamReader(content.ReadAsStream());
                    string responseString = reader.ReadToEnd();
                    streams = JsonConvert.DeserializeObject<TwitchStreamList>(responseString).Data;
                }
                return streams;
            }
            catch (Exception e)
            {
                Log.Write(e);
                return null;
            }
        } 
        public List<TwitchUser> GetUsers(List<TwitchTeamMember> members, TwitchAccessToken accessToken)
        {
            List<TwitchUser> users = null;
            try
            {
                string idQuery = "id=" + members[0].UserId;
                for (int i = 1; i < members.Count; i++) idQuery += "&id=" + members[i].UserId;
                string url = $"{BaseApiUrl}/users?{idQuery}";

                HttpClient httpClient = HttpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken.AccessToken}");
                httpClient.DefaultRequestHeaders.Add("Client-Id", ClientId);
                HttpResponseMessage response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, url));
                if(response.IsSuccessStatusCode){
                    HttpContent content = response.Content;
                    StreamReader reader = new StreamReader(content.ReadAsStream());
                    string responseString = reader.ReadToEnd();
                    users = JsonConvert.DeserializeObject<TwitchUserList>(responseString).Data;
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
