using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using WebApplication2.Classes;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Ficha> _fichaCollection;

        public FichaController(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("MongoDBConnection");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("RPGAPI");
            _fichaCollection = database.GetCollection<Ficha>("ficha");
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var fichas = _fichaCollection.Find(_ => true).ToList();
                return Ok(fichas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar os dados no MongoDB: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var ficha = _fichaCollection.Find(f => f._id == id).FirstOrDefault();

                if (ficha == null)
                {
                    return NotFound();
                }

                return Ok(ficha);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar os dados no MongoDB: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Ficha ficha)
        {
            try
            {
                ficha._id = null;
                _fichaCollection.InsertOne(ficha);
                return Ok("Dados de ficha armazenados com sucesso no MongoDB!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao armazenar os dados no MongoDB: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Ficha updatedFicha)
        {
            try
            {
                var existingFicha = _fichaCollection.Find(f => f._id == id).FirstOrDefault();

                if (existingFicha == null)
                {
                    return NotFound();
                }

                updatedFicha._id = existingFicha._id;
                _fichaCollection.ReplaceOne(f => f._id == id, updatedFicha);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar os dados no MongoDB: {ex.Message}");
            }
        }
    }
}
