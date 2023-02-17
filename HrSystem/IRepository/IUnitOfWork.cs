using HrSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrSystem.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Manager> Managers { get; }

        IGenericRepository<Department> Departments { get; }

        IGenericRepository<Job> Jobs { get; }

        IGenericRepository<ApiUser> ApiUsers { get; }


        Task Save();
    }
}
