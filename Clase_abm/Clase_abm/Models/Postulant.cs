using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.ComponentModel.DataAnnotations;


namespace Clase_abm.Models
{
    public class Postulant
    {
        public int PostID { get; set; }
        [Required(ErrorMessage = "Campo inválido")]
        public string PostName { get; set; }
        [Required(ErrorMessage = "Campo inválido"),Range(18,25)]
        public int PostAge { get; set; }
        [Required(ErrorMessage = "Campo inválido")]
        public Country Country { get; set; }
    }
}
