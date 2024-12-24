using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(soundyard.club.Startup))]

namespace soundyard.club
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
