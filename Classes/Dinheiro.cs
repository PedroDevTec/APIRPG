using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApplication2.Classes
{
    public class Dinheiro
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string Moeda { get; set; }
        public decimal Quantidade { get; set; }
        // Outros campos relacionados a Dinheiro
    }
}
