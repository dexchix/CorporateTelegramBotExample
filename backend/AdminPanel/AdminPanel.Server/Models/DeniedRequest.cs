namespace AdminPanel.Server.Models
{
    public class DeniedRequest
    {
        public string Id { get; set; }  
        public string Reason { get; set; }
    }

    public class AprovedRequest
    {
        public string Id { get; set; }
    }
}
