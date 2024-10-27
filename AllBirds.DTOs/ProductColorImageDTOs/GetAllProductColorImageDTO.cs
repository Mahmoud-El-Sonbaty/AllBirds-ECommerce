using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.ProductColorImageDTOs
{
    public class GetAllProductColorImageDTO
    {
        public int? ProductColorId { get; set; }

        public string? ImagePath { get; set; }
    }
}
