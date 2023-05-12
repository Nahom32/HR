using Human_Resources.Models;

namespace Human_Resources.Data.ViewModels
{
    public class EmployeeTableViewModel
    {
        public List<Employee> Employees { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }

}
