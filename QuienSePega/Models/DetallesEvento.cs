using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuienSePega.Models
{
    public class DetallesEvento
    {
        public String Creador { get; set; }

        public String Origen { get; set; }

        public String Destino { get; set; }

        public String HoraInicio { get; set; }

        public String HoraFin { get; set; }

        public int idEvento1 { get; set; }

        public int NumeroIntegrantes { get; set; }

        public String LatitudOrigen { get; set; }

        public String LongitudOrigen { get; set; }

        public String LatitudDestino { get; set; }

        public String LongitudDestino { get; set; }
    }
}