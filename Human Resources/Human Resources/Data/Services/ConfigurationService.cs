using Human_Resources.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Human_Resources.Data.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly AppDbContext _context;
        public ConfigurationService(AppDbContext context)
        {

            _context = context;

        }
        public async Task<Configuration> GetById(int id)
        {
            var configuration = await _context.Configurations.FirstOrDefaultAsync(n => n.Id == id);
            if (configuration == null)
            {
                throw new Exception("The configuration file isn't found");
            }
            else
            {
                return configuration;
            }
        }

        public async Task UpdateConfiguration(Configuration configuration)
        {
           _context.Configurations.Update(configuration);
           await _context.SaveChangesAsync();
        }
    }
}
