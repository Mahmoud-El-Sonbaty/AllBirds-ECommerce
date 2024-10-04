using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.RatingDTOs
{
    public class CURatingDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }

        [Range(0, 5)]
        public float Rate { get; set; }

        [StringLength(128, MinimumLength = 32)]
        public string? Review { get; set; }
    }
}
