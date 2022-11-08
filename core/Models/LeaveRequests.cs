using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core.Models
{
    public class LeaveRequests
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveRequestCode { get; set; }
        public int EmployeeCode { get; set; } = 0;
        public int LeaveTypeCode { get; set; } = 0;
        public DateTime FromDate { get; set; } = new DateTime(1901, 01, 01);
        public DateTime EndDate { get; set; } = new DateTime(1901, 01, 01);
        public int NoOfDays { get; set; } = 0;
        public string Reason { get; set; } = string.Empty;
        public string RequestStatus { get; set; } = "P";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; } = 0;
        public DateTime UpdatedDate { get; set; } = new DateTime(1901, 01, 01);
        public int UpdatedBy { get; set; } = 0;
        public bool Deleted { get; set; } = false;
        public DateTime DeletedDate { get; set; } = new DateTime(1901, 01, 01);
        public int DeletedBy { get; set; } = 0;
    }
}
