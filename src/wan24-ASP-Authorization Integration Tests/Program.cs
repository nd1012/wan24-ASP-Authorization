using wan24.ASP.Authorization;

namespace wan24_ASP_Authorization_Integration_Tests
{
    public partial class Program
    {
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
