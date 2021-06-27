using Newtonsoft.Json;
using System.Collections.Generic;

namespace ageofqueenscom.Code.JsonClasses
{


    public class TwitchAccessToken
    {
        [JsonProperty("access_token")]
        public string AccessToken;
        [JsonProperty("refresh_token")]
        public string RefreshToken;
        [JsonProperty("expires_in")]
        public int ExpiresIn;
        [JsonProperty("scope")]
        public string[] Scope;
        [JsonProperty("token_type")]
        public string TokenType;
    }

    public class TwitchUserList
    {
        [JsonProperty("data")]
        public List<TwitchUser> Data;
    }

    public class TwitchUser
    {
        [JsonProperty("broadcaster_type")]
        public string BroadcasterType;
        [JsonProperty("description")]
        public string Description;
        [JsonProperty("display_name")]
        public string DisplayName;
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("login")]
        public string Login;
        [JsonProperty("offline_image_url")]
        public string OfflineImageUrl;
        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl;
        [JsonProperty("type")]
        public string Type;
        [JsonProperty("view_count")]
        public int ViewCount;
        [JsonProperty("created_at")]
        public string CreatedAt;
    }

    public class TwitchTeamList
    {
        [JsonProperty("data")]
        public List<TwitchTeam> Data;
    }

    public class TwitchTeam
    {
        [JsonProperty("users")]
        public List<TwitchTeamMember> TwitchTeamMemberList;
        [JsonProperty("background_image_url")]
        public string BackgroundImageUrl;
        [JsonProperty("banner")]
        public string Banner;
        [JsonProperty("created_at")]
        public string CreatedAt;
        [JsonProperty("updated_at")]
        public string UpdatedAt;
        [JsonProperty("info")]
        public string Info;
        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl;
        [JsonProperty("team_name")]
        public string TeamName;
        [JsonProperty("team_display_name")]
        public string TeamDisplayName;
        [JsonProperty("id")]
        public string Id;
    }

    public class TwitchTeamMember
    {
        [JsonProperty("user_id")]
        public string UserId;
        [JsonProperty("user_name")]
        public string UserName;
        [JsonProperty("user_login")]
        public string UserLogin;
    }

    public class TwitchStreamList
    {
        [JsonProperty("data")]
        public List<TwitchStream> Data;
    }

    public class TwitchStream
    {
        [JsonProperty("id")]
        public string Id;
        [JsonProperty("user_id")]
        public string UserId;
        [JsonProperty("user_login")]
        public string UserLogin;
        [JsonProperty("user_name")]
        public string UserName;
        [JsonProperty("game_id")]
        public string GameId;
        [JsonProperty("game_name")]
        public string GameName;
        [JsonProperty("type")]
        public string Type;
        [JsonProperty("title")]
        public string Title;
        [JsonProperty("viewer_count")]
        public int ViewerCount;
        [JsonProperty("started_at")]
        public string StartedAt;
        [JsonProperty("language")]
        public string Language;
        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl;
        [JsonProperty("tag_ids")]
        public string[] TagIds;
        [JsonProperty("is_mature")]
        public bool IsMature;
    }
}

