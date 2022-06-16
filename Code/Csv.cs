using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using Ageofqueenscom.Models;

namespace Ageofqueenscom.Code
{
    public class Csv
    {
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
    }
}
