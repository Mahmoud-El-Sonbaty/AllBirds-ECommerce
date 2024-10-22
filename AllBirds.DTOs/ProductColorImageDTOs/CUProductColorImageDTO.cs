using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AllBirds.DTOs.ProductColorImageDTOs
{
    public class CUProductColorImageDTO
    {
        public int Id { get; set; }

        public int ProductColorId { get; set; }

        public IFormFile? ImageData { get; set; }
        [MaxLength(1000)]
        public string? ImagePath { get; set; }
    }
}
