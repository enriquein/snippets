using System;

namespace System
{
    public static class DecimalExtensions
    {
        public static string ToStringSafe(this decimal? value, string format)
        {
            string val;
            try
            {
                val = value.Value.ToString(format);
            }
            catch (FormatException)
            {
                val = 0M.ToString();
            }
            catch (Exception)
            {
                val = 0M.ToString(format);
            }
            return val;
        }
    }
}
