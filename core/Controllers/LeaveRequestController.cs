using core.Models;
using Microsoft.AspNetCore.Mvc;

namespace core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public LeaveRequestController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        [Route("GetPendingLeaveRequests/{EmployeeCode}")]
        public async Task<ActionResult<LeaveRequests>> GetPendingLeaveRequests(int EmployeeCode)
        {
            var result = await databaseContext.LeaveRequests.Where(e => e.EmployeeCode == EmployeeCode && e.Deleted == false && e.RequestStatus == "P").ToListAsync();

            if (!result.Any())
                return BadRequest("No Pending Leave Requests found!");

            return Ok(result);
        }

        [HttpPost]
        [Route("AddLeaveRequests")]
        public async Task<ActionResult<LeaveRequests>> AddLeaveRequests(LeaveRequests leaveRequest)
        {
            databaseContext.LeaveRequests.Add(leaveRequest);

            await databaseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("UpdatePendingLeaveRequests")]
        public async Task<ActionResult<LeaveRequests>> UpdatePendingLeaveRequests(LeaveRequests request)
        {
            var result = await databaseContext.LeaveRequests.Where(e => e.LeaveRequestCode == request.LeaveRequestCode && e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("Leave request not found");

            result[0].RequestStatus = request.RequestStatus;
            result[0].UpdatedBy = request.UpdatedBy;
            result[0].UpdatedDate = DateTime.Now;

            await databaseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("GetLeaveHistory/{EmployeeCode}")]
        public async Task<ActionResult<LeaveRequests>> GetLeaveHistory(int EmployeeCode)
        {
            var result = await databaseContext.LeaveRequests.Where(e => e.EmployeeCode == EmployeeCode && e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("No Leave Requests found!");

            return Ok(result);
        }

        [HttpPut]
        [Route("DeleteLeaveRequest")]
        public async Task<ActionResult<LeaveRequests>> DeleteLeaveRequest(LeaveRequests request)
        {
            var result = await databaseContext.LeaveRequests.Where(e => e.LeaveRequestCode == request.LeaveRequestCode && e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("Leave request not found");

            result[0].Deleted = request.Deleted;
            result[0].DeletedDate = DateTime.Now;
            result[0].DeletedBy = request.DeletedBy;

            await databaseContext.SaveChangesAsync();

            return Ok();
        }
    }
}
