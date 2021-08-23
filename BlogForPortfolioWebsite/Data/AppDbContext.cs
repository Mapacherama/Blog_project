using BlogForPortfolioWebsite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace BlogForPortfolioWebsite.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options)
        {
            
        }

       
    }
}