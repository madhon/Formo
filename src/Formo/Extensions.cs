namespace Formo
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class Extensions
    {
        internal static T OrFallbackTo<T>(this T self, IEnumerable<T> args) where T : class
        {
            return self.Concat(args).FirstOrDefault(x => x != default(T));
        }

        internal static IEnumerable<T> Concat<T>(this T self, IEnumerable<T> args)
        {
            yield return self;
            foreach (var arg in args) yield return arg;
        }
    }
}