namespace ECommerce.Application.DTOs.Email
{
    public class EmailSendDTO
    {
        public string ReceiverEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsHtml { get; set; }

        public string? Link { get; set; }
    }
}