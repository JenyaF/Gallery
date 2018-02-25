using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Reflection;


namespace Galery.Models
{
    public class PictUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string Direction { get; set; }
        public int Price { get; set; }
        public byte[] Data { get; set; }
        public int IdUser { get; set; }
    }
     class ComparePictUser: IEqualityComparer<PictUser>
    {
        public bool Equals(PictUser p1, PictUser p2)
        {
            if (p1.Id == p2.Id)
                return true;
            else return false;
        }
        public int GetHashCode(PictUser p)
        {           
            return p.Id;
        }

    }
    public class Wrapper
    {
        public string Words { get; set; }
        string[] masWords;
        public Picture Picture { get; set; }
        public int IdUser{ get; set; }
        public int IdPict{ get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<PictUser> PictureUser { get
            {
                var res = Pictures.Join(Users,
        p => p.UserId,
        c => c.Id,
        (p, c) => new PictUser
        {
            Id = p.Id,
            Title = p.Title,
            Direction = p.Direction,
            Price = p.Price,
            Data=p.Data,
            Name = c.Name,
            Surname=c.Surname
        });
                if (IdUser != 0)
                {
                   return res.Where(p => IdUser == p.IdUser);
                }
                return res;
            }
        }

        public Picture GetPict()
        {
            return Pictures.FirstOrDefault(p => p.Id == IdPict);
        }
        public Picture GetPict(int IdPict)
        {
            return Pictures.FirstOrDefault(p => p.Id == IdPict);
        }

        public IEnumerable<Picture> GetPicts(int IdUser)
        {

            if (IdUser != 0)
            {
                return Pictures.Where(p => p.UserId == IdUser);
                
            }
            return null;
        }
        public IEnumerable<PictUser> GetPictUser()
        {
            try
            {
                var pict = GetPicts(IdUser);
                var user = Users.FirstOrDefault(u => u.Id == IdUser);
                List<PictUser> pictUser = new List<PictUser>();
                foreach (var p in pict)
                {
                    pictUser.Add(new PictUser
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Direction = p.Direction,
                        Price = p.Price,
                        Data = p.Data,
                        Name = user.Name,
                        Surname = user.Surname,
                        IdUser=user.Id
                    });
                }
                return pictUser;
            }
            catch
            {
                return Pictures.Join(Users,
        p => p.UserId,
        c => c.Id,
        (p, c) => new PictUser
        {
            Id = p.Id,
            Title = p.Title,
            Direction = p.Direction,
            Price = p.Price,
            Data = p.Data,
            Name = c.Name,
            Surname = c.Surname,
            IdUser=c.Id
        }); 
            }

        }
        public PictUser GetPictUserOne()
        {
            Picture p = GetPict();
            var u = Users.FirstOrDefault(us => us.Id == p.UserId);
            return new PictUser
            {
                Id = p.Id,
                Title = p.Title,
                Direction = p.Direction,
                Price = p.Price,
                Data = p.Data,
                Name = u.Name,
                Surname = u.Surname,
                IdUser = u.Id
            }; 
            
        }
        public IEnumerable<PictUser> GetSearchPictUser()
        {
             masWords = Words.Split(' ');
            Func<User, bool> SearchUsFunc = SearchUs;
            IEnumerable<User> us = Users.Where(SearchUsFunc);
            IEnumerable<Picture> pict = Pictures.Where(p =>
            {
                foreach (string word in masWords)
                {
                    if (word == p.Title || word == p.Direction)
                        return true;
                }
                return false;
            });
           IEnumerable<PictUser> pu1= pict.Join(Users,
        p => p.UserId,
        c => c.Id,
        (p, c) => new PictUser
        {
            Id = p.Id,
            Title = p.Title,
            Direction = p.Direction,
            Price = p.Price,
            Data = p.Data,
            Name = c.Name,
            Surname = c.Surname,
            IdUser = c.Id
        });
            IEnumerable<PictUser> pu2 = Pictures.Join(us,
       p => p.UserId,
       c => c.Id,
       (p, c) => new PictUser
       {
           Id = p.Id,
           Title = p.Title,
           Direction = p.Direction,
           Price = p.Price,
           Data = p.Data,
           Name = c.Name,
           Surname = c.Surname,
           IdUser = c.Id
       });
            return pu1.Union(pu2,new ComparePictUser());
        }
        bool SearchUs(User u)
        {
            foreach (string word in masWords)
            {
                if (word == u.Name || word == u.Surname)
                    return true;
            }
            return false;
         }
    
    }
  
}