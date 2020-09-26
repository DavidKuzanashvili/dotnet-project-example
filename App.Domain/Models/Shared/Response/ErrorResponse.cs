using System.Collections.Generic;

namespace App.Domain.Models.Shared.Response
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Errors = new List<ErrorKeyValuePair>();
        }

        public List<ErrorKeyValuePair> Errors { get; set; }
    }

    public class ErrorKeyValuePair
    {
        public ErrorKeyValuePair()
        {
            Values = new string[0];
        }

        public string Key { get; set; }
        public string[] Values { get; set; }
    }
}
