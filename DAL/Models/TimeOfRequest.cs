using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class TimeOfRequest
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }    
        public DateTime StartDate {  get; set; }
        public DateTime EndDate { get; set; }   
        public string Description { get; set; } 
        public RequestStatus  Status { get; set; }

        [ForeignKey("FK_Employe")]
        public Guid EmployeId { get; set; }
        public Employe Employe { get; set; }
    }
}
