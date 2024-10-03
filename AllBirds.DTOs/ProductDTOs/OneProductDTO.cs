using AllBirds.DTOs.RatingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.ProductDTOs
{
    public class OneProductDTO
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public decimal Price { get; set; }
        public bool FreeShipping { get; set; }
        public int RatingCount { get; set; }
        public int Rating { get; set; }
        public List<string> ImagesPath { get; set; }
        public List<AllRatingDTO>? AllRatings { get; set; }
    }
}
