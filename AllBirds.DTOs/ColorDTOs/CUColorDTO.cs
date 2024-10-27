
using System.ComponentModel.DataAnnotations;


namespace AllBirds.DTOs.ColorDTOs
{
    public class CUColorDTO
    {
        public int Id { get; set; }

        [StringLength(32, MinimumLength = 3)]
        public string NameAr { get; set; }
        [MaxLength(32)]
        public string NameEn { get; set; }

        [StringLength(10, MinimumLength = 3)]
        public string Code { get; set; }
    }
}
