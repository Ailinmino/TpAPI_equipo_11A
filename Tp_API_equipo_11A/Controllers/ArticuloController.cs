using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tp_API_equipo_11A.Models;

namespace Tp_API_equipo_11A.Controllers
{
    public class ArticuloController : ApiController
    {
        // GET: api/Articulo
        public IEnumerable<Articulo> Get()//LISTAR
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            return negocio.listar();
        }

        // GET api/values/5 BUSCAR
        public Articulo Get(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo articulo = negocio.listar().Find(x => x.Id == id);
            return articulo;
        }

        // POST: api/Articulo
        public void Post([FromBody]ArticuloDto articulo)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo nuevo = new Articulo();
            Imagen imagen;

            nuevo.Codigo = articulo.Codigo;
            nuevo.Nombre = articulo.Nombre;
            nuevo.Descripcion = articulo.Descripcion;
            nuevo.Marca = new Marca { Id = articulo.IdMarca };
            nuevo.Categoria = new Categoria { Id = articulo.IdCategoria };
            nuevo.Precio = articulo.Precio;
            nuevo.Imagen = new List<Imagen>();
            imagen = new Imagen { Url = articulo.Imagen };
            nuevo.Imagen.Add(imagen);

            negocio.agregarArticulo(nuevo);
        }

        // PUT: api/Articulo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Articulo/5
        public void Delete(int id)
        {
        }
    }
}
