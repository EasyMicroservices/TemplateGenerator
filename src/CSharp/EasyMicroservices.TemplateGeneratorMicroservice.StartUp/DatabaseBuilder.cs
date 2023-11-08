using EasyMicroservices.Cores.Relational.EntityFrameworkCore.Intrerfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EasyMicroservices.TemplateGeneratorMicroservice
{
    public class DatabaseBuilder : IEntityFrameworkCoreDatabaseBuilder
    {
        readonly IConfiguration _config;
        public DatabaseBuilder(IConfiguration config)
        {
            _config = config;
        }

        public void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TemplateGenerator");
            //optionsBuilder.UseSqlServer("Server=.;Database=TemplateGenerator;User ID=test;Password=test1234;Trusted_Connection=True;TrustServerCertificate=True");
            //optionsBuilder.UseSqlServer(_config.GetConnectionString("local"));
        }
    }
}
