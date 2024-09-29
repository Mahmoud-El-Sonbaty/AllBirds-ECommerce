using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class ProductImage : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

        [Required, StringLength(255)]
        public string ImagePath { get; set; }
    }
}
