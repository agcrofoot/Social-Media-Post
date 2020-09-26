using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fall_2020_starter_code
{
    public class PostFile
    {
        //Retrieves the posts from the file
        public static List<Posts> GetPosts()
        {
            List<Posts> BigAlsPosts = new List<Posts>();
            StreamReader inFile = null;
            try
            {
                //Tries the given path
                inFile = new StreamReader(@"C:\Users\birdc\source\repos\PA1\posts.txt");
            }
            catch
            {
                //Presents if the file is not found
                Console.WriteLine("Something went wrong.");
            }

            string line = inFile.ReadLine();
            while(line != null)
            {
                //Separates the values by '#'
                string[] temp = line.Split("#");
                //Parses the post ID
                int id = int.Parse(temp[0]);
                //Adds them to BigAlsPosts list
                BigAlsPosts.Add(new Posts(){ID = id, Text = temp[1], Timestamp = temp[2]});
                line = inFile.ReadLine();
            }
            //Closes the file
            inFile.Close();

            return BigAlsPosts;
        }

        //Saves a new post to the file
        public static void SavePost(Posts newPost)
        {
            StreamWriter outFile = File.AppendText(@"C:\Users\birdc\source\repos\PA1\posts.txt");
            outFile.WriteLine(newPost.ToFile());
            outFile.Close();
        }

        //Saves the whole set of posts to the file
        public static void Save(List<Posts> BigAlsPosts)
        {
            StreamWriter outFile = new StreamWriter(@"C:\Users\birdc\source\repos\PA1\posts.txt");
            foreach(Posts post in BigAlsPosts)
            {
                outFile.WriteLine(post.ToFile());
            }
            outFile.Close();
        }
    }
}