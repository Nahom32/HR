using Human_Resources.Data.Helpers;
using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IConfigurationService
    {
       
        public Task<Configuration> GetById(int id);
        public Task UpdateConfiguration(Configuration configuration);
        
    }
}
