using wan24.ASP.Authorization;
using wan24.Core;

namespace wan24_ASP_Authorization_Integration_Tests
{
    public partial class Program
    {
        static Program()
        {
            Bootstrap.Async().Wait();
            Settings.LogLevel = LogLevel.Trace;
            Logging.Logger = FileLogger.CreateAsync("tests.log", LogLevel.Trace).Result;
            //DisposableBase.CreateStackInfo = true;
            //DisposableRecordBase.CreateStackInfo = true;
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.AddWan24AuthZ();
            builder.AddWan24FakeAuthN();

            var app = builder.Build();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
