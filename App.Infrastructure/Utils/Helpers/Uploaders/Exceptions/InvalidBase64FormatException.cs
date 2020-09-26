using System;

namespace App.Infrastructure.Utils.Helpers.Uploaders.Exceptions
{
    public class InvalidBase64FormatException : Exception
    {
        public InvalidBase64FormatException()
            : base()
        {
        }

        public InvalidBase64FormatException(string message)
            : base(message)
        {
        }
    }
}
