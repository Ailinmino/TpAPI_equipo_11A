using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MarcaNegocio
    {
            public Marca obtenerPorId(int id)
            {
                AccesoDatos datos = new AccesoDatos();
                try
                {
                    datos.setearConsulta("SELECT Id, Descripcion FROM MARCAS WHERE Id = @id");
                    datos.setearParametros("id", id);
                    datos.ejecutarLectura();

                    if (datos.Lector.Read())
                    {
                        return new Marca
                        {
                            Id = (int)datos.Lector["Id"],
                            Descripcion = datos.Lector["Descripcion"].ToString()
                        };
                    }

                    return null;
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





