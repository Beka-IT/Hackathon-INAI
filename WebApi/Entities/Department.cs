using System.Reflection.Metadata;

namespace WebApi.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
        public List<DepartmentOperations> DepartmentOperations { get; } = new();
        public List<OperationType> Operations { get; } = new();

    }
}
