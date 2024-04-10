namespace DAL.Models
{
    public class Employe
    {
        public Guid Id { get; set; }
        public string Login { get; set; }   
        public string FullName { get; set; }    
        public string Department {  get; set; } 
        public bool IsAutorized { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<RequestsForDays> RequestsForDays { get; set; }
    }
}
