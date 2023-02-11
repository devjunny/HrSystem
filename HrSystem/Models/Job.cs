using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrSystem.Models
{
    public class Job
    {
       
        public Guid Id { get; set; }
        public string job_title { get; set; }
       
        public double min_salary { get; set; }
        
        public double max_salary { get; set; }

    }
}
