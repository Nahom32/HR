using Human_Resources.Models;
namespace Human_Resources.Data.ViewModels
{
    public class CertificationListViewModel
    {
        public CertificationListViewModel()
        {
            Certifications = new List<Certification>();
        }
        public List<Certification> Certifications { get; set; }
        public int EmployeeId { get; set; }

    }
}
