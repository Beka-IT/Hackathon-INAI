using Microsoft.Extensions.Hosting;

namespace WebApi.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Number { get; set; }
        public ICollection<Department> Departments { get; } = new List<Department>();
    }
}
