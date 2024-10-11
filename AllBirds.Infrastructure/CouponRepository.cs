using AllBirds.Application.Contracts;
using AllBirds.Context;
using AllBirds.Models;

namespace AllBirds.Infrastructure
{
    public class CouponRepository : GenericRepository<Coupon,int>, ICouponRepository
    {
        public CouponRepository(AllBirdsContext context) : base(context)
        {
        }

        // Additional repository methods can be implemented here if needed
    }
}
