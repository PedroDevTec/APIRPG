using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApplication2.Classes
{
    public class Ficha
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public string Pericia { get; set; }
        public string Raca { get; set; }
        public string Regiao { get; set; }
        public string Titulo { get; set; }
        public int Honra { get; set; }
    }
}

