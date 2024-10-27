using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.ColorDTOs
{
    public class GetColorDTO
    {
        public string NameAr { get; set; }

        public string NameEn { get; set; }
        public string Code { get; set; }
    }
}
