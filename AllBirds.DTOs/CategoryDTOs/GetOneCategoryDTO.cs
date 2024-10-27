using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.CategoryDTOs
{
    public class GetOneCategoryDTO
    {
        [StringLength(40, MinimumLength = 4)]
        public string NameAr { get; set; }

        [StringLength(40, MinimumLength = 4)]
        public string NameEn { get; set; }
        public int ParentCategoryId { get; set; } = 0;
        public int Level { get; set; } = 0;
        public string? ImagePath { get; set; }
        public bool IsParentCategory { get; set; } = false;
    }
}
