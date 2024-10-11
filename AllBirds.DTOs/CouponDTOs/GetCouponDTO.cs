
namespace AllBirds.DTOs.CouponDTOs
{
    public class GetCouponDTO
    {
        public string Code { get; set; }
        public int Discount { get; set; }
        public bool IsActive { get; set; }
    }
}
