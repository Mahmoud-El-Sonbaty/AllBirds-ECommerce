using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class OrderState : BaseEntity<int>
    {
        public string StateAr { get; set; }
        public string StateEn { get; set; }
    }
}
