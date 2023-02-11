using HrSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrSystem.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData
            (
                new Department
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    department_name = "Finance",
                    ManagerId = new Guid("bc528192-2726-4f85-b7f1-cc278a87052e")

                },
                new Department
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    department_name = "Accounting",
                    ManagerId = new Guid("d36acd2e-1506-4362-9e16-6772008883b8")
                }
            );

        }
    }
}
