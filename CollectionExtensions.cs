namespace DotNetNuke.Prototype.CollectionExtensions
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Linq;

    using DotNetNuke.Common;

    public static class CollectionExtensions
    {
        /// <summary>Gets the value from the dictionary, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="InvalidCastException">
        /// This conversion is not supported. 
        /// -or- 
        /// the value is <c>null</c> and <typeparamref name="T"/> is a value type.
        /// -or-
        /// the value does not implement the <see cref="IConvertible"/> interface.
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is <c>null</c></exception>
        public static T Get<T>(this IDictionary dictionary, string key)
        {
            return dictionary.Get(key, default(T));
        }

        /// <summary>Gets the value from the dictionary, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the dictionary doesn't have a value for the given <paramref name="key"/>.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="InvalidCastException">
        /// This conversion is not supported. 
        /// -or- 
        /// the value is <c>null</c> and <typeparamref name="T"/> is a value type.
        /// -or-
        /// the value does not implement the <see cref="IConvertible"/> interface.
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is <c>null</c></exception>
        public static T Get<T>(this IDictionary dictionary, string key, T defaultValue)
        {
            return dictionary.Get(key, defaultValue, ConvertValue<T>);
        }

        /// <summary>Gets the value from the dictionary, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="converter">A function to convert the value as an object to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is <c>null</c></exception>
        public static T Get<T>(this IDictionary dictionary, string key, Func<object, T> converter)
        {
            return dictionary.Get(key, default(T), converter);
        }

        /// <summary>Gets the value from the dictionary, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the dictionary doesn't have a value for the given <paramref name="key"/>.</param>
        /// <param name="converter">A function to convert the value as an object to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is <c>null</c></exception>
        public static T Get<T>(this IDictionary dictionary, string key, T defaultValue, Func<string, T> converter)
        {
            return dictionary.Get(key, defaultValue, (object value) => ConvertValue(value, converter));
        }

        /// <summary>Gets the value from the dictionary, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="converter">A function to convert the value as an object to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is <c>null</c></exception>
        public static T Get<T>(this IDictionary dictionary, string key, Func<string, T> converter)
        {
            return dictionary.Get(key, default(T), (object value) => ConvertValue(value, converter));
        }

        /// <summary>Gets the value from the dictionary, returning the <paramref key="defaultValue"/> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the dictionary doesn't have a value for the given <paramref name="key"/>.</param>
        /// <param name="converter">A function to convert the value as an object to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is <c>null</c></exception>
        public static T Get<T>(this IDictionary dictionary, string key, T defaultValue, Func<object, T> converter)
        {
            Requires.NotNull("dictionary", dictionary);

            return dictionary.Contains(key) ? converter(dictionary[key]) : defaultValue;
        }

        /// <summary>Converts the <paramref name="value"/> into a <typeparamref name="T"/> instance.</summary>
        /// <typeparam name="T">The type of the value to return</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        private static T ConvertValue<T>(object value)
        {
            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the <paramref name="value" /> into a <typeparamref name="T" /> instance.</summary>
        /// <typeparam name="T">The type of the value to return</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <param name="converter">A function to convert a <see cref="string"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T" /> instance.</returns>
        private static T ConvertValue<T>(object value, Func<string, T> converter)
        {
            var formattable = value as IFormattable;
            return converter(value == null ? null : formattable == null ? value.ToString() : formattable.ToString(null, CultureInfo.InvariantCulture));
        }
    }
}
