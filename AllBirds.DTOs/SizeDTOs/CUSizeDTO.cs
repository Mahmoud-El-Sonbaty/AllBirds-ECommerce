using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.SizeDTOs
{
    public class CUSizeDTO
    {
        public int Id { get; set; }

        [StringLength(5, MinimumLength = 1)]
        public string SizeNumber { get; set; }
    }
}
