using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace cleantalk.csharp.Helpers
{
    public static class EnumHelper
    {
        public static TEnum? ToEnum<TEnum>(this string value, bool ignoreCase = true)
            where TEnum : struct
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            TEnum result;
            if (Enum.TryParse(value, ignoreCase, out result))
            {
                return result;
            }

            return null;
        }

        public static string ToName(this Enum value)
        {
            var attribute = value.GetAttribute<DisplayAttribute>();

            return attribute == null ? value.ToString() : attribute.Name;
        }

        private static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            return attributes.Any() ? (T)attributes[0] : null;
        }
    }
}