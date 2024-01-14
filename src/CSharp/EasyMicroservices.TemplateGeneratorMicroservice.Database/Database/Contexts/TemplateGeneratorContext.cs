using EasyMicroservices.Cores.Relational.EntityFrameworkCore;
using EasyMicroservices.Cores.Relational.EntityFrameworkCore.Intrerfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Database.Contexts
{
    public class TemplateGeneratorContext : RelationalCoreContext
    {
        public TemplateGeneratorContext(IEntityFrameworkCoreDatabaseBuilder builder) : base(builder)
        {
        }


        public DbSet<FormEntity> Forms { get; set; }
        public DbSet<FormDetailEntity> FormDetails { get; set; }
        public DbSet<FormFilledEntity> FormFills { get; set; }
        public DbSet<FormItemEntity> FormItems { get; set; }
        public DbSet<FormItemValueEntity> FormItemValues { get; set; }
        public DbSet<ItemTypeEntity> ItemTypes { get; set; }
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<ActionEntity> Actions { get; set; }
        public DbSet<FormItemEventEntity> FormItemEvents { get; set; }
        public DbSet<FormItemEventActionEntity> FormItemEventActions { get; set; }
        public DbSet<FormItemEventActionExecutionEntity> FormItemEventActionCallHistories { get; set; }
        public DbSet<FormItemActionJobEntity> FormItemActionJobs { get; set; }
        
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
                   .HasForeignKey(x => x.ItemTypeId).OnDelete(DeleteBehavior.Restrict);

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
                model.HasIndex(x => x.Type).IsUnique();
            });

            modelBuilder.Entity<ItemTypeEntity>().HasData(
                new ItemTypeEntity()
                {
                    Id = 1,
                    Title = "DateTime",
                    Type = DataTypes.ItemType.DateTime
                },
                new ItemTypeEntity()
                {
                    Id = 2,
                    Title = "DateOnly",
                    Type = DataTypes.ItemType.DateOnly
                },
                new ItemTypeEntity()
                {
                    Id = 3,
                    Title = "TimeOnly",
                    Type = DataTypes.ItemType.TimeOnly
                },
                new ItemTypeEntity()
                {
                    Id = 4,
                    Title = "Label",
                    Type = DataTypes.ItemType.Label
                },
                new ItemTypeEntity()
                {
                    Id = 5,
                    Title = "CheckBox",
                    Type = DataTypes.ItemType.CheckBox
                },
                new ItemTypeEntity()
                {
                    Id = 6,
                    Title = "CheckList",
                    Type = DataTypes.ItemType.CheckList
                },
                new ItemTypeEntity()
                {
                    Id = 7,
                    Title = "OptionList",
                    Type = DataTypes.ItemType.OptionList
                },
                new ItemTypeEntity()
                {
                    Id = 8,
                    Title = "TextBox",
                    Type = DataTypes.ItemType.TextBox
                },
                new ItemTypeEntity()
                {
                    Id = 9,
                    Title = "Table",
                    Type = DataTypes.ItemType.Table
                },
                new ItemTypeEntity()
                {
                    Id = 10,
                    Title = "Row",
                    Type = DataTypes.ItemType.Row
                },
                new ItemTypeEntity()
                {
                    Id = 11,
                    Title = "AutoIncrementNumber",
                    Type = DataTypes.ItemType.AutoIncrementNumber
                });

            modelBuilder.Entity<EventEntity>().HasData(
                new EventEntity()
                {
                    Id = 1,
                    CreationDateTime = DateTime.Now,
                    Name = "Click"
                },
                new EventEntity()
                {
                    Id = 2,
                    CreationDateTime = DateTime.Now,
                    Name = "TextChanged"
                },
                new EventEntity()
                {
                    Id = 3,
                    CreationDateTime = DateTime.Now,
                    Name = "ItemSelected"
                });

            modelBuilder.Entity<ActionEntity>().HasData(
                new ActionEntity()
                {
                    Id = 1,
                    CreationDateTime = DateTime.Now,
                    JobName = "OpenDialog"
                },
                new ActionEntity()
                {
                    Id = 2,
                    CreationDateTime = DateTime.Now,
                    JobName = "OpenResponsibleDialog"
                },
                new ActionEntity()
                {
                    Id = 3,
                    CreationDateTime = DateTime.Now,
                    JobName = "OpenPage"
                },
                new ActionEntity()
                {
                    Id = 4,
                    CreationDateTime = DateTime.Now,
                    JobName = "CallExternalApi"
                },
                new ActionEntity()
                {
                    Id = 5,
                    CreationDateTime = DateTime.Now,
                    JobName = "SendResult"
                },
                new ActionEntity()
                {
                    Id = 6,
                    CreationDateTime = DateTime.Now,
                    JobName = "Close"
                });
        }
    }
}