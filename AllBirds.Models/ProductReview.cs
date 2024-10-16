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
        public virtual CustomUser? Client { get; set; }
        public float Rating { get; set; }

        [MaxLength(100)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? Review { get; set; }
    }
}
