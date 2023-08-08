using EasyMicroservices.TemplateGeneratorMicroservice.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EasyMicroservices.TemplateGeneratorMicroservice
{
    public class DatabaseBuilder : IDatabaseBuilder
    {
        readonly IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();
        public void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TemplateGenerator");
            //optionsBuilder.UseSqlServer("Server=.;Database=TemplateGenerator;User ID=test;Password=test1234;Trusted_Connection=True;TrustServerCertificate=True");
            //optionsBuilder.UseSqlServer(config.GetConnectionString("local"));
        }
    }
}
