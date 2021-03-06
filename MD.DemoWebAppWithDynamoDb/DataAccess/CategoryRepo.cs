﻿/// <summary>
/// Author: Meghnath Das
/// Description:
/// URL: http://meghnathdas.github.io/
/// </summary>
namespace MD.DemoWebAppWithDynamoDb.DataAccess
{
    using Amazon.DynamoDBv2.DataModel;
    using Amazon.DynamoDBv2.DocumentModel;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MD.Core.DynamoDb.Helpers;
    using Amazon.DynamoDBv2.Model;

    public class CategoryRepo: IRepo<NoteCategory>
    {
        private readonly AppDbContext _dbContext;
        public CategoryRepo(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public ICollection<NoteCategory> GetItems(string id)
        {
            List<ScanCondition> conditions = new List<ScanCondition>();
            if (!string.IsNullOrWhiteSpace(id))
                conditions.Add(new ScanCondition(nameof(NoteCategory.Id), ScanOperator.Equal, id));

            return _dbContext.ScanAsync<NoteCategory>(conditions).GetRemainingAsync().Result;
        }
        internal ICollection<NoteCategory> GetItems(IEnumerable<string> ids)
        {
            ids = ids.Where(id => !string.IsNullOrWhiteSpace(id)).Distinct();

            List<ScanCondition> conditions = new List<ScanCondition>();
            if (ids.Any())
                conditions.Add(
                        new ScanCondition(nameof(NoteCategory.Id), ScanOperator.In, ids.ToArray())
                    );

            return _dbContext.ScanAsync<NoteCategory>(conditions).GetRemainingAsync().Result;
        }
        public NoteCategory AddItem(NoteCategory itemToAdd)
        {
            if (string.IsNullOrWhiteSpace(itemToAdd.Name))
                throw new Exception($"{nameof(itemToAdd.Name)} must be provided");

            itemToAdd.Id = Guid.NewGuid().ToString();

            _dbContext.SaveAsync<NoteCategory>(itemToAdd).Wait();

            return itemToAdd;
        }
        public void UpdateItem(string id, NoteCategory itmToUpdate)
        {
            var catgFound = GetItems(id);
            if (!catgFound.Any())
                throw new KeyNotFoundException("Item requested for modification not found");

            itmToUpdate.Id = id;
            _dbContext.SaveAsync<NoteCategory>(itmToUpdate).Wait();
        }
        public void RemoveItem(string id)
        {
            var catgToDel = GetItems(id);
            if (!catgToDel.Any())
                throw new KeyNotFoundException("Item requested for deletion not found");

            _dbContext.DeleteAsync<NoteCategory>(catgToDel.First()).Wait();
        }
    }
}
