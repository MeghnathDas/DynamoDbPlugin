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

    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepo<NoteCategory> _categoryRepo;
        public CategoryController(IRepo<NoteCategory> categoryRepo)
        {
            this._categoryRepo = categoryRepo;
        }
        // GET: api/categories
        [HttpGet]
        public IEnumerable<NoteCategory> Get()
        {
            return _categoryRepo.GetItems(null);
        }

        // GET: api/categories/5
        [HttpGet("{id}", Name = "GetCategory")]
        public NoteCategory Get(string id)
        {
            return _categoryRepo.GetItems(id).FirstOrDefault();
        }

        // POST: api/categories
        [HttpPost]
        public NoteCategory Post(NoteCategory catgToAdd)
        {
            return _categoryRepo.AddItem(catgToAdd);
        }

        // PUT: api/categories/5
        [HttpPut("{id}")]
        public void Put(string id, NoteCategory catgToUpdate)
        {
            _categoryRepo.UpdateItem(id, catgToUpdate);
        }

        // DELETE: api/categories/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _categoryRepo.RemoveItem(id);
        }
    }
}
