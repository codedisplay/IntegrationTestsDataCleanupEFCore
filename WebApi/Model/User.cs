using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        public List<Blog> Blogs { get; set; }
    }
}
