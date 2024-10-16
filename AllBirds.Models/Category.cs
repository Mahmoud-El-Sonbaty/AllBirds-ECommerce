using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class Category : BaseEntity<int>
    {
        //[MaxLength(100)]
        //public string CategoryNo { get; set; }
        
        [MaxLength(100)]
        public string NameAr { get; set; }

        [MaxLength(100)]
        public string NameEn { get; set; }
        public int ParentCategoryId { get; set; } = 0;
        //public virtual Category? ParentCategory { get; set; }
        public int Level { get; set; } = 0;
        public bool IsParentCategory { get; set; } = false;

        [MaxLength(1000)]
        public string? ImagePath { get; set; }
        public virtual ICollection<CategoryProduct>? Products { get; set; }
        //public virtual ICollection<CategorySize>? AvailableSizes { get; set; }
        //public virtual ICollection<CategorySpecification>? MustSpecifications { get; set; }
    }
}
