using System;
using System.Collections.Generic;

using R5T.T0062;
using R5T.T0063;


namespace R5T.D0097.I001
{
    public static class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="TypeBasedJsonReserializer{TBase}"/> implementation of <see cref="ITypeBasedJsonReserializer{TBase}"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// /// Note: the <see cref="TypeBasedJsonReserializer{TBase}"/> service should not be used directly, but should be sub-classed (or used a as "has-a" component) in a class specific to the <typeparamref name="TBase"/> type.
        /// </summary>
        public static IServiceAction<ITypeBasedJsonReserializer<TBase>> AddTypeBasedJsonReserializerAction<TBase>(this IServiceAction _,
            IEnumerable<IServiceAction<ITypeBasedJsonSerializationHandler<TBase>>> typeBaseJsonSerializationHandlers)
        {
            var serviceAction = _.New<ITypeBasedJsonReserializer<TBase>>(services => services.AddTypeBasedJsonReserializer(
                typeBaseJsonSerializationHandlers));

            return serviceAction;
        }
    }
}
