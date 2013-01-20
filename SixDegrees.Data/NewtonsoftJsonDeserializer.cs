﻿using System.IO;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

namespace SixDegrees.Data
{
    public class NewtonsoftJsonDeserializer : IDeserializer
    {
        private readonly JsonSerializer _serializer;

        /// <summary>
        ///     Default serializer
        /// </summary>
        public NewtonsoftJsonDeserializer()
        {
            ContentType = "application/json";
            _serializer = new JsonSerializer
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Include,
                    DefaultValueHandling = DefaultValueHandling.Include
                };
        }

        /// <summary>
        ///     Default serializer with overload for allowing custom Json.NET settings
        /// </summary>
        public NewtonsoftJsonDeserializer(JsonSerializer serializer)
        {
            ContentType = "application/json";
            _serializer = serializer;
        }

        /// <summary>
        ///     Content type for serialized content
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        ///     Unused for JSON Serialization
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        ///     Unused for JSON Serialization
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        ///     Unused for JSON Serialization
        /// </summary>
        public string Namespace { get; set; }


        public T Deserialize<T>(IRestResponse response)
        {
            using (var stringReader = new StringReader(response.Content))
            {
                using (var jsonReader = new JsonTextReader(stringReader))
                {
                    return _serializer.Deserialize<T>(jsonReader);
                }
            }
        }

        /// <summary>
        ///     Serialize the object as JSON
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>JSON as String</returns>
        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    jsonTextWriter.QuoteChar = '"';

                    _serializer.Serialize(jsonTextWriter, obj);

                    string result = stringWriter.ToString();
                    return result;
                }
            }
        }
    }
}