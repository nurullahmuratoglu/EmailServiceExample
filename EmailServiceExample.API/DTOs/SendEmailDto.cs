namespace EmailServiceExample.API.DTOs
{
    public class SendEmailDto
    {
        public int EmailId { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
