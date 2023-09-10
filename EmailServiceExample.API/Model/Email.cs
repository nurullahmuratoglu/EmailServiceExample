namespace EmailServiceExample.API.Model
{
    public class Email
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; } 
        IEnumerable<EmailHistory>? EmailHistory { get; set; }
        
    }
}