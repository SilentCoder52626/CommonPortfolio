namespace CommonPortfolio.Domain.Entity.Email
{
    public class EmailConfiguration
    {
        public virtual string? From { get; set; }
        public virtual string? SmtpServer { get; set; }
        public virtual int Port { get; set; }
        public virtual string? UserName { get; set; }
        public virtual string? Password { get; set; }
    }
}
