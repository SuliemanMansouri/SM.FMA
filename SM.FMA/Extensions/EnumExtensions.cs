using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SM.FMA.Extensions
{
    public static class EnumExtensions
    {
        public static Dictionary<T, string> GetEnumDisplayNames<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToDictionary(
                    e => e,
                    e =>
                    {
                        var fieldInfo = e.GetType().GetField(e.ToString());
                        if (fieldInfo == null) return e.ToString();
                        var displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();
                        return displayAttribute?.GetName() ?? e.ToString();
                    });
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();
            if (memberInfo == null) return enumValue.ToString();
            var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.GetName() ?? enumValue.ToString();
        }
    }
}
