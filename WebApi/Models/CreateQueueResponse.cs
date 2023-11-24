namespace WebApi.Models
{
    public class CreateQueueResponse
    {
        public DateTime StartDate { get; set; }
        public int ClientId { get; set; }
        public int DepartmentId { get; set; }
        public int OperationId { get; set; }
    }
}
