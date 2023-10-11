using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApplication2.Classes
{
    public class Consumiveis
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string status { get; set; }
        // Outros campos relacionados a Consumíveis
    }
}
