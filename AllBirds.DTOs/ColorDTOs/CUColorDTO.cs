
using System.ComponentModel.DataAnnotations;


namespace AllBirds.DTOs.ColorDTOs
{
    public class CUColorDTO
    {
        public int Id { get; set; }

        [StringLength(32, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(10, MinimumLength = 3)]
        public string Code { get; set; }
    }
}
