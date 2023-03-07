using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Common.Keycloak.Models
{
    public class CreateAccountModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Group is required")]
        public List<string> groups { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        
    }
}