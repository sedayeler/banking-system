﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.DebitCard
{
    public class CreateDebitCardDto
    {
        public int AccountId { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public decimal Balance { get; set; }
    }
}
