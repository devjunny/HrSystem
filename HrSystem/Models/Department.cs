using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrSystem.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string department_name { get; set; }

        [ForeignKey(nameof(Manager))]
        public Guid ManagerId { get; set; }
        public Manager Manager { get; set; }

        public virtual IList<Manager> Managers { get; set; }
    }
}
