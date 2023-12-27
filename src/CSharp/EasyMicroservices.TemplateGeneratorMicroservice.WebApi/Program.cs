using CompileTimeMapper;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.Cores.Relational.EntityFrameworkCore.Intrerfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Contexts;
using EasyMicroservices.TemplateGeneratorMicroservice.Helpers;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var app = CreateBuilder(args);
            var build = await app.Build<TemplateGeneratorContext>(true);
            UnitOfWork.MapperTypeAssembly = typeof(FormDetailEntity_FormDetailContract_Mapper);
            var scope = build.Services.CreateScope();
            ApplicationManager.Instance.DependencyManager = scope.ServiceProvider.GetService<IUnitOfWork>();
            build.MapControllers();
            build.Run();
        }

        static WebApplicationBuilder CreateBuilder(string[] args)
        {
            var app = StartUpExtensions.Create<TemplateGeneratorContext>(args);
            app.Services.Builder<TemplateGeneratorContext>("TemplateGenerator")
                .UseDefaultSwaggerOptions();
            app.Services.AddTransient((serviceProvider) => new UnitOfWork(serviceProvider));
            app.Services.AddTransient<TemplateGeneratorContext>();
            app.Services.AddTransient<IEntityFrameworkCoreDatabaseBuilder, DatabaseBuilder>();
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