using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Certification
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        public string CertificationName { get; set; }
        public string CertifyingCompany { get; set; }
        public DateTime CertificationDate { get; set; }
        public string CredentialLink { get; set; }
    }
}
