using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.CategoryDTOs
{
    public class CUCategoryDTO
    {
        public int Id { get; set; }
        
        [StringLength(40, MinimumLength = 4)]
        public string NameAr { get; set; }
        
        [StringLength(40, MinimumLength = 4)]
        public string NameEn { get; set; }
    }
}
