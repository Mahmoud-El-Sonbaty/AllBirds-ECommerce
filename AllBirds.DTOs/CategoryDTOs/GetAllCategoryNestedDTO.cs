using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.CategoryDTOs
{
    public class GetAllCategoryNestedDTO
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int ParentCategoryId { get; set; }
        public int Level { get; set; }
        public bool IsParentCategory { get; set; }
        public List<GetAllCategoryNestedDTO>? Children { get; set; }
    }
}
