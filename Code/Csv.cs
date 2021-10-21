using ageofqueenscom.Code;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;

namespace ageofqueenscom.Code
{
    public static class Csv
    {

        public static List<IntroductionModel> LoadIntroductions()
        {
            List<IntroductionModel> list = new List<IntroductionModel>();

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
                    IntroductionModel item = new IntroductionModel();
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

        public static SeedSoloTableModel LoadSeedSoloTable(int firstCol)
        {
            SeedSoloTableModel table = new SeedSoloTableModel();

            string path = @"Csv/SoloSeed.csv";
            try
            {
                using TextFieldParser csvReader = new TextFieldParser(Path.Combine(Directory.GetCurrentDirectory(), path));
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { "," });

                table.ColNames = new List<string>();
                table.Rows = new List<SeedSoloRowModel>();

                // Skip one Row
                csvReader.ReadFields();

                // Read the cols
                string[] cols = csvReader.ReadFields();
                table.ColNames.Add(cols[firstCol]);
                table.ColNames.Add(cols[firstCol + 1]);
                table.ColNames.Add(cols[firstCol + 2]);
                table.ColNames.Add(cols[firstCol + 3]);


                // Read every row
                while (!csvReader.EndOfData)
                {
                    string[] fields = csvReader.ReadFields();
                    SeedSoloRowModel seed = new SeedSoloRowModel();
                    if (String.IsNullOrEmpty(fields[firstCol])) break;
                    seed.SeedNumber = fields[firstCol];
                    seed.PlayerName = fields[firstCol + 1];
                    seed.SeedingElo = fields[firstCol + 2];
                    seed.AoeNetUrl = fields[firstCol + 3];
                    table.Rows.Add(seed);
                }
            }
            catch (Exception e)
            {
                Log.Write(e);
                return null;
            }

            return table;
        }

        public static SeedTeamTableModel LoadSeedTeamTable(int firstCol)
        {
            SeedTeamTableModel table = new SeedTeamTableModel();

            string path = @"Csv/TeamSeed.csv";
            try
            {
                using TextFieldParser csvReader = new TextFieldParser(Path.Combine(Directory.GetCurrentDirectory(), path));
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { "," });

                table.ColNames = new List<string>();
                table.Rows = new List<SeedTeamRowModel>();

                // Read the cols
                string[] cols = csvReader.ReadFields();
                table.ColNames.Add(cols[firstCol]);
                table.ColNames.Add(cols[firstCol + 1]);
                table.ColNames.Add(cols[firstCol + 2]);
                table.ColNames.Add(cols[firstCol + 3]);
                //table.ColNames.Add(cols[firstCol + 4]);
                table.ColNames.Add(cols[firstCol + 5]);
                //table.ColNames.Add(cols[firstCol + 6]);

                // Read every row
                while (!csvReader.EndOfData)
                {
                    string[] fields = csvReader.ReadFields();
                    SeedTeamRowModel seed = new SeedTeamRowModel();
                    if (String.IsNullOrEmpty(fields[firstCol])) break;
                    seed.SeedNumber = fields[firstCol];
                    seed.TeamName = fields[firstCol + 1];
                    seed.TeamElo = fields[firstCol + 2];
                    seed.PlayerOneName = fields[firstCol + 3];
                    seed.PlayerOneAoeNetUrl = fields[firstCol + 4];
                    seed.PlayerTwoName = fields[firstCol + 5];
                    seed.PlayerTwoAoeNetUrl = fields[firstCol + 6];
                    table.Rows.Add(seed);
                }
            }
            catch (Exception e)
            {
                Log.Write(e);
                return null;
            }

            return table;
        }

        public static CastRosterTableModel LoadRosterTable()
        {
            CastRosterTableModel rosterTable = new CastRosterTableModel();

            string path = @"Csv/CastingRoster.csv";
            try
            {
                using TextFieldParser csvReader = new TextFieldParser(Path.Combine(Directory.GetCurrentDirectory(), path));
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { "," });

                rosterTable.RosterCols = new List<string>();
                rosterTable.RosterRows = new List<CastRoasterRowModel>();

                csvReader.ReadLine();
                csvReader.ReadLine();
                csvReader.ReadLine();

                // Read the cols
                string[] cols = csvReader.ReadFields();
                rosterTable.RosterCols.Add(cols[0]);
                rosterTable.RosterCols.Add(cols[1]);
                rosterTable.RosterCols.Add(cols[2]);
                rosterTable.RosterCols.Add(cols[3]);
                rosterTable.RosterCols.Add(cols[4]);
                //rosterTable.RosterCols.Add(cols[5]);
                //rosterTable.RosterCols.Add(cols[6]);
                rosterTable.RosterCols.Add(cols[7]);
                //rosterTable.RosterCols.Add(cols[8]);
                //rosterTable.RosterCols.Add(cols[9]);
                rosterTable.RosterCols.Add(cols[10]);
                rosterTable.RosterCols.Add(cols[11]);

                // Read every row
                while (!csvReader.EndOfData)
                {
                    string[] fields = csvReader.ReadFields();
                    CastRoasterRowModel row = new CastRoasterRowModel
                    {
                        Round = fields[0],
                        GameDate = fields[1],
                        GameTime = fields[2],
                        PlayerName = fields[3],
                        StreamerName = fields[4],
                        StreamerUrl = fields[5],
                        StreamerLanguage = fields[6],
                        CasterName = fields[7],
                        CasterUrl = fields[8],
                        CasterLanguage = fields[9],
                        CastDay = fields[10],
                        CastTime = fields[11]
                    };
                    rosterTable.RosterRows.Add(row);
                }
            }
            catch (Exception e)
            {
                Log.Write(e);
                return null;
            }

            return rosterTable;
        }

        public static List<BlogpostModel> LoadBlogposts()
        {
            List<BlogpostModel> blogpostList = new List<BlogpostModel>();

            string path = @"Csv/BlogPosts.csv";

            try
            {
                using TextFieldParser csvReader = new TextFieldParser(Path.Combine(Directory.GetCurrentDirectory(), path));
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { "," });
                while (!csvReader.EndOfData)
                {
                    string[] fields = csvReader.ReadFields();
                    BlogpostModel blogpost = new BlogpostModel
                    {
                        Title = fields[0],
                        Content = fields[1],
                        ImageUrl = fields[2],
                        Author = fields[3],
                        Created = fields[4]
                    };
                    blogpostList.Add(blogpost);
                }
            }
            catch (Exception e)
            {
                Log.Write(e);
                return null;
            }

            return blogpostList;
        }

        public static List<LeaderboardPlayerModel> LoadLeaderboardRM()
        {
            List<LeaderboardPlayerModel> playerList = new List<LeaderboardPlayerModel>();
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
                    LeaderboardPlayerModel player = new LeaderboardPlayerModel();
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

        public static List<ModModel> LoadMods()
        {
            List<ModModel> modList = new List<ModModel>();

            string path = @"Csv/Mods.csv";

            try
            {
                using TextFieldParser csvReader = new TextFieldParser(Path.Combine(Directory.GetCurrentDirectory(), path));
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { "," });
                while (!csvReader.EndOfData)
                {
                    string[] fields = csvReader.ReadFields();
                    ModModel mod = new ModModel
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

    }
}
