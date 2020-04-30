/// <summary>
/// Author: Meghnath Das
/// Description:
/// URL: http://meghnathdas.github.io/
/// </summary>
namespace MD.Core.DynamoDb
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    internal class DbTableInfo
    {
        public string tableName { get; set; }
        public ICollection<DbTableAttributeInfo> attributes { get; set; }
    }
}
