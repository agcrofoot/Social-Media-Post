using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SQLite;

namespace pa3_agcrofoot_1.Database
{
    public class ReadPost : IReadAllPosts
    {
        //Retrieves the posts from the file
        public List<Posts> GetPosts()
        {
            List<Posts> BigAlsPosts = new List<Posts>();
            string cs = @"URI = file:C:\Users\birdc\source\repos\pa3-agcrofoot-1\posts.db";
            using var con = new SQLiteConnection(cs);
            con.Open();
            string stm = "SELECT * FROM BigAlsPosts";
            using var cmd = new SQLiteCommand(stm, con);

            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                Posts temp = new Posts(){ID = rdr.GetInt32(0), Text = rdr.GetString(1), Timestamp = rdr.GetString(2)};
                BigAlsPosts.Add(temp);
            }

            return BigAlsPosts;
               
        }
    }
}