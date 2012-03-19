
namespace System
{
    public static class DateTimeExtensions
    {
        public static string ToStringSafe(this DateTime? date, string format)
        {
            var returnValue = string.Empty;

            try
            {
                returnValue = date.Value.ToString(format);
            }
            catch
            {
                returnValue = string.Empty;
            }
            
            return returnValue;
        }

        public static string ToShortDate(this DateTime? date)
        {
            return date.ToStringSafe("d");
        }
    }
}
