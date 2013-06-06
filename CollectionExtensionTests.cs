namespace DotNetNuke.Prototype.CollectionExtensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Web.UI;
    using System.Xml;
    using System.Xml.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class CollectionExtensionTests : AssertionHelper
    {
        [Test]
        public void get_null_string_from_hashtable_for_missing_value()
        {
            var table = new Hashtable { { "app id", "abc123" } };

            var value = table.GetValueOrDefault<string>("cat id");

            Expect(value, Is.Null);
        }

        [Test]
        public void can_get_string_from_hashtable()
        {
            var table = new Hashtable { { "app id", "abc123" } };

            var value = table.GetValueOrDefault<string>("app id");

            Expect(value, Is.EqualTo("abc123"));
        }

        [Test]
        public void get_string_from_hashtable_when_default_is_provided()
        {
            var table = new Hashtable { { "app id", "abc123" } };

            var value = table.GetValueOrDefault("app id", "abracadabra");

            Expect(value, Is.EqualTo("abc123"));
        }

        [Test]
        public void can_get_default_string_from_hashtable()
        {
            var table = new Hashtable { { "app id", "abc123" } };

            var value = table.GetValueOrDefault("cat id", "Frank");

            Expect(value, Is.EqualTo("Frank"));
        }

        [Test]
        public void can_get_bool_from_hashtable()
        {
            var table = new Hashtable { { "app id", "true" } };

            var value = table.GetValueOrDefault<bool>("app id");

            Expect(value, Is.True);
        }

        [Test]
        public void get_bool_from_hashtable_when_default_is_provided()
        {
            var table = new Hashtable { { "app id", "true" } };

            var value = table.GetValueOrDefault("app id", false);

            Expect(value, Is.True);
        }

        [Test]
        public void can_get_default_bool_from_hashtable()
        {
            var value = true;
            var table = new Hashtable { { "app id", "abc123" } };

            value = table.GetValueOrDefault("Allow Windows Live Writer", value);

            Expect(value, Is.True);
        }

        [Test]
        public void get_false_from_hashtable_for_missing_value()
        {
            var table = new Hashtable { { "app id", "abc123" } };

            var value = table.GetValueOrDefault<bool>("Allow Windows Live Writer");

            Expect(value, Is.False);
        }

        [Test]
        public void get_bool_with_custom_converter_from_hashtable()
        {
            var table = new Hashtable { { "allow", "on" } };

            var value = table.GetValueOrDefault(
                "allow", 
                v =>
                {
                    bool allowed;
                    if (!bool.TryParse(v, out allowed))
                    {
                        allowed = v.Equals("on", StringComparison.Ordinal);
                    }

                    return allowed;
                });

            Expect(value, Is.True);
        }

        [Test]
        public void get_int()
        {
            var collection = new Dictionary<string, string> { { "appId", "123" } };

            var value = collection.GetValueOrDefault<int>("appId");

            Expect(value, Is.EqualTo(123));
        }

        [Test]
        public void get_decimal()
        {
            var collection = new Dictionary<string, string> { { "appId", "1.23" } };

            var value = collection.GetValueOrDefault<decimal>("appId");

            Expect(value, Is.EqualTo(1.23m));
        }

        [Test]
        [SetCulture("nl-NL")]
        public void get_decimal_from_other_culture()
        {
            var collection = new Dictionary<string, string> { { "appId", "1.23" } };

            var value = collection.GetValueOrDefault<decimal>("appId");

            Expect(value, Is.EqualTo(1.23m));
        }

        [Test]
        public void get_datetime()
        {
            var collection = new Dictionary<string, string> { { "startDate", "05/04/2012 00:00:00" } };

            var value = collection.GetValueOrDefault<DateTime>("startDate");

            Expect(value, Is.EqualTo(new DateTime(2012, 5, 4)));
        }

        [Test]
        [SetCulture("nl-NL")]
        public void get_datetime_from_other_culture()
        {
            var collection = new Dictionary<string, string> { { "startDate", "05/04/2012 00:00:00" } };

            var value = collection.GetValueOrDefault<DateTime>("startDate");

            Expect(value, Is.EqualTo(new DateTime(2012, 5, 4)));
        }

        [Test]
        public void get_from_statebag()
        {
            var collection = new StateBag { { "appId", "123" } };

            var value = collection.GetValueOrDefault<string>("appId");

            Expect(value, Is.EqualTo("123"));
        }

        [Test]
        public void get_from_xnode()
        {
            var node = new XElement(
                "parent",
                new XElement("id", 14));
            
            var value = node.GetValueOrDefault<int>("id");

            Expect(value, Is.EqualTo(14));
        }

        [Test]
        public void get_from_xmlnode()
        {
            var doc = new XmlDocument();
            doc.LoadXml(@"
<parent>
    <id>13</id>
</parent>");

            var value = doc.DocumentElement.GetValueOrDefault<int>("id");

            Expect(value, Is.EqualTo(13));
        }

        [Test]
        public void can_get_timespan_with_custom_converter()
        {
            var collection = new Hashtable { { "length", "1:10:10" } };

            var value = collection.GetValueOrDefault("length", TimeSpan.Parse);

            Expect(value, Is.EqualTo(TimeSpan.FromSeconds(4210)));
        }

        [Test]
        public void can_get_empty_boolean_from_form()
        {
            var collection = new NameValueCollection { { "text", "blah" } };

            var value = collection.GetValueOrDefault("radio", CollectionExtensions.GetFlexibleBooleanParsingFunction());

            Expect(value, Is.False);
        }

        [Test]
        public void can_get_boolean_from_form()
        {
            var collection = new NameValueCollection { { "radio", "on" } };

            var value = collection.GetValueOrDefault("radio", CollectionExtensions.GetFlexibleBooleanParsingFunction());

            Expect(value, Is.True);
        }

        [Test]
        public void flexible_boolean_parsing_is_case_insensitive()
        {
            var collection = new NameValueCollection { { "question", "YES" } };

            var value = collection.GetValueOrDefault("question", CollectionExtensions.GetFlexibleBooleanParsingFunction("yes"));

            Expect(value, Is.True);
        }

        [Test]
        public void can_convert_namevaluecollection_to_lookup()
        {
            var collection = new NameValueCollection { { "question", "YES" } };

            var lookup = collection.ToLookup();

            Expect(lookup["question"], Is.EquivalentTo(new[] { "YES" }));
        }

        [Test]
        public void can_convert_namevaluecollection_with_multiple_values_to_lookup()
        {
            var collection = new NameValueCollection { { "question", "A" }, { "question", "B" }, { "question", "C" }, };

            var lookup = collection.ToLookup();

            Expect(lookup["question"], Is.EquivalentTo(new[] { "A", "B", "C", }));
        }

        [Test]
        public void throws_invalidoperationexception_when_lookup_has_multiple_values()
        {
            var collection = new NameValueCollection { { "state", "CA" }, { "state", "BC" } };

            Expect(() => collection.GetValueOrDefault<string>("state"), Throws.InvalidOperationException);
        }

        [Test]
        public void throws_argumentnullexception_when_dictionary_is_null()
        {
            IDictionary dictionary = null;

            Expect(() => dictionary.GetValueOrDefault<int>("value ID"), Throws.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("dictionary"));
        }

        [Test]
        public void throws_argumentnullexception_when_xelement_is_null()
        {
            XElement node = null;

            Expect(() => node.GetValueOrDefault<int>("value ID"), Throws.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("node"));
        }

        [Test]
        public void throws_argumentnullexception_when_xmlnode_is_null()
        {
            XmlNode node = null;

            Expect(() => node.GetValueOrDefault<int>("value ID"), Throws.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("node"));
        }

        [Test]
        public void throws_invalidcastexception_when_type_is_not_supported()
        {
            var dictionary = new Dictionary<string, string> { { "length", "1:10:10" } };

            Expect(() => dictionary.GetValueOrDefault<TimeSpan>("length"), Throws.TypeOf<InvalidCastException>());
        }

        [Test]
        public void throws_invalidcastexception_when_value_is_null_for_value_type()
        {
            var dictionary = new Dictionary<string, string> { { "length", null } };

            Expect(() => dictionary.GetValueOrDefault<int>("length"), Throws.TypeOf<InvalidCastException>());
        }

        [Test]
        public void does_not_throw_invalidcastexception_when_value_is_null_for_reference_type()
        {
            var dictionary = new Dictionary<string, string> { { "length", null } };

            var value = dictionary.GetValueOrDefault<ApplicationException>("length");

            Expect(value, Is.Null);
        }

        [Test]
        public void throws_formatexception_when_value_cannot_be_parsed()
        {
            var dictionary = new Dictionary<string, string> { { "ID", "abc123" } };

            Expect(() => dictionary.GetValueOrDefault<int>("ID"), Throws.TypeOf<FormatException>());
        }

        [Test]
        public void tolookup_throws_argumentnullexception_when_namevaluecollection_is_null()
        {
            NameValueCollection col = null;

            Expect(() => col.ToLookup(), Throws.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("collection"));
        }
    }
}