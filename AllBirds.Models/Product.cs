using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class Product : BaseEntity<int>
    {
        [StringLength(64, MinimumLength = 4)]
        public string NameAr { get; set; }

        [StringLength(64, MinimumLength = 4)]
        public string NameEn { get; set; }

        [Range(10, 3000)]
        public decimal Price { get; set; }

        [Range(10, 1000)]
        public int UnitsInStock { get; set; }

        [StringLength(255, MinimumLength = 15)]
        public string? DescriptionAr { get; set; }

        [StringLength(255, MinimumLength = 15)]
        public string? DescriptionEn { get; set; }

        public bool FreeShipping { get; set; } = false;

        public virtual ICollection<CategoryProduct>? Categories { get; set; }
        public virtual ICollection<Color>? AvailableColors { get; set; }
        public virtual ICollection<Size>? AvailableSizes { get; set; }
        public virtual ICollection<ProductReview>? Reviews { get; set; }
        public virtual ICollection<ProductImage>? Images { get; set; }
    }
}
