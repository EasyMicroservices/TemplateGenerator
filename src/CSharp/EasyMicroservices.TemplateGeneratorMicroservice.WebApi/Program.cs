using EasyMicroservices.Cores.AspEntityFrameworkCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.Cores.Database.Managers;
using EasyMicroservices.Cores.Interfaces;
using EasyMicroservices.Cores.Relational.EntityFrameworkCore.Intrerfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Contexts;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using EasyMicroservices.TemplateGeneratorMicroservice.DatabaseLogics;
using EasyMicroservices.TemplateGeneratorMicroservice.Helpers;
using EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var app = CreateBuilder(args);
            var build = await app.BuildWithUseCors<TemplateGeneratorContext>(null, true);
            UnitOfWork.MapperTypeAssembly = typeof(ItemTypeDatabaseLogic);
            var scope = build.Services.CreateScope();
            ApplicationManager.Instance.DependencyManager = scope.ServiceProvider.GetService<IUnitOfWork>();
            build.MapControllers();
            build.Run();
        }

        public static WebApplicationBuilder CreateBuilder(string[] args)
        {
            var app = StartUpExtensions.Create<TemplateGeneratorContext>(args);
            app.Services.Builder<TemplateGeneratorContext>("TemplateGenerator")
                .UseDefaultSwaggerOptions();
            app.Services.AddTransient((serviceProvider) => new UnitOfWork(serviceProvider));
            app.Services.AddTransient<TemplateGeneratorContext>();
            app.Services.AddTransient<IEntityFrameworkCoreDatabaseBuilder, DatabaseBuilder>();
            app.Services.AddTransient<IUnitOfWork, AppUnitOfWork>();
            app.Services.AddTransient<IBaseUnitOfWork, AppUnitOfWork>();
            app.Services.AddTransient<AppUnitOfWork>();
            app.Services.AddTransient<FormItemLogic>();
            
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