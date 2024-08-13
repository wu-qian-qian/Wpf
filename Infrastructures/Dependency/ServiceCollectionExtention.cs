using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dependency
{
    public static class ServiceCollectionExtention
    {
        public static ServiceCollection AddDependencyInjection(this ServiceCollection services, IEnumerable<Assembly> assemblys)
        {
            foreach (Assembly assembly in assemblys)
            {
                //object标识基类，带有该标识将会自动注入或者带有特性标识
                var types = assembly.GetTypes().Where(p => (!p.IsAbstract && p.IsAssignableFrom(typeof(IDependencyInjection))) || p.GetCustomAttribute<DependencyAttribute>() != null);
                foreach (var type in types)
                {
                    //type可以添加规则
                    var custom = type.GetCustomAttribute<DependencyAttribute>();
                    if (custom != null && custom.CanDependency == false)
                        continue;
                    services.AddScoped(type);
                }
            }
            return services;
        }
    }
}
