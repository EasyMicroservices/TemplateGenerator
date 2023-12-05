using EasyMicroservices.Cores.AspEntityFrameworkCoreApi.Interfaces;
using EasyMicroservices.Cores.Database.Logics;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.TemplateGeneratorMicroservice.Database.Entities;
using EasyMicroservices.TemplateGeneratorMicroservice.DataTypes;
using EasyMicroservices.TemplateGeneratorMicroservice.Helpers;
using EasyMicroservices.TemplateGeneratorMicroservice.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMicroservices.TemplateGeneratorMicroservice.DatabaseLogics
{
    public class ItemTypeDatabaseLogic : LongIdDatabaseLogicBase<ItemTypeEntity>
    {
        public ItemTypeDatabaseLogic(IUnitOfWork unitOfWork) :
            base(unitOfWork.GetReadableOf<ItemTypeEntity>(), unitOfWork.GetWritableOf<ItemTypeEntity>(), unitOfWork)
        {

        }

        public static async Task<long> GetItemTypeIdByType(ItemType itemType)
        {
            await using var database = ApplicationManager.Instance.DependencyManager.GetDatabase();
            var readable = database.GetReadableOf<ItemTypeEntity>();
            var result = await readable
                 .Where(x => x.Type == itemType).Select(x => x.Id).FirstOrDefaultAsync();
            if (result == default)
                throw new Exception($"ItemType by type {itemType} not found!");
            return result;
        }
    }
}