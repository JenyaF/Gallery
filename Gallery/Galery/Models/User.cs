using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Galery.Models;

namespace Galery.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public User()
        {
            Pictures = new List<Picture>();
        }
    }
}