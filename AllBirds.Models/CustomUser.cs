using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class CustomUser : IdentityUser<int>
    {
        [MaxLength(15)]
        public string? FirstName { get; set; }

        [MaxLength(15)]
        public string? LastName { get; set; }

        [MaxLength(128)]
        public string? ImagePath { get; set; }

        [MaxLength(15)]
        public string? Country { get; set; }

        [MaxLength(15)]
        public string? City { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [MaxLength(10)]
        public string? PostalCode { get; set; }
        public virtual ICollection<OrderMaster>? ClientOrders { get; set; }
        public virtual ICollection<ProductReview>? ProductReviews { get; set; }
        public virtual ICollection<ClientFavorite>? ClientFavorites { get; set; }
    }
}
