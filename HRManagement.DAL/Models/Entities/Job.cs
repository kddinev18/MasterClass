using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HRManagement.DAL.Models.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.DAL.Models.Entities
{
    public partial class Job : IEntity
    {
        public Job()
        {
            Employees = new HashSet<Employee>();
            JobHistories = new HashSet<JobHistory>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(256)]
        public string Title { get; set; } = null!;
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        [StringLength(256)]
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedOn { get; set; }
        [StringLength(256)]
        public string? UpdatedBy { get; set; }

        [InverseProperty(nameof(Employee.Job))]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty(nameof(JobHistory.Job))]
        public virtual ICollection<JobHistory> JobHistories { get; set; }
    }
}
