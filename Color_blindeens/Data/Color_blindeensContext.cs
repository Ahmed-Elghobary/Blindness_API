using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Color_blindeens.Models;

namespace Color_blindeens.Data
{
    public class Color_blindeensContext : DbContext
    {
        public Color_blindeensContext (DbContextOptions<Color_blindeensContext> options)
            : base(options)
        {
        }

        public DbSet<Color_blindeens.Models.User> User { get; set; } = default!;
    }
}
