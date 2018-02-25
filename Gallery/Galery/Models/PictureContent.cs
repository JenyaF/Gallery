using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Galery.Models
{
    public class PictureContent:DbContext
    {
       /* public PictureContent() : base("DefaultConnection") { }
        static PictureContent() 
        {
         
            Database.SetInitializer<PictureContent>(new MyContextInitializer());
        }*/
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<User> Users { get; set; }
    }
}