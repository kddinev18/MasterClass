﻿using HRManagement.DAL.Data.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagement.DAL.Data.Entities
{
    [Table("JobHistory")]
    public partial class JobHistory : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int JobId { get; set; }
        public int DepartmentId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        [StringLength(256)]
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedOn { get; set; }
        [StringLength(265)]
        public string? UpdatedBy { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("JobHistories")]
        public virtual Department Department { get; set; } = null!;
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("JobHistories")]
        public virtual Employee Employee { get; set; } = null!;
        [ForeignKey(nameof(JobId))]
        [InverseProperty("JobHistories")]
        public virtual Job Job { get; set; } = null!;
    }
}
