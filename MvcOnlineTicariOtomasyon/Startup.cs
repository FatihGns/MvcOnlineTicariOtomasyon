using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MvcOnlineTicariOtomasyon.Startup))]

namespace MvcOnlineTicariOtomasyon
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // SignalR rotasını ekle
            app.MapSignalR();
        }
    }
}