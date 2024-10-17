using System.ComponentModel.DataAnnotations;

namespace AllBirds.DTOs.ProductDTOs
{
    public class CUProductDTO
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string ProductNo { get; set; }

        [StringLength(150, MinimumLength = 4)]
        public string NameAr { get; set; }
        
        [StringLength(150, MinimumLength = 4)]
        public string NameEn { get; set; }

        [Range(10, 1000)]
        public decimal Price { get; set; }

        //[StringLength(500, MinimumLength = 10)]
        public List<string>? HighlightsAr { get; set; }

        //[StringLength(500, MinimumLength = 10)]
        public List<string>? HighlightsEn { get; set; }

        [StringLength(500, MinimumLength = 10)]
        public string? SustainabilityAr { get; set; }

        [StringLength(500, MinimumLength = 10)]
        public string? SustainabilityEn { get; set; }

        [StringLength(1000, MinimumLength = 10)]
        public List<string>? SustainableMaterialsAr { get; set; }

        [StringLength(1000, MinimumLength = 10)]
        public List<string>? SustainableMaterialsEn { get; set; }

        [StringLength(600, MinimumLength = 10)]
        public string? ShippingAndReturnsAr { get; set; }

        [StringLength(600, MinimumLength = 10)]
        public string? ShippingAndReturnsEn { get; set; }

        [StringLength(1000, MinimumLength = 10)]
        public string? CareGuideAr { get; set; }

        [StringLength(1000, MinimumLength = 10)]
        public string? CareGuideEn { get; set; }

        [Range(0, 100)]
        public int Discount { get; set; } = 0;
        public bool FreeShipping { get; set; } = false;
        public List<int>? CategoriesId { get; set; }
        //public List<CUProductColorImageDTO>? ColorsImages { get; set; }
    }
}
