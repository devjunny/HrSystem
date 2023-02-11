using HrSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrSystem.DTOs
{
    public class CreateDepartmentDTO
    {
        public string department_name { get; set; }
        [ForeignKey(nameof(Manager))]
        public Guid ManagerId { get; set; }
       
    }


    public class DepartmentDTO : CreateDepartmentDTO
    {
        [Column("DepartmentId")]
        public Guid Id { get; set; }

        public Manager Manager { get; set; }

    }
}
