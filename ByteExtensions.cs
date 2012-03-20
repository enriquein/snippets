using System;

namespace ExtensionMethods
{
    /// <summary>
    /// Converts an array of bytes to its string representation.
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Improves upon the original ToString method. This method actually
        /// converts the byte array into its string representation.
        /// </summary>
        /// <param name="p">This byte array.</param>
        /// <returns>A string that represents the byte values in the array.</returns>
        public static string ToStringEx(this byte[] p)
        {
            if (p == null)
                return "";

            return Convert(p);
        }

        /// <summary>
        /// Improves upon the original ToString method. This method actually
        /// converts the byte into its string representation.
        /// </summary>
        /// <param name="p">This byte.</param>
        /// <returns>A string that represents the byte.</returns>
        public static string ToStringEx(this byte p)
        {
            byte[] byteArray = { p };

            return Convert(byteArray);
        }

        private static string Convert(byte[] p)
        {
            return BitConverter.ToString(p).Replace("-", "");
        }
    }
}