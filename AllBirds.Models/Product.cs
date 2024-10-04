using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllBirds.Models
{
    public class Product : BaseEntity<int>
    {
        [MaxLength(64)]
        public string NameAr { get; set; }

        [MaxLength(64)]
        public string NameEn { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [MaxLength(25)]
        public string? DescriptionAr { get; set; }

        [MaxLength(255)]
        public string? DescriptionEn { get; set; }

        public int Discount { get; set; }

        public bool FreeShipping { get; set; } = false;

        public virtual ICollection<CategoryProduct>? Categories { get; set; }
        public virtual ICollection<Color>? AvailableColors { get; set; }
        public virtual ICollection<ProductSize>? AvailableSizes { get; set; }
        public virtual ICollection<ProductReview>? Reviews { get; set; }
    }
}
