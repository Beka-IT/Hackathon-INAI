using WebApi.Enums;

namespace WebApi.Entities
{
    public class Queue
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ClientId { get; set; }
        public int DepartmentId { get; set; }
        public int OperationId { get; set; }
        public int ENumber { get; set; }
        public QueueStatus Status { get; set; }
    }
}
