﻿using Dominio;
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
        public HttpResponseMessage Get(int id)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo articulo = negocio.listar().Find(x => x.Id == id);
                if (articulo == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"No se encontró el artículo con ID {id}.");
                }
                return Request.CreateResponse(HttpStatusCode.OK, articulo);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }
        }

        // POST: api/Articulo
        public void Post([FromBody] ArticuloDto articulo)
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Articulo/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                if (negocio.listar().Find(x => x.Id == id) == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"No se encontró el artículo con ID {id}.");
                }
                negocio.eliminarFisico(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Artículo eliminado correctamente.");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.");
            }
        }
    }
}
