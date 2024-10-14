using System.ComponentModel.DataAnnotations;

namespace AllBirds.DTOs.ProductDTOs
{
    public class CUProductDTO
    {
        public int Id { get; set; }

        [StringLength(64, MinimumLength = 4)]
        public string NameAr { get; set; }
        
        [StringLength(64, MinimumLength = 4)]
        public string NameEn { get; set; }

        [Range(10, 3000)]
        public decimal Price { get; set; }

        //[Range(10, 1000)]
        //public int UnitsInStock { get; set; }

        [StringLength(255, MinimumLength = 15)]
        public string? DescriptionAr { get; set; }

        [StringLength(255, MinimumLength = 15)]
        public string? DescriptionEn { get; set; }
        public bool FreeShipping { get; set; } = false;
        public List<int>? CategoriesId { get; set; }
        //public List<CUProductColorImageDTO>? ColorsImages { get; set; }
    }
}
