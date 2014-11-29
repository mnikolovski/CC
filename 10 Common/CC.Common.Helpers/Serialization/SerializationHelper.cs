using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Formatting = Newtonsoft.Json.Formatting;

namespace CC.Common.Helpers.Serialization
{
    public static class SerializationHelper
    {
        /// <summary>
        /// Serialize an instance to json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="shouldIdent"></param>
        /// <param name="includeTypes"></param>
        /// <returns></returns>
        public static string SerializeToJSon<T>(this T instance, bool shouldIdent = true, bool includeTypes = false)
        {            
            var _serializer = new JsonSerializer();
            _serializer.Converters.Add(new IsoDateTimeConverter());
            _serializer.NullValueHandling = NullValueHandling.Ignore;
            if (includeTypes)
            {
                _serializer.TypeNameHandling = TypeNameHandling.All;
            }
            else
            {
                _serializer.TypeNameHandling = TypeNameHandling.None;
            }

            _serializer.TypeNameAssemblyFormat = FormatterAssemblyStyle.Full;
            
            using (var _ms = new MemoryStream())
            {
                using (var _sw = new StreamWriter(_ms))
                {
                    using (var _writer = new JsonTextWriter(_sw))
                    {
                        _writer.Formatting = shouldIdent ? Formatting.Indented :  Formatting.None;
                        _serializer.Serialize(_writer, instance);
                        _sw.Flush();
                        var _json = Encoding.UTF8.GetString(_ms.ToArray());
                        return _json;
                    }
                }
            }
        }

        /// <summary>
        /// Deserialize an instace from json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeFromJSon<T>(string json)
        {
            var _jsonSerializerSettings = new JsonSerializerSettings();
            _jsonSerializerSettings.TypeNameHandling = TypeNameHandling.All;
            _jsonSerializerSettings.TypeNameAssemblyFormat = FormatterAssemblyStyle.Full;
            _jsonSerializerSettings.Converters.Add(new IsoDateTimeConverter());

            var _result = JsonConvert.DeserializeObject<T>(json, _jsonSerializerSettings);

            return _result;
        }
    }
}
