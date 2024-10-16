using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class Size : BaseEntity<int>
    {
        [MaxLength(10)]
        public string SizeNumber { get; set; }
        public virtual ICollection<ProductColorSize>? Products { get; set; }
        //public virtual ICollection<CategorySize>? Categories { get; set; }
        public virtual ICollection<OrderDetail>? Orders { get; set; }
    }
}
