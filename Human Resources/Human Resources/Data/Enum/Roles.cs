using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Data.Enum
{
    public enum Roles
    {
        User,
        [Display(Name="HR Manager")]
        HRManager
    }

}
