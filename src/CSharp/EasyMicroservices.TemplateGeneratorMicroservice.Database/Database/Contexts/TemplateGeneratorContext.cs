using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Contexts
{
    public class TemplateGeneratorContext : DbContext
    {
        public DbSet<FormEntity> Forms { get; set; }
        public DbSet<FormDetailEntity> FormDetails { get; set; }
        public DbSet<FormFilledEntity> FormFills { get; set; }
        public DbSet<FormItemEntity> FormItems { get; set; }
        public DbSet<FormItemValueEntity> FormItemValues { get; set; }
        public DbSet<ItemTypeEntity> ItemTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FormEntity>(model =>
            {
                model.HasKey(r => r.Id);
            });

            modelBuilder.Entity<FormDetailEntity>(model =>
            {
                model.HasKey(x => x.Id);

                model.HasOne(x => x.Form)
                   .WithMany(x => x.FormDetails)
                   .HasForeignKey(x => x.FormId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FormFilledEntity>(model =>
            {
                model.HasKey(x => x.Id);

                model.HasOne(x => x.Form)
                   .WithMany(x => x.FormFills)
                   .HasForeignKey(x => x.FormId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FormItemEntity>(model =>
            {
                model.HasKey(x => x.Id);

                model.HasOne(x => x.Form)
                   .WithMany(x => x.FormItems)
                   .HasForeignKey(x => x.FormId).OnDelete(DeleteBehavior.Restrict);

                model.HasOne(x => x.ItemType)
                   .WithMany(x => x.FormItems)
                   .HasForeignKey(x => x.FormId).OnDelete(DeleteBehavior.Restrict);

                model.HasOne(x => x.ParentFormItem)
                   .WithMany(x => x.Children)
                   .HasForeignKey(x => x.ParentFormItemId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FormItemValueEntity>(model =>
            {
                model.HasKey(x => x.Id);

                model.HasOne(x => x.FormFilled)
                   .WithMany(x => x.FormItemValues)
                   .HasForeignKey(x => x.FormFilledId).OnDelete(DeleteBehavior.Restrict);

                model.HasOne(x => x.FormItem)
                   .WithMany(x => x.FormItemValues)
                   .HasForeignKey(x => x.FormItemId).OnDelete(DeleteBehavior.Restrict);
            });
            
            modelBuilder.Entity<ItemTypeEntity>(model =>
            {
                model.HasKey(x => x.Id);
            });
        }
    }
}