﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMagic.Models.DTOs.Bills
{
    public class BillAddProductDTO
    {
        public int Id { get; set; }
        public string BillDate { get; set; }
        public int Total { get; set; }
    }
}
