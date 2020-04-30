/// <summary>
/// Author: Meghnath Das
/// Description:
/// URL: http://meghnathdas.github.io/
/// </summary>
namespace MD.DemoWebAppWithDynamoDb.Models
{
    using Amazon.DynamoDBv2.DataModel;
    using System;

    [DynamoDBTable("Note")]
    public class Note
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string _CategoryId { get; set; }

        [DynamoDBIgnore]
        public virtual NoteCategory Category { get; set; }
    }
}
