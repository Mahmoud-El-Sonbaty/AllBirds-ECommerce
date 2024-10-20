using AllBirds.DTOs.Shared;
using AllBirds.DTOs.SpecificationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Application.Services.SpecificationServices
{
    public interface ISpecificationService
    {
        public Task<ResultView<CUSpecificationDTO>> CreateAsync(CUSpecificationDTO entity);
        public Task<ResultView<CUSpecificationDTO>> UpdateAsync(CUSpecificationDTO entity);
        public Task<ResultView<GetSpecificationDTO>> SoftDeleteAsync(int id);
        public Task<ResultView<GetSpecificationDTO>> HardDeleteAsync(int id);
        public Task<ResultView<List<CUSpecificationDTO>>> GetAllAsync();
        public Task<ResultView<GetSpecificationDTO>> GetByIdAsync(int id);
        public Task<int> SaveChangesAsync();
    }
}
