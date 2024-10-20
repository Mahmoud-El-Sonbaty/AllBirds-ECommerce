using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class ProductDetail : BaseEntity<int>
    {

        [MaxLength(60)]
        public string Title { get; set; }

        [MaxLength(800)]
        public string Description { get; set; }

        [MaxLength(1000)]
        public string ImagePath { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
