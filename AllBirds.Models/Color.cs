using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class Color : BaseEntity<int>
    {
        [MaxLength(32)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Code { get; set; }
        public virtual ICollection<ProductColor>? Products { get; set; }
    }
}
