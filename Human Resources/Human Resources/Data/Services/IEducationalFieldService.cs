using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IEducationalFieldService
    {
        public Task<List<EducationalField>> GetAll();
        public Task<EducationalField> GetById(int id);
        public Task AddEducationalField(EducationalField educationalField);
        public void UpdateEducationalField(EducationalField educationalField);
        public void DeleteEducationalField(EducationalField educationalField);

    }
}
