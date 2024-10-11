using System.ComponentModel.DataAnnotations;

namespace AllBirds.DTOs.CouponDTOs
{
    public class CUCouponDTO
    {
        public int Id { get; set; }

        [StringLength(8, MinimumLength = 3)]
        public string Code { get; set; }
        public int Discount { get; set; }
        public bool IsActive { get; set; }
    }
}
