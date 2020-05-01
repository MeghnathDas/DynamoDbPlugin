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
    public class NoteRepo: IRepo<Note>
    {
        private readonly AppDbContext _dbContext;
        private readonly IRepo<NoteCategory> _catgRepo;
        public NoteRepo(AppDbContext dbContext, IRepo<NoteCategory> catgRepo)
        {
            this._dbContext = dbContext;
            this._catgRepo = catgRepo;
        }
        public ICollection<Note> GetItems(string id)
        {
            List<ScanCondition> conditions = new List<ScanCondition>();
            if (!string.IsNullOrWhiteSpace(id))
                conditions.Add(new ScanCondition(nameof(Note.Id), ScanOperator.Equal, id));

            var notes = _dbContext.ScanAsync<Note>(conditions).GetRemainingAsync().Result;
            var catgIds = notes.Select(note => note._CategoryId);
            if (catgIds.Any())
            {
                var catgs = ((CategoryRepo)_catgRepo).GetItems(catgIds);
                notes.ForEach(note =>
                    note.Category = catgs.FirstOrDefault(catg => catg.Id.Equals(note._CategoryId))
                );
            }
            return notes;
        }
        public Note AddItem(Note itmToAdd)
        {
            if (string.IsNullOrWhiteSpace(itmToAdd.Title))
                throw new Exception($"Must provide value for {nameof(itmToAdd.Title).ToLower()}");

            List<ScanCondition> conditions = new List<ScanCondition> {
                new ScanCondition(nameof(NoteCategory.Id), ScanOperator.Equal, itmToAdd._CategoryId)
            };

            var parentCatg = _dbContext.ScanAsync<NoteCategory>(conditions).GetRemainingAsync().Result;

            if (!parentCatg.Any())
                throw new Exception("A valid category must be assigned");

            itmToAdd.Id = Guid.NewGuid().ToString();
            itmToAdd.CreatedOn = DateTime.Now;

            _dbContext.SaveAsync<Note>(itmToAdd).Wait();

            return itmToAdd;
        }
        public void UpdateItem(string id, Note noteToUpdate)
        {
            var noteFound = GetItems(id);
            if (!noteFound.Any())
                throw new KeyNotFoundException("Item requested for modification not found");

            noteToUpdate.Id = id;
            noteToUpdate.CreatedOn = noteFound.First().CreatedOn;
            noteToUpdate.LastUpdatedOn = DateTime.Now;
            _dbContext.SaveAsync<Note>(noteToUpdate).Wait();
        }
        public void RemoveItem(string id)
        {
            var noteToDel = GetItems(id);
            if (!noteToDel.Any())
                throw new KeyNotFoundException("Item requested for deletion not found");

            _dbContext.DeleteAsync<Note>(noteToDel.First()).Wait();
        }
    }
}
