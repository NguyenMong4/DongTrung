using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Models
{
    public class UsersModel
    {
        public string id { get; set; } = null!;
        public string username { get; set; } = null!;
        public long createdTimestamp { get; set; }
        public DateTimeOffset CreateAt { get; set; }
    }
}