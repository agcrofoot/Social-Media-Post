using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace fall_2020_starter_code
{
    class Program
    {
        static void Main(string[] args)
        {
            //Allows code to loop back
            Menu();
        }
        public static void Menu()
        {
            Console.Clear();
            int menuChoice = 0;
            while(menuChoice != 5)
            {
                //Presents menu options
                Console.WriteLine("Enter '1' to show all posts.");
                Console.WriteLine("Enter '2' to add a post.");
                Console.WriteLine("Enter '3' to delete a post.");
                Console.WriteLine("Enter '4' to reseed the database.");
                Console.WriteLine("Enter '5' to exit.");
                try
                {
                    //Tries to parse the input and check if it is within the boundaries
                    menuChoice = int.Parse(Console.ReadLine());
                    if(menuChoice < 1 || menuChoice > 5)
                    {
                        throw new Exception("Please enter a valid input.");
                    }
                }
                catch(Exception e)
                {
                    //Presents error message 
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to return to the Menu.");
                    Console.ReadKey();
                    Menu();
                }
                finally
                {
                    //If the input goes through, it is then routed
                    //If the input was '1', the current posts are displayed
                    if(menuChoice == 1)
                    {
                        Console.Clear();
                        List<Posts> BigAlsPosts = PostFile.GetPosts();
                        BigAlsPosts.Sort();
                        foreach(Posts post in BigAlsPosts)
                        {
                            Console.WriteLine("Post ID " + post.ID + " " + post.Text + " " + post.Timestamp);
                        }

                        Console.WriteLine("Press any key to return to the Menu.");
                        Console.ReadKey();
                        Menu();
                    }
                    //If the input was '2', the user is prompted to add their new post
                    else if(menuChoice == 2)
                    {
                        Console.Clear();
                        List<Posts> BigAlsPosts = PostFile.GetPosts();
                        //If the list comes back from the file empty. 
                        if(BigAlsPosts.Count == 0)
                        {
                            Console.WriteLine("Enter your post.");
                            //the ID is set to '1' automatically.
                            Posts newPost = new Posts(){ID = 1, Text = Console.ReadLine(), Timestamp = DateTime.Now.ToString()};
                            Console.WriteLine("Post ID " + newPost.ID + " " + newPost.Text + " " + newPost.Timestamp);
                            PostFile.SavePost(newPost);
                        }
                        //If there are already items in the list.
                        else
                        {
                            //This list compiles the postIDs into one list so that the code can reference it easier.
                            List<int> postIDs = new List<int>();
                            //The loop runs through the BigAlsPosts list and adds the postIDs to the postIDs list.
                            foreach(Posts post in BigAlsPosts)
                            {
                                postIDs.Add(post.ID);
                            }
                            //Sets the maximum number assigned as a post ID as the lastPost
                            int lastPost = postIDs.Max();
                            //This adds 1 to the most recent post ID, and sets it as the current postID
                            int currentPost = lastPost + 1;
                            Console.WriteLine("Enter your post.");
                            Posts newPost = new Posts(){ID = currentPost, Text = Console.ReadLine(), Timestamp = DateTime.Now.ToString()};
                            Console.WriteLine("Post ID " + newPost.ID + " " + newPost.Text + " " + newPost.Timestamp);
                            BigAlsPosts.Sort();
                            PostFile.SavePost(newPost);

                        }
                        
                        Console.WriteLine("Press any key to return to the Menu.");
                        Console.ReadKey();
                        Menu();
           
                    }
                    //If the input was '3', the user is prompted to delete a post.
                    else if(menuChoice == 3)
                    {
                        Console.Clear();
                        List<Posts> BigAlsPosts = PostFile.GetPosts();
                        //If the list comes back from the file empty.
                        if(BigAlsPosts.Count == 0)
                        {
                            Console.WriteLine("There are no posts to delete.");
                        }
                        else
                        {
                            BigAlsPosts.Sort();
                            List<int> postIDs = new List<int>();
                            //Adds the postIDs to the postID list and displays them to the user
                            foreach(Posts post in BigAlsPosts)
                            {
                                Console.WriteLine("Post ID " + post.ID + " " + post.Text + " " + post.Timestamp);
                                postIDs.Add(post.ID);
                            }
                            while(true)
                            {
                                Console.WriteLine("Enter the ID of the post you wish to delete.");
                                try
                                {
                                    //Tries to parse the input, sets the maximum ID as the lastPost and the minimum as the firstPost
                                    int deleteID = int.Parse(Console.ReadLine());
                                    int lastPost = postIDs.Max();
                                    int firstPost = postIDs.Min();
                                    //If the input is outside the boundaries, throws an error message
                                    if(deleteID < firstPost || deleteID > lastPost)
                                    {
                                        throw new Exception("Please enter a valid ID.");
                                    }
                                    //If the input goes through
                                    else
                                    {
                                        //Removes the post corresponding to the ID input
                                        BigAlsPosts.Remove(new Posts(){ID = deleteID, Text = " ", Timestamp = " "});
                                        //Displays the updated posts
                                        foreach(Posts post in BigAlsPosts)
                                        {
                                            Console.WriteLine("Post ID " + post.ID + " " + post.Text + " " + post.Timestamp);
                                        }
                                        BigAlsPosts.Sort();
                                        PostFile.Save(BigAlsPosts);
                                        Console.WriteLine("Press any key to return to the Menu.");
                                        Console.ReadKey();
                                        break;
                                    }
                                }
                                //Throws an error message if the input was in incorrect format
                                catch(Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    continue;
                                }
                            }
                        }
                    }

                    //If the input was '4' the user will be routed
                    else if(menuChoice == 4)
                    {
                        string cs = @"URI = file:C:\Users\birdc\source\repos\pa3-agcrofoot-1\posts.db";
                        using var con = new SQLiteConnection(cs);
                        con.Open();

                        string stm = "select SQLITE_VERSION()";

                        using var cmd = new SQLiteCommand(stm, con);
                        string version = cmd.ExecuteScalar().ToString();
                        Console.WriteLine($"SQLite version : {version}");


                    }

                    //Exits the code
                    else if(menuChoice == 5)
                    {
                        Console.Clear();
                        System.Environment.Exit(0);
                    }
                }
            }
        }
    }
}
