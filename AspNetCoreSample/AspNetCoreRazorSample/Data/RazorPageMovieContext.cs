using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspNetCoreRazorSample.Models;

namespace AspNetCoreRazorSample.Data
{
    public class RazorPageMovieContext : DbContext
    {
        public RazorPageMovieContext (DbContextOptions<RazorPageMovieContext> options)
            : base(options)
        {
        }

        public DbSet<AspNetCoreRazorSample.Models.Movie> Movie { get; set; }
    }
}
