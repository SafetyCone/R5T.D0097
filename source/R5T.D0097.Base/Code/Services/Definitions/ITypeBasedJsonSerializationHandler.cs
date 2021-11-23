using System;

using Newtonsoft.Json.Linq;

using R5T.T0064;


namespace R5T.D0097
{
    /// <summary>
    /// Handles JSON deserialization for all types implementing or descending from a type. (Serialization of any type to JSON is easy).
    /// Type-based.
    /// Synchronous service.
    /// </summary>
    [ServiceDefinitionMarker]
    public interface ITypeBasedJsonSerializationHandler<TBase> : IServiceDefinition
    {
        /// <summary>
        /// Used by factory during serialization. The machine message type is used to find the handler that will provide
        /// Successful handler identifications can be cached using type as the key.
        /// During serialization the handler is only needed for providing the serialization type identifier.
        /// </summary>
        bool CanHandle(Type machineMessageType);

        bool CanHandle(string serializationTypeIdentifier);

        TBase Deserialize(string serializationTypeIdentifier, JObject json);

        /// <summary>
        /// Specification used by factory during deserialization. The serialization will specify this type identifier, and the factory will lookup this handler based on that key.
        /// </summary>
        string GetSerializationTypeIdentitifer(Type machineMessageType);
    }
}
