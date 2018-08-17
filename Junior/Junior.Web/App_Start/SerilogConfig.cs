using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Junior.Web
{
    public static class SerilogConfig
    {
        public static void Configure()
        {
            var assemblyFolder = AppDomain.CurrentDomain.BaseDirectory;

            var log = new LoggerConfiguration()
                .WriteTo.RollingFile($@"{assemblyFolder}Logs\logs.txt",
                                    outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss}] {Level:u3}: {Message:lj} {Exception} {NewLine}")
                .CreateLogger();

            Log.Logger = log;
            Log.Information("Application started");
        }
    }
}