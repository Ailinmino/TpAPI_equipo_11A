using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominio;

namespace Tp_API_equipo_11A.Models
{
    public class ArticuloDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdMarca { get; set; }
        public int IdCategoria { get; set; }
        public float Precio { get; set; }
        public List<Imagen> Imagen { get; set; }
    }
}