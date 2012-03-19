
namespace System
{
    public static class NullableExtensions
    {
        public static string ToStringSafe<T>(this Nullable<T> value) where T: struct
        {
            return value.HasValue ? value.Value.ToString() : string.Empty;
        }
    }
}
