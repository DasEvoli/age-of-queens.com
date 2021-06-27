using ageofqueenscom.Code.JsonClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ageofqueenscom.Code
{
    public static class Twitch
    {
        private static readonly string Host = "https://id.twitch.tv/oauth2/token";

        public static TwitchAccessToken GetAccessToken(string clientId, string clientSecret)
        {
            try
            {
                string url = Host + "?client_id=" + clientId + "&client_secret=" + clientSecret + "&grant_type=" + "client_credentials";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using Stream stream = response.GetResponseStream();
                using StreamReader reader = new StreamReader(stream);
                string responseString = reader.ReadToEnd();

                TwitchAccessToken accessToken = JsonConvert.DeserializeObject<TwitchAccessToken>(responseString);
                return accessToken;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static TwitchTeam GetTwitchTeam(string team, string clientId, string accessToken)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.twitch.tv/helix/teams?name=" + team);
                request.Headers["Authorization"] = "Bearer " + accessToken;
                request.Headers["Client-Id"] = clientId;
                using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using Stream stream = response.GetResponseStream();
                using StreamReader reader = new StreamReader(stream);
                string responseString = reader.ReadToEnd();

                TwitchTeamList twitchTeamList = JsonConvert.DeserializeObject<TwitchTeamList>(responseString);
                return twitchTeamList.Data[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<TwitchStream> GetStreams(List<string> ids, string clientId, string accessToken)
        {
            try
            {
                string query = "user_id=" + ids[0];

                for (int i = 1; i < ids.Count; i++)
                {
                    query += "&user_id=" + ids[i];
                }

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.twitch.tv/helix/streams?" + query);
                request.Headers["Authorization"] = "Bearer " + accessToken;
                request.Headers["Client-Id"] = clientId;
                using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using Stream stream = response.GetResponseStream();
                using StreamReader reader = new StreamReader(stream);
                string responseString = reader.ReadToEnd();

                TwitchStreamList list = JsonConvert.DeserializeObject<TwitchStreamList>(responseString);
                return list.Data;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static List<TwitchUser> GetUsers(List<string> ids, string clientId, string accessToken)
        {
            try
            {
                string query = "id=" + ids[0];

                for (int i = 1; i < ids.Count; i++)
                {
                    query += "&id=" + ids[i];
                }

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.twitch.tv/helix/users?" + query);
                request.Headers["Authorization"] = "Bearer " + accessToken;
                request.Headers["Client-Id"] = clientId;
                using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using Stream stream = response.GetResponseStream();
                using StreamReader reader = new StreamReader(stream);
                string responseString = reader.ReadToEnd();

                TwitchUserList list = JsonConvert.DeserializeObject<TwitchUserList>(responseString);
                return list.Data;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
