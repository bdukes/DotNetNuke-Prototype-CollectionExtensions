using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace ExtensionCandidates
{
    public static class WeakDictionaryExtensions
    {
        public static T Get<T>(this IDictionary dictionary, string name, Func<object, T> converter = null)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");
            else if (!dictionary.Contains(name))
                throw new ArgumentException("name");
            else
                return (converter ?? DefaultConversion<T>)(dictionary[name]);
        }

        public static T Get<T>(this IDictionary dictionary, string name, Func<string, T> converter)
        { return dictionary.Get(name, (object value) => converter(value != null ? value.ToString() : null)); }

        public static T GetOrDefault<T>(this IDictionary dictionary, string name, T defaultValue = default(T), Func<object, T> converter = null)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");
            else
                return dictionary.Contains(name)
                           ? (converter ?? DefaultConversion<T>)(dictionary[name])
                           : defaultValue;
        }

        public static T GetOrDefault<T>(this IDictionary dictionary, string name, T defaultValue, Func<string, T> converter)
        { return dictionary.GetOrDefault(name, defaultValue, (object value) => converter(value != null ? value.ToString() : null)); }

        private static T DefaultConversion<T>(object value)
        {
            if (value is IConvertible)
                return (T)Convert.ChangeType(value, typeof(T));
            else
                return value is T ? (T)value : default(T);
        }
    }

    public static class NameValueCollectionExtensions
    {
        public static ILookup<string, string> ToLookup(this NameValueCollection collection)
        {
            return collection.AllKeys
                             .SelectMany(key => ParseValues(key, collection.GetValues(key)))
                             .ToLookup(pair => pair.Key, pair => pair.Value);
        }

        private static IEnumerable<KeyValuePair<string, string>> ParseValues(string key, string[] values)
        {
            return (values.Length == 1
                       ? values[0].Split(',')
                       : values).Select(value => new KeyValuePair<string, string>(key, value));
        }
    }

    public static class Usage
    {
        private static readonly IDictionary Map = new Hashtable
            {
            {"convertable-bool", "true"},
            {"parsable-bool", "true"},
            {"onoff-bool", "on"}
            };

        public static bool ExplicitConversion()
        { return Map.GetOrDefault("parsable-bool", false, bool.Parse); }

        public static bool ImplicitConversion()
        { return Map.GetOrDefault<bool>("convertable-bool"); }

        public static bool InlineConversion()
        { return Map.GetOrDefault("onoff-bool", converter: value => value.Equals("on")); }
    }
}