using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PlaceService.Application.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void EnsureContextMigrated<T>(this IApplicationBuilder applicationBuilder) where T : DbContext
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var contextObj = serviceScope.ServiceProvider.GetService(typeof(T));

                var context = contextObj as DbContext;

                context.Database.Migrate();
            }
        }
    }
}
