﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HRManagement.DAL.Models.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.DAL.Models.Entities
{
    public partial class Department : IEntity
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
            JobHistories = new HashSet<JobHistory>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(256)]
        public string Name { get; set; } = null!;
        public int ManagerId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        [StringLength(256)]
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedOn { get; set; }
        [StringLength(256)]
        public string? UpdatedBy { get; set; }

        [ForeignKey(nameof(ManagerId))]
        [InverseProperty(nameof(Employee.Departments))]
        public virtual Employee Manager { get; set; } = null!;
        [InverseProperty(nameof(Employee.Department))]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty(nameof(JobHistory.Department))]
        public virtual ICollection<JobHistory> JobHistories { get; set; }
    }
}