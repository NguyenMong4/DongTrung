using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.DTO
{
    public class UserDto
    {
        public string id { get; set; } = null!;
        public string username { get; set; } = null!;
        public long createdTimestamp { get; }
        public DateTimeOffset CreateAt { get; set; }
        public string email { get; set; } = null!;
        public string[] groups { get; set; } = null!;

        public static implicit operator HttpContent(UserDto v)
        {
            throw new NotImplementedException();
        }
    }

}