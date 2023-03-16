﻿using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Human_Resources.Data.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;
        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddDepartment(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }
        
        public void DeleteDepartment(Department department)
        {
            
            if (department != null)
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();   
            }
            else
            {
                throw new Exception($"The Value was not found ");
            }
        }

        public async Task<List<Department>> GetAll()
        {
            var retLis = await _context.Departments.ToListAsync();
            return retLis;
            
        }

        public async Task<Department> GetById(int id)
        {
            var retval = await _context.Departments.FindAsync(id);
            if (retval != null)
            {
                return retval;
            }
            else
            {
                throw new Exception($"The Value was not found with an id {id}");
            }
        }

        public void UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();

        }
    }
}
