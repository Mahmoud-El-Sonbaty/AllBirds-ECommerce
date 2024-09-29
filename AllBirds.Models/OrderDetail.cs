using System;
using System.Collections.Generic;
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
        public virtual Product? Product { get; set; }
        public Size SizePurchased { get; set; }
        public Color ColorPurchased { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
        //public decimal DetailPrice { get; set; } = Product.Price * this.Quantity;
    }
}
