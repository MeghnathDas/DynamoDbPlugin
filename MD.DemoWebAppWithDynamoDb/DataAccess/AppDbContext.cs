﻿/// <summary>
/// Author: Meghnath Das
/// Description:
/// URL: http://meghnathdas.github.io/
/// </summary>
namespace MD.DemoWebAppWithDynamoDb.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using MD.Core.DynamoDb;
    using Microsoft.Extensions.Logging;
    using Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(IAmazonDynamoDB dynamoDB, ILogger<DbContext> logger) : base(dynamoDB, logger)
        {            
        }
        public override void OnModelCreating(EntityBuilder entityBuilder)
        {
            entityBuilder.Entity<NoteCategory>(new NoteCategorySeedDataProvider());
            entityBuilder.Entity<Note>();

            /**Instead of adding Entities individually the following method can be used 
            to include all available entities marked with DynamoDBTable attribute**/
            //entityBuilder.IncludeAllAvailableEntities();
        }
    }
    internal class NoteCategorySeedDataProvider : EntitySeedDataProvider<NoteCategory>
    {
        public override void SetDataToCreateOrUpdate(List<NoteCategory> data)
        {
            data.AddRange(new NoteCategory[] {
                new NoteCategory {
                    Id = "system_catg_important",
                    Name = "Important"
                },
                new NoteCategory {
                    Id = "system_catg_daily_task",
                    Name = "Daily Task"
                }
            });
        }

        public override void SetDataToDelete(List<NoteCategory> data)
        {
            //If needed to delete data from db
        }
    }
}
