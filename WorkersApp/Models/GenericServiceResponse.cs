namespace WorkersApp.Models
{
    public class GenericServiceResponse<T>
    {
        public Result Status { get; set; }
        public enum Result { Success, InvalidData, OperationFailed };
        public T? Response { get; set; }
    }
}
