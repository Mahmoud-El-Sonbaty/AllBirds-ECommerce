using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.OrderDetailsDTOs
{
    public class GetAllCartCheckoutDetailsDTO
    {
        public int Id { get; set; }
        public string ProductNameAr { get; set; }
        public string ProductNameEn { get; set; }
        public string ColorNameAr { get; set; }
        public string ColorNameEn { get; set; }
        public decimal DetailPrice { get; set; }
        public string SizeNumber { get; set; }
        public string ImagePath { get; set; }
    }
}
