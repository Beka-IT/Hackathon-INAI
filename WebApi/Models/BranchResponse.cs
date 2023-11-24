using WebApi.Entities;

namespace WebApi.Models
{
    public class BranchResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public List<OperationType> AllowedOperations {  get; set; }
        public List<Department> Departments { get; set; }
    }
}
