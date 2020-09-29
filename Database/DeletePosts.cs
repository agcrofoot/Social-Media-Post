using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SQLite;

namespace pa3_agcrofoot_1.Database
{
    public class DeletePosts : IDeletePost
    {
        public void DeletePost()
        {
            string cs = @"URI = file:C:\Users\birdc\source\repos\pa3-agcrofoot-1\posts.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = "DROP TABLE IF EXISTS BigAlsPosts";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE BigAlsPosts(id INTEGER PRIMARY KEY, text TEXT, timestamp TEXT)";
            cmd.ExecuteNonQuery();
            
        }
    }
}
