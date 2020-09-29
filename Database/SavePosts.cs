using System;
using System.Data.SQLite;

namespace pa3_agcrofoot_1.Database
{
    public class SavePosts : ISeedPosts, ISavePosts
    {
        public void SavePost(Posts BigAlsPosts)
        {
            string cs = @"URI = file:C:\Users\birdc\source\repos\pa3-agcrofoot-1\posts.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = @"INSERT INTO BigAlsPosts(id, text, timestamp) VALUES(@id, @text, @timestamp)";
            cmd.Parameters.AddWithValue("@id", BigAlsPosts.ID);
            cmd.Parameters.AddWithValue("@text", BigAlsPosts.Text);
            cmd.Parameters.AddWithValue("@timestamp", BigAlsPosts.Timestamp);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        
        public void SeedPosts()
        {
            string cs = @"URI = file:C:\Users\birdc\source\repos\pa3-agcrofoot-1\posts.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = "DROP TABLE IF EXISTS BigAlsPosts";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE BigAlsPosts(id INTEGER PRIMARY KEY, text TEXT, timestamp TEXT)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"INSERT INTO BigAlsPosts(text, timestamp) VALUES(@text, @timestamp)";
            cmd.Parameters.AddWithValue("@text", "Hey this is a test text");
            cmd.Parameters.AddWithValue("@timestamp", "9/25/2020 5:00:45 PM");
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"INSERT INTO BigAlsPosts(text, timestamp) VALUES(@text, @timestamp)";
            cmd.Parameters.AddWithValue("@text", "Hey this is a second test text");
            cmd.Parameters.AddWithValue("@timestamp", "9/25/2020 6:15:25 PM");
            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }

        public void CreateTable()
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

        public void DeletePosts()
        {
            string cs = @"URI = file:C:\Users\birdc\source\repos\pa3-agcrofoot-1\posts.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = @"DELETE FROM BigAlsPosts";

        }
    }
}