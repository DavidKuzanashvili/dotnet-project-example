using System.Collections.Generic;

namespace App.Domain.Models.Shared.Pagination
{
    public class PaginationResponse<T>
    {
        public List<T> Data { get; set; }
        public int FilterdRecords { get; set; }
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
