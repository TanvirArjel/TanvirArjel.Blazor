using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TanvirArjel.Blazor.DependencyInjection
{
    /// <summary>
    /// This is doc
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// This is doc
        /// </summary>
        /// <param name="services"></param>
        public static void AddComponents(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            AddComponents(services, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void AddComponents(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Replace(ServiceDescriptor.Transient<IComponentActivator, ServiceProviderComponentActivator>());

            List<Assembly> assembliesToBeScanned = new List<Assembly>();

            if (assemblies == null || assemblies.Count() == 0)
            {
                assembliesToBeScanned.Add(Assembly.GetEntryAssembly());
            }
            else
            {
                assembliesToBeScanned = assemblies.OfType<Assembly>().ToList();
            }

            List<Type> componentsToBeRegistered = assembliesToBeScanned
                .SelectMany(assembly => assembly.GetTypes().Where(p => typeof(IComponent).IsAssignableFrom(p) && p.IsClass)).ToList();

            foreach (Type component in componentsToBeRegistered)
            {
                services.AddTransient(component);
            }
        }
    }
}
