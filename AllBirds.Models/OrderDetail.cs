using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class OrderDetail : BaseEntity<int>
    {
        public int OrderMasterId { get; set; }
        public virtual OrderMaster? OrderMaster { get; set; }
        public int ProductId { get; set; }
        public virtual ProductColor? Product { get; set; }
        public int SizeId { get; set; }
        public Size? SizePurchased { get; set; }
        public int Quantity { get; set; }
        [MaxLength(128)]
        public string? Notes { get; set; }
        //public decimal DetailPrice { get; set; } = Product.Price * this.Quantity;
    }
}
