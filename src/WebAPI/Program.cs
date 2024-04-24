using UtterlyComplete.Infrastructure.Data;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDataAbstractionLayer(builder.Configuration);
            builder.Services.AddRepositories();
            builder.Services.AddControllers();

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");
            app.MapControllers();

            app.Run();
        }
    }
}
