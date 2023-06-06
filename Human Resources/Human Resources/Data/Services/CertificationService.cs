using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resources.Data.Services
{
    public class CertificationService : ICertificationService
    {
        private readonly AppDbContext _context;
        public CertificationService(AppDbContext context)
        {

            _context = context;

        }
        public async Task AddCertification(Certification certificate)
        {
            var value =  await _context.Certifications.FirstOrDefaultAsync(n=>n.CredentialLink == certificate.CredentialLink);
            if (value == null)
            {
                await _context.Certifications.AddAsync(certificate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCertification(Certification certificate)
        {
            var toDelete = await _context.Certifications.FirstOrDefaultAsync(n => n.Id == certificate.Id);
            if (toDelete != null)
            {
                _context.Certifications.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("The asked Leave doesn't exist");
            }
        }

        public async Task<List<Certification>> GetAll()
        {
            var certificates = await _context.Certifications.ToListAsync();
            return certificates;
        }

        public async Task<Certification> GetById(int id)
        {
            var certificate = await _context.Certifications.FirstOrDefaultAsync(n => n.Id==id);
            if(certificate != null)
            {
                return certificate;
            }
            else
            {
                throw new Exception("The asked leave doesn't exist");

            }
        }

        public async Task UpdateCertification(Certification certificate)
        {
            _context.Certifications.Update(certificate);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Certification>> FindByEmployeeId(int employeeId)
        {
            
            var allCertificates = await _context.Certifications.Where(n => n.EmployeeId ==  employeeId).ToListAsync();
            if(allCertificates != null)
            {
                return allCertificates;
            }
            else
            {
                return new List<Certification>();
            }

        }
    }
}
