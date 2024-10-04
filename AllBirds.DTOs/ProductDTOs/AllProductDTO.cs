using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.ProductDTOs
{
    public class AllProductDTO
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public decimal Price { get; set; }
        public List<string> ImagesPath { get; set; }
    }
}
