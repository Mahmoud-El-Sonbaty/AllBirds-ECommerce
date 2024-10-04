using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class Coupon : BaseEntity<int>
    {
        public string Code { get; set; }
        public int Discount { get; set; }
        public bool IsActive { get; set; }
    }
}
