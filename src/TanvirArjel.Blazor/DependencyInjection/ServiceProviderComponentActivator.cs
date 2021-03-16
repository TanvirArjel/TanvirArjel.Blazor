// <copyright file="ServiceProviderComponentActivator.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

using System;
using Microsoft.AspNetCore.Components;

namespace TanvirArjel.Blazor.DependencyInjection
{
    /// <summary>
    /// This will dependency injection support in blazor components.
    /// </summary>
    internal class ServiceProviderComponentActivator : IComponentActivator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceProviderComponentActivator"/> class.
        /// </summary>
        /// <param name="serviceProvider">The instance of <see cref="IServiceProvider"/>.</param>
        public ServiceProviderComponentActivator(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the instance of <see cref="IServiceProvider"/>.
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <inheritdoc/>
        public IComponent CreateInstance(Type componentType)
        {
            if (componentType == null)
            {
                throw new ArgumentNullException(nameof(componentType));
            }

            object instance = ServiceProvider.GetService(componentType);

            if (instance == null)
            {
                instance = Activator.CreateInstance(componentType);
            }

            if (instance is not IComponent component)
            {
                throw new ArgumentException($"The type {componentType.FullName} does not implement {nameof(IComponent)}.", nameof(componentType));
            }

            return component;
        }
    }
}
