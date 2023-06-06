using Human_Resources.Data.ViewModels;
using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface ICertificationService
    {
        public Task<List<Certification>> GetAll();
        public Task<Certification> GetById(int id);
        public Task AddCertification(Certification certificate);
        public Task UpdateCertification(Certification certificate);
        public Task DeleteCertification(Certification certificate);
        public  Task<List<Certification>> FindByEmployeeId(int employeeId);
    }
}
