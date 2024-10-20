using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.SpecificationDTOs
{
    public class CUSpecificationDTO
    {
        public int Id { get; set; } = 0;

        [StringLength(100, MinimumLength = 5)]
        public string NameAr { get; set; }

        [StringLength(100, MinimumLength = 5)]
        public string NameEn { get; set; }
    }
}
