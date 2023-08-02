using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApplication2.Classes;

namespace WebApplication2.Controllers
{



    [ApiController]
    [Route("[controller]")]
    public class AtributosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public readonly Atributos att;

        public AtributosController(Atributos atri, IConfiguration configuration)
        {
            att = atri;
            _configuration = configuration;

        }
        [HttpGet]
        public IActionResult Get()
        {
            var rng = new Random();
            var value= rng.Next(0, 20);
            var crit = value * rng.Next(0, 20);

            var newvalue = rng.Next(5,30);
            var dnf = newvalue - rng.Next(0, 20);


            var calculo = new[]
            {
                new {Força = rng.Next(0, 20), 
                    Inteligênci = rng.Next(0, 20), 
                    Vitalidade = rng.Next(0, 20), 
                    Sorte = rng.Next(0, 20), 
                    Crítico = crit, 
                    DanoSofrido = dnf, TXT = ""}
            };

            foreach (var item in calculo)
            {
                if (item.DanoSofrido > 20)
                {
                    var txt = new[]
                    {
                        new {Força = rng.Next(0, 20), 
                            Inteligênci = rng.Next(0, 20), 
                            Vitalidade = rng.Next(0, 20), 
                            Sorte = rng.Next(0, 20), 
                            Crítico = crit, 
                            DanoSofrido = dnf, 
                            TXT = "VOCÊ PEGOU O PASSE LIVRE PRO ONLYFANS DO BUENINHO"}
                    };

                    return Ok(txt);  
                }
            }

            return Ok(calculo);
        }

        [HttpPost("Armazenar")]
        public IActionResult ArmazenarAtributos([FromBody] AtributosData atributos)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("MongoDBConnection");
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("RPGAPI");
                var collection = database.GetCollection<AtributosData>("Atributos");

                // Certifique-se de que o ID está nulo antes de inserir no banco de dados
                atributos.Id = null;

                collection.InsertOne(atributos);

                return Ok("Dados armazenados com sucesso no MongoDB!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao armazenar os dados no MongoDB: {ex.Message}");
            }
        }

        [HttpGet("/api/atributos/{id}")]
        public IActionResult GetAtributosPorId(string id)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("MongoDBConnection");
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("RPGAPI");
                var collection = database.GetCollection<AtributosData>("Atributos");

                var filtro = Builders<AtributosData>.Filter.Eq("_id", ObjectId.Parse(id));

                // Defina a projeção para retornar apenas os campos que você deseja
                var projection = Builders<AtributosData>.Projection
                    .Include(atributo => atributo.Força)
                    .Include(atributo => atributo.Vitalidade)
                    .Include(atributo => atributo.Nome);

                var atributos = collection.Find(filtro)
                                         .Project<AtributosData>(projection)
                                         .FirstOrDefault();

                if (atributos == null)
                {
                    return NotFound("Atributos não encontrados com o ID fornecido.");
                }

                return Ok(atributos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar os dados no MongoDB: {ex.Message}");
            }
        }

        [HttpGet("Todos")]
        public IActionResult GetAtributos()
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("MongoDBConnection");
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("RPGAPI");
                var collection = database.GetCollection<AtributosData>("Atributos");

                var atributos = collection.Find(_ => true).ToList();

                if (atributos.Count == 0)
                {
                    return NotFound("Nenhum atributo encontrado.");
                }

                return Ok(atributos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar os dados no MongoDB: {ex.Message}");
            }
        }
        [HttpPut("/api/atributos/{id}")]
        public IActionResult UpdateAtributos(string id, [FromBody] AtributosData atributosAtualizados)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("MongoDBConnection");
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("RPGAPI");
                var collection = database.GetCollection<AtributosData>("Atributos");

                var filtro = Builders<AtributosData>.Filter.Eq("_id", ObjectId.Parse(id));

                var atributosAntigos = collection.Find(filtro).FirstOrDefault();

                if (atributosAntigos == null)
                {
                    return NotFound("Atributos não encontrados com o ID fornecido.");
                }

                atributosAntigos.Força = atributosAtualizados.Força;
                atributosAntigos.Inteligência = atributosAtualizados.Inteligência;
                atributosAntigos.Vitalidade = atributosAtualizados.Vitalidade;
                atributosAntigos.Sorte = atributosAtualizados.Sorte;
                atributosAntigos.Nome = atributosAtualizados.Nome;

                collection.ReplaceOne(filtro, atributosAntigos);

                return Ok("Atributos atualizados com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar os dados no MongoDB: {ex.Message}");
            }
        }
    }
}
