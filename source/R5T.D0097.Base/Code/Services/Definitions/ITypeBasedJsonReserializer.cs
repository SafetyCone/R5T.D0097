using System;

using Newtonsoft.Json.Linq;

using R5T.T0064;


namespace R5T.D0097
{
    /// <summary>
    /// Handles JSON de/serialization for types implementing or inheriting from a base type.
    /// </summary>
    [ServiceDefinitionMarker]
    public interface ITypeBasedJsonReserializer<TBase> : ITypeBasedJsonDeserializer<TBase>, ITypeBasedJsonSerializer<TBase>
    {
    }
}
