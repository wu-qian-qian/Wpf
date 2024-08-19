using EventModule.Local;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventModule
{
    public class Demo
    {
        [STAThread]
        public static void Main(string[] args) {
            IServiceCollection services = new ServiceCollection();
            services.AddEventBus(AppDomain.CurrentDomain.GetAssemblies().Where(p => p.GetName().Name.Contains("Demo")));
            var sp = services.BuildServiceProvider();
            var e = sp.GetRequiredService<IEventBus>();
            e.Publish("Test", "你好");
        }
    }
    [EventNameAttribute("Test")]
    public class TestEvent : IEventHandler
    {
        public async Task Handle<T>(T eventData)
        {
            Console.WriteLine(eventData);
        }
    }
}
