namespace ECommerce.Shared.RequestModel
{
    public class RequestParams
    {
        public string? search { get; set; }

        private const int maxPageSize = 50;

        public int pageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int pageSize
        {
            get
            {
                return _pageSize;
            }

            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public virtual int recordCount { get; set; }

        public string? sortBy { get; set; }

        public string? orderBy { get; set; }
    }
}