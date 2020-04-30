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
    public class NoteRepo: IRepo<Note>
    {
        private readonly AppDbContext _dbContext;
        public NoteRepo(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public ICollection<Note> GetItems(string id)
        {
            List<ScanCondition> conditions = new List<ScanCondition>();
            if (!string.IsNullOrWhiteSpace(id))
                conditions.Add(new ScanCondition(nameof(Note.id), ScanOperator.Equal, id));

            return _dbContext.ScanAsync<Note>(conditions).GetRemainingAsync().Result;
        }
        public Note AddItem(Note itmToAdd)
        {
            if (string.IsNullOrWhiteSpace(itmToAdd.Title))
                throw new Exception($"Must provide value for {nameof(itmToAdd.Title).ToLower()}");

            List<ScanCondition> conditions = new List<ScanCondition> {
                new ScanCondition(nameof(NoteCategory.id), ScanOperator.Equal, itmToAdd._CategoryId)
            };

            var parentCatg = _dbContext.ScanAsync<NoteCategory>(conditions).GetRemainingAsync().Result;

            if (parentCatg == null)
                throw new Exception("A valid category must be assigned");

            itmToAdd.id = Guid.NewGuid().ToString();
            itmToAdd.CreatedOn = DateTime.Now;

            _dbContext.SaveAsync<Note>(itmToAdd).Wait();

            return itmToAdd;
        }
        public void UpdateItem(string id, Note noteToUpdate)
        {
            var noteFound = GetItems(id);
            if (!noteFound.Any())
                throw new KeyNotFoundException("Item requested for modification not found");

            noteToUpdate.id = id;
            noteToUpdate.LastUpdatedOn = DateTime.Now;
            _dbContext.SaveAsync<Note>(noteToUpdate).Wait();
        }
        public void RemoveItem(string id)
        {
            var noteToDel = GetItems(id);
            if (!noteToDel.Any())
                throw new KeyNotFoundException("Item requested for deletion not found");

            _dbContext.DeleteAsync<Note>(noteToDel).Wait();
        }
    }
}
