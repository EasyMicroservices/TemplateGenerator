using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Common;
using EasyMicroservices.TemplateGeneratorMicroservice.Contracts.Requests;
using EasyMicroservices.TemplateGeneratorMicroservice.Database;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

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
            builder.Services.AddSwaggerGen(options =>
            {
                options.SchemaFilter<GenericFilter>();
                options.SchemaFilter<XEnumNamesSchemaFilter>();
            });
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

    public class GenericFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var type = context.Type;

            if (type.IsGenericType == false)
                return;

            schema.Title = $"{type.Name[0..^2]}<{type.GenericTypeArguments[0].Name}>";
        }
    }

    public class XEnumNamesSchemaFilter : ISchemaFilter
    {
        private const string NAME = "x-enumNames";
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            var typeInfo = context.Type;
            // Chances are something in the pipeline might generate this automatically at some point in the future
            // therefore it's best to check if it exists.
            if (typeInfo.IsEnum && !model.Extensions.ContainsKey(NAME))
            {
                var names = Enum.GetNames(context.Type);
                var arr = new OpenApiArray();
                arr.AddRange(names.Select(name => new OpenApiString(name)));
                model.Extensions.Add(NAME, arr);
            }
        }
    }
}