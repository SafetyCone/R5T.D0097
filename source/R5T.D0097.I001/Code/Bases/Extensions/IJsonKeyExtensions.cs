using System;

using R5T.T0090;

using R5T.D0097.I001;


namespace System
{
    public static class IJsonKeyExtensions
    {
        public static string Payload(this IJsonKey _)
        {
            return JsonKeys.Payload;
        }

        public static string SerializationTypeIdentifier(this IJsonKey _)
        {
            return JsonKeys.SerializationTypeIdentifier;
        }
    }
}
