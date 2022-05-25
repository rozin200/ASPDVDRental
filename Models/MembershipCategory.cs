﻿using System.ComponentModel.DataAnnotations;

namespace RopeyDVDRental.Models
{
    public class MembershipCategory
    {
        [Key]
        public int MemberCategoryNumber { get; set; }

        public string MembershipCategoryDescription { get; set; }

        public int MembershipCategoryTotalLoans { get; set; }
    }
}
