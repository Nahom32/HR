using Human_Resources.Models;

namespace Human_Resources.Data.ViewModels
{
    public class EmployeedropdownViewModel
    {
        public EmployeedropdownViewModel()
        {
            Employees = new List<Employee>();
        }
        public List<Employee> Employees { get; set; }
    }
}
