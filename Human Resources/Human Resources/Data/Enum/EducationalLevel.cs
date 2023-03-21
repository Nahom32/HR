using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Data.Enum
{
    public enum EducationalLevel
    {
       [Display(Name ="Diploma")] 
       DIPLOMA,
       [Display(Name ="Bachelor's of Science(BSc.)")]
       BSC,
       [Display(Name = "Master's of Science(MSc.)")]
       MSC,
       [Display(Name = "Ph.D")]
       PHD,
       [Display(Name = "Bachelor's of Arts(BA.)")]
       BA,
       [Display(Name = "Master's of Arts(MSc.)")]
       MA,
       [Display(Name = "HighSchool and Under")]
       U12
    }
}
