namespace Formo.Tests
{
    using NUnit.Framework;

    public class Formo_ConnectionString : ConfigurationTestBase
    {
        [Test]
        public void Can_get_ConnectionString_by_name()
        {
            var connString = configuration.ConnectionString.LocalConnection;

            Assert.That(connString.ConnectionString, Is.EqualTo("localhost"));
        }

        [Test]
        public void Should_get_null_if_ConnectionString_is_wrong()
        {
            var connString = configuration.ConnectionString.BogusThingNotReal;

            Assert.That(connString.ConnectionString, Is.Empty);
        }
    }
}
