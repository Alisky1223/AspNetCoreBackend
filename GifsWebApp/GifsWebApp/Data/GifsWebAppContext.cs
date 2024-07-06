using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GifsWebApp.Models;

namespace GifsWebApp.Data
{
    public class GifsWebAppContext : DbContext
    {
        public GifsWebAppContext (DbContextOptions<GifsWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<GifsWebApp.Models.Gif> Gif { get; set; } = default!;
    }
}
