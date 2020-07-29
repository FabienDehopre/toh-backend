using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TourOfHeroes.Backend
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApiContext>();
                AddInitialData(context);
            }
        }

        private static void AddInitialData(ApiContext context)
        {
            context.Heroes.AddRange(
                new Models.Hero { Id = 11, Name = "Dr Nice" },
                new Models.Hero { Id = 12, Name = "Narco" },
                new Models.Hero { Id = 13, Name = "Bombasto" },
                new Models.Hero { Id = 14, Name = "Celeritas" },
                new Models.Hero { Id = 15, Name = "Magneta" },
                new Models.Hero { Id = 16, Name = "RubberMan" },
                new Models.Hero { Id = 17, Name = "Dynama" },
                new Models.Hero { Id = 18, Name = "Dr IQ" },
                new Models.Hero { Id = 19, Name = "Magma" },
                new Models.Hero { Id = 20, Name = "Tornado" }
            );
            context.SaveChanges();
        }
    }
}
