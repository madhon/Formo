namespace Formo
{
    using System.Configuration;
    using System.Dynamic;

    internal class ConnectionStringsConfiguration : DynamicObject
    {
        private readonly ConnectionStringSettingsCollection _connectionStrings;

        internal ConnectionStringsConfiguration(ConnectionStringSettingsCollection connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public ConnectionStringSettings Get(string key) => _connectionStrings[key];

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = _connectionStrings[binder.Name] ?? new ConnectionStringSettings();
            return true;
        }
    }
}