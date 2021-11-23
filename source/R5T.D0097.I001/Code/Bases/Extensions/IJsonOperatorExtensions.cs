using System;

using Newtonsoft.Json.Linq;

using R5T.T0090;

using Instances = R5T.D0097.I001.Instances;


namespace System
{
    public static class IJsonOperatorExtensions
    {
        public static JObject GetPayload(this IJsonOperator _,
            JObject json)
        {
            var output = json[Instances.JsonKey.Payload()];
            return output as JObject; // TODO, check if actually a JObject.
        }

        public static string GetSerializationTypeIdentifier(this IJsonOperator _,
            JObject json)
        {
            var serializationTypeIdentifier = json[Instances.JsonKey.SerializationTypeIdentifier()].Value<string>();
            return serializationTypeIdentifier;
        }
    }
}
