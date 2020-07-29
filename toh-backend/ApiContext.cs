using Microsoft.EntityFrameworkCore;
using TourOfHeroes.Backend.Models;

namespace TourOfHeroes.Backend
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Hero> Heroes { get; set; }
    }
}
