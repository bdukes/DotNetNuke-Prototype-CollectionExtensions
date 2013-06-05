namespace DotNetNuke.Prototype.CollectionExtensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;

    using DotNetNuke.Common;

    public static class CollectionExtensions
    {
        /// <summary>Gets the value from the dictionary, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="InvalidCastException">
        /// the value is <c>null</c> and <typeparamref name="T"/> is a value type, or
        /// the value does not implement the <see cref="IConvertible"/> interface and
        /// no cast is defined from the value to <typeparamref name="T"/>
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is <c>null</c></exception>
        public static T Get<T>(this IDictionary dictionary, string key)
        {
            return dictionary.Get(key, default(T));
        }

        /// <summary>Gets the value from the lookup, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="lookup">The lookup.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="InvalidCastException">
        /// the value is <c>null</c> and <typeparamref name="T"/> is a value type, or
        /// the value does not implement the <see cref="IConvertible"/> interface and
        /// no cast is defined from the value to <typeparamref name="T"/>
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="lookup"/> is <c>null</c></exception>
        /// <exception cref="InvalidOperationException"><paramref name="lookup"/> has multiple values for the given <paramref name="key"/></exception>
        public static T Get<T>(this ILookup<string, string> lookup, string key)
        {
            return lookup.ToDictionary(key).Get<T>(key);
        }

        /// <summary>Gets the value from the collection, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="InvalidCastException">
        /// the value is <c>null</c> and <typeparamref name="T"/> is a value type, or
        /// the value does not implement the <see cref="IConvertible"/> interface and
        /// no cast is defined from the value to <typeparamref name="T"/>
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c></exception>
        /// <exception cref="InvalidOperationException"><paramref name="collection"/> has multiple values for the given <paramref name="key"/></exception>
        public static T Get<T>(this NameValueCollection collection, string key)
        {
            return collection.ToLookup().Get<T>(key);
        }

        /// <summary>Gets the value from the XML node's child elements, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="node">An XML node which containers other elements.</param>
        /// <param name="key">The name of the element from which to get the value.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="InvalidCastException">
        /// the value is <c>null</c> and <typeparamref name="T"/> is a value type, or
        /// the value does not implement the <see cref="IConvertible"/> interface and
        /// no cast is defined from the value to <typeparamref name="T"/>
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c></exception>
        public static T Get<T>(this XContainer node, string key)
        {
            return node.ToDictionary().Get<T>(key);
        }

        /// <summary>Gets the value from the XML node's child elements, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="node">An XML node which containers other elements.</param>
        /// <param name="key">The name of the element from which to get the value.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="InvalidCastException">
        /// the value is <c>null</c> and <typeparamref name="T"/> is a value type, or
        /// the value does not implement the <see cref="IConvertible"/> interface and
        /// no cast is defined from the value to <typeparamref name="T"/>
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c></exception>
        public static T Get<T>(this IXPathNavigable node, string key)
        {
            return node.ToDictionary().Get<T>(key);
        }

        /// <summary>Gets the value from the dictionary, returning <paramref name="defaultValue"/> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the dictionary doesn't have a value for the given <paramref name="key"/>.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="InvalidCastException">
        /// the value is <c>null</c> and <typeparamref name="T"/> is a value type, or
        /// the value does not implement the <see cref="IConvertible"/> interface and
        /// no cast is defined from the value to <typeparamref name="T"/>
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is <c>null</c></exception>
        public static T Get<T>(this IDictionary dictionary, string key, T defaultValue)
        {
            return dictionary.Get(key, defaultValue, ConvertValue<T>);
        }

        /// <summary>Gets the value from the lookup, returning <paramref name="defaultValue"/> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="lookup">The lookup.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the lookup doesn't have a value for the given <paramref name="key"/>.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="InvalidCastException">
        /// the value is <c>null</c> and <typeparamref name="T"/> is a value type, or
        /// the value does not implement the <see cref="IConvertible"/> interface and
        /// no cast is defined from the value to <typeparamref name="T"/>
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="lookup"/> is <c>null</c></exception>
        /// <exception cref="InvalidOperationException"><paramref name="lookup"/> has multiple values for the given <paramref name="key"/></exception>
        public static T Get<T>(this ILookup<string, string> lookup, string key, T defaultValue)
        {
            return lookup.ToDictionary(key).Get(key, defaultValue);
        }

        /// <summary>Gets the value from the collection, returning <paramref name="defaultValue"/> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the collection doesn't have a value for the given <paramref name="key"/>.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="InvalidCastException">
        /// the value is <c>null</c> and <typeparamref name="T"/> is a value type, or
        /// the value does not implement the <see cref="IConvertible"/> interface and
        /// no cast is defined from the value to <typeparamref name="T"/>
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c></exception>
        /// <exception cref="InvalidOperationException"><paramref name="collection"/> has multiple values for the given <paramref name="key"/></exception>
        public static T Get<T>(this NameValueCollection collection, string key, T defaultValue)
        {
            return collection.ToLookup().Get(key, defaultValue);
        }

        /// <summary>Gets the value from the XML node's child elements, returning <paramref name="defaultValue"/>  if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="node">An XML node which containers other elements.</param>
        /// <param name="key">The name of the element from which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the node doesn't have a value for the given <paramref name="key"/>.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="InvalidCastException">
        /// the value is <c>null</c> and <typeparamref name="T"/> is a value type, or
        /// the value does not implement the <see cref="IConvertible"/> interface and
        /// no cast is defined from the value to <typeparamref name="T"/>
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c></exception>
        public static T Get<T>(this XContainer node, string key, T defaultValue)
        {
            return node.ToDictionary().Get(key, defaultValue);
        }

        /// <summary>Gets the value from the XML node's child elements, returning <paramref name="defaultValue"/>  if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="node">An XML node which containers other elements.</param>
        /// <param name="key">The name of the element from which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the node doesn't have a value for the given <paramref name="key"/>.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="InvalidCastException">
        /// the value is <c>null</c> and <typeparamref name="T"/> is a value type, or
        /// the value does not implement the <see cref="IConvertible"/> interface and
        /// no cast is defined from the value to <typeparamref name="T"/>
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c></exception>
        public static T Get<T>(this IXPathNavigable node, string key, T defaultValue)
        {
            return node.ToDictionary().Get(key, defaultValue);
        }

        /// <summary>Gets the value from the dictionary, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="converter">A function to convert the value as an <see cref="object"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is <c>null</c></exception>
        public static T Get<T>(this IDictionary dictionary, string key, Func<object, T> converter)
        {
            return dictionary.Get(key, default(T), converter);
        }

        /// <summary>Gets the value from the lookup, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="lookup">The lookup.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="converter">A function to convert the value as an <see cref="object"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="lookup"/> is <c>null</c></exception>
        /// <exception cref="InvalidOperationException"><paramref name="lookup"/> has multiple values for the given <paramref name="key"/></exception>
        public static T Get<T>(this ILookup<string, string> lookup, string key, Func<object, T> converter)
        {
            return lookup.ToDictionary(key).Get(key, converter);
        }

        /// <summary>Gets the value from the collection, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="converter">A function to convert the value as an <see cref="object"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c></exception>
        /// <exception cref="InvalidOperationException"><paramref name="collection"/> has multiple values for the given <paramref name="key"/></exception>
        public static T Get<T>(this NameValueCollection collection, string key, Func<object, T> converter)
        {
            return collection.ToLookup().Get(key, converter);
        }

        /// <summary>Gets the value from the XML node's child elements, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="node">An XML node which containers other elements.</param>
        /// <param name="key">The name of the element from which to get the value.</param>
        /// <param name="converter">A function to convert the value as an <see cref="object"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c></exception>
        public static T Get<T>(this XContainer node, string key, Func<object, T> converter)
        {
            return node.ToDictionary().Get(key, converter);
        }

        /// <summary>Gets the value from the XML node's child elements, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="node">An XML node which containers other elements.</param>
        /// <param name="key">The name of the element from which to get the value.</param>
        /// <param name="converter">A function to convert the value as an <see cref="object"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c></exception>
        public static T Get<T>(this IXPathNavigable node, string key, Func<object, T> converter)
        {
            return node.ToDictionary().Get(key, converter);
        }

        /// <summary>Gets the value from the dictionary, returning <paramref name="defaultValue"/> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="converter">A function to convert the value as an <see cref="string"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is <c>null</c></exception>
        public static T Get<T>(this IDictionary dictionary, string key, Func<string, T> converter)
        {
            return dictionary.Get(key, default(T), (object value) => ConvertValue(value, converter));
        }

        /// <summary>Gets the value from the lookup, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="lookup">The lookup.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="converter">A function to convert the value as an <see cref="string"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="lookup"/> is <c>null</c></exception>
        /// <exception cref="InvalidOperationException"><paramref name="lookup"/> has multiple values for the given <paramref name="key"/></exception>
        public static T Get<T>(this ILookup<string, string> lookup, string key, Func<string, T> converter)
        {
            return lookup.ToDictionary(key).Get(key, converter);
        }

        /// <summary>Gets the value from the collection, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="converter">A function to convert the value as an <see cref="string"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c></exception>
        /// <exception cref="InvalidOperationException"><paramref name="collection"/> has multiple values for the given <paramref name="key"/></exception>
        public static T Get<T>(this NameValueCollection collection, string key, Func<string, T> converter)
        {
            return collection.ToLookup().Get(key, converter);
        }

        /// <summary>Gets the value from the XML node's child elements, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="node">An XML node which containers other elements.</param>
        /// <param name="key">The name of the element from which to get the value.</param>
        /// <param name="converter">A function to convert the value as an <see cref="string"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c></exception>
        public static T Get<T>(this XContainer node, string key, Func<string, T> converter)
        {
            return node.ToDictionary().Get(key, converter);
        }

        /// <summary>Gets the value from the XML node's child elements, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="node">An XML node which containers other elements.</param>
        /// <param name="key">The name of the element from which to get the value.</param>
        /// <param name="converter">A function to convert the value as an <see cref="string"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c></exception>
        public static T Get<T>(this IXPathNavigable node, string key, Func<string, T> converter)
        {
            return node.ToDictionary().Get(key, converter);
        }

        /// <summary>Gets the value from the dictionary, returning the default value of <typeparamref key="T" /> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the dictionary doesn't have a value for the given <paramref name="key"/>.</param>
        /// <param name="converter">A function to convert the value as an <see cref="string"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is <c>null</c></exception>
        public static T Get<T>(this IDictionary dictionary, string key, T defaultValue, Func<string, T> converter)
        {
            return dictionary.Get(key, defaultValue, (object value) => ConvertValue(value, converter));
        }

        /// <summary>Gets the value from the lookup, returning <paramref name="defaultValue"/> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="lookup">The lookup.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the lookup doesn't have a value for the given <paramref name="key"/>.</param>
        /// <param name="converter">A function to convert the value as an <see cref="string"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="lookup"/> is <c>null</c></exception>
        /// <exception cref="InvalidOperationException"><paramref name="lookup"/> has multiple values for the given <paramref name="key"/></exception>
        public static T Get<T>(this ILookup<string, string> lookup, string key, T defaultValue, Func<string, T> converter)
        {
            return lookup.ToDictionary(key).Get(key, defaultValue, converter);
        }

        /// <summary>Gets the value from the collection, returning <paramref name="defaultValue"/> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the collection doesn't have a value for the given <paramref name="key"/>.</param>
        /// <param name="converter">A function to convert the value as an <see cref="string"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c></exception>
        /// <exception cref="InvalidOperationException"><paramref name="collection"/> has multiple values for the given <paramref name="key"/></exception>
        public static T Get<T>(this NameValueCollection collection, string key, T defaultValue, Func<string, T> converter)
        {
            return collection.ToLookup().Get(key, defaultValue, converter);
        }

        /// <summary>Gets the value from the XML node's child elements, returning <paramref name="defaultValue"/> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="node">An XML node which containers other elements.</param>
        /// <param name="key">The name of the element from which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the node doesn't have a value for the given <paramref name="key"/>.</param>
        /// <param name="converter">A function to convert the value as a <see cref="string"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c></exception>
        public static T Get<T>(this XContainer node, string key, T defaultValue, Func<string, T> converter)
        {
            return node.ToDictionary().Get(key, defaultValue, converter);
        }

        /// <summary>Gets the value from the XML node's child elements, returning <paramref name="defaultValue"/> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="node">An XML node which containers other elements.</param>
        /// <param name="key">The name of the element from which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the node doesn't have a value for the given <paramref name="key"/>.</param>
        /// <param name="converter">A function to convert the value as a <see cref="string"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c></exception>
        public static T Get<T>(this IXPathNavigable node, string key, T defaultValue, Func<string, T> converter)
        {
            return node.ToDictionary().Get(key, defaultValue, converter);
        }

        /// <summary>Gets the value from the lookup, returning <paramref name="defaultValue"/> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="lookup">The lookup.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the lookup doesn't have a value for the given <paramref name="key"/>.</param>
        /// <param name="converter">A function to convert the value as an <see cref="object"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="lookup"/> is <c>null</c></exception>
        /// <exception cref="InvalidOperationException"><paramref name="lookup"/> has multiple values for the given <paramref name="key"/></exception>
        public static T Get<T>(this ILookup<string, string> lookup, string key, T defaultValue, Func<object, T> converter)
        {
            return lookup.ToDictionary(key).Get(key, defaultValue, converter);
        }

        /// <summary>Gets the value from the collection, returning <paramref name="defaultValue"/> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the collection doesn't have a value for the given <paramref name="key"/>.</param>
        /// <param name="converter">A function to convert the value as an <see cref="object"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c></exception>
        /// <exception cref="InvalidOperationException"><paramref name="collection"/> has multiple values for the given <paramref name="key"/></exception>
        public static T Get<T>(this NameValueCollection collection, string key, T defaultValue, Func<object, T> converter)
        {
            return collection.ToLookup().Get(key, defaultValue, converter);
        }

        /// <summary>Gets the value from the XML node's child elements, returning <paramref name="defaultValue"/> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="node">An XML node which containers other elements.</param>
        /// <param name="key">The name of the element from which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the node doesn't have a value for the given <paramref name="key"/>.</param>
        /// <param name="converter">A function to convert the value as an <see cref="object"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c></exception>
        public static T Get<T>(this XContainer node, string key, T defaultValue, Func<object, T> converter)
        {
            return node.ToDictionary().Get(key, defaultValue, converter);
        }

        /// <summary>Gets the value from the XML node's child elements, returning <paramref name="defaultValue"/> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="node">An XML node which containers other elements.</param>
        /// <param name="key">The name of the element from which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the node doesn't have a value for the given <paramref name="key"/>.</param>
        /// <param name="converter">A function to convert the value as an <see cref="object"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c></exception>
        public static T Get<T>(this IXPathNavigable node, string key, T defaultValue, Func<object, T> converter)
        {
            return node.ToDictionary().Get(key, defaultValue, converter);
        }

        /// <summary>Gets the value from the dictionary, returning the <paramref key="defaultValue"/> if the value doesn't exist.</summary>
        /// <typeparam name="T">The type of the value to retrieve</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key by which to get the value.</param>
        /// <param name="defaultValue">The default value to return if the dictionary doesn't have a value for the given <paramref name="key"/>.</param>
        /// <param name="converter">A function to convert the value as an <see cref="object"/> to a <typeparamref name="T"/> instance.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="dictionary"/> or <paramref name="converter"/> is <c>null</c></exception>
        public static T Get<T>(this IDictionary dictionary, string key, T defaultValue, Func<object, T> converter)
        {
            Requires.NotNull("dictionary", dictionary);
            Requires.NotNull("converter", converter);

            return dictionary.Contains(key) ? converter(dictionary[key]) : defaultValue;
        }

        /// <summary>Converts the <paramref name="node"/> to a <see cref="Dictionary{TKey,TValue}"/>.</summary>
        /// <param name="node">The node.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c></exception>
        private static Dictionary<string, string> ToDictionary(this XContainer node)
        {
            Requires.NotNull("node", node);
            return node.Descendants().ToDictionary(n => n.Name.ToString(), n => n.Value);
        }

        /// <summary>Converts the <paramref name="node"/> to a <see cref="Dictionary{TKey,TValue}"/>.</summary>
        /// <param name="node">The node.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is <c>null</c></exception>
        private static Dictionary<string, string> ToDictionary(this IXPathNavigable node)
        {
            Requires.NotNull("node", node);
            return node.CreateNavigator().SelectChildren(XPathNodeType.Element).Cast<XPathNavigator>().ToDictionary(n => n.Name, n => n.Value);
        }

        /// <summary>Converts the <paramref name="lookup" /> to a <see cref="Dictionary{TKey,TValue}" /> for the specific <paramref name="key" />.</summary>
        /// <param name="lookup">The lookup.</param>
        /// <param name="key">The key.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}" /> instance with zero or one key/value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="lookup" /> is <c>null</c></exception>
        /// <exception cref="InvalidOperationException">There were multiple values for the given key</exception>
        private static Dictionary<string, string> ToDictionary(this ILookup<string, string> lookup, string key)
        {
            Requires.NotNull("lookup", lookup);
            try
            {
                return lookup[key].ToDictionary(v => key);
            }
            catch (ArgumentException exc)
            {
                // TODO: Localize this
                throw new InvalidOperationException("There were multiple values for the given key", exc);
            }
        }

        /// <summary>Converts the <paramref name="collection"/> to an <see cref="ILookup{TKey,TElement}"/>.</summary>
        /// <param name="collection">The collection.</param>
        /// <returns>An <see cref="ILookup{TKey,TElement}"/> instance.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="collection"/> is <c>null</c></exception>
        private static ILookup<string, string> ToLookup(this NameValueCollection collection)
        {
            Requires.NotNull("collection", collection);
            return collection.AllKeys
                             .SelectMany(key => ParseValues(key, collection.GetValues(key)))
                             .ToLookup(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>Wraps the <paramref name="values"/> into <see cref="KeyValuePair{TKey,TValue}"/> instances.</summary>
        /// <param name="key">The key.</param>
        /// <param name="values">The values.</param>
        /// <returns>A sequence of <see cref="KeyValuePair{TKey,TValue}"/> instances.</returns>
        private static IEnumerable<KeyValuePair<string, string>> ParseValues(string key, string[] values)
        {
            return (values.Length == 1
                        ? values[0].Split(',')
                        : values).Select(value => new KeyValuePair<string, string>(key, value));
        }

        /// <summary>Converts the <paramref name="value"/> into a <typeparamref name="T"/> instance.</summary>
        /// <typeparam name="T">The type of the value to return</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <typeparamref name="T"/> instance.</returns>
        /// <exception cref="InvalidCastException">
        /// the value is <c>null</c> and <typeparamref name="T"/> is a value type, or
        /// the value does not implement the <see cref="IConvertible"/> interface and
        /// no cast is defined from the value to <typeparamref name="T"/>
        /// </exception>
        private static T ConvertValue<T>(object value)
        {
            if (value is IConvertible)
            {
                return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            }

            if (value == null)
            {
                if (typeof(T).IsValueType)
                {
                    // TODO: this should probably return the default value if called from GetOrDefault...
                    throw new InvalidCastException();
                }

                return (T)value; // null
            }

            if (typeof(T) == typeof(string))
            {
                return (T)(object)value.ToString();
            }

            return (T)value;
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
