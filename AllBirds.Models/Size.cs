using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class Size : BaseEntity<int>
    {
        public string SizeNumber { get; set; }
    }
}
