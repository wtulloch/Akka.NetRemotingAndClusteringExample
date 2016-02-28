using Serilog;

namespace Shareables
{
    public static class LoggerSetup
    {
        public static ILogger LogToConsoleOnly()
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .WriteTo.ColoredConsole()
                .CreateLogger();
        }

        public static ILogger LogToConsoleAndSeq(string seqUrl)
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .WriteTo.ColoredConsole()
                .WriteTo.Seq(seqUrl)
                .CreateLogger();
        }
    }
}