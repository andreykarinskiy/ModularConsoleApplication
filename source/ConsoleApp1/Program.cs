using Serilog;
using Serilog.Context;
using System;
using System.ComponentModel;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string cefPattern = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} host CEF:0|Nexign|VideoanAlyst|1.0|{EventId}|{Message:lj}|{Level:u3}|{NewLine}";

            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: cefPattern)
                .WriteTo.File("c:\\temp\\cef.log", outputTemplate: cefPattern)
                .CreateLogger();

            Log.Logger = logger;



            // Запись события аудита.
            logger.Audith(new UserLoggedIn("123"));
            
            //using (LogContext.PushProperty("EventId", AuditableOperation.UserLoggedIn))
            //{
            //    Log.Information("Оператор id:{UserId} зашел в систему.", 12345);
            //}

            Log.CloseAndFlush();



            Console.ReadLine();
        }
    }

    public abstract class AuditEvent
    {
        protected AuditEvent(int eventId, string userId)
        {
            EventId = eventId;

            UserId = userId;
        }

        public int EventId { get; }

        public string UserId { get; }

        protected abstract string Format { get; }

        public override string ToString()
        {
            return Format;
        }
    }

    public sealed class UserLoggedIn : AuditEvent
    {
        public UserLoggedIn(string userId) : base(eventId: 100, userId)
        {
        }

        protected override string Format => $"Оператор id:{UserId} зашел в систему.";
    }
}
