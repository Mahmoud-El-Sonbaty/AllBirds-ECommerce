using AllBirds.Application.Contracts;
using AllBirds.Context;
using AllBirds.Models;

namespace AllBirds.Infrastructure
{
    public class SizeRepository : GenericRepository<Size,int>, ISizeRepository
    {
        public SizeRepository(AllBirdsContext context) : base(context)
        {
        }

        // Additional repository methods can be implemented here if needed
    }
}
