using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SM.FMA.Extensions;

public static class EnumExtensions
{
    public static Dictionary<TEnum, string> GetEnumDisplayNames<TEnum>() where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .ToDictionary(
                e => e,
                e => e.GetType()
                      .GetField(e.ToString())
                      ?.GetCustomAttribute<DisplayAttribute>()?.GetName() ?? e.ToString());
    }
} 