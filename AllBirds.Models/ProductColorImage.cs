using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AllBirds.Models
{
    public class ProductColorImage : BaseEntity<int>
    {
        public int ProductColorId { get; set; }
        [InverseProperty("Images")]
        public virtual ProductColor? ProductColor { get; set; }

        [Required, MaxLength(255)]
        public string ImagePath { get; set; }
    }
}
