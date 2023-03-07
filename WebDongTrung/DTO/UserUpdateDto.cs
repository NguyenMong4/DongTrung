using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.DTO
{
    public class UserUpdateDto
    {
        public string Id { get; set; } = null!;
        public string? Email { get; set; }
        public string? Groups { get; set; }
        public string? Phone {get;set;}
        public string? Address{get;set;}
    }

}