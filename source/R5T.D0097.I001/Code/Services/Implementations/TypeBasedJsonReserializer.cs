using System;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

using R5T.Magyar;

using R5T.T0064;


namespace R5T.D0097.I001
{
    [ServiceImplementationMarker]
    public class TypeBasedJsonReserializer<TBase> : ITypeBasedJsonReserializer<TBase>, IServiceImplementation
    {
        private IEnumerable<ITypeBasedJsonSerializationHandler<TBase>> TypeBasedJsonSerializationHandlers { get; }

        private Dictionary<Type, string> SerializationTypeIdentifiersByType { get; } = new Dictionary<Type, string>();
        private Dictionary<string, ITypeBasedJsonSerializationHandler<TBase>> HandlersBySerializationTypeIdentifier { get; } = new Dictionary<string, ITypeBasedJsonSerializationHandler<TBase>>();


        public TypeBasedJsonReserializer(IEnumerable<ITypeBasedJsonSerializationHandler<TBase>> typeBasedJsonSerializationHandlers)
        {
            this.TypeBasedJsonSerializationHandlers = typeBasedJsonSerializationHandlers;
        }

        public WasSuccessful<TBase> Deserialize(JObject json)
        {
            var serializationTypeIdentifier = Instances.JsonOperator.GetSerializationTypeIdentifier(json);

            var hasHandler = this.HasHandler(serializationTypeIdentifier);
            if (!hasHandler)
            {
                return WasSuccessful.Unsuccessful<TBase>();
            }

            var payload = Instances.JsonOperator.GetPayload(json);

            var machineMessage = hasHandler.Result.Deserialize(serializationTypeIdentifier, payload);

            return WasSuccessful.Successful(machineMessage);
        }

        private WasFound<ITypeBasedJsonSerializationHandler<TBase>> HasHandler(string serializationTypeIdentifier)
        {
            var handlerAlreadyRegistered = this.HandlersBySerializationTypeIdentifier.ContainsKey(serializationTypeIdentifier);
            if (handlerAlreadyRegistered)
            {
                // Return the handler.
                var handler = this.HandlersBySerializationTypeIdentifier[serializationTypeIdentifier];

                return WasFound.Found(handler);
            }

            // Else, need to look for it.
            foreach (var handler in this.TypeBasedJsonSerializationHandlers)
            {
                var handlerCanHandleSerializationTypeIdentifier = handler.CanHandle(serializationTypeIdentifier);
                if (handlerCanHandleSerializationTypeIdentifier)
                {
                    // Register it for later use.
                    this.HandlersBySerializationTypeIdentifier.Add(serializationTypeIdentifier, handler);

                    // Then return the handler.
                    return WasFound.Found(handler);
                }
            }

            // Else, if here, there is no handler for this data.
            return WasFound.NotFound<ITypeBasedJsonSerializationHandler<TBase>>();
        }

        public WasSuccessful<JObject> Serialize(TBase message)
        {
            var machineMessageType = message.GetType();

            var hasSerializationTypeIdentifier = this.HasSerializationTypeIdentifier(machineMessageType);
            if (!hasSerializationTypeIdentifier)
            {
                return WasSuccessful.Unsuccessful<JObject>();
            }

            var payloadJson = JToken.FromObject(message);

            var json = new JObject
            {
                [Instances.JsonKey.SerializationTypeIdentifier()] = hasSerializationTypeIdentifier.Result,
                [Instances.JsonKey.Payload()] = payloadJson
            };

            return WasSuccessful.From(json);
        }

        private WasFound<string> HasSerializationTypeIdentifier(Type machineMessageType)
        {
            var serializeTypeIdentifierAlreadyRegistered = this.SerializationTypeIdentifiersByType.ContainsKey(machineMessageType);
            if (serializeTypeIdentifierAlreadyRegistered)
            {
                var serializationTypeIdentifier = this.SerializationTypeIdentifiersByType[machineMessageType];

                return WasFound.Found(serializationTypeIdentifier);
            }

            // Else, need to look for a handler.
            foreach (var handler in this.TypeBasedJsonSerializationHandlers)
            {
                var handlerCanHandleMachineMessageType = handler.CanHandle(machineMessageType);
                if (handlerCanHandleMachineMessageType)
                {
                    // Get the serialization type identifier.
                    var serializationTypeIdentifier = handler.GetSerializationTypeIdentitifer(machineMessageType);

                    // Register it for later use.
                    this.SerializationTypeIdentifiersByType.Add(machineMessageType, serializationTypeIdentifier);

                    // Then return it.
                    return WasFound.Found(serializationTypeIdentifier);
                }
            }

            // Else, if here, there is no handler for this type.
            return WasFound.NotFound<string>();
        }
    }
}
