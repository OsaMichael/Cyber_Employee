using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cyber_Employee.Startup))]
namespace Cyber_Employee
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}