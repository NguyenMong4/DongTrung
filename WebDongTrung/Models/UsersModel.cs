using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Models
{
    public class UsersModel
    {
        public string id { get; set; }
        public string username { get; set; }
        public long createdTimestamp { get; set; }
        public DateTimeOffset CreateAt { get; set; }
    }
}