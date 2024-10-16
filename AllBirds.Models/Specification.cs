using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class Specification : BaseEntity<int>
    {
        [MaxLength(100)]
        public string NameAr { get; set; }

        [MaxLength(100)]
        public string NameEn { get; set; }
        //public virtual ICollection<CategorySpecification>? Categories { get; set; }
        public virtual ICollection<ProductSpecification>? Products { get; set; }
    }
}
