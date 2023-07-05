using ApiCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Context
{
    public class ApiCatalogContext : DbContext
    {
        public ApiCatalogContext(DbContextOptions<ApiCatalogContext> options) : base(options) 
        { 
        
        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }
    }
}
