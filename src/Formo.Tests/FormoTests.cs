namespace Formo.Tests
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using Formo.Tests.TestModels;
    using Shouldly;
    using Xunit;
    using Configuration = Formo.Configuration;

    public class When_forced_to_use_a_string_key : ConfigurationTestBase
    {
        public When_forced_to_use_a_string_key()
        {
            
        }
        public When_forced_to_use_a_string_key(string sectionName)
            : base(sectionName)
        {
        }

        [Fact]
        public void Get_method_should_return_value()
        {
            var actual = configuration.Get("weird:key");
            actual.ShouldBe("some value");
        }

        [Fact]
        public void Get_method_should_work_with_type_parameters()
        {
            var actual = configuration.Get<int>("NumberOfRetries");
            actual.ShouldBeAssignableTo<int>();
        }
    }

    public class When_using_typed_configuration_values : ConfigurationTestBase
    {
        private dynamic germanConfiguration;
        private readonly string _sectionName;
        public When_using_typed_configuration_values()
        {

        }
        public When_using_typed_configuration_values(string sectionName)
            : base(sectionName)
        {
            germanConfiguration = new Configuration(_sectionName, new CultureInfo("de"));
            _sectionName = sectionName;
        }

        [Fact]
        public void Method_should_convert_to_int()
        {
            var actual = configuration.NumberOfRetries<int>();
            actual.ShouldBe(12);
        }

        [Fact]
        public void Method_should_convert_to_decimal()
        {
            var actual = configuration.AcceptableFailurePercentage<decimal>();
            actual.ShouldBe(1.05);
        }

        [Fact]
        public void Method_should_convert_to_DateTime()
        {
            var actual = configuration.ApplicationBuildDate<DateTime>();
            actual.ShouldBe(new DateTime(1999, 11, 4, 6, 23, 0));
        }

        [Fact]
        public void Method_should_convert_to_DateTime_of_ConfiguredCulture()
        {
            var actual = germanConfiguration.GermanDate<DateTime>();
            actual.ShouldBe(new DateTime(2002, 1, 22));
        }

        [Fact]
        public void Method_should_convert_to_bool()
        {
            var actual = configuration.IsLoggingEnabled<bool>();
            actual.ShouldBe(true);
        }

        [Fact]
        public void Should_throw_nice_exception_when_could_not_parse()
        {
            var ex = Should.Throw<InvalidCastException>(() => configuration.NonParsableInt<Int32>());

            ex.Message.ShouldBe("Unable to cast setting value 'NOT_AN_INT' to 'System.Int32'" + Environment.NewLine +
                "> Could not obtain value 'NonParsableInt' from configuration file" + Environment.NewLine);
        }
    }

    public class When_key_is_in_configuration_file : ConfigurationTestBase
    {
        public When_key_is_in_configuration_file()
        {

        }
        public When_key_is_in_configuration_file(string sectionName)
            : base(sectionName)
        {
        }

        [Fact]
        public void Property_should_return_expected_value()
        {
            configuration.ApiKey.ShouldBe("a0c5837ebb094b578b436f03121bb022");
        }

        [Fact]
        public void Method_should_return_expected_value()
        {
            configuration.ApiKey().ShouldBe("a0c5837ebb094b578b436f03121bb022");
        }

        [Fact]
        public void Method_should_ignore_defaults()
        {
            var actual = configuration.ApiKey("defaultvalue");
            actual.ShouldNotBe("defaultvalue");
        }

        [Fact]
        public void Method_should_ignore_many_defaults()
        {
            var actual = configuration.ApiKey("test", "another");
            actual.ShouldNotBe("test");
            actual.ShouldNotBe("another");
        }
    }

    public class When_key_isnt_in_configuration_file : ConfigurationTestBase
    {
        public When_key_isnt_in_configuration_file()
            : base(false)
        {

        }

        protected When_key_isnt_in_configuration_file(string sectionName, bool throwIfNull)
            : base(sectionName, throwIfNull)
        {
        }

        //[Fact]
        //public void Method_with_many_params_should_return_first_non_null()
        //{
        //    string first = null;
        //    var second = default(string);
        //    var third = "i exist";
        //    third.ShouldBeSameAs(third, configuration.Missing(first, second, third));
        //    Assert.AreEqual(third, configuration.Missing(first, second, third));
        //}

        [Fact]
        public void Method_looking_for_bool_should_behave_as_ConfigurationManager()
        {
            var key = "IsSettingMissing";
            var expected = ConfigurationManager.AppSettings[key];
            var actual = configuration.IsSettingMissing<bool>();
            actual.ShouldBe(expected);
        }

        //[Fact]
        //public void Method_with_param_should_return_first()
        //{
        //    "blargh".ShouldBe("");
        //    Assert.AreEqual("blargh", configuration.Missing("blargh"));
        //}
    }

    public class When_key_isnt_in_configuration_file_and_ThrowIfNull_set_to_false : ConfigurationTestBase
    {
        public When_key_isnt_in_configuration_file_and_ThrowIfNull_set_to_false()
            : base(false)
        {

        }
        public When_key_isnt_in_configuration_file_and_ThrowIfNull_set_to_false(string sectionName)
            : base(sectionName, false)
        {
        }

        [Fact]
        public void Property_should_be_null()
        {
            Assert.Null(configuration.Missing);
        }

        [Fact]
        public void Method_should_be_null()
        {
            Assert.Null(configuration.Misssing());
        }

    }

    public class When_key_isnt_in_configuration_file_and_ThrowIfNull_set_to_true : ConfigurationTestBase
    {
        public When_key_isnt_in_configuration_file_and_ThrowIfNull_set_to_true()
            : base(true)
        {

        }
        public When_key_isnt_in_configuration_file_and_ThrowIfNull_set_to_true(string sectionName)
            : base(sectionName, true)
        {
        }

        [Fact]
        public void Property_should_throw_NullReferenceException()
        {
            var missing = default(dynamic);
            var ex = Should.Throw<InvalidOperationException>(() => missing = configuration.Missing);
            missing.ShouldBeNull();
            ex.Message.ShouldBe("Unable to locate a value for 'Missing' from configuration file");
        }

        [Fact]
        public void Method_should_be_null()
        {
            var missing = default(dynamic);
            var ex = Should.Throw<InvalidOperationException>(() => missing = configuration.Missing);

            missing.ShouldBeNull();
            ex.Message.ShouldBe("Unable to locate a value for 'Missing' from configuration file");
        }

    }

    public class When_getting_a_collection_from_missing_custom_section : ConfigurationTestBase
    {
        public When_getting_a_collection_from_missing_custom_section()
            : base("bindAllSection")
        {
        }

        [InlineData(101, "Chris")]
        [InlineData(102, "Marisol")]
        [InlineData(103, "Allison")]
        [InlineData(104, "Ryan")]
        [InlineData(105, "Ben")]
        [InlineData(106, "Laurie")]
        [InlineData(107, "Paige")]
        [InlineData(108, "Nitya")]
        [Fact]
        public void BindPairs_should_return_a_collection_of_Users_with_properties_set(int id, string name)
        {
            var config = (Configuration)configuration;
            var collection = config.BindPairs<User, int, string>(x => x.ID, x => x.Name).ToArray();

            collection.Count().ShouldBe(8);
            collection.First(x => x.ID == id).Name.ShouldBe(name);
        }
    }

    public class When_key_isnt_in_conguration : ConfigurationTestBase
    {
        [Fact]
        public void Method_with_string_argument_should_use_argument()
        {
            bool unspecified = configuration.NonExisting<bool>("true");
            unspecified.ShouldBe(true);
        }

        [Fact]
        public void Method_with_same_type_bool_argument_should_use_argument()
        {
            bool unspecified = configuration.NonExisting<bool>(true);

            unspecified.ShouldBe(true);
        }

        [Fact]
        public void Method_with_same_type_date_argument_should_use_default_argument()
        {
            DateTime unspecified = configuration.NonExisting<DateTime>(new DateTime(2014, 2, 10));

            unspecified.ShouldBe(new DateTime(2014, 2, 10));
        }

        [Fact]
        public void Method_with_inheritance_default_argument_should_use_it()
        {
            var instance = new TestImplementation();

            ITest unspecified = configuration.NonExisting<ITest>(instance);

            unspecified.ShouldBeSameAs(instance);
        }

        private interface ITest
        {
            void Test();
        }

        private class TestImplementation : ITest
        {
            public void Test()
            {
                Console.WriteLine("Testing");
            }
        }
    }
}
