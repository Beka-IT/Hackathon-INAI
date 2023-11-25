using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BranchesController(AppDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public List<BranchResponse> GetBranches()
        {
            var branches =  _db.Branches.Select(x => new BranchResponse
            {
                Id = x.Id,
                Title = x.Title,
                Address = x.Address,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                Number = x.Number,
            }).ToList();
            
            for(int i = 0; i < branches.Count; i++)
            {
                branches[i].Departments = _db.Departments.Where(x => x.BranchId == branches[i].Id)
                    .Select(x => new DepartmentResponse { 
                        Id = x.Id,
                        Title = x.Title,
                        BranchId = x.BranchId,
                        OperationIds = _db.DepartmentOperations
                            .Where(o => x.Id == o.DepartmentId)
                            .Select(x => x.OperationId).ToHashSet().ToList(),
                        SpecialistId = x.SpecialistId,
                        Specialist = _db.Users.FirstOrDefault(u => u.Id == x.SpecialistId)
                    })
                    .ToList();

                branches[i].AllowedOperations = _db.DepartmentOperations
                    .Where(x => branches[i].Departments.Select(x => x.Id).Contains(x.DepartmentId))
                    .Select(x => x.Operation).ToHashSet().ToList();
            }
            return branches;
        }

        [HttpGet]
        public List<OperationType> GetOperations()
        {
            return _db.Operations.ToList();
        }

        [HttpGet]
        public string[] GetStatisticForHour()
        {
            string[] arr = new string[4];

            arr[0] = _db.Queues.Count(x => x.StartDate.Hour >= 9 && x.EndDate.Hour < 12).ToString();
            arr[1] = _db.Queues.Count(x => x.StartDate.Hour >= 12 && x.EndDate.Hour < 15).ToString();
            arr[2] = _db.Queues.Count(x => x.StartDate.Hour >= 15 && x.EndDate.Hour < 18).ToString();
            arr[3] = _db.Queues.Count(x => x.StartDate.Hour >= 18).ToString();

            return arr;
        }

        [HttpGet]
        public List<SpecialistStatistics> GetSpecialistStatistics()
        {
            var departmentsIds = _db.Queues.Select(x => x.DepartmentId).ToList();
            var specialists = _db.Users.ToList().Where(x => x.Role == 0 && departmentsIds.Contains(x.DepartmentId));
            var response = specialists.Select(u => new SpecialistStatistics
            {
                Fullname = u.Fullname,
                Count = departmentsIds.Count(x => x == u.DepartmentId)
            }).ToList();

            return response;
        }
    }
}
