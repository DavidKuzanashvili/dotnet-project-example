namespace App.Domain.Models.Shared.Queries
{
    public class CommonQuery
    {
        public CommonQuery()
        {
            if (!string.IsNullOrWhiteSpace(QueryString))
            {
                QueryString = QueryString.Trim().ToLower();
            }
        }

        public bool StrictComparison { get; set; }
        public string QueryString { get; set; }
        public bool OrderByDesc { get; set; }
    }
}
