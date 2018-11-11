using System;
using System.Collections.Generic;
using System.Linq;
using Clase_abm.Datab;
using Clase_abm.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clase_abm.Controllers
{
    public class PostulantController : ControllerBase
    {

        private readonly IData data;

        public PostulantController(IData dataGen)
        {
            this.data = dataGen;
        }

        [HttpGet]
        [Route("/api/Postulant/{id}")]
        public Postulant Get(int id)
        {
            return data.GetPostulant(id);
        }
        [HttpGet]
        [Route("/api/Postulant/")]
        public IEnumerable<Postulant> Get()
        {
            return data.GetPostulantAll();
        }
        [HttpPost]
        [Route("/api/Postulant/")]
        public void Post([FromBody] Postulant postulant)
        {
            if (this.ModelState.IsValid && (postulant.PostName.IndexOf(" ") != -1))
            {
                data.Insert(postulant);
            }
        }

        [HttpPut]
        [Route("/api/Postulant/")]
        public Postulant Put([FromBody] Postulant postulant)
        {
            if (this.ModelState.IsValid)
            {
                return data.Update(postulant);
            }
            //else throw new Exception("Esta maaaaal");
            else
            {
                var errores = this.ModelState.Values.ToArray(); //devuelvo la cantidad de errores
                throw new Exception();
            }
        }

        [HttpDelete]
        [Route("/api/Postulant/{id}")]
        public void Delete(int id)
        {
            data.Delete(id);
        }
    }
}
