using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using System.Text.Json;
using System.Xml.Linq;
using va_veis_healthdatarepo.Models.Responses;

namespace va_veis_healthdatarepo.Utilities
{
    public static class Serialization
    {
        /// <summary>
        /// Serialize an object to a string using Data Contract Json Serializer.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="classInstance">Instance of the object.</param>
        /// <returns></returns>
        public static string DataContractSerialize<T>(T classInstance)
        {
            using (var memoryStream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(memoryStream, classInstance);

                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// Deserialize an object to a string using Data Contract Json Serializer.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="payload">Instance of the object.</param>
        /// <returns>Deserialized Instance.</returns>
        public static T DataContractJsonDeserialize<T>(string payload)
        {
            using (var stream = new MemoryStream())
            {
                var settings = new DataContractJsonSerializerSettings()
                {
                    DateTimeFormat = new DateTimeFormat("s"),
                    KnownTypes = Assembly.GetExecutingAssembly().DefinedTypes
                };

                var data = Encoding.UTF8.GetBytes(payload);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var deserializer = new DataContractJsonSerializer(typeof(T), settings);

                return (T)deserializer.ReadObject(stream);
            }
        }

        /// <summary>
        /// Deserialize an object to a string using Data Contract Json Serializer.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="payload">Instance of the object.</param>
        /// <returns>Deserialized Instance.</returns>
        public static T DataContractDeserialize<T>(string payload)
        {
            using (var stream = new MemoryStream())
            {
                var settings = new DataContractSerializerSettings()
                {
                    KnownTypes = Assembly.GetExecutingAssembly().DefinedTypes
                };

                var data = Encoding.UTF8.GetBytes(payload);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var deserializer = new DataContractSerializer(typeof(T), settings);

                return (T)deserializer.ReadObject(stream);
            }
        }

        /// <summary>
        /// Deserializes a Json response message.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="response">String response.</param>
        /// <returns>Deserialized Instance.</returns>
        public static T DeserializeTokenResponse<T>(string response)
        {
            using (var stream = new MemoryStream())
            {
                var settings = new DataContractJsonSerializerSettings()
                {
                    DateTimeFormat = new DateTimeFormat("s"),
                    KnownTypes = Assembly.GetExecutingAssembly().DefinedTypes
                };

                var serializer = new DataContractJsonSerializer(typeof(T), settings);
                var writer = new StreamWriter(stream);
                writer.Write(response);
                writer.Flush();
                stream.Position = 0;
                var responseObject = (T)serializer.ReadObject(stream);

                return responseObject;
            }
        }

        /// <summary>
        /// Deserializes a Json response message.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="payload">String response.</param>
        /// <returns>Deserialized Instance.</returns>
        public static T JsonDeserialize<T>(string payload)
        {
            return JsonSerializer.Deserialize<T>(payload);
        }

        /// <summary>
        /// Serialize an object to a string using Json Serializer.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="classInstance">Instance of the object.</param>
        /// <returns></returns>
        public static string JsonSerialize<T>(T classInstance)
        {
            return JsonSerializer.Serialize(classInstance, typeof(T));
        }

        /// <summary>
        /// Serializes Class instance to a Stream.
        /// </summary>
        /// <typeparam name="T">Type of Class.</typeparam>
        /// <param name="obj">Instance of Class.</param>
        /// <returns>Memory Stream.</returns>
        public static MemoryStream ObjectToStream<T>(T obj)
        {
            var stream = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(T));
            ser.WriteObject(stream, obj);
            stream.Position = 0;

            return stream;
        }

        /// <summary>
        /// Deserialize Response.
        /// </summary>
        /// <typeparam name="T">Class Type.</typeparam>
        /// <param name="returnValue">Response as string.</param>
        /// <returns>Response Class Instance.</returns>
        public static T DeserializeResponse<T>(string returnValue)
        {
            T retObj;

            var enc = new UTF8Encoding();

            using (var ms = new MemoryStream())
            {
                var settings = new DataContractJsonSerializerSettings()
                {
                    DateTimeFormat = new DateTimeFormat("s"),
                    KnownTypes = Assembly.GetExecutingAssembly().DefinedTypes
                };

                ms.Write(enc.GetBytes(returnValue), 0, returnValue.Length);

                ms.Position = 0;

                var ser = new DataContractJsonSerializer(typeof(T), settings);
                retObj = (T)ser.ReadObject(ms);
            }
            return retObj;
        }

        /// <summary>
        /// Deserialize Response.
        /// </summary>
        /// <typeparam name="T">Class Type.</typeparam>
        /// <param name="returnValue">Response as string.</param>
        /// <returns>Response Class Instance.</returns>
        public static T DeserializeXmlResponse<T>(string returnValue)
        {
            T retObj;

            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(returnValue))
            {
                retObj = (T)serializer.Deserialize(reader);
            }

            return retObj;
        }

        /// <summary>
        /// Deserialize Response.
        /// </summary>
        /// <typeparam name="T">Class Type.</typeparam>
        /// <param name="stream"></param>
        /// <param name="returnValue">Response as string.</param>
        /// <returns>Response Class Instance.</returns>
        public static T DeserializeXmlResponse<T>(Stream stream)
        {
            T retObj;

            var serializer = new XmlSerializer(typeof(T));
            retObj = (T)serializer.Deserialize(stream);

            return retObj;
        }

        /// <summary>
        /// Serialize class instance to XML using XmlSerializer.
        /// </summary>
        /// <typeparam name="T">Type of instance.</typeparam>
        /// <param name="classInstance">Instance of type.</param>
        /// <returns>XML string.</returns>
        public static string XmlSerializeInstance<T>(T classInstance)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, classInstance);

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
