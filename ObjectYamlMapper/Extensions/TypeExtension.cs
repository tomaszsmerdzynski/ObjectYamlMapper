using System;
using System.Collections.Generic;

namespace ObjectYamlMapper.Extensions
{
    public static class TypeExtension
    {
        private static List<Type> _numericTypes => new List<Type>()
        {
            typeof(int),
            typeof(short),
            typeof(long),
            typeof(decimal),
            typeof(float),
            typeof(double)
        };

        public static bool IsString(this Type type)
        {
            return type == typeof(string);
        }

        public static bool IsNumeric(this Type type)
        {
            return _numericTypes.Contains(type);
        }

        public static bool IsCustomObject(this Type type)
        {
            return type.IsClass && !type.IsGenericType && type != typeof(string);
        }
    }
}
