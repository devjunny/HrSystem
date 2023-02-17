using HrSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrSystem.Configuration
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasData
            (
              new Manager
              {
                  Id = new Guid("bc528192-2726-4f85-b7f1-cc278a87052e"),
                  manager_firstName = "Kenny",
                  manager_lastName = "Rodgers",
                  phone = "0243784512",
                  email = "rodgers@live.com",
                  address = "Kofi Shito Street 45"
              },
              new Manager
              {
                  Id = new Guid("d36acd2e-1506-4362-9e16-6772008883b8"),
                  manager_firstName = "Dolly",
                  manager_lastName = "Parton",
                  phone = "0206857945",
                  email = "dolly@live.com",
                  address = "Olusegu Obasanjo 12T"
              }
            );
        }
    }
}
