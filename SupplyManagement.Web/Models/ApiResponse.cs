namespace SupplyManagement.Web.Models
{
    public class ApiResponse<T>
    {
        public T Obj { get; set; }
        public int Code { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
