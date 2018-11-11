using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Clase_abm.Models;

namespace Clase_abm.Datab
{
        public interface IData
        {
            void Insert(Postulant postulant);
            Postulant GetPostulant(int id);
            IEnumerable<Postulant> GetPostulantAll();
            void Delete(int id);
            Postulant Update(Postulant postulant);
        }
}
