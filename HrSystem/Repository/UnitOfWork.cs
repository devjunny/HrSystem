using HrSystem.IRepository;
using HrSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrSystem.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        IGenericRepository<Manager> _managers;

        IGenericRepository<Department> _departments;

        IGenericRepository<Job> _jobs;

        IGenericRepository<ApiUser> _apiuser;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Manager> Managers => _managers ??= new GenericRepository<Manager>(_context);

        public IGenericRepository<Department> Departments => _departments ??= new GenericRepository<Department>(_context);


        public IGenericRepository<Job> Jobs => _jobs ??= new GenericRepository<Job>(_context);

        public IGenericRepository<ApiUser> ApiUsers => _apiuser ??= new GenericRepository<ApiUser>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
