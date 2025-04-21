namespace ECommerce.Shared.ResponseModel
{
    public class ResponseStructure
    {
        public bool Success;
        public object Data;
        public string Error;
    }

    public class ResponseMetadata<T>
    {
        public int PageNumber = 1;
        public int PageSize;
        public int TotalRecordsCount;
        public T Records;
    }
}