using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexasHoldEm.Data
{
    public class UserContext : DbContext {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public UserContext() { }
        public DbSet<UserManager> userManager { get; set; }
    }
}
