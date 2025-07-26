using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public  string Summamry { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public ProductBrand Brand { get; set; }
        public ProductType Type { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
