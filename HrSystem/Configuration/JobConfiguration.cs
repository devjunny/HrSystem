using HrSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrSystem.Configuration
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasData
            (
                new Job
                {
                    Id = new Guid("02a73fcc-7bdf-4f47-99e5-3a00cbbd69d4"),
                    job_title = "Software Engineering",
                    min_salary = 1500,
                    max_salary = 5000,
                },
                new Job
                {
                    Id = new Guid("dbc5233f-d894-43cb-a3e3-e6b842a67feb"),
                    job_title = "Accountant",
                    min_salary = 1500,
                    max_salary = 4000,
                }
            );
        }
    }
}
