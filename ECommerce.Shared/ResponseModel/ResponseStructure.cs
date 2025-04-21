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
        public int Page_Number = 1;
        public int Page_Size;
        public int Total_Records_Count;
        public T Records;
    }
}