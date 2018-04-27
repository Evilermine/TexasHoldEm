using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TexasHoldEm.Models
{
    public partial class Players
    {
        //[Key]
        //[Required]
        public string Username { get; set; }
        //[Required]
        public string Password { get; set; }
        ///[Required]
        ////[MaxLength(32)]
        public string Firstname { get; set; }
        //[Required]
        //[MaxLength(32)]
        public string Lastname { get; set; }
        /////[Required]
        //[MaxLength(64)]
        public string Email { get; set; }
        //[Required]
        public int Wallet { get; set; }
    }
}
