
namespace Formo.Tests
{
    using System.Globalization;

    public class ConfigurationTestBase
    {
        protected ConfigurationTestBase(string sectionName, bool throwIfNull = false)
        {
            configuration = new Configuration(sectionName, CultureInfo.InvariantCulture) { ThrowIfNull = throwIfNull };
        }

        protected ConfigurationTestBase(bool throwIfNull = false)
        {
            configuration = new Configuration(CultureInfo.InvariantCulture) { ThrowIfNull = throwIfNull };
        }

        protected readonly dynamic configuration;
    }
}
