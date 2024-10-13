using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.ProductDTOs
{
    public class CUProductColorImageDTO
    {
        public int Id { get; set; }
        public int ColorId { get; set; }
        public string ImagePath { get; set; }
    }
}
