using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.ProductDetailDTOs
{
    public class CUProductDetails
    {
        public int? Id { get; set; }

        [Required , StringLength (60 , MinimumLength = 3)]
        public string? Title { get; set; }

        [Required, StringLength(60, MinimumLength = 3)]
        public string? Description { get; set; }

        [MaxLength(1000)]
        public IFormFile? ImageData { get; set; }

        [MaxLength(1000)]
        public string? ImagePath { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
