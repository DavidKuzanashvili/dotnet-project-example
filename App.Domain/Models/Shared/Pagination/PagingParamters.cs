namespace App.Domain.Models.Shared.Pagination
{
    public class PagingParameters
    {
        public PagingParameters()
        {
            PageNumber = 1;
            PageSize = 15;
        }

        public PagingParameters(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
