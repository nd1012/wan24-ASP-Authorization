using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using wan24.Core;

namespace wan24_ASP_Authorization_Tests
{
    public static class Initialization
    {
        [ModuleInitializer]
        public static void Init()
        {
            Bootstrap.Async(typeof(Initialization).Assembly).Wait();
            Settings.LogLevel = LogLevel.Trace;
            Logging.Logger = FileLogger.CreateAsync("tests.log", LogLevel.Trace).Result;
            DisposableBase.CreateStackInfo = true;
            DisposableRecordBase.CreateStackInfo = true;
            Logging.WriteInfo("wan24-ASP-Authorization tests initialized");
        }
    }
}
