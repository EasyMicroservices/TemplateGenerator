using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Requests;
using EasyMicroservices.TemplateGeneratorMicroservice.Database;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;

namespace EasyMicroservices.TemplateGeneratorMicroservice.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped((serviceProvider) => new DependencyManager().GetContractLogic<FormEntity, CreateFormRequestContract, FormContract>());
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IDatabaseBuilder>(serviceProvider => new DatabaseBuilder());

            var app = builder.Build();
            app.UseDeveloperExceptionPage();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            StartUp startUp = new StartUp();
            await startUp.Run(new DependencyManager());
            app.Run();
        }
    }
}