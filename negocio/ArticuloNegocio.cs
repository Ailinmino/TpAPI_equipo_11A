using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar() //1. Metodo para que lea los registros de la base de datos
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
            SELECT A.Id, A.Nombre, A.Codigo, A.Descripcion, 
                   A.IdCategoria, A.IdMarca, 
                   M.Descripcion AS Marca, 
                   C.Descripcion AS Categoria, 
                   A.Precio, 
                   I.ImagenUrl AS ImagenUrl, I.Id AS IdImagen
            FROM ARTICULOS A
            LEFT JOIN MARCAS M ON A.IdMarca = M.Id
            LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id
            LEFT JOIN IMAGENES I ON A.Id = I.IdArticulo");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int id = (int)datos.Lector["Id"];
                    Articulo existente = lista.FirstOrDefault(a => a.Id == id);
                    if (existente == null)
                    {
                        Articulo aux = new Articulo();
                        aux.Id = id;
                        aux.Codigo = datos.Lector["Codigo"]?.ToString();
                        aux.Nombre = datos.Lector["Nombre"]?.ToString();
                        aux.Descripcion = datos.Lector["Descripcion"]?.ToString();

                        aux.Marca = new Marca
                        {
                            Id = datos.Lector["IdMarca"] != DBNull.Value ? (int)datos.Lector["IdMarca"] : 0,
                            Descripcion = datos.Lector["Marca"]?.ToString()
                        };

                        aux.Categoria = new Categoria
                        {
                            Id = datos.Lector["IdCategoria"] != DBNull.Value ? (int)datos.Lector["IdCategoria"] : 0,
                            Descripcion = datos.Lector["Categoria"]?.ToString()
                        };

                        aux.Precio = datos.Lector["Precio"] != DBNull.Value ? Convert.ToSingle(datos.Lector["Precio"]) : 0;
                        aux.Imagenes = new List<Imagen>();

                        if (datos.Lector["IdImagen"] != DBNull.Value)
                        {
                            aux.Imagenes.Add(new Imagen
                            {
                                Id = (int)datos.Lector["IdImagen"],
                                Url = datos.Lector["ImagenUrl"]?.ToString()
                            });
                        }

                        lista.Add(aux);
                    }
                    else
                    {
                        if (datos.Lector["IdImagen"] != DBNull.Value)
                        {
                            existente.Imagenes.Add(new Imagen
                            {
                                Id = (int)datos.Lector["IdImagen"],
                                Url = datos.Lector["ImagenUrl"]?.ToString()
                            });
                        }
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void agregarArticulo(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria) VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @IDMarca, @IDCategoria); SELECT SCOPE_IDENTITY();");
                datos.setearParametros("@Codigo", nuevo.Codigo);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Descripcion", nuevo.Descripcion);
                datos.setearParametros("@Precio", nuevo.Precio);
                datos.setearParametros("@IdMarca", nuevo.Marca.Id);
                datos.setearParametros("@IdCategoria", nuevo.Categoria.Id);

                // Obtener el ID generado del artículo
                nuevo.Id = datos.ejecutarScalar();

                /* // Si hay imagen, insertarla con el ID del artículo
                 if (!string.IsNullOrEmpty(nuevo.Imagen[0].Url))
                 {
                     datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");
                     datos.setearParametros("@IdArticulo", nuevo.Id);
                     datos.setearParametros("@ImagenUrl", nuevo.Imagen[0].Url);
                     datos.ejecutarAccion();
                 }*/
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void agregarImagenes(int idArticulo, List<string> urls)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                foreach (var url in urls)
                {
                    datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");
                    datos.setearParametros("@IdArticulo", idArticulo);
                    datos.setearParametros("@ImagenUrl", url);
                    datos.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool existeCodigo(string codigo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM ARTICULOS WHERE Codigo = @Codigo");
                datos.setearParametros("@Codigo", codigo);

                int cantidad = datos.ejecutarScalar();
                return cantidad > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Articulo articulo)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Precio = @Precio where Id = @Id");
                accesoDatos.setearParametros("@Codigo", articulo.Codigo);
                accesoDatos.setearParametros("@Nombre", articulo.Nombre);
                accesoDatos.setearParametros("@Descripcion", articulo.Descripcion);
                accesoDatos.setearParametros("@IdMarca", articulo.Marca.Id);
                accesoDatos.setearParametros("@IdCategoria", articulo.Categoria.Id);
                accesoDatos.setearParametros("@Precio", articulo.Precio);
                accesoDatos.setearParametros("@Id", articulo.Id);
                accesoDatos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }

        public void eliminarLogico(int ID_articulo)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("update from ARTICULOS set ACTIVO = 0 where Id = @ID_articulo");
                datos.setearParametros("ID_articulo", ID_articulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void eliminarFisico(int ID_articulo)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where Id = @ID_articulo");
                datos.setearParametros("ID_articulo", ID_articulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Articulo obtenerPorId(int id)
        {
            Articulo articulo = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT A.Id, A.Nombre, A.Descripcion, A.Codigo, A.Precio,
                               I.Id AS IdImagen, I.ImagenUrl,
                               M.Id AS IdMarca, M.Descripcion AS NombreMarca,
                               C.Id AS IdCategoria, C.Descripcion AS NombreCategoria
                               FROM ARTICULOS A
                               LEFT JOIN IMAGENES I ON A.Id = I.IdArticulo
                               LEFT JOIN MARCAS M ON A.IdMarca = M.Id
                               LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id                               
                               WHERE A.Id = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    if (articulo == null)
                    {
                        articulo = new Articulo();
                        articulo.Id = (int)datos.Lector["Id"];
                        articulo.Nombre = datos.Lector["Nombre"].ToString();
                        articulo.Descripcion = datos.Lector["Descripcion"].ToString();
                        articulo.Codigo = datos.Lector["Codigo"]?.ToString();
                        articulo.Precio = (float)(datos.Lector["Precio"] != DBNull.Value ? Convert.ToDecimal(datos.Lector["Precio"]) : 0);
                        articulo.Imagenes = new List<Imagen>();
                    }

                    articulo.Marca = new Marca
                    {
                        Id = datos.Lector["IdMarca"] != DBNull.Value ? (int)datos.Lector["IdMarca"] : 0,
                        Descripcion = datos.Lector["NombreMarca"]?.ToString()
                    };

                    articulo.Categoria = new Categoria
                    {
                        Id = datos.Lector["IdCategoria"] != DBNull.Value ? (int)datos.Lector["IdCategoria"] : 0,
                        Descripcion = datos.Lector["NombreCategoria"]?.ToString()
                    };

                    if (datos.Lector["IdImagen"] != DBNull.Value)
                    {
                        Imagen imagen = new Imagen
                        {
                            Id = (int)datos.Lector["IdImagen"],
                            Url = datos.Lector["ImagenUrl"].ToString()
                        };
                        articulo.Imagenes.Add(imagen);
                    }
                }

                return articulo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
