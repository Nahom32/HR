using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Data.Enum
{
    public enum LeaveType
    {
        Annual,
        Maternity,
        Paternity,
        Wedding,
        Mourning,
        Exam,
        Sick,
        [Display(Name = "Special Purposes")]
        Special_Purpose,
        [Display(Name ="Public Holiday")]
        Public_Holiday,

    }
}
