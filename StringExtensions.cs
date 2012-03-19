using System.Globalization;

namespace System
{
    public static class StringExtensions
    {
        public static string SafeTrim(this string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value.Trim(); 
        }

        public static string ReplaceEmpty(this string value)
        {
            return (string.IsNullOrEmpty(value)) ? "N/A" : value;
        }

        public static string ToPhone(this string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return string.Empty;

            if (phone.Length == 10)
                return "(" + phone.Substring(0, 3) + ") " + phone.Substring(3, 3) + "-" + phone.Substring(6, 4);
            else
                return phone;
        }

        public static string RemoveChars(this string value, string[] chars)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            string val = value;
            for (int i = 0; i < chars.Length - 1; i++)
            {
                val = val.Replace(chars[i], string.Empty);
            }
            return val;
        }
        
        public static string ToTitleCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            var ti = CultureInfo.InvariantCulture.TextInfo;
            return ti.ToTitleCase(ti.ToLower(value));
        }

        public static DateTime? ToDate(this string value)
        {
            DateTime parsed;
            if (DateTime.TryParse(value, out parsed))
                return parsed;
            else
                return (DateTime?)null;
        }

        public static Nullable<T> To<T>(this string value) where T : struct
        {
            try
            {
                var type = typeof(T);
                var temp = default(T);
                // We use a bit of reflection magic since TryParse isn't inherited by any of the value types.
                var method = type.GetMethod("TryParse", new[] { typeof(string), Type.GetType(string.Format("{0}&", type.FullName)) });
                return (Nullable<T>)method.Invoke(null, new object[] { value, temp });
            }
            catch
            {
                return (Nullable<T>)null;
            }
        }

        public static string ToSSN(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            if (value.Length == 9)
                return string.Format("{0}-{1}-{2}", value.Substring(0, 3), value.Substring(3, 2), value.Substring(5, 4));
            else
                return value;
        }
    }
}
