using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace OpenMinesweeper.Core
{
    public abstract class SerializableContent
    {
        public SerializableContent()
        {

        }

        /// <summary>
        /// Serializes an object to its XML element equivalent.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public XDocument SerializeXML()
        {
            var doc = new XDocument();          
       
            using (var writer = doc.CreateWriter())
            {
                var serializer = new XmlSerializer(GetType());
                serializer.Serialize(writer, this);                
            }

            return doc;
        }

        /// <summary>
        /// Deserializes an XML object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static T DeserializeXML<T>(XDocument doc) where T : SerializableContent
        {
            T obj = default(T);

            if(doc != null)
            {
                using (var reader = doc.CreateReader())
                {
                    var serializer = new XmlSerializer(typeof(T));
                    obj = (T)serializer.Deserialize(reader);
                }
            }           

            return obj;
        }

        /// <summary>
        /// Deserializes an XML path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static T DeserializeXML<T>(string filepath) where T : SerializableContent
        {
            var xml = XDocument.Load(filepath);
            return DeserializeXML<T>(xml);
        }

        /// <summary>
        /// Serializes an object to its JSON element equivalent.
        /// </summary>
        /// <returns></returns>
        public string SerializeJSON()
        {
            byte[] json = null;

            //Create a stream to serialize the object to.  
            using (var ms = new MemoryStream())
            {
                // Serializer the User object to the stream.  
                var ser = new DataContractJsonSerializer(GetType());
                ser.WriteObject(ms, this);
                json = ms.ToArray();              
            }
                      
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        /// <summary>
        /// Deserializes a JSON object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeJSON<T>(string json)
        {
            if (string.IsNullOrEmpty(json)) return default(T);

            T obj = default(T);

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var ser = new DataContractJsonSerializer(obj.GetType());
                obj = (T)ser.ReadObject(ms);
            }
  
            return obj;
        }
    }
}
