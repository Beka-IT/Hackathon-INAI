namespace WebApi.Entities
{
    public class OperationType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DurationInMinutes { get; set; }
        public List<DepartmentOperations> DepartmentOperations { get; } = new();
        public List<Department> Departments { get; } = new();
    }
}
