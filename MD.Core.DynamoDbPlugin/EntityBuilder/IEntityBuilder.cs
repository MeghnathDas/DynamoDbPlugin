/// <summary>
/// Author: Meghnath Das
/// Description:
/// URL: http://meghnathdas.github.io/
/// </summary>
namespace MD.Core.DynamoDb
{
    public interface IEntityBuilder
    {
        void IncludeAllAvailableEntities();
        void Entity<TEntity>() where TEntity : class;

    }
}
