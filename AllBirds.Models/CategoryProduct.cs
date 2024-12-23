﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.Models
{
    public class CategoryProduct : BaseEntity<int>
    {
        public int? CategoryId { get; set; } 
        public virtual Category? Category { get; set; }
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
