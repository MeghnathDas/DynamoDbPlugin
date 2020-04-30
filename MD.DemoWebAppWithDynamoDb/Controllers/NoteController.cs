/// <summary>
/// Author: Meghnath Das
/// Description:
/// URL: http://meghnathdas.github.io/
/// </summary>
namespace MD.DemoWebAppWithDynamoDb.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MD.DemoWebAppWithDynamoDb.DataAccess;
    using MD.DemoWebAppWithDynamoDb.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IRepo<Note> _noteRepo;
        public NoteController(IRepo<Note> noteRepo)
        {
            this._noteRepo = noteRepo;
        }
        // GET: api/notes
        [HttpGet]
        public IEnumerable<Note> Get()
        {
            return _noteRepo.GetItems(null);
        }

        // GET: api/notes/5
        [HttpGet("{id}", Name = "GetNote")]
        public Note Get(string id)
        {
            return _noteRepo.GetItems(id).FirstOrDefault();
        }

        // POST: api/notes
        [HttpPost]
        public Note Post(Note noteToAdd)
        {
            return _noteRepo.AddItem(noteToAdd);
        }

        // PUT: api/notes/5
        [HttpPut("{id}")]
        public void Put(string id, Note noteToUpdate)
        {
            _noteRepo.UpdateItem(id, noteToUpdate);
        }

        // DELETE: api/notes/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _noteRepo.RemoveItem(id);
        }
    }
}
