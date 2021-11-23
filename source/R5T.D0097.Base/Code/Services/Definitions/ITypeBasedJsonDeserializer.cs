using System;

using Newtonsoft.Json.Linq;

using R5T.Magyar;

using R5T.T0064;


namespace R5T.D0097
{
    /// <summary>
    /// Handles JSON deserialization for types implementing or inheriting from a base type.
    /// </summary>
    [ServiceDefinitionMarker]
    public interface ITypeBasedJsonDeserializer<TBase> : IServiceDefinition
    {
        WasSuccessful<TBase> Deserialize(JObject json);
    }
}
