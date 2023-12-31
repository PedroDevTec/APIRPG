﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using WebApplication2.Classes;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArmaduraController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Armadura> _armaduraCollection;

        public ArmaduraController(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("MongoDBConnection");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("RPGAPI");
            _armaduraCollection = database.GetCollection<Armadura>("armadura");
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var armaduras = _armaduraCollection.Find(_ => true).ToList();
                return Ok(armaduras);
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
                var armadura = _armaduraCollection.Find(a => a._id == id).FirstOrDefault();

                if (armadura == null)
                {
                    return NotFound();
                }

                return Ok(armadura);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar os dados no MongoDB: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Armadura armadura)
        {
            try
            {
                armadura._id = null;
                _armaduraCollection.InsertOne(armadura);
                return Ok("Dados de armadura armazenados com sucesso no MongoDB!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao armazenar os dados no MongoDB: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Armadura updatedArmadura)
        {
            try
            {
                var existingArmadura = _armaduraCollection.Find(a => a._id == id).FirstOrDefault();

                if (existingArmadura == null)
                {
                    return NotFound();
                }

                updatedArmadura._id = existingArmadura._id;
                _armaduraCollection.ReplaceOne(a => a._id == id, updatedArmadura);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar os dados no MongoDB: {ex.Message}");
            }
        }
    }
}
