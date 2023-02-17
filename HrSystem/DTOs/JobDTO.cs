using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrSystem.DTOs
{
    public class CreateJobDTO
    {
        public string job_title { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid amount")]
        public double min_salary { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid amount")]
        public double max_salary { get; set; }
    }


    public class JobDTO : CreateJobDTO
    {
        [Column("JobId")]
        public Guid Id { get; set; }
       
    }
}
