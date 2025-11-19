using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MiPrimerServidorApi
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EstadoAula
    {
        Activa,
        Inactiva,
        EnReparacion
    }

    public class Aula
    {
        public int Identificador { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public EstadoAula Estado { get; set; }
        public List<Alumno> Alumnos { get; set; } = new List<Alumno>();
    }
}