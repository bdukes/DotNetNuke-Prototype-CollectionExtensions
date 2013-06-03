namespace DotNetNuke.Prototype.CollectionExtensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
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

            var value = table.Get<string>("cat id");

            Expect(value, Is.Null);
        }

        [Test]
        public void can_get_string_from_hashtable()
        {
            var table = new Hashtable { { "app id", "abc123" } };

            var value = table.Get<string>("app id");

            Expect(value, Is.EqualTo("abc123"));
        }

        [Test]
        public void get_string_from_hashtable_when_default_is_provided()
        {
            var table = new Hashtable { { "app id", "abc123" } };

            var value = table.Get("app id", "abracadabra");

            Expect(value, Is.EqualTo("abc123"));
        }

        [Test]
        public void can_get_default_string_from_hashtable()
        {
            var table = new Hashtable { { "app id", "abc123" } };

            var value = table.Get("cat id", "Frank");

            Expect(value, Is.EqualTo("Frank"));
        }

        [Test]
        public void can_get_bool_from_hashtable()
        {
            var table = new Hashtable { { "app id", "true" } };

            var value = table.Get<bool>("app id");

            Expect(value, Is.True);
        }

        [Test]
        public void get_bool_from_hashtable_when_default_is_provided()
        {
            var table = new Hashtable { { "app id", "true" } };

            var value = table.Get("app id", false);

            Expect(value, Is.True);
        }

        [Test]
        public void can_get_default_bool_from_hashtable()
        {
            var value = true;
            var table = new Hashtable { { "app id", "abc123" } };

            value = table.Get("Allow Windows Live Writer", value);

            Expect(value, Is.True);
        }

        [Test]
        public void get_false_from_hashtable_for_missing_value()
        {
            var table = new Hashtable { { "app id", "abc123" } };

            var value = table.Get<bool>("Allow Windows Live Writer");

            Expect(value, Is.False);
        }

        [Test]
        public void get_bool_with_custom_converter_from_hashtable()
        {
            var table = new Hashtable { { "allow", "on" } };

            var value = table.Get(
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

            var value = collection.Get<int>("appId");

            Expect(value, Is.EqualTo(123));
        }

        [Test]
        public void get_decimal()
        {
            var collection = new Dictionary<string, string> { { "appId", "1.23" } };

            var value = collection.Get<decimal>("appId");

            Expect(value, Is.EqualTo(1.23m));
        }

        [Test]
        [SetCulture("nl-NL")]
        public void get_decimal_from_other_culture()
        {
            var collection = new Dictionary<string, string> { { "appId", "1.23" } };

            var value = collection.Get<decimal>("appId");

            Expect(value, Is.EqualTo(1.23m));
        }

        [Test]
        public void get_datetime()
        {
            var collection = new Dictionary<string, string> { { "startDate", "05/04/2012 00:00:00" } };

            var value = collection.Get<DateTime>("startDate");

            Expect(value, Is.EqualTo(new DateTime(2012, 5, 4)));
        }

        [Test]
        [SetCulture("nl-NL")]
        public void get_datetime_from_other_culture()
        {
            var collection = new Dictionary<string, string> { { "startDate", "05/04/2012 00:00:00" } };

            var value = collection.Get<DateTime>("startDate");

            Expect(value, Is.EqualTo(new DateTime(2012, 5, 4)));
        }

        [Test]
        public void get_from_statebag()
        {
            var collection = new StateBag { { "appId", "123" } };

            var value = collection.Get<string>("appId");

            Expect(value, Is.EqualTo("123"));
        }

        [Test]
        public void get_from_xnode()
        {
            var node = new XElement(
                "parent",
                new XElement("id", 14));
            
            var value = node.Get<int>("id");

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

            var value = doc.DocumentElement.Get<int>("id");

            Expect(value, Is.EqualTo(13));
        }

        [Test]
        public void can_get_timespan_with_custom_converter()
        {
            var collection = new Hashtable { { "length", "1:10:10" } };

            var value = collection.Get("length", TimeSpan.Parse);

            Expect(value, Is.EqualTo(TimeSpan.FromSeconds(4210)));
        }

        [Test]
        public void throws_argumentnullexception_when_dictionary_is_null()
        {
            IDictionary dictionary = null;

            Expect(() => dictionary.Get<int>("value ID"), Throws.TypeOf<ArgumentNullException>().With.Property("ParamName").EqualTo("dictionary"));
        }

        [Test]
        public void throws_invalidcastexception_when_type_is_not_supported()
        {
            var dictionary = new Dictionary<string, string> { { "length", "1:10:10" } };

            Expect(() => dictionary.Get<TimeSpan>("length"), Throws.TypeOf<InvalidCastException>());
        }

        [Test]
        public void throws_formatexception_when_value_cannot_be_parsed()
        {
            var dictionary = new Dictionary<string, string> { { "ID", "abc123" } };

            Expect(() => dictionary.Get<int>("ID"), Throws.TypeOf<FormatException>());
        }
    }
}