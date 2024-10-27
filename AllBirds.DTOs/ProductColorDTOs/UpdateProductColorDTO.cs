using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.ProductColorDTOs
{
    public class UpdateProductColorDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public int ColorId { get; set; }

        public int MainImageId { get; set; }
        public virtual List<IFormFile>? Images { get; set; }
    }
}
