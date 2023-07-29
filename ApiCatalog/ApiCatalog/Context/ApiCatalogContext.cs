using ApiCatalog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Context
{
    public class ApiCatalogContext : IdentityDbContext
    {
        public ApiCatalogContext(DbContextOptions<ApiCatalogContext> options) : base(options) 
        { 
        
        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }
    }
}
