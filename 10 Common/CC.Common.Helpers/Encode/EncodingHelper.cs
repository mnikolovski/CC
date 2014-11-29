using System.Text;

namespace CC.Common.Helpers.Encode
{
    public static class EncodingHelper
    {
        /// <summary>
        /// Retrieve byte array from a string (UTF8 encoding used as default)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string value)
        {
            return value.ToBytes(Encoding.UTF8);
        }

        /// <summary>
        /// Retrieve string from a given byte array (UTF8 encoding used as default)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FromBytes(this byte[] value)
        {
            return value.FromBytes(Encoding.UTF8);
        }

        /// <summary>
        /// Retrieve byte array from a string and a given encoding
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }

        /// <summary>
        /// Retrieve string from a given byte array
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string FromBytes(this byte[] value, Encoding encoding)
        {
            return encoding.GetString(value);
        }
    }
}
