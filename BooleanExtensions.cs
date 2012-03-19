
namespace System
{
    public static class BooleanExtensions
    {
        public static string ToYesNo(this bool? value)
        {
            return (value.HasValue) ? value.Value.ToYesNo() : false.ToYesNo();
        }

        public static string ToYesNo(this bool value)
        {
            return (value) ? "Yes" : "No";
        }
    }
}
