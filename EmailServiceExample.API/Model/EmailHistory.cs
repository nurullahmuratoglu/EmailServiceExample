namespace EmailServiceExample.API.Model
{
    public class EmailHistory
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string SenderEmail { get; set; }
        public DateTime SendDate { get; set; }
        public int EmailId { get; set; }
        public Email Email { get; set; }
        
    }
}
