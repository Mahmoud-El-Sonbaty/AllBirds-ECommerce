using AllBirds.Application.Contracts;
using AllBirds.Context;
using AllBirds.Models;

namespace AllBirds.Infrastructure
{
    public class ColorRepository : GenericRepository<Color, int>, IColorRepository
    {
        public ColorRepository(AllBirdsContext context) : base(context)
        {
        }

    }
}
