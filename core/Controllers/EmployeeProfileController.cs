using core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProfileController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;

        public EmployeeProfileController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        [Route("GetEmployeeProfiles")]
        public async Task<ActionResult<EmployeeProfile>> GetProfiles()
        {
            var result = await databaseContext.EmployeeProfile.Where(e => e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("No Employees found!");

            return Ok(result);
        }

        [HttpGet]
        [Route("GetEmployeeProfile/{EmployeeCode}")]
        public async Task<ActionResult<EmployeeProfile>> GetProfile(int EmployeeCode)
        {
            var result = await databaseContext.EmployeeProfile.Where(e => e.EmployeeCode == EmployeeCode && e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("Employee not found");

            return Ok(result);
        }

        [HttpGet]
        [Route("GetEmployeeProfile2/{ReportingPersonCode}")]
        public async Task<ActionResult<EmployeeProfile>> GetProfile2(int ReportingPersonCode)
        {
            var result = await databaseContext.EmployeeProfile.Where(e => e.ReportingPersonCode == ReportingPersonCode && e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("Employee not found");

            return Ok(result);
        }

        [HttpPost]
        [Route("AddEmployeeProfile")]
        public async Task<ActionResult<EmployeeProfile>> AddProfile(EmployeeProfile request)
        {
            //cannot be blank
            //only contain characters and spaces
            if (request.Fullname == "")
                return BadRequest("Fullname cannot be null/empty");
            else
            {
                var fullnameRegex = @"^[a-zA-Z ]+$";
                if (Regex.IsMatch(request.Fullname, fullnameRegex) == false)
                    return BadRequest("Invalid Fullname");
            }

            //cannot be blank
            //only contain characters, numbers and spaces
            //must be unique
            var isUnique = await databaseContext.EmployeeProfile.Where(e => e.Number == request.Number && e.Deleted == false).ToListAsync();

            if (isUnique.Any())
                return BadRequest("Number already exists!");
            else if (request.Number == "")
                return BadRequest("Number cannot be null/empty");
            else
            {
                var numberRegex = @"^[a-zA-Z0-9 ]+$";
                if (Regex.IsMatch(request.Number, numberRegex) == false)
                    return BadRequest("Invalid Number");
            }

            databaseContext.EmployeeProfile.Add(request);

            await databaseContext.SaveChangesAsync();

            return Ok(await databaseContext.EmployeeProfile.Where(e => e.Number == request.Number && e.Deleted == false).ToListAsync());
        }

        [HttpPut]
        [Route("UpdateEmployeeProfile")]
        public async Task<ActionResult<EmployeeProfile>> UpdateProfile(EmployeeProfile request)
        {
            var result = await databaseContext.EmployeeProfile.Where(e => e.EmployeeCode == request.EmployeeCode && e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("Employee not found");

            //cannot be blank
            //only contain characters and spaces
            if (request.Fullname == "")
                return BadRequest("Fullname cannot be null/empty");
            else
            {
                var fullnameRegex = @"^[a-zA-Z ]+$";
                if (Regex.IsMatch(request.Fullname, fullnameRegex) == false)
                    return BadRequest("Invalid Fullname");
            }

            result[0].Fullname = request.Fullname;
            result[0].EmployeeTypeCode = request.EmployeeTypeCode;
            result[0].ReportingPersonCode = request.ReportingPersonCode;
            result[0].UpdatedDate = DateTime.Now;
            result[0].UpdatedBy = request.UpdatedBy;

            await databaseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("DeleteEmployeeProfile")]
        public async Task<ActionResult<EmployeeProfile>> DeleteProfile(EmployeeProfile request)
        {
            var result = await databaseContext.EmployeeProfile.Where(e => e.EmployeeCode == request.EmployeeCode && e.Deleted == false).ToListAsync();

            if (!result.Any())
                return BadRequest("Employee not found");

            result[0].Deleted = request.Deleted;
            result[0].DeletedDate = DateTime.Now;
            result[0].DeletedBy = request.DeletedBy;

            await databaseContext.SaveChangesAsync();

            return Ok(result);
        }
    }
}
