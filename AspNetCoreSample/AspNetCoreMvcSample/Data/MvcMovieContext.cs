using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using AspNetCoreMvcSample.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreMvcSample.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options) : base(options)
        {
            
        }

        public DbSet<Movie> Movie { get; set; }
    }
}
