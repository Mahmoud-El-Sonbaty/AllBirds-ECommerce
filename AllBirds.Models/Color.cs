using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class Color : BaseEntity<int>
    {
        [MaxLength(10)]
        public string ColorCode { get; set; }
    }
}
