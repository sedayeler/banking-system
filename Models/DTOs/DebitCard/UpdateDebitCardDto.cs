﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.DebitCard
{
    public class UpdateDebitCardDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
    }
}
