using AllBirds.DTOs.CategorySizeDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.CategorySizeDTOS
{
    public class CreateOrUpdateCategorySizeDTO
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        //public Category? Category { get; set; }
        public int? SizeId { get; set; }
      //  public Size? Size { get; set; }
    }
}
