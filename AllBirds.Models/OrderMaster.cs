using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class OrderMaster : BaseEntity<int>
    {
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }
        public decimal Total { get; set; }
        public int OrderStateId { get; set; }
        public virtual OrderState? OrderState { get; set; }
        [MaxLength(128)]
        public string? Notes { get; set; }
        public virtual Coupon? CouponId { get; set; }
        public virtual Coupon? Coupon { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
