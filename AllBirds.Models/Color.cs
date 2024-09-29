using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class Color : BaseEntity<int>
    {
        public string ColorName { get; set; }
    }
}
