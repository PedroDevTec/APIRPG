using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApplication2.Classes
{
    public class Armadura
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Raridade { get; set; }
        public string Tipo { get; set; }
        public int Defesa { get; set; }
        // Outros campos relacionados a Armadura
    }
}
