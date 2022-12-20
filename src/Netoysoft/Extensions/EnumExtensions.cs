﻿using System.ComponentModel;
using System.Reflection;

namespace System
{
    /// <summary>
    /// EnumExtensions
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// GetAttributeOfType
        /// </summary>
        public static T? GetAttributeOfType<T>(this Enum value) where T : Attribute
            => value.GetType().GetField(value.ToString())?.GetCustomAttribute<T>();

        /// <summary>
        /// GetDescription
        /// </summary>
        public static string GetDescription(this Enum value)
            => value.GetAttributeOfType<DescriptionAttribute>()?.Description ?? string.Empty;

    }
}