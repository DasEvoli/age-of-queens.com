using ageofqueenscom.Code.JsonClasses;
using ageofqueenscom.Code;
using ageofqueenscom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ageofqueenscom.Controllers
{
    public class TwitchController : Controller
    {
        private IConfiguration configuration;
        public TwitchController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        public IActionResult Index()
        {
            string clientId = configuration.GetSection("Twitch").GetSection("ClientId").Value;

            TwitchViewModel model = new TwitchViewModel();
            TwitchAccessToken accessToken = Twitch.GetAccessToken(clientId, configuration.GetSection("Twitch").GetSection("ClientSecret").Value);
            if (accessToken != null)
            {
                model.TwitchTeam = Twitch.GetTwitchTeam("ageofqueens", clientId, accessToken.AccessToken);
            }

            if (model.TwitchTeam != null)
            {
                // Get all ids from every member in Twitch Team
                List<string> MemberIds = new List<string>();
                foreach (TwitchTeamMember item in model.TwitchTeam.TwitchTeamMemberList)
                {
                    MemberIds.Add(item.UserId);
                }
                model.TwitchStreamsList = Twitch.GetStreams(MemberIds, clientId, accessToken.AccessToken);
                model.TwitchUserList = Twitch.GetUsers(MemberIds, clientId, accessToken.AccessToken);
            }

                

            return View(model);
        }
    }
}
