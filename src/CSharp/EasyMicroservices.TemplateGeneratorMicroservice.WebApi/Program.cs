using EasyMicroservices.TemplateGeneratorMicroservice.Database.Contexts;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi;
using EasyMicroservices.Cores.Relational.EntityFrameworkCore.Intrerfaces;
using EasyMicroservices.TemplateGeneratorMicroservice;
using Microsoft.OpenApi.Models;
using EasyMicroservices.TemplateGeneratorMicroservice.Helpers;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var app = CreateBuilder(args);
            var build = await app.Build<TemplateGeneratorContext>(true);
            ApplicationManager.Instance.DependencyManager = new DependencyManager(build.Services);
            build.MapControllers();
            build.Run();
        }

        static WebApplicationBuilder CreateBuilder(string[] args)
        {
            var app = StartUpExtensions.Create<TemplateGeneratorContext>(args);
            app.Services.Builder<TemplateGeneratorContext>();
            app.Services.AddTransient((serviceProvider) => new UnitOfWork(serviceProvider));
            app.Services.AddTransient(serviceProvider => new TemplateGeneratorContext(serviceProvider.GetService<IEntityFrameworkCoreDatabaseBuilder>()));
            app.Services.AddTransient<IEntityFrameworkCoreDatabaseBuilder, DatabaseBuilder>();
            StartUpExtensions.AddWhiteLabel("TemplateGenerator", "RootAddresses:WhiteLabel");
            return app;
        }

        public static async Task Run(string[] args, Action<IServiceCollection> use)
        {
            var app = CreateBuilder(args);
            use?.Invoke(app.Services);
            var build = await app.Build<TemplateGeneratorContext>();
            build.MapControllers();
            build.Run();
        }
    }
}