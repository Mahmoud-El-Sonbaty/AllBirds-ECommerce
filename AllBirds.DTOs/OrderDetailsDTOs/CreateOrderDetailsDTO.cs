﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBirds.DTOs.OrderDetailsDTOs
{
    public class CreateOrderDetailsDTO
    {
        public int Id { get; set; }

        public int OrderMasterId { get; set; }

        public int ProductId { get; set; }

        public decimal DetailPrice { get; set; }

        public int Quantity { get; set; }

        [MaxLength(128)]
        public string? Notes { get; set; }

    }
}
