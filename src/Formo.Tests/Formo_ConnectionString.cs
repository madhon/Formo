namespace Formo.Tests
{
    using Xunit;
    using Shouldly;

    public class Formo_ConnectionString : ConfigurationTestBase
    {
        [Fact]
        public void Can_get_ConnectionString_by_name()
        {
            var connString = configuration.ConnectionString.LocalConnection;
            connString.ConnectionString.ToString().ShouldBe("localhost");
        }

        [Fact]
        public void Should_get_null_if_ConnectionString_is_wrong()
        {
            var connString = configuration.ConnectionString.BogusThingNotReal;

            connString.ConnectionString.ToString().ShouldBe(string.Empty);
        }
    }
}
