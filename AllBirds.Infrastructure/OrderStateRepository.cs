using AllBirds.Application.Contracts;
using AllBirds.Context;
using AllBirds.Models;

namespace AllBirds.Infrastructure
{
    public class OrderStateRepository : GenericRepository<OrderState, int>, IOrderStateRepository
    {
        public OrderStateRepository(AllBirdsContext context) : base(context)
        {
        }

        // Additional repository methods can be implemented here if needed
    }
}
