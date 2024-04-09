using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HRManagement.DAL.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.DAL.Data.Entities
{
    public partial class Employee : IEntity
    {
        public Employee()
        {
            Departments = new HashSet<Department>();
            InverseManager = new HashSet<Employee>();
            JobHistories = new HashSet<JobHistory>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(256)]
        public string FirstName { get; set; } = null!;
        [StringLength(256)]
        public string LastName { get; set; } = null!;
        [StringLength(256)]
        public string Email { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string PhoneNumber { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public int JobId { get; set; }
        public int? ManagerId { get; set; }
        public int DepartmentId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        [StringLength(256)]
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedOn { get; set; }
        [StringLength(256)]
        public string? UpdatedBy { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("Employees")]
        public virtual Department Department { get; set; } = null!;
        [ForeignKey(nameof(JobId))]
        [InverseProperty("Employees")]
        public virtual Job Job { get; set; } = null!;
        [ForeignKey(nameof(ManagerId))]
        [InverseProperty(nameof(Employee.InverseManager))]
        public virtual Employee? Manager { get; set; }
        [InverseProperty("Manager")]
        public virtual ICollection<Department> Departments { get; set; }
        [InverseProperty(nameof(Employee.Manager))]
        public virtual ICollection<Employee> InverseManager { get; set; }
        [InverseProperty(nameof(JobHistory.Employee))]
        public virtual ICollection<JobHistory> JobHistories { get; set; }
    }
}
