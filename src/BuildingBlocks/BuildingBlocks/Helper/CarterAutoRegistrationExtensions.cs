using Carter;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class CarterAutoRegistrationExtensions
{
    public static IServiceCollection AddCarterAutoDiscovery(
       this IServiceCollection services,
       IEnumerable<Assembly> assemblies,
       Action<CarterConfigurator>? configure = null)
    {
        return services.AddCarter(null, config =>
        {
            configure?.Invoke(config);

            var moduleTypes = assemblies
                .SelectMany(a =>
                {
                    try { return a.GetExportedTypes(); }
                    catch { return Array.Empty<Type>(); }
                })
                .Where(t => typeof(ICarterModule).IsAssignableFrom(t)
                            && t.IsClass
                            && !t.IsAbstract)
                .ToArray();

            if (moduleTypes.Length > 0)
                config.WithModules(moduleTypes);
        });
    }

    //public static IServiceCollection AddCarterAutoDiscovery(this IServiceCollection services, Action<CarterConfigurator>? configure = null)
    //{
    //    return services.AddCarter(null, config =>
    //    {
    //        // Apply user config if provided
    //        configure?.Invoke(config);

    //        // Find all Carter modules in the AppDomain
    //        var moduleTypes = AppDomain.CurrentDomain
    //            .GetAssemblies()
    //            .Where(a => !a.IsDynamic &&
    //                        !string.IsNullOrWhiteSpace(a.FullName))
    //            .SelectMany(a =>
    //            {
    //                try
    //                {
    //                    return a.GetExportedTypes();
    //                }
    //                catch
    //                {
    //                    return Array.Empty<Type>();
    //                }
    //            })
    //            .Where(t => typeof(ICarterModule).IsAssignableFrom(t) &&
    //                        t.IsClass &&
    //                        !t.IsAbstract)
    //            .ToArray();

    //        // Register all discovered modules
    //        if (moduleTypes.Length > 0)
    //            config.WithModules(moduleTypes);
    //    });
    //}
}
