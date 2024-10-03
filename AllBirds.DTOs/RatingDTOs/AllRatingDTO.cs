using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.RatingDTOs
{
    public class AllRatingDTO
    {
        public int Id { get; set; }
        public float Rating { get; set; }
        public string? Review { get; set; }
        public string? RatingHead { get; set; }
        public string? SizePurchased { get; set; }
        public string? ClientName { get; set; }
        public string? ReviewDate { get; set; }
    }
}
