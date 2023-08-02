using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApplication2.Classes
{
    public class AtributosData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Força { get; set; }
        public int Inteligência { get; set; }
        public int Vitalidade { get; set; }
        public int Sorte { get; set; }
        public string Nome { get; set; }
    }
}
