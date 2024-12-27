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
        public int page_size = 0;
        public int total_records_count = 0;
        public T records;
    }

    public class ResponseTreeMetadata<T>
    {
        public T records;
    }
}