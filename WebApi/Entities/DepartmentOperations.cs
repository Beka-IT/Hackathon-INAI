namespace WebApi.Entities
{
    public class DepartmentOperations
    {
        public int Id { get; set; }
        public int OperationId { get; set; }
        public int DepartmentId {  get; set; }
        public OperationType Operation { get; set; }
        public Department Department { get; set; }

    }
}
