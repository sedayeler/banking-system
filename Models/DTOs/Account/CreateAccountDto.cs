﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Account
{
    public class CreateAccountDto
    {
        public int CustomerId { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
    }
}
