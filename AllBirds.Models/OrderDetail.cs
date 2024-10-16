using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class OrderDetail : BaseEntity<int>
    {
        public int OrderMasterId { get; set; }
        public virtual OrderMaster? OrderMaster { get; set; }
        public int ProductColorSizeId { get; set; }
        public virtual ProductColorSize? ProductColorSize { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal DetailPrice { get; set; }

        [MaxLength(512)]
        public string? Notes { get; set; }
    }
}
