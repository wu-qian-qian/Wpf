using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
 

namespace EventModule.Local
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            List<Type> busType = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
               var bus = assembly.GetTypes().Where(p => !p.IsAbstract && (typeof(IEventHandler)).IsAssignableFrom(p));
                busType.AddRange(bus);
            }
            return AddEventBus(services, busType);
        }
        public static IServiceCollection AddEventBus(this IServiceCollection services, IEnumerable<Type> handlerTypes)
        {
            foreach (Type type in handlerTypes)
            {
                services.AddScoped(type, type);
            } 
            services.AddSingleton<IEventBus>(sp =>
            {
                var manager = new EventManager();
                EventModule bus = new EventModule(sp,new EventManager());
                foreach (Type type in handlerTypes)
                {
                    var eventNames = type.GetCustomAttributes<EventNameAttribute>();
                    if (!eventNames.Any())
                        throw new ApplicationException($"{type}为标记特性");
                    foreach (var eventName in eventNames)
                    {
                        bus.Subscribe(eventName.Name, type);
                    }
                }
                return bus;
            });
            return services;
        }

    }
}
