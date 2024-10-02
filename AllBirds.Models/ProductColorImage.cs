using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class ProductColorImage : BaseEntity<int>
    {
        public int ProductId { get; set; }
        public virtual ProductColor? Product { get; set; }

        [Required, MaxLength(255)]
        public string ImagePath { get; set; }
    }
}
