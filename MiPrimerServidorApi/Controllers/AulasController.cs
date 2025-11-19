using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MiPrimerServidorApi.Controllers
{
    [Route("api/[controller]")] // La ruta será: api/aulas
    [ApiController]
    public class AulasController : ControllerBase
    {
        // Simulamos una base de datos en memoria (static para que no se borre entre peticiones)
        private static List<Aula> _aulas = new List<Aula>();

        // GET: api/aulas
        [HttpGet]
        public ActionResult<IEnumerable<Aula>> Get()
        {
            return Ok(_aulas);
        }

        // GET api/aulas/5
        [HttpGet("{identificador}")]
        public ActionResult<Aula> Get(int identificador)
        {
            var aula = _aulas.FirstOrDefault(a => a.Identificador == identificador);

            if (aula == null)
                return NotFound("Aula no encontrada");

            return Ok(aula);
        }

        // POST api/aulas
        // Aquí envías el aula completa (con o sin alumnos)
        [HttpPost]
        public ActionResult Post([FromBody] Aula nuevaAula)
        {
            // Verificamos si ya existe un aula con ese ID
            if (_aulas.Any(a => a.Identificador == nuevaAula.Identificador))
            {
                return BadRequest("Ya existe un aula con ese Identificador.");
            }

            _aulas.Add(nuevaAula);
            return CreatedAtAction(nameof(Get), new { identificador = nuevaAula.Identificador }, nuevaAula);
        }

        // PUT api/aulas/5
        [HttpPut("{identificador}")]
        public ActionResult Put(int identificador, [FromBody] Aula aulaEditada)
        {
            var aulaExistente = _aulas.FirstOrDefault(a => a.Identificador == identificador);

            if (aulaExistente == null)
                return NotFound("Aula no encontrada");

            // Actualizamos los datos
            aulaExistente.Ubicacion = aulaEditada.Ubicacion;
            aulaExistente.Estado = aulaEditada.Estado;
            aulaExistente.Alumnos = aulaEditada.Alumnos; // Actualiza la lista de alumnos

            return NoContent();
        }

        // DELETE api/aulas/5
        [HttpDelete("{identificador}")]
        public ActionResult Delete(int identificador)
        {
            var aula = _aulas.FirstOrDefault(a => a.Identificador == identificador);

            if (aula == null)
                return NotFound("Aula no encontrada");

            _aulas.Remove(aula);
            return NoContent();
        }
    }
}