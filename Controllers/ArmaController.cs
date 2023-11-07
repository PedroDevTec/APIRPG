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
    public class ArmaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Arma> _armaCollection;

        public ArmaController(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("MongoDBConnection");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("RPGAPI");
            _armaCollection = database.GetCollection<Arma>("arma");
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var armas = _armaCollection.Find(_ => true).ToList();
                return Ok(armas);
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
                var arma = _armaCollection.Find(a => a._id == id).FirstOrDefault();

                if (arma == null)
                {
                    return NotFound();
                }

                return Ok(arma);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar os dados no MongoDB: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Arma arma)
        {
            try
            {
                arma._id = null;
                _armaCollection.InsertOne(arma);
                return Ok("Dados da arma armazenados com sucesso no MongoDB!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao armazenar os dados no MongoDB: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Arma updatedArma)
        {
            try
            {
                var existingArma = _armaCollection.Find(a => a._id == id).FirstOrDefault();

                if (existingArma == null)
                {
                    return NotFound();
                }

                updatedArma._id = existingArma._id;
                _armaCollection.ReplaceOne(a => a._id == id, updatedArma);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar os dados no MongoDB: {ex.Message}");
            }
        }
    }
}
