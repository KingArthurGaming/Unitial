using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Unitial.Web.Areas.Identity.IdentityHostingStartup))]
namespace Unitial.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}