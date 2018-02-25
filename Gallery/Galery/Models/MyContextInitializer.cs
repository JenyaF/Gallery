using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Drawing;

namespace Galery.Models
{
    public class MyContextInitializer: DropCreateDatabaseAlways<PictureContent>
    {
        protected override void Seed(PictureContent db)
        {
            User u1 = new User { Login = "JekF@ukr.net",Password = "123", Name = "Jek", Surname = "Bon" };
            User u2 = new User { Login = "FradyF@ukr.net", Password = "111", Name = "Fredy", Surname = "Fray" };
            User u3 = new User { Login = "MayaS@ukr.net",Password = "222", Name = "Maya", Surname = "Sheva" };
            User u4 = new User { Login = "LiB@ukr.net",Password = "123", Name = "Li", Surname = "Brouny" };
            User u5 = new User { Login = "FilB@ukr.net", Password = "123", Name = "Fil", Surname = "Byx" };
            db.Users.AddRange(new List<User> { u1, u2, u3, u4, u5 });
            db.SaveChanges();
           


        } 
    }
}