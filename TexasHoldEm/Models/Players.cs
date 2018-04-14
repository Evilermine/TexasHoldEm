using System;
using System.Collections.Generic;

namespace TexasHoldEm.Models
{
    public partial class Players
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int Wallet { get; set; }
    }
}
