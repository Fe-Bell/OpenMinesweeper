using System;
using System.Text.RegularExpressions;

namespace OpenMinesweeper.Core.Utils
{
    public static class NumericUtils
    {
        static NumericUtils()
        {

        }

        /// <summary>
        /// If the string represents a hex number, converts it to it's respective short value. 
        /// Only valid for bases 10 (AABBCCDDEE) and 16 (0xAABBCCDDEE).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static short HexStringToInt16(string value)
        {
            //Check if the value is an actual hex representation
            if (IsHex(value))
            {
                //If yes, then convert to int using base 16
                return Convert.ToInt16(value, 16);
            }
            else
            {
                //Check if it is a word or a number (Count must be 0)
                if (Regex.Matches(value, @"[a-zA-Z]").Count == 0)
                {
                    //If not, then convert to int using base 10
                    return Convert.ToInt16(value, 10);
                }
                else
                {
                    //If not a valid number, then throw exception.
                    throw new Exception("The value provided is not a valid hexadecimal string.");
                }
            }
        }
        /// <summary>
        /// If the string represents a hex number, converts it to it's respective ushort value. 
        /// Only valid for bases 10 (AABBCCDDEE) and 16 (0xAABBCCDDEE).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ushort HexStringToUInt16(string value)
        {
            //Check if the value is an actual hex representation
            if (IsHex(value))
            {
                //If yes, then convert to int using base 16
                return Convert.ToUInt16(value, 16);
            }
            else
            {
                //Check if it is a word or a number (Count must be 0)
                if (Regex.Matches(value, @"[a-zA-Z]").Count == 0)
                {
                    //If not, then convert to int using base 10
                    return Convert.ToUInt16(value, 10);
                }
                else
                {
                    //If not a valid number, then throw exception.
                    throw new Exception("The value provided is not a valid hexadecimal string.");
                }
            }
        }
        /// <summary>
        /// If the string represents a hex number, converts it to it's respective int32 value. 
        /// Only valid for bases 10 (AABBCCDDEE) and 16 (0xAABBCCDDEE).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int HexStringToInt32(string value)
        {
            //Check if the value is an actual hex representation
            if (IsHex(value))
            {
                //If yes, then convert to int using base 16
                return Convert.ToInt32(value, 16);
            }
            else
            {
                //Check if it is a word or a number (Count must be 0)
                if (Regex.Matches(value, @"[a-zA-Z]").Count == 0)
                {
                    //If not, then convert to int using base 10
                    return Convert.ToInt32(value, 10);
                }
                else
                {
                    //If not a valid number, then throw exception.
                    throw new Exception("The value provided is not a valid hexadecimal string.");
                }
            }
        }
        /// <summary>
        /// If the string represents a hex number, converts it to it's respective uint value. 
        /// Only valid for bases 10 (AABBCCDDEE) and 16 (0xAABBCCDDEE).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static uint HexStringToUInt32(string value)
        {
            //Check if the value is an actual hex representation
            if (IsHex(value))
            {
                //If yes, then convert to int using base 16
                return Convert.ToUInt32(value, 16);
            }
            else
            {
                //Check if it is a word or a number (Count must be 0)
                if (Regex.Matches(value, @"[a-zA-Z]").Count == 0)
                {
                    //If not, then convert to int using base 10
                    return Convert.ToUInt32(value, 10);
                }
                else
                {
                    //If not a valid number, then throw exception.
                    throw new Exception("The value provided is not a valid hexadecimal string.");
                }
            }
        }
        /// <summary>
        /// If the string represents a hex number, converts it to it's respective long value. 
        /// Only valid for bases 10 (AABBCCDDEE) and 16 (0xAABBCCDDEE).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long HexStringToInt64(string value)
        {
            //Check if the value is an actual hex representation
            if (IsHex(value))
            {
                //If yes, then convert to int using base 16
                return Convert.ToInt64(value, 16);
            }
            else
            {
                //Check if it is a word or a number (Count must be 0)
                if (Regex.Matches(value, @"[a-zA-Z]").Count == 0)
                {
                    //If not, then convert to int using base 10
                    return Convert.ToInt64(value, 10);
                }
                else
                {
                    //If not a valid number, then throw exception.
                    throw new Exception("The value provided is not a valid hexadecimal string.");
                }
            }
        }
        /// <summary>
        /// If the string represents a hex number, converts it to it's respective ulong value. 
        /// Only valid for bases 10 (AABBCCDDEE) and 16 (0xAABBCCDDEE).
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ulong HexStringToUInt64(string value)
        {
            //Check if the value is an actual hex representation
            if (IsHex(value))
            {
                //If yes, then convert to int using base 16
                return Convert.ToUInt64(value, 16);
            }
            else
            {
                //Check if it is a word or a number (Count must be 0)
                if (Regex.Matches(value, @"[a-zA-Z]").Count == 0)
                {
                    //If not, then convert to int using base 10
                    return Convert.ToUInt64(value, 10);
                }
                else
                {
                    //If not a valid number, then throw exception.
                    throw new Exception("The value provided is not a valid hexadecimal string.");
                }
            }
        }
        /// <summary>
        /// Checks if the given string is an hexadecimal number. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsHex(string value)
        {
            //Checks the string for a 0xAABBCCDDEE or just AABBCCDDEE pattern.
            return value.StartsWith("0x") && Regex.IsMatch(value, @"\A\b(0[xX])?[0-9a-fA-F]+\b\Z");// || Regex.IsMatch(value, @"\A\b[0-9a-fA-F]+\b\Z");
        }
    }
}
