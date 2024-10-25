namespace LMT.Api.ExceptionHandling
{
    public class EmailNotConfirmedException : Exception
    {
        public EmailNotConfirmedException() : base("EmailNotConfirmedException") { }
        public EmailNotConfirmedException(string message) : base(message) { }
    }
}
