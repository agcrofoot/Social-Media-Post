using System.Collections.Generic;

namespace pa3_agcrofoot_1.Database
{
    public interface IReadAllPosts
    {
        public List<Posts> GetPosts();
    }
}