using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class ProductSpecification : BaseEntity<int>
    {
        public int SpecificationId { get; set; }
        public Specification? Specification { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [MaxLength(1500)]
        public string Content { get; set; }
    }
}
