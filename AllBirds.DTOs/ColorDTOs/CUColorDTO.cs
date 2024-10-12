using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
