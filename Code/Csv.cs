using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Ageofqueenscom.Models;

namespace Ageofqueenscom.Code
{
    public class Csv
    {
        public static List<IntroductionsViewModel.Introduction> LoadIntroductions()
        {
            string path = "Csv/introductions.csv";
            using TextFieldParser csvReader = new TextFieldParser(Path.Combine(Directory.GetCurrentDirectory(), path));
            csvReader.CommentTokens = new string[] { "#" };
            csvReader.SetDelimiters(new string[] { "," });

            List< IntroductionsViewModel.Introduction> list = new List< IntroductionsViewModel.Introduction>();
            while (!csvReader.EndOfData)
            {
                IntroductionsViewModel.Introduction item = new  IntroductionsViewModel.Introduction();
                string[] fields = csvReader.ReadFields();
                item.Name = fields[1];
                item.Description = fields[2];
                item.ImageUrl = fields[3];
                item.TwitterUrl = fields[4];
                item.YoutubeUrl = fields[5];
                item.TwitchUrl = fields[6];
                item.InstagramUrl = fields[7];
                list.Add(item);
            }
            return list;
        }
        
        public static List<HomeViewModel.Blogpost> LoadBlogposts()
        {
            string path = "Csv/blog_posts.csv";
            using TextFieldParser csvReader = new TextFieldParser(Path.Combine(Directory.GetCurrentDirectory(), path));
            csvReader.CommentTokens = new string[] { "#" };
            csvReader.SetDelimiters(new string[] { "," });

            List<HomeViewModel.Blogpost> blogpostList = new List<HomeViewModel.Blogpost>();
            while (!csvReader.EndOfData)
            {
                string[] fields = csvReader.ReadFields();
                HomeViewModel.Blogpost blogpost = new HomeViewModel.Blogpost
                {
                    Title = fields[0],
                    Content = fields[1],
                    ImageUrl = fields[2],
                    Author = fields[3],
                    Created = fields[4]
                };
                blogpostList.Add(blogpost);
            }
            blogpostList.Reverse(); // So the newest Blogposts are on the top while we still can edit the csv from top to bottom.
            return blogpostList;
        }

        public static List<LeaderboardViewModel.LeaderboardPlayer> LoadLeaderboardRM()
        {
            string path = "Csv/leaderboard_rm.csv";
            using TextFieldParser csvReader = new TextFieldParser(Path.Combine(Directory.GetCurrentDirectory(), path));
            csvReader.CommentTokens = new string[] { "#" };
            csvReader.SetDelimiters(new string[] { "," });

            List<LeaderboardViewModel.LeaderboardPlayer> playerList = new List<LeaderboardViewModel.LeaderboardPlayer>();
            csvReader.ReadLine();   // We don't have write access to the file so we need to skip lines which are not commented
            csvReader.ReadLine();
            csvReader.ReadLine();
            csvReader.ReadLine();
            csvReader.ReadLine();
            csvReader.ReadLine();
            csvReader.ReadLine();
            while (!csvReader.EndOfData)
            {
                string[] fields = csvReader.ReadFields();
                LeaderboardViewModel.LeaderboardPlayer player = new LeaderboardViewModel.LeaderboardPlayer();
                if (String.IsNullOrEmpty(fields[1])) break; // We don't have write access to the file and many lines are not filled but still there
                player.Name = fields[1];
                player.Country = fields[2];
                player.Id = int.Parse(fields[3]);
                player.Rating = int.Parse(fields[5]);
                player.HighestRating = int.Parse(fields[6]);
                playerList.Add(player);
            }
            return playerList;
        }

        public static List<ModsViewModel.Mod> LoadMods()
        {
            string path = "Csv/mods.csv";
            using TextFieldParser csvReader = new TextFieldParser(Path.Combine(Directory.GetCurrentDirectory(), path));
            csvReader.CommentTokens = new string[] { "#" };
            csvReader.SetDelimiters(new string[] { "," });

            List<ModsViewModel.Mod> modList = new List<ModsViewModel.Mod>();
            while (!csvReader.EndOfData)
            {
                string[] fields = csvReader.ReadFields();
                ModsViewModel.Mod mod = new ModsViewModel.Mod
                {
                    Title = fields[0],
                    Description = fields[1],
                    Creator = fields[2],
                    Date = fields[3],
                    Id = fields[4],
                    Category = fields[5],
                    ImageUrl = fields[6],
                    ModUrl = "https://www.ageofempires.com/mods/details/" + fields[4]
                };
                modList.Add(mod);
            }
            return modList;
        }

        public static ActiveEventViewModel LoadActiveEvent(){
            string path = "Csv/current_event_data.csv";
            using TextFieldParser csvReader = new TextFieldParser(Path.Combine(Directory.GetCurrentDirectory(), path));
            csvReader.CommentTokens = new string[] { "#" };
            csvReader.SetDelimiters(new string[] { "," });

            ActiveEventViewModel model = new ActiveEventViewModel();
            model.ActiveGameEventGameList = new List<ActiveEventViewModel.ActiveEventGame>();                
            
            // Sets the meta data for the model.
            string[] fields = csvReader.ReadFields();
            model.Title = fields[0];
            model.Information = fields[1];
            model.RegistrationLink = fields[2];
            model.Image = fields[3];
            
            // Reads every game in the event and pushes it into a list in the model.
            while (!csvReader.EndOfData)
            {
                fields = csvReader.ReadFields();
                ActiveEventViewModel.ActiveEventGame gameModel = new ActiveEventViewModel.ActiveEventGame(){
                    Date = fields[0],
                    ActiveEventTeams = new List<string>(),
                    Maps = fields[9],
                    Mode = fields[10],
                    Information = fields[11]
                };
                // Adds the teams. We can have 8 teams max.
                for(int i = 1; i < 9; i++){
                    if(!String.IsNullOrEmpty(fields[i])) gameModel.ActiveEventTeams.Add(fields[i]);
                }
                model.ActiveGameEventGameList.Add(gameModel);
            }
            return model;
        }
    }
}
