using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FriendZone.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}
