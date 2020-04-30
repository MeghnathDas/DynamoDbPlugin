/// <summary>
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

    public interface IRepo<TEntity>
    {
        ICollection<TEntity> GetItems(string id);
        TEntity AddItem(TEntity itmToAdd);
        void UpdateItem(string id, TEntity itmToUpdate);
        void RemoveItem(string id);
    }
}
