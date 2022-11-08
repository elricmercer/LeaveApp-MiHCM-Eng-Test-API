using core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTypeController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public EmployeeTypeController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        [Route("GetEmployeeTypes")]
        public async Task<ActionResult<EmployeeType>> GetEmployeeTypes()
        {
            var result = await databaseContext.EmployeeType.Where(e => e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("No Employee Types found!");

            return Ok(result);
        }

        [HttpGet]
        [Route("GetEmployeeType/{EmployeeTypeCode}")]
        public async Task<ActionResult<EmployeeType>> GetEmployeeType(int EmployeeTypeCode)
        {
            var result = await databaseContext.EmployeeType.Where(e => e.Deleted == false && e.EmployeeTypeCode == EmployeeTypeCode).ToListAsync();

            if (!result.Any())
                return BadRequest("Employee Type not found");

            return Ok(result);
        }

        [HttpPost]
        [Route("AddEmployeeType")]
        public async Task<ActionResult<EmployeeType>> AddEmployeeType(EmployeeType request)
        {
            //cannot be blank
            //only contain characters, numbers and spaces
            //must be unique
            var isUnique = await databaseContext.EmployeeType.Where(e => e.Deleted == false && e.EmployeeTypeName == request.EmployeeTypeName).ToListAsync();

            if (isUnique.Any())
                return BadRequest("Employee Type already exists!");
            else if (request.EmployeeTypeName == "")
                return BadRequest("Employee type name cannot be null/empty");
            else
            {
                var eTypeRegex = @"^[a-zA-Z0-9 ]+$";
                if (Regex.IsMatch(request.EmployeeTypeName, eTypeRegex) == false)
                    return BadRequest("Invalid Employee type name");
            }

            databaseContext.EmployeeType.Add(request);

            await databaseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("UpdateEmployeeTypeName")]
        public async Task<ActionResult<EmployeeType>> UpdateEmployeeTypeName(EmployeeType request)
        {
            var result = await databaseContext.EmployeeType.Where(e => e.EmployeeTypeCode == request.EmployeeTypeCode && e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("Employee Type not found");

            //cannot be blank
            //only contain characters, numbers and spaces
            //must be unique
            var isUnique = await databaseContext.EmployeeType.Where(e => e.EmployeeTypeName == request.EmployeeTypeName && e.Deleted == false).ToListAsync();

            if (isUnique.Any())
                return BadRequest("Employee Type already exists!");
            else if (request.EmployeeTypeName == "")
                return BadRequest("Employee type name cannot be null/empty");
            else
            {
                var eTypeRegex = @"^[a-zA-Z0-9 ]+$";
                if (Regex.IsMatch(request.EmployeeTypeName, eTypeRegex) == false)
                    return BadRequest("Invalid Employee type name");
            }

            result[0].EmployeeTypeName = request.EmployeeTypeName;

            await databaseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("UpdateEmployeeType")]
        public async Task<ActionResult<EmployeeType>> UpdateEmployeeType(EmployeeType request)
        {
            var result = await databaseContext.EmployeeType.Where(e => e.EmployeeTypeCode == request.EmployeeTypeCode && e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("Employee Type not found");

            result[0].UpdatedDate = DateTime.Now;
            result[0].UpdatedBy = request.UpdatedBy;

            await databaseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("DeleteEmployeeType")]
        public async Task<ActionResult<EmployeeType>> DeleteProfile(EmployeeType request)
        {
            var result = await databaseContext.EmployeeType.Where(e => e.EmployeeTypeCode == request.EmployeeTypeCode && e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("Employee Type not found");

            result[0].Deleted = request.Deleted;
            result[0].DeletedDate = DateTime.Now;
            result[0].DeletedBy = request.DeletedBy;

            await databaseContext.SaveChangesAsync();

            return Ok();
        }
    }
}
