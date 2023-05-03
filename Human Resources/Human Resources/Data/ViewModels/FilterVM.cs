using Human_Resources.Data.Enum;
using Human_Resources.Models;
using X.PagedList;

namespace Human_Resources.Data.ViewModels
{
    public class FilterVM
    {

        public  State State { get; set; }
        public int Page { get; set; } = 1;
        public IPagedList<Employee> Employees{ get; set; }
    }
}
