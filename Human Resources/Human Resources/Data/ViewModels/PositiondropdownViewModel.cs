using Human_Resources.Data.Services;
using Human_Resources.Models;

namespace Human_Resources.Data.ViewModels
{
    public class PositiondropdownViewModel
    {
        public PositiondropdownViewModel()
        {
            Positions = new List<Position>();
        }
        public List<Position> Positions { get; set; }
    }

}
