namespace ECommerce.Shared.ResponseModel
{
    public class ResponseStructure
    {
        public bool success;
        public object data;
        public string error;
    }

    public class ResponseMetadata<T>
    {
        public int page_number = 1;
        public int page_size;
        public int total_records_count;
        public T records;
    }
}