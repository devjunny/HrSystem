using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HrSystem.DTOs
{
    public class CreateManagerDTO
    {
        public string manager_firstName { get; set; }
        public string manager_lastName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
    }



    public class ManagerDTO : CreateManagerDTO
    {
        public Guid Id { get; set; }
      
    }
}
