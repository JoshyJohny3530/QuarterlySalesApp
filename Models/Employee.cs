using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuarterlySalesApp.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfHire { get; set; }

        public int? ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public Employee? Manager { get; set; }

        public ICollection<Employee>? Subordinates { get; set; }
    }
}
