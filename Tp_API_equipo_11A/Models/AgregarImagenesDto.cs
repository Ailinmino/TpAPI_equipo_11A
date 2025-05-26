using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dominio;
using Negocio;

namespace Tp_API_equipo_11A.Models
{
    public class AgregarImagenesDto
    {
        public int IdArticulo { get; set; }
        public List<string> Imagenes { get; set; }
    }
}