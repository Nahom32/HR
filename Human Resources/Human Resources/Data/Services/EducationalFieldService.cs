using Human_Resources.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Human_Resources.Data.Services
{
    public class EducationalFieldService : IEducationalFieldService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EducationalFieldService> _logger;
        public EducationalFieldService(AppDbContext context, ILogger<EducationalFieldService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddEducationalField(EducationalField educationalField)
        {
            await _context.EducationalFields.AddAsync(educationalField);
            await _context.SaveChangesAsync();
        }


        public void DeleteEducationalField(EducationalField educationalField)
        {

            if (educationalField != null)
            {
                _context.EducationalFields.Remove(educationalField);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("The Value was not found ");
            }
        }

        public async Task<List<EducationalField>>GetAll()
        {
            var retLis = await _context.EducationalFields.ToListAsync();
            return retLis;

        }

        public async Task<EducationalField> GetById(int id)
        {
            var retval = await _context.EducationalFields.FindAsync(id);
            if (retval != null)
            {
                return retval;
            }
            else
            {
                throw new Exception($"The Value was not found with an id {id}");
            }
        }

        public void UpdateEducationalField(EducationalField educationalField)
        {
            _context.EducationalFields.Update(educationalField);
            _context.SaveChanges();

        }
    }
}

