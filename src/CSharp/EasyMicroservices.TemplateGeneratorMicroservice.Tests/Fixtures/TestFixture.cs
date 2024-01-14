using EasyMicroservices.Cores.AspEntityFrameworkCoreApi;
using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.IdentityMicroservice.BackgroundServices;
using EasyMicroservices.IdentityMicroservice.Database.Contexts;
using EasyMicroservices.IdentityMicroservice.Interfaces;
using EasyMicroservices.IdentityMicroservice.WebApi.Controllers;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Contexts;
using EasyMicroservices.TemplateGeneratorMicroservice.DatabaseLogics;
using EasyMicroservices.TemplateGeneratorMicroservice.Helpers;
using EasyMicroservices.TemplateGeneratorMicroservice.WebApi;
using EasyMicroservices.TemplateGeneratorMicroservice.WebApi.Controllers;
using Identity.GeneratedServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TemplateGenerators.GeneratedServices;
using Xunit;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Tests.Fixtures;
public class TestFixture : IAsyncLifetime
{
    public IServiceProvider ServiceProvider { get; private set; }
    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
        var app = Program.CreateBuilder(null);
        UnitOfWork.MapperTypeAssembly = typeof(ItemTypeDatabaseLogic);
        string authBaseUrl = $"http://localhost:{2007}";
        string generatorBaseUrl = $"http://localhost:{1050}";
        app.Services.AddSingleton(s => new HttpClient());
        app.Services.AddTransient(s => new AuthenticationClient(authBaseUrl, s.GetService<HttpClient>()));
        app.Services.AddTransient(s => new NoParentFormItemClient(generatorBaseUrl, s.GetService<HttpClient>()));
        app.Services.AddMvc().AddApplicationPart(typeof(FormController).Assembly);

        var build = await app.BuildWithUseCors<TemplateGeneratorContext>(default, true);
        build.MapControllers();
        var scope = build.Services.CreateScope();
        ApplicationManager.Instance.DependencyManager = scope.ServiceProvider.GetService<IUnitOfWork>();
        ServiceProvider = app.Services.BuildServiceProvider();
        _ = build.RunAsync();
    }
}