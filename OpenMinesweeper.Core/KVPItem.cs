using OpenMinesweeper.Core.Utils;
using System.Xml.Serialization;

namespace OpenMinesweeper.Core
{
    /// <summary>
    /// A simple Key-Value object.
    /// </summary>
    public class KVPItem
    {
        /// <summary>
        /// Defines a Key for an item.
        /// </summary>
        [XmlAttribute]
        public string Key { get; set; }
        /// <summary>
        /// Defines a Value for an item.
        /// </summary>
        [XmlAttribute]
        public string Value { get; set; }
        /// <summary>
        /// Defines the type of the Key.
        /// </summary>
        [XmlAttribute]
        public string TypeOfKey { get; set; }
        /// <summary>
        /// Defines the type of the Value.
        /// </summary>
        [XmlAttribute]
        public string TypeOfValue { get; set; }

        public KVPItem()
        {
            if (string.IsNullOrEmpty(TypeOfKey))
            {
                TypeOfKey = typeof(string).FullName;
            }

            if (string.IsNullOrEmpty(TypeOfValue))
            {
                TypeOfValue = typeof(string).FullName;
            }
        }

        /// <summary>
        /// Returns the Key cast to TypeOfKey. Null if the type is unknown or invalid.
        /// </summary>
        /// <returns></returns>
        public object GetParsedKey()
        {
            if (string.IsNullOrEmpty(TypeOfKey))
            {
                TypeOfKey = typeof(string).FullName;
            }

            var typeOfKey = System.Type.GetType(TypeOfKey);
            if (typeOfKey == null)
            {
                return null;
            }

            if (typeOfKey == typeof(string))
            {
                return Key.ToString();
            }
            else if (typeOfKey == typeof(uint))
            {
                try
                {
                    return NumericUtils.HexStringToUInt32(Key);
                }
                catch
                {
                    return null;
                }
            }
            else if (typeOfKey == typeof(int))
            {
                try
                {
                    return NumericUtils.HexStringToInt32(Key);
                }
                catch
                {
                    return null;
                }
            }
            else if (typeOfKey == typeof(ulong))
            {
                try
                {
                    return NumericUtils.HexStringToUInt64(Key);
                }
                catch
                {
                    return null;
                }
            }
            else if (typeOfKey == typeof(long))
            {
                try
                {
                    return NumericUtils.HexStringToInt64(Key);
                }
                catch
                {
                    return null;
                }
            }
            else if (typeOfKey == typeof(short))
            {
                try
                {
                    return NumericUtils.HexStringToInt16(Key);
                }
                catch
                {
                    return null;
                }
            }
            else if (typeOfKey == typeof(ushort))
            {
                try
                {
                    return NumericUtils.HexStringToUInt16(Key);
                }
                catch
                {
                    return null;
                }
            }
            else if (typeOfKey == typeof(double))
            {
                try
                {
                    return System.Convert.ToDouble(Key);
                }
                catch
                {
                    return null;
                }
            }
            else if (typeOfKey == typeof(decimal))
            {
                try
                {
                    return System.Convert.ToDecimal(Key);
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }
        /// <summary>
        /// Returns the Value cast to TypeOfValue. Null if the type is unknown or invalid.
        /// </summary>
        /// <returns></returns>
        public object GetParsedValue()
        {
            if (string.IsNullOrEmpty(TypeOfValue))
            {
                TypeOfValue = typeof(string).FullName;
            }

            var typeOfValue = System.Type.GetType(TypeOfValue);
            if(typeOfValue == null)
            {
                return null;
            }

            if(typeOfValue == typeof(string))
            {
                return Value.ToString();
            }
            else if(typeOfValue == typeof(uint))
            {
                try
                {               
                    return NumericUtils.HexStringToUInt32(Value);
                }
                catch
                {
                    return null;
                }
            }
            else if (typeOfValue == typeof(int))
            {
                try
                {
                    return NumericUtils.HexStringToInt32(Value);
                }
                catch
                {
                    return null;
                }
            }
            else if (typeOfValue == typeof(ulong))
            {
                try
                {
                    return NumericUtils.HexStringToUInt64(Value);
                }
                catch
                {
                    return null;
                }
            }
            else if (typeOfValue == typeof(long))
            {
                try
                {
                    return NumericUtils.HexStringToInt64(Value);
                }
                catch
                {
                    return null;
                }
            }
            else if (typeOfValue == typeof(short))
            {
                try
                {
                    return NumericUtils.HexStringToInt16(Value);
                }
                catch
                {
                    return null;
                }
            }
            else if (typeOfValue == typeof(ushort))
            {
                try
                {
                    return NumericUtils.HexStringToUInt16(Value);
                }
                catch
                {
                    return null;
                }
            }
            else if (typeOfValue == typeof(double))
            {
                try
                {
                    return System.Convert.ToDouble(Value);
                }
                catch
                {
                    return null;
                }
            }
            else if (typeOfValue == typeof(decimal))
            {
                try
                {
                    return System.Convert.ToDecimal(Value);
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }
    }
}