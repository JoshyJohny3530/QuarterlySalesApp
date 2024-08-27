using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuarterlySalesApp.Models
{
    public class SalesData
    {
        public int SalesDataId { get; set; }

        [Range(1, 4)]
        public int Quarter { get; set; }

        [Range(2001, 2100)]
        public int Year { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
    }
}
