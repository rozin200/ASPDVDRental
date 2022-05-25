﻿using System.ComponentModel.DataAnnotations;

namespace RopeyDVDRental.Models
{
    public class LoanType
    {
        [Key]
        public int LoanTypeNumber { get; set; }

        public string Loantype { get; set; }

        public int LoanDuration { get; set; }
    }
}
