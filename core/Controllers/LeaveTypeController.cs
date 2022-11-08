using core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public LeaveTypeController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        [Route("GetLeaveTypes")]
        public async Task<ActionResult<LeaveType>> GetLeaveTypes()
        {
            var result = await databaseContext.LeaveType.Where(e => e.Deleted == false).ToListAsync();
            
            if (!result.Any())
                return BadRequest("Leave type not found");
            
            return Ok(result);
        }

        [HttpGet]
        [Route("GetLeaveType/{LeaveTypeCode}")]
        public async Task<ActionResult<LeaveType>> GetLeaveType(int LeaveTypeCode)
        {
            var result = await databaseContext.LeaveType.Where(e => e.LeaveTypeCode == LeaveTypeCode && e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("Leave type not found");

            return Ok(result);
        }

        [HttpPost]
        [Route("AddLeaveType")]
        public async Task<ActionResult<LeaveType>> AddLeaveType(LeaveType request)
        {
            //cannot be blank
            //only contain characters, numbers and spaces
            //must be unique
            var isUnique = await databaseContext.LeaveType.Where(e => e.LeaveTypeName == request.LeaveTypeName && e.Deleted == false).ToListAsync();

            if (isUnique.Any())
                return BadRequest("Leave type already exists!");
            else if (request.LeaveTypeName == "")
                return BadRequest("Leave type name cannot be null/empty");
            else
            {
                var leaveRegex = @"^[a-zA-Z0-9 ]+$";
                if (Regex.IsMatch(request.LeaveTypeName, leaveRegex) == false)
                    return BadRequest("Invalid Leave name");
            }

            databaseContext.LeaveType.Add(request);

            await databaseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("UpdateLeaveTypeName")]
        public async Task<ActionResult<LeaveType>> UpdateLeaveTypeName(LeaveType request)
        {
            var result = await databaseContext.LeaveType.Where(e => e.LeaveTypeCode == request.LeaveTypeCode && e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("Leave Type not found");

            //cannot be blank
            //only contain characters, numbers and spaces
            //must be unique
            var isUnique = await databaseContext.LeaveType.Where(e => e.LeaveTypeName == request.LeaveTypeName && e.Deleted == false).ToListAsync();

            if (isUnique.Any())
                return BadRequest("Leave type already exists!");
            else if (request.LeaveTypeName == "")
                return BadRequest("Leave type name cannot be null/empty");
            else
            {
                var leaveRegex = @"^[a-zA-Z0-9 ]+$";
                if (Regex.IsMatch(request.LeaveTypeName, leaveRegex) == false)
                    return BadRequest("Invalid Leave name");
            }

            result[0].LeaveTypeName = request.LeaveTypeName;

            await databaseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("UpdateLeaveType")]
        public async Task<ActionResult<LeaveType>> UpdateLeaveType(LeaveType request)
        {
            var result = await databaseContext.LeaveType.Where(e => e.LeaveTypeCode == request.LeaveTypeCode && e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("Leave Type not found");

            result[0].UpdatedDate = DateTime.Now;
            result[0].UpdatedBy = request.UpdatedBy;

            await databaseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("DeleteLeaveTypes")]
        public async Task<ActionResult<LeaveType>> DeleteLeaveTypes(LeaveType request)
        {
            var result = await databaseContext.LeaveType.Where(e => e.LeaveTypeCode == request.LeaveTypeCode && e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("Leave type not found");

            result[0].Deleted = request.Deleted;
            result[0].DeletedDate = DateTime.Now;
            result[0].DeletedBy = request.DeletedBy;

            await databaseContext.SaveChangesAsync();

            return Ok();
        }
    }
}
