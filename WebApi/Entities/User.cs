using System.Text.Json.Serialization;
using WebApi.Enums;

namespace WebApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Pin {  get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public RoleType Role { get; set; }
        public int? DepartmentId { get; set; }
    }
}
