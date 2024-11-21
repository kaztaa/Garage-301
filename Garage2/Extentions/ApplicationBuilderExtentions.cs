using Garage301.Data;
using Gym.Data;

namespace Garage301.Extentions
{
    public static class ApplicationBuilderExtentions
    {
        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<Garage301Context>();

                try
                {
                    await SeedData.Init(context, services);
                }
                catch (Exception)
                {

                    throw;
                }
                return app;
            }
        }
    }
}
