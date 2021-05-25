using System;

namespace MongoDBApi.Models
{
    public class InfectadoDto
    {
        public DateTime DataNascimento { get; set; }
        public string sexo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}