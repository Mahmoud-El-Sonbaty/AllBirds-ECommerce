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
        public virtual Client? Client { get; set; }
        public float Rating { get; set; }

        [MaxLength(64)]
        public string? RatingHead { get; set; }

        [MaxLength(128)]
        public string? Review { get; set; }
    }
}
