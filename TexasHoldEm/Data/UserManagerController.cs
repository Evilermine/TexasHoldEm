using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TexasHoldEm.Data
{
    public class UserManager
    {
        [Key]
        [Display(Name = "Usename")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Wallet")]
        public int Wallet { get; set; }
    }
}