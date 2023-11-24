namespace WebApi.Entities
{
    public class Queue
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ClientId { get; set; }
        public int BranchId {  get; set; }
        public int DepartmentId { get; set; }
    }
}
