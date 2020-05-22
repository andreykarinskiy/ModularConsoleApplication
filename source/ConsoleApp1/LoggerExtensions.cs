using Serilog;
using Serilog.Context;

namespace ConsoleApp1
{
    public static class LoggerExtensions
    {
        public static void Audith(this ILogger logger, AuditEvent auditEvent)
        {
            using (LogContext.PushProperty("EventId", auditEvent.EventId))
            {
                logger.Information(auditEvent.ToString());
            }
        }
    }
}
