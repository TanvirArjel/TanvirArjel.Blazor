// <copyright file="ServiceCollectionExtensions.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

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
    /// Contains all the extension methods of <see cref="IServiceCollection"/> for adding
    /// Blazor components to the dependency injection containders.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// This method will add all the blazor components from the calling assembly to the dependency injection container.
        /// </summary>
        /// <param name="services">The type to be extended.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is <see langword="null"/>.</exception>
        public static void AddComponents(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            Assembly[] assemblies = new[] { Assembly.GetCallingAssembly() };

            AddComponents(services, assemblies);
        }

        /// <summary>
        /// This method will add all the blazor components from the provided assembly to the dependency injection container.
        /// </summary>
        /// <param name="services">The type to be extended.</param>
        /// <param name="assembly">The assembly containing the components to be registered.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is <see langword="null"/>.</exception>
        public static void AddComponents(this IServiceCollection services, Assembly assembly)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            Assembly[] assemblies = new Assembly[] { assembly ?? Assembly.GetCallingAssembly() };

            AddComponents(services, assemblies);
        }

        /// <summary>
        /// This method will add all the blazor components from the provided assemblies to the dependency injection container.
        /// </summary>
        /// <param name="services">The type to be extended.</param>
        /// <param name="assemblies">The assemblies containing the components to be registered.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is <see langword="null"/>.</exception>
        public static void AddComponents(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Replace(ServiceDescriptor.Transient<IComponentActivator, ServiceProviderComponentActivator>());

            if (assemblies?.Any() == false)
            {
                assemblies = new List<Assembly>() { Assembly.GetCallingAssembly() };
            }

            List<Type> componentsToBeRegistered = assemblies
                .SelectMany(assembly => assembly.GetTypes().Where(p => typeof(IComponent).IsAssignableFrom(p) && p.IsClass)).ToList();

            foreach (Type component in componentsToBeRegistered)
            {
                services.AddTransient(component);
            }
        }
    }
}
