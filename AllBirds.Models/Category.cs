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
        [StringLength(40, MinimumLength = 4)]
        public string NameAr { get; set; }

        [StringLength(40, MinimumLength = 4)]
        public string NameEn { get; set; }
        public int ParentId { get; set; }
        public int Level { get; set; } = 0;
        public bool IsParent { get; set; } = false;
        public virtual ICollection<CategoryProduct>? Products { get; set; }
        public virtual ICollection<Size>? AvailableSizes { get; set; }
    }
}
