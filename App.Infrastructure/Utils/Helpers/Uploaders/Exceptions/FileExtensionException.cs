using System;

namespace App.Infrastructure.Utils.Helpers.Uploaders.Exceptions
{
    public class FileExtensionException : Exception
    {
        public FileExtensionException()
            : base()
        {
        }

        public FileExtensionException(string message)
            : base(message)
        {
        }
    }
}
