using System;

namespace App.Domain.Models.Shared.Queries
{
    public class BlogQuery : CommonQuery
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
