using WebApi.Entities;

namespace WebApi.Models
{
    public class DepartmentResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
        public List<DepartmentOperations> DepartmentOperations { get; } = new();
        public List<int> OperationIds { get; set; } = new();
        public int SpecialistId { get; set; }
        public User Specialist { get; set; }
    }
}
