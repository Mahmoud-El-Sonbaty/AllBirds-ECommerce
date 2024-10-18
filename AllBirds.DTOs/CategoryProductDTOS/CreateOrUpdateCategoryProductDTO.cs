using AllBirds.DTOs.CategoryDTOs;
using AllBirds.DTOs.CategoryProductDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.CategoryProductDTOS
{
    public class CreateOrUpdateCategoryProductDTO
    {
        public int Id { get; set; } 
        public int? CategoryId { get; set; }
       // public virtual Category? Category { get; set; }
        public int? ProductId { get; set; }
        //public virtual Product? Product { get; set; }
    }
}



