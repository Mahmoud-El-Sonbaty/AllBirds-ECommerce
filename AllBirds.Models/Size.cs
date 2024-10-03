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
        [MaxLength(2)]
        public string SizeNumber { get; set; }
        public virtual ICollection<ProductSize>? Products { get; set; }
    }
}
