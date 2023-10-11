using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApplication2.Classes
{
    public class Recursos
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Material { get; set; }
        public int Quantidade { get; set; }
        // Outros campos relacionados a Recursos
    }
}
