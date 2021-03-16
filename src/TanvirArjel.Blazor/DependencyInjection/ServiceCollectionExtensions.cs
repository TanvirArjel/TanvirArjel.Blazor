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
        /// This method will add all the blazor components from calling assembly to the dependency injection container.
        /// </summary>
        /// <param name="services">The type to be extended.</param>
        public static void AddComponents(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            AddComponents(services, (Assembly[])null);
        }

        /// <summary>
        /// This method will add all the blazor components from the provided assembly to the dependency injection container.
        /// </summary>
        /// <param name="services">The type to be extended.</param>
        /// <param name="assembly">The assembly containing the components to be registered.</param>
        public static void AddComponents(this IServiceCollection services, Assembly assembly)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            Assembly[] assemblies = assembly != null ? new Assembly[] { assembly } : null;

            AddComponents(services, assemblies);
        }

        /// <summary>
        /// This method will add all the blazor components from the provided assemblies to the dependency injection container.
        /// </summary>
        /// <param name="services">The type to be extended.</param>
        /// <param name="assemblies">The assemblies containing the components to be registered.</param>
        public static void AddComponents(this IServiceCollection services, Assembly[] assemblies)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.Replace(ServiceDescriptor.Transient<IComponentActivator, ServiceProviderComponentActivator>());

            List<Assembly> assembliesToBeScanned = new List<Assembly>();

            if (assemblies == null || assemblies.Length == 0)
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
