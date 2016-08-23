using System;

namespace Formo
{
    internal class ThrowHelper
    {
        internal static Exception FailedCast(Type attemptedType, object value, string optionalMessage = null, Exception ex = null)
        {
            var message = $"Unable to cast setting value '{value ?? "(null)"}' to '{attemptedType}'";

            if (optionalMessage != null)
                message += (Environment.NewLine + "> " + optionalMessage + Environment.NewLine);

            return new InvalidCastException(message, ex);
        }
    }
}