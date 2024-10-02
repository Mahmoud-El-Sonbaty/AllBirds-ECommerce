using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class ProductColor : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int ColorId { get; set; }
        public virtual Color? Color { get; set; }
        public int UnitsInStock { get; set; }
        public virtual ICollection<ProductColorImage>? Images { get; set; }
    }
}
