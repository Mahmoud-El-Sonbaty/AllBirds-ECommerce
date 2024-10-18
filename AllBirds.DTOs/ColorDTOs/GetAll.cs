using AllBirds.DTOs.CouponDTOs;
using AllBirds.DTOs.OrderStateDTOs;
using AllBirds.DTOs.SizeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.ColorDTOs
{
    public record GetAll
    {
        public List<CUColorDTO> cUColorDTOs {  get; set; }
        public List<CUSizeDTO> cUSizeDTOs {  get; set; }
        public List<CUCouponDTO> cUCoupons {  get; set; }
        public List<CUOrderStateDTO> OrderStateDTOs {  get; set; }
    }
}
