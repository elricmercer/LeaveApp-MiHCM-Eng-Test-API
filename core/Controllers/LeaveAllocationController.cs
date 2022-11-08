using core.Models;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public LeaveAllocationController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        [Route("GetLeaveAllocations")]
        public async Task<ActionResult<LeaveAllocation>> GetLeaveAllocations()
        {
            var result = await databaseContext.LeaveAllocation.Where(e => e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("No Leave Allocations found!");

            return Ok(result);
        }

        [HttpGet]
        [Route("GetLeaveAllocation/{EmployeeCode}")]
        public async Task<ActionResult<LeaveAllocation>> GetLeaveAllocation(int EmployeeCode)
        {
            var result = await databaseContext.LeaveAllocation.Where(e => e.EmployeeCode == EmployeeCode && e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("Leave(s) not found");

            return Ok(result);
        }

        [HttpGet]
        [Route("GetLeaveAllocation2/{EmployeeCode}/{LeaveTypeCode}")]
        public async Task<ActionResult<LeaveAllocation>> GetLeaveAllocation2(int EmployeeCode, int LeaveTypeCode)
        {
            var result = await databaseContext.LeaveAllocation.Where(e => e.EmployeeCode == EmployeeCode && e.Deleted == false && e.LeaveTypeCode == LeaveTypeCode).ToListAsync();

            if (!result.Any())
                return BadRequest("Leave(s) not found");

            return Ok(result);
        }

        [HttpPost]
        [Route("AddLeaveAllocation")]
        public async Task<ActionResult<List<LeaveAllocation>>> AddLeaveAllocation(LeaveAllocation request)
        {
            //Check for if leave has already been given
            var isLeaveGiven = await databaseContext.LeaveAllocation.Where(e => e.EmployeeCode == request.EmployeeCode && e.LeaveTypeCode == request.LeaveTypeCode && e.Deleted == false).ToListAsync();

            if (isLeaveGiven.Any())
                return BadRequest("Leave type already given!");

            databaseContext.LeaveAllocation.Add(request);

            await databaseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("UpdateLeaveAllocation")]
        public async Task<ActionResult<LeaveAllocation>> UpdateLeaveAllocation(LeaveAllocation request)
        {
            var result = await databaseContext.LeaveAllocation.Where(e => e.EmployeeCode == request.EmployeeCode && e.LeaveTypeCode == request.LeaveTypeCode && e.Deleted == false).ToListAsync();
            if (!result.Any())
                return BadRequest("Leave allocation not found");

            result[0].Used = request.Used;
            result[0].UpdatedDate = DateTime.Now;
            result[0].UpdatedBy = request.UpdatedBy;

            await databaseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("DeleteLeaveAllocation")]
        public async Task<ActionResult<LeaveAllocation>> DeleteLeaveAllocation(LeaveAllocation request)
        {
            var result = await databaseContext.LeaveAllocation.Where(e => e.EmployeeCode == request.EmployeeCode && e.LeaveTypeCode == request.LeaveTypeCode && e.Deleted == false).ToListAsync();
            if (!result.Any())
                return BadRequest("Leave allocation not found");

            result[0].Deleted = request.Deleted;
            result[0].DeletedDate = DateTime.Now;
            result[0].DeletedBy = request.DeletedBy;

            await databaseContext.SaveChangesAsync();

            return Ok();
        }
    }
}
