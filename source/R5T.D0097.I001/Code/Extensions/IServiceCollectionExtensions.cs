using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using R5T.T0063;


namespace R5T.D0097.I001
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="TypeBasedJsonReserializer{TBase}"/> implementation of <see cref="ITypeBasedJsonReserializer{TBase}"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// Note: the <see cref="TypeBasedJsonReserializer{TBase}"/> service should not be used directly, but should be sub-classed (or used a as "has-a" component) in a class specific to the <typeparamref name="TBase"/> type.
        /// </summary>
        public static IServiceCollection AddTypeBasedJsonReserializer<TBase>(this IServiceCollection services,
            IEnumerable<IServiceAction<ITypeBasedJsonSerializationHandler<TBase>>> typeBasedJsonSerializationHandlers)
        {
            services
                .Run(typeBasedJsonSerializationHandlers)
                .AddSingleton<ITypeBasedJsonReserializer<TBase>, TypeBasedJsonReserializer<TBase>>();

            return services;
        }
    }
}