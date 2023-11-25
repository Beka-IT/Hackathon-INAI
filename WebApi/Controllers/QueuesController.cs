using Microsoft.AspNetCore.Mvc;
using WebApi.Constants;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QueuesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public QueuesController(AppDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public List<Queue> GetByDepartmentId(int departmentId)
        {
            return _db.Queues.Where(x => x.EndDate >= DateTime.Now && x.DepartmentId == departmentId).ToList();
        }

        [HttpGet]
        public int GetByPin(string pin, int depId)
        {
            var user = _db.Users.FirstOrDefault(x => x.Pin == pin);

            if (user == null) return -1; 
            
            return _db.Queues.FirstOrDefault(x => x.ClientId == user.Id && x.DepartmentId == depId).ENumber;

        }

        [HttpPost]
        public IActionResult Add(CreateQueueResponse req)
        {
            var operation = _db.Operations.FirstOrDefault(x => x.Id == req.OperationId);
            var queue = new Queue
            {
                ClientId = req.ClientId,
                OperationId = req.OperationId,
                DepartmentId = req.DepartmentId,
                StartDate = req.StartDate,
                EndDate = req.StartDate.AddMinutes(operation.DurationInMinutes)
            };

            if (queue.EndDate.Hour >= TimeConstants.EndOfWorkDay)
            {
                return BadRequest("Банк работает только до 18:00!");
            }

            var queues = _db.Queues
                .Where(x => x.StartDate.Date == req.StartDate.Date && x.DepartmentId == req.DepartmentId)
                .ToList();

            queues.Add(queue);
            queues = queues.OrderBy(x => x.StartDate).ToList();

            for(int i = 0; i < queues.Count; i++)
            {
                if (queues.IndexOf(queue) == i)
                {
                    if((i-1) >= 0)
                    {
                        if (queues[i - 1].EndDate > queue.StartDate) 
                            return BadRequest("На этот промежуток времени очередь забронирована!");
                    }
                    if ((i + 1) < queues.Count)
                    {
                        if (queues[i + 1].StartDate < queue.EndDate)
                            return BadRequest("На этот промежуток времени очередь забронирована!");
                    }
                }
            }

           queue.ENumber = queues.Max(x => x.ENumber) == 0 ? 1 : queues.Max(x => x.ENumber) + 1;
            _db.Queues.Add(queue);
            _db.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public void FinishQueue(int queueId)
        {
            var queue = _db.Queues.FirstOrDefault(x => x.Id == queueId);
            queue.EndDate = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
