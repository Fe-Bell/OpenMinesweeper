using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace OpenMinesweeper.Core
{
    /// <summary>
    /// A standard class to keep software configurations.
    /// This class should be serialized to a file.
    /// </summary>
    public class SoftwareConfig : SerializableContent
    {
        /// <summary>
        /// Keeps settings saved by the user.
        /// </summary>
        public List<KVPItem> CurrentSettings { get; set; }
        /// <summary>
        /// Keeps lists of resources, such as texts, images, objects.
        /// </summary>
        public List<GeneralResources.Resource> Resources { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public SoftwareConfig()
        {
            CurrentSettings = new List<KVPItem>();
            Resources = new List<GeneralResources.Resource>();
        }

        /// <summary>
        /// Keeps lists of resources, such as texts, images, objects.
        /// </summary>
        public static class GeneralResources
        {
            /// <summary>
            /// Defines a base ressource.
            /// </summary>
            [XmlInclude(typeof(EnumResource))]
            [XmlInclude(typeof(FileResource))]
            [XmlInclude(typeof(LanguageResource))]
            [XmlInclude(typeof(ObjectResource))]
            [XmlInclude(typeof(TextResource))]
            public class Resource
            {
                [XmlAttribute]
                public string Key { get; set; }
                public List<KVPItem> Content { get; set; }
                public Resource()
                {
                    Content = new List<KVPItem>();

                    if (Content.Count != 0)
                    {
                        Content.ForEach(x =>
                        {
                            if (string.IsNullOrEmpty(x.TypeOfKey))
                            {
                                x.TypeOfKey = typeof(string).FullName;
                            }
                            if (string.IsNullOrEmpty(x.TypeOfValue))
                            {
                                x.TypeOfValue = typeof(string).FullName;
                            }
                        });
                    }
                }

                public Dictionary<string, string> GetDictionary() => Content.ToDictionary(x => x.Key, y => y.Value);
            }
            /// <summary>
            /// Defines a file resource.
            /// </summary>
            public class FileResource : Resource
            {
                public enum DataSource_e
                {
                    [XmlEnum("1")]
                    FilePath = 1,
                    [XmlEnum("2")]
                    RawStream = 2
                }

                [XmlAttribute]
                public DataSource_e DataSource { get; set; }
            }
            /// <summary>
            /// Defines a text resource.
            /// </summary>
            public class TextResource : Resource
            {

            }
            /// <summary>
            /// Defines an enumeration resource.
            /// </summary>
            public class EnumResource : TextResource
            {
                [XmlAttribute]
                public string EnumBaseType { get; set; }

                [XmlAttribute]
                public string EnumName { get; set; }

                public EnumResource()
                {
                    EnumBaseType = typeof(int).Name;

                    Content = new List<KVPItem>();

                    if (Content.Count != 0)
                    {
                        Content.ForEach(x =>
                        {
                            if (string.IsNullOrEmpty(x.TypeOfKey))
                            {
                                x.TypeOfKey = typeof(uint).FullName;
                            }
                            if (string.IsNullOrEmpty(x.TypeOfValue))
                            {
                                x.TypeOfValue = typeof(string).FullName;
                            }
                        });
                    }
                }
            }
            /// <summary>
            /// Defines a language resource.
            /// </summary>
            public class LanguageResource : TextResource
            {
                [XmlAttribute]
                public string LanguageName { get; set; }

                public LanguageResource()
                {
                    Content = new List<KVPItem>();

                    if (Content.Count != 0)
                    {
                        Content.ForEach(x =>
                        {
                            if (string.IsNullOrEmpty(x.TypeOfKey))
                            {
                                x.TypeOfKey = typeof(uint).FullName;
                            }
                            if (string.IsNullOrEmpty(x.TypeOfValue))
                            {
                                x.TypeOfValue = typeof(string).FullName;
                            }
                        });
                    }
                }
            }
            /// <summary>
            /// Defines an object resource.
            /// </summary>
            public class ObjectResource : Resource
            {
                [XmlAttribute]
                public string TypeOfObject { get; set; }

                public Type GetObjectType() => Type.GetType(TypeOfObject);
            }
        }
    }
}
