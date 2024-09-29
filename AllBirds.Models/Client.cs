using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class Client : BaseEntity<int>
    {
        [StringLength(15, MinimumLength = 3)]
        public string? FirstName { get; set; }
        
        [StringLength(15, MinimumLength = 3)]
        public string? LastName { get; set; }

        public string? Country { get; set; }

        [StringLength(15, MinimumLength = 6)]
        public string? City { get; set; }

        [StringLength(255, MinimumLength = 10)]
        public string? Address { get; set; }

        [StringLength(10, MinimumLength = 5)]
        public string? ZipCode { get; set; }

        public int UserId { get; set; }

        public virtual IdentityUser<int>? User { get; set; }
    }
}
