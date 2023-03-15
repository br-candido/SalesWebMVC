using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _context;

        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(dp => dp.Name).ToListAsync();
        }
    }
}
