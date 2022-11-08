using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core.Models
{
    public class EmployeeType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeTypeCode { get; set; }
        public string EmployeeTypeName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; } = 0;
        public DateTime UpdatedDate { get; set; } = new DateTime(1901, 01, 01);
        public int UpdatedBy { get; set; } = 0;
        public bool Deleted { get; set; } = false;
        public DateTime DeletedDate { get; set; } = new DateTime(1901, 01, 01);
        public int DeletedBy { get; set; } = 0;
    }
}
