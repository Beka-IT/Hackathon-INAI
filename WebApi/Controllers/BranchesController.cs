using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BranchesController
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
            }).ToList();
            
            for(int i = 0; i < branches.Count; i++)
            {
                branches[i].Departments = _db.Departments.Where(x => x.BranchId == branches[i].Id).ToList();
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
    }
}
