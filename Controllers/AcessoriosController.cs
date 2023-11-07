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
    public class AcessoriosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Acessorios> _acessoriosCollection;

        public AcessoriosController(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("MongoDBConnection");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("RPGAPI");
            _acessoriosCollection = database.GetCollection<Acessorios>("acessorios");
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var acessorios = _acessoriosCollection.Find(_ => true).ToList();
                return Ok(acessorios);
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
                var acessorio = _acessoriosCollection.Find(a => a._id == id).FirstOrDefault();

                if (acessorio == null)
                {
                    return NotFound();
                }

                return Ok(acessorio);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar os dados no MongoDB: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Acessorios acessorio)
        {
            try
            {
                acessorio._id = null;
                _acessoriosCollection.InsertOne(acessorio);
                return Ok("Dados do acessório armazenados com sucesso no MongoDB!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao armazenar os dados no MongoDB: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Acessorios updatedAcessorio)
        {
            try
            {
                var existingAcessorio = _acessoriosCollection.Find(a => a._id == id).FirstOrDefault();

                if (existingAcessorio == null)
                {
                    return NotFound();
                }

                updatedAcessorio._id = existingAcessorio._id;
                _acessoriosCollection.ReplaceOne(a => a._id == id, updatedAcessorio);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar os dados no MongoDB: {ex.Message}");
            }
        }
    }
}
