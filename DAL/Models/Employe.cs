using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Employe
    {
        [Key]
        public Guid Id { get; set; }
        public string FullName { get; set; }    
        public string Department {  get; set; } 
        public bool IsAutorized { get; set; }

        public ICollection<TimeOfRequest> TimeOfRequests { get; set; }
    }
}
