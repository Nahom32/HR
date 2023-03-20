using Human_Resources.Data.Services;
using Human_Resources.Models;

namespace Human_Resources.Data.ViewModels
{
    public class DepartmentdropdownViewModel
    {
        public DepartmentdropdownViewModel()
        {
            Departments = new List<Department>();
        }
        public List<Department> Departments { get; set; }
    }
}
