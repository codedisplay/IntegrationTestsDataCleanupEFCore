using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public List<Post> Posts { get; set; }
    }
}
