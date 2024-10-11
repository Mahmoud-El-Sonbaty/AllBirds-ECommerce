using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.OrderStateDTOs
{
    public class CUOrderStateDTO
    {
        public int Id { get; set; }

        [StringLength(16, MinimumLength = 3)]
        public string StateAr { get; set; }

        [StringLength(16, MinimumLength = 3)]
        public string StateEn { get; set; }
    }
}
