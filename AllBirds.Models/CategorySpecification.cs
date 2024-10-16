using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class CategorySpecification : BaseEntity<int>
    {
        public int SpecificationId { get; set; }
        public Specification? Specification { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
