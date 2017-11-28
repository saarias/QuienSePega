using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuienSePega.Models
{
    public class Evento
    {
        [Key]
        public int Id { get; set; }

        public String Creador { get; set; }

        public int IdLugarOrigen { get; set; }

        public int IdLugarDestino { get; set; }

        public Boolean Estado { get; set; }
        
        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

    }
}