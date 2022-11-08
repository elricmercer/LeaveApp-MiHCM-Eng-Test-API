using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core.Models
{
    public class EmployeeProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeCode { get; set; }
        public string Number { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public int EmployeeTypeCode { get; set; } = 0;
        public int ReportingPersonCode { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; } = 0;
        public DateTime UpdatedDate { get; set; } = new DateTime(1901, 01, 01);
        public int UpdatedBy { get; set; } = 0;
        public bool Deleted { get; set; } = false;
        public DateTime DeletedDate { get; set; } = new DateTime(1901, 01, 01);
        public int DeletedBy { get; set; } = 0;
    }
}
