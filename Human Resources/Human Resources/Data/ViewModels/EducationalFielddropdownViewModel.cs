using Human_Resources.Models;

namespace Human_Resources.Data.ViewModels
{
    public class EducationalFielddropdownViewModel
    {
        public EducationalFielddropdownViewModel()
        {
            EducationalFields = new List<EducationalField>();  
        }
        public List<EducationalField> EducationalFields { get; set;}

    }
}
