/// <summary>
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
    using System.Threading.Tasks;
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
                conditions.Add(new ScanCondition(nameof(NoteCategory.id), ScanOperator.Equal, id));

            return _dbContext.ScanAsync<NoteCategory>(conditions).GetRemainingAsync().Result;
        }
        public NoteCategory AddItem(NoteCategory itemToAdd)
        {
            if (string.IsNullOrWhiteSpace(itemToAdd.Name))
                throw new Exception($"{nameof(itemToAdd.Name)} must be provided");

            itemToAdd.id = Guid.NewGuid().ToString();

            _dbContext.SaveAsync<NoteCategory>(itemToAdd).Wait();

            return itemToAdd;
        }
        public void UpdateItem(string id, NoteCategory itmToUpdate)
        {
            var catgFound = GetItems(id);
            if (!catgFound.Any())
                throw new KeyNotFoundException("Item requested for modification not found");

            itmToUpdate.id = id;
            _dbContext.SaveAsync<NoteCategory>(itmToUpdate).Wait();
        }
        public void RemoveItem(string id)
        {
            var catgToDel = GetItems(id);
            if (!catgToDel.Any())
                throw new KeyNotFoundException("Item requested for deletion not found");

            _dbContext.DeleteAsync<NoteCategory>(catgToDel).Wait();
        }
    }
}
