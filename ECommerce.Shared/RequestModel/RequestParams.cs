namespace ECommerce.Shared.RequestModel
{
    public class RequestParams
    {
        public string? Search { get; set; }

        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }

            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }

        public virtual int RecordCount { get; set; }

        public string? SortBy { get; set; }

        public string? OrderBy { get; set; }
    }
}