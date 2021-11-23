using System;

using Newtonsoft.Json.Linq;

using R5T.Magyar;

using R5T.T0064;


namespace R5T.D0097
{
    /// <summary>
    /// Handles JSON serialization for types implementing or inheriting from a base type.
    /// Serializes in a way that allows the implenting or descending type to be recovered.
    /// Synchronous service on purpose.
    /// Synchronous due to use in separate thread.
    /// </summary>
    [ServiceDefinitionMarker]
    public interface ITypeBasedJsonSerializer<TBase> : IServiceDefinition
    {
        WasSuccessful<JObject> Serialize(TBase message);
    }
}
