using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class ProductColor : BaseEntity<int>
    {
        public int ProductId { get; set; }
        [InverseProperty("AvailableColors")]
        public virtual Product? Product { get; set; }
        public int ColorId { get; set; }
        public virtual Color? Color { get; set; }
        //[ForeignKey(nameof(ProductColorImage))]
        public int MainImageId { get; set; }
        //public virtual ProductColorImage MainImage { get; set; }
        [InverseProperty("ProductColor")]
        public virtual ICollection<ProductColorImage>? Images { get; set; } 
        public virtual ICollection<ProductColorSize>? AvailableSizes { get; set; }
    }
}
