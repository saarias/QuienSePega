using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuienSePega.Models
{
    public class Lugares
    {
        [Key]
        public int Id { get; set; }
        
        public String Latitud { get; set; }

        public String Longitud { get; set; }

        public String Tipo { get; set; }

        public String Nombre { get; set; }
    }
}