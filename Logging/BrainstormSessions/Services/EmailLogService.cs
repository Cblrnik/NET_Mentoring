using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;
using System.Net;
using System.Net.Mail;

namespace BrainstormSessions.Services
{
    public interface IEmailLogService
    {
        void Log(string message, LogEventLevel level);
    }

    public class EmailLogService : IEmailLogService
    {
        private readonly ILogger _logger;
        public EmailLogService() 
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Email(
                    restrictedToMinimumLevel: LogEventLevel.Error,
                    connectionInfo: new EmailConnectionInfo()
                    {
                        EmailSubject = "API ERROR!!!",
                        NetworkCredentials = new NetworkCredential("kinkskygame@gmail.com", "Pass"),
                        FromEmail = "kinkskygame@gmail.com",
                        ToEmail = "rnehaikov@gmail.com",
                        MailServer = "smtp.gmail.com",
                        EnableSsl = true,
                        Port = 587
                    }
                 ).CreateLogger();
        }

        public void Log(string message, LogEventLevel level) 
        {
            _logger.Write(level, message);
        }
    }
}
