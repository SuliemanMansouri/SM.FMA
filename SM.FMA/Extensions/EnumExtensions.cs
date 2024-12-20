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
                    e => e.GetType()
                          .GetField(e.ToString())
                          .GetCustomAttribute<DisplayAttribute>()?.GetName() ?? e.ToString());
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                ?.GetName() ?? enumValue.ToString();
        }
    }
}
