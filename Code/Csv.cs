using ageofqueenscom.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;

namespace ageofqueenscom.Code
{
    public static class Csv
    {
        public static List< IntroductionsViewModel.IntroductionModel> LoadIntroductions()
        {
            List< IntroductionsViewModel.IntroductionModel> list = new List< IntroductionsViewModel.IntroductionModel>();

            string path = @"Csv/Introductions.csv";
            try
            {
                using TextFieldParser csvReader = new TextFieldParser(Path.Combine(Directory.GetCurrentDirectory(), path));
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { "," });

                // Skip one Row (Change later)
                csvReader.ReadFields();

                // Read every row
                while (!csvReader.EndOfData)
                {
                    string[] fields = csvReader.ReadFields();
                    IntroductionsViewModel.IntroductionModel item = new  IntroductionsViewModel.IntroductionModel();
                    item.Name = fields[1];
                    item.Description = fields[2];
                    item.ImageUrl = fields[3];
                    item.TwitterUrl = fields[4];
                    item.YoutubeUrl = fields[5];
                    item.TwitchUrl = fields[6];
                    item.InstagramUrl = fields[7];
                    list.Add(item);
                }
            }
            catch (Exception e)
            {
                Log.Write(e);
                return null;
            }

            return list;
        }
        public static List<HomeViewModel.BlogpostModel> LoadBlogposts()
        {
            List<HomeViewModel.BlogpostModel> blogpostList = new List<HomeViewModel.BlogpostModel>();

            string path = @"Csv/BlogPosts.csv";

            try
            {
                using TextFieldParser csvReader = new TextFieldParser(Path.Combine(Directory.GetCurrentDirectory(), path));
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { "," });
                while (!csvReader.EndOfData)
                {
                    string[] fields = csvReader.ReadFields();
                    HomeViewModel.BlogpostModel blogpost = new HomeViewModel.BlogpostModel
                    {
                        Title = fields[0],
                        Content = fields[1],
                        ImageUrl = fields[2],
                        Author = fields[3],
                        Created = fields[4]
                    };
                    blogpostList.Add(blogpost);
                }
                blogpostList.Reverse();
            }
            catch (Exception e)
            {
                Log.Write(e);
                return null;
            }

            return blogpostList;
        }

        public static List<LeaderboardViewModel.LeaderboardPlayerModel> LoadLeaderboardRM()
        {
            List<LeaderboardViewModel.LeaderboardPlayerModel> playerList = new List<LeaderboardViewModel.LeaderboardPlayerModel>();
            string path = @"Csv/LeaderboardRM.csv";
            try
            {
                using TextFieldParser csvReader = new TextFieldParser(Path.Combine(Directory.GetCurrentDirectory(), path));
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.ReadLine();
                csvReader.ReadLine();
                csvReader.ReadLine();
                csvReader.ReadLine();
                csvReader.ReadLine();
                csvReader.ReadLine();
                csvReader.ReadLine();
                while (!csvReader.EndOfData)
                {
                    string[] fields = csvReader.ReadFields();
                    LeaderboardViewModel.LeaderboardPlayerModel player = new LeaderboardViewModel.LeaderboardPlayerModel();
                    if (String.IsNullOrEmpty(fields[1])) break;
                    player.Name = fields[1];
                    player.Country = fields[2];
                    player.Id = int.Parse(fields[3]);
                    player.Rating = int.Parse(fields[5]);
                    player.HighestRating = int.Parse(fields[6]);
                    playerList.Add(player);
                }
            }
            catch (Exception e)
            {
                Log.Write(e);
                return null;
            }
            return playerList;
        }

        public static List<ModsViewModel.ModModel> LoadMods()
        {
            List<ModsViewModel.ModModel> modList = new List<ModsViewModel.ModModel>();

            string path = @"Csv/Mods.csv";

            try
            {
                using TextFieldParser csvReader = new TextFieldParser(Path.Combine(Directory.GetCurrentDirectory(), path));
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { "," });
                while (!csvReader.EndOfData)
                {
                    string[] fields = csvReader.ReadFields();
                    ModsViewModel.ModModel mod = new ModsViewModel.ModModel
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
            }
            catch (Exception e)
            {
                Log.Write(e);
                return null;
            }

            return modList;
        }

        public static ActiveEventViewModel InitializeActiveEvent(){
            string path = @"Csv/Current_Event_Data.csv";
            try
            {
                using TextFieldParser csvReader = new TextFieldParser(Path.Combine(Directory.GetCurrentDirectory(), path));
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { "," });
                ActiveEventViewModel model = new ActiveEventViewModel();
                model.ActiveGameEventGameList = new List<ActiveEventViewModel.ActiveEventGameModel>();
                while (!csvReader.EndOfData)
                {
                    string[] fields = csvReader.ReadFields();
                    if(csvReader.LineNumber == 3){
                        model.Title = fields[0];
                        model.Information = fields[1];
                        model.RegistrationLink = fields[2];
                        model.Image = fields[3];
                        continue;
                    }

                    ActiveEventViewModel.ActiveEventGameModel gameModel = new ActiveEventViewModel.ActiveEventGameModel(){
                        Date = fields[0],
                        ActiveEventTeams = new List<string>(),
                        Maps = fields[9],
                        Mode = fields[10],
                        Information = fields[11]
                    };
                    for(int i = 1; i < 9; i++){
                        if(!String.IsNullOrEmpty(fields[i])) gameModel.ActiveEventTeams.Add(fields[i]);
                    }
                    model.ActiveGameEventGameList.Add(gameModel);
                }
                return model;
            }
            catch (Exception e)
            {
                Log.Write(e);
                return null;
            }
        }
    }
}
