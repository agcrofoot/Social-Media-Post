using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using pa3_agcrofoot_1.Database;

namespace pa3_agcrofoot_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int menuChoice = 0;
            while(menuChoice != 5)
            {
                //Presents menu options
                Console.Clear();
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
                }
                finally
                {
                    //If the input goes through, it is then routed
                    //If the input was '1', the current posts are displayed
                    if(menuChoice == 1)
                    {
                        Console.Clear();
                        IReadAllPosts readPosts = new ReadPost();
                        List<Posts> BigAlsPosts = readPosts.GetPosts();
                        BigAlsPosts.Sort();
                        foreach(Posts post in BigAlsPosts)
                        {
                            Console.WriteLine(post.ToString());
                        }

                        Console.WriteLine("Press any key to return to the Menu.");
                        Console.ReadKey();
                    }

                    //If the input was '2', the user is prompted to add their new post
                    else if(menuChoice == 2)
                    {
                        Console.Clear();
                        SavePosts savePosts = new SavePosts();
                        IReadAllPosts readPosts = new ReadPost();
                        List<Posts> BigAlsPosts = readPosts.GetPosts();
                        if(BigAlsPosts.Count == 0)
                        {
                            Console.WriteLine("Enter your post.");
                            Posts newPost = new Posts(){ID = 1, Text = Console.ReadLine(), Timestamp = DateTime.Now.ToString()};
                            BigAlsPosts.Sort();
                            savePosts.SavePost(newPost);
                            Console.Clear();
                            Console.WriteLine(newPost.ToString());
                        }
                        else
                        {
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
                            BigAlsPosts.Sort();
                            savePosts.SavePost(newPost);
                            Console.Clear();
                            Console.WriteLine(newPost.ToString());
                        }
                        Console.WriteLine("Press any key to return to the Menu.");
                        Console.ReadKey();
           
                    }
                    //If the input was '3', the user is prompted to delete a post.
                    else if(menuChoice == 3)
                    {
                        Console.Clear();
                        SavePosts savePosts = new SavePosts();
                        DeletePosts deletePosts = new DeletePosts();
                        IReadAllPosts readPosts = new ReadPost();
                        List<Posts> BigAlsPosts = readPosts.GetPosts();
                        if(BigAlsPosts.Count == 0)
                        {
                            Console.WriteLine("There are no posts to delete.");
                            Console.WriteLine("Press any key to return to the Menu.");
                            Console.ReadKey();
                        }
                        else
                        {
                            BigAlsPosts.Sort();
                            foreach(Posts post in BigAlsPosts)
                            {
                                Console.WriteLine(post.ToString());
                            }
                            while(true)
                            {
                                Console.WriteLine("Enter the ID of the post you would like to delete.");
                                try
                                {
                                    int deleteID = int.Parse(Console.ReadLine());
                                    BigAlsPosts.Remove(new Posts(){ID = deleteID, Text = " ", Timestamp = " "});
                                    BigAlsPosts.Sort();
                                    deletePosts.DeletePost();
                                    foreach(Posts post in BigAlsPosts)
                                    {
                                        Console.WriteLine(post.ToString());
                                        savePosts.SavePost(post);
                                    }
                                    Console.WriteLine("Press any key to return to the Menu.");
                                    Console.ReadKey();
                                    break;
                                }
                                catch(Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Please try again.");
                                    Console.ReadKey();
                                }
                            }  
                        }
                    }

                    //If the input was '4' the user will be routed
                    else if(menuChoice == 4)
                    {
                        Console.Clear();
                        ISeedPosts saveObject = new SavePosts();
                        saveObject.SeedPosts();

                        IReadAllPosts readPosts = new ReadPost();
                        List<Posts> BigAlsPosts = readPosts.GetPosts();

                        foreach(Posts post in BigAlsPosts)
                        {
                            Console.WriteLine(post.ToString());
                        }
                        Console.WriteLine("Press any key to return to the Menu.");
                        Console.ReadKey();
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
