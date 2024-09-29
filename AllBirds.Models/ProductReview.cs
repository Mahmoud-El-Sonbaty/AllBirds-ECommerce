using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class ProductReview : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int ClientId { get; set; }
        public virtual IdentityUser<int>? Client { get; set; }

        [Range(0, 5)]
        public float Rating { get; set; }

        [StringLength(64, MinimumLength = 20)]
        public string? RatingHead { get; set; }

        [StringLength(128, MinimumLength = 32)]
        public string? Review { get; set; }
    }
}
