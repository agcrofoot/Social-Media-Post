using System;
using System.Collections.Generic;

namespace pa3_agcrofoot_1
{
    public class Posts : IComparable<Posts>, IEquatable<Posts>
    {
        //Established getters and setters
       public int ID{get; set;}
       public string Text{get; set;}
       public string Timestamp{get; set;}

       //Uses the IComparable to compare Timestamps of each post
       public int CompareTo(Posts temp)
       {
           return temp.Timestamp.CompareTo(this.Timestamp);
       }

       //Gets the posts ready to be saved to the file
       public override string ToString()
       {
           return ID + " " + Text + " " + Timestamp;
       }
       public bool Equals(Posts temp)
       {
           if(temp == null) return false;
           return (this.ID.Equals(temp.ID));
       }
    }
}