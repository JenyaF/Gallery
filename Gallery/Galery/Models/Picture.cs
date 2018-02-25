using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Galery.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public string Direction { get; set; }
        public int Price { get; set; }
        public byte[] Data { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}