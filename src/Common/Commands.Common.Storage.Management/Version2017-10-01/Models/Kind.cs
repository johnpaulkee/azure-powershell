// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.Storage.Version2017_10_01.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Runtime;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines values for Kind.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Kind
    {
        [EnumMember(Value = "Storage")]
        Storage,
        [EnumMember(Value = "StorageV2")]
        StorageV2,
        [EnumMember(Value = "BlobStorage")]
        BlobStorage
    }
    internal static class KindEnumExtension
    {
        internal static string ToSerializedValue(this Kind? value)
        {
            return value == null ? null : ((Kind)value).ToSerializedValue();
        }

        internal static string ToSerializedValue(this Kind value)
        {
            switch( value )
            {
                case Kind.Storage:
                    return "Storage";
                case Kind.StorageV2:
                    return "StorageV2";
                case Kind.BlobStorage:
                    return "BlobStorage";
            }
            return null;
        }

        internal static Kind? ParseKind(this string value)
        {
            switch( value )
            {
                case "Storage":
                    return Kind.Storage;
                case "StorageV2":
                    return Kind.StorageV2;
                case "BlobStorage":
                    return Kind.BlobStorage;
            }
            return null;
        }
    }
}
