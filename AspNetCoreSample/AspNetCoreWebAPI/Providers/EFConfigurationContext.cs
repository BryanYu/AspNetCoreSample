using AspNetCoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebAPI.Providers
{
    public class EFConfigurationContext : DbContext
    {
        public EFConfigurationContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<EFConfigurationValue> Values { get; set; }
    }
}