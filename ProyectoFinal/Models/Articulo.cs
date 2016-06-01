using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PF_SOA.Models
{
    public class Articulo
    {
        public int Id { get; set; }

        public string Producto { get; set; }

        public string Costo { get; set; }

        public string Referencia { get; set; }
    }
}