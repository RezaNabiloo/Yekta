using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace BSG.ADInventory.Common
{
    public static class EnumHelper<T>
    {
        private static string lookupResource(Type resourceManagerProvider, string resourceKey)
        {
            var resourceKeyProperty = resourceManagerProvider.GetProperty(resourceKey,
                BindingFlags.Static | BindingFlags.Public, null, typeof(string),
                new Type[0], null);
            if (resourceKeyProperty != null)
            {
                return (string)resourceKeyProperty.GetMethod.Invoke(null, null);
            }

            return resourceKey; // Fallback with the key name
        }

        public static string GetDisplayName<T>(T value)
        {
            try
            {
                if (value == null) return string.Empty; // Handle null value

                var fieldInfo = value.GetType().GetField(value.ToString());

                if (fieldInfo == null) return value.ToString(); // If fieldInfo is not found

                var descriptionAttributes = fieldInfo.GetCustomAttributes(
                    typeof(DisplayAttribute), false) as DisplayAttribute[];

                if (descriptionAttributes == null || descriptionAttributes.Length == 0)
                    return value.ToString();

                if (descriptionAttributes[0].ResourceType != null)
                    return lookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

                return descriptionAttributes[0].Name;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static SelectList GetEnumSelectList()
    {
            Type enumType = typeof(T);

            // Check if T is actually an enum
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            // Get enum values
            var enumValues = Enum.DataValueType.GetValues(enumType).Cast<T>();

            // Create SelectList
            var selectListItems = enumValues
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = GetDisplayName(e)
                }).ToList();

            return new SelectList(selectListItems, "Value", "Text");
        }
    }
}
