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
        public HttpResponseMessage Post([FromBody] ArticuloDto articulo)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();

                //CODIGO
                if (string.IsNullOrWhiteSpace(articulo.Codigo) || articulo.Codigo.Length > 50)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'Codigo' está vacìo o es demasiado extenso.");
                }

                //NOMBRE
                if (string.IsNullOrWhiteSpace(articulo.Nombre) || articulo.Nombre.Length > 50)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'Nombre' está vacìo o es demasiado extenso");
                }

                //DESCRIPCION
                if (string.IsNullOrWhiteSpace(articulo.Descripcion) || articulo.Descripcion.Length > 50)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'Descripcion' está vacìo o es demasiado extenso");
                }

                //MARCA
                if (articulo.IdMarca <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'IdMarca' debe ser mayor a cero.");
                }

                //CATEGORIA
                if (articulo.IdCategoria <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'IdCategoria' debe ser mayor a cero.");
                }

                //PRECIO
                if (articulo.Precio <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'Precio' debe ser mayor a cero.");
                }

                Articulo nuevo = new Articulo();

                nuevo.Codigo = articulo.Codigo;
                nuevo.Nombre = articulo.Nombre;
                nuevo.Descripcion = articulo.Descripcion;
                nuevo.Marca = new Marca { Id = articulo.IdMarca };
                nuevo.Categoria = new Categoria { Id = articulo.IdCategoria };
                nuevo.Precio = articulo.Precio;

                negocio.agregarArticulo(nuevo);
                return Request.CreateResponse(HttpStatusCode.Created, "Artículo creado correctamente.");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Error interno: {ex.Message}");
            }
        }

        public HttpResponseMessage Post(int id, [FromBody] List<string> urls)
        {
            try
            {
                ImagenesNegocio negocio = new ImagenesNegocio();
                negocio.agregarImagenes(id, urls);
                return Request.CreateResponse(HttpStatusCode.Created, "Imágenes agregadas correctamente.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Error interno: {ex.Message}");
            }

        }



        // PUT: api/Articulo/5
        public HttpResponseMessage Put(int id, [FromBody] ArticuloDto articulo)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();

                if (negocio.obtenerPorId(id) == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, $"No se encontró el artículo con ID {id}.");
                }


                //CODIGO
                if (string.IsNullOrWhiteSpace(articulo.Codigo) || articulo.Codigo.Length > 50)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'Codigo' está vacìo o es demasiado extenso.");
                }

                //NOMBRE
                if (string.IsNullOrWhiteSpace(articulo.Nombre) || articulo.Nombre.Length > 50)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'Nombre' está vacìo o es demasiado extenso");
                }

                //DESCRIPCION
                if (string.IsNullOrWhiteSpace(articulo.Descripcion) || articulo.Descripcion.Length > 50)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'Descripcion' está vacìo o es demasiado extenso");
                }

                //MARCA
                if (articulo.IdMarca <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'IdMarca' debe ser mayor a cero.");
                }

                //CATEGORIA
                if (articulo.IdCategoria <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'IdCategoria' debe ser mayor a cero.");
                }

                //PRECIO
                if (articulo.Precio <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El campo 'Precio' debe ser mayor a cero.");
                }

                Articulo editado = new Articulo();
                

                editado.Id = id;
                editado.Codigo = articulo.Codigo;
                editado.Nombre = articulo.Nombre;
                editado.Descripcion = articulo.Descripcion;
                editado.Marca = new Marca { Id = articulo.IdMarca };
                editado.Categoria = new Categoria { Id = articulo.IdCategoria };
                editado.Precio = articulo.Precio;

                negocio.modificar(editado);
                return Request.CreateResponse(HttpStatusCode.Created, "Artículo modificado correctamente.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Error interno: {ex.Message}");
            }
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
