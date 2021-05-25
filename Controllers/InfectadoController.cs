using System;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDBApi.Data.Collections;
using MongoDBApi.Models;

namespace MongoDBApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infectado> _infectadosCollection;

        public InfectadoController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto)
        {
            var infectado = new Infectado(dto.DataNascimento,dto.sexo,dto.Latitude,dto.Longitude);

            _infectadosCollection.InsertOne(infectado);

            return StatusCode(201,"Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();

            return Ok(infectados);
        }

        [HttpPut]
        public ActionResult AtualizarInfectado([FromBody] InfectadoDto dto)
        {
            var novosDados = new Infectado(dto.DataNascimento,dto.sexo,dto.Latitude,dto.Longitude) ;

            _infectadosCollection.ReplaceOne(Builders<Infectado>.Filter.Where( _ => _.DataNascimento == dto.DataNascimento),novosDados);

            return Ok("Atualizado com sucesso");
        }

        [HttpDelete("{dataNascimento}")]
        public ActionResult AtualizarInfectado(DateTime dataNascimento)
        {
            _infectadosCollection.DeleteOne(Builders<Infectado>.Filter.Where( _ => _.DataNascimento == dataNascimento));

            return Ok("Registro deletado com sucesso.");
        }

    }
}