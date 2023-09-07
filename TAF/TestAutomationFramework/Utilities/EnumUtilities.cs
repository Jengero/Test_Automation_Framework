namespace TAF.Core.Utilities
{
    public static class EnumUtilities
    {
        public static T ParseEnum<T>(string value) where T : IComparable, IFormattable, IConvertible
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}