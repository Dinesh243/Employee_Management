﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAppCore.Entity.Employee_Details.Entity
{
    public partial class Employee_Location
    {
        [Key]
        public int Location_Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Location { get; set; }
        public bool Is_Deleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Created_Time_Stamp { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Updated_Time_Stamp { get; set; }
    }
}