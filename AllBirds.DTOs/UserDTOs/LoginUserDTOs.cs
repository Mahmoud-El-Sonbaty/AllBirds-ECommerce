using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.UserDTOs
{
    public record LoginUserDTOs
    {
        [Required , EmailAddress]
        [Remote("Login", "Account", HttpMethod = "Post", ErrorMessage = "This Email Does Not Exist ! Please Enter Email Valid")]
        public string Email { get; set; }

        [Required , DataType(DataType.Password)]
        [Remote("Login", "Account", HttpMethod = "Post", ErrorMessage = "This Password Does Not Exist ! Please Enter Password Valid")]
        public string Password { get; set; }
        public bool rememperMe { get; set; }
    }
}
