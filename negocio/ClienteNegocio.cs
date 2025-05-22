using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ClienteNegocio
    {
        public void agregarCliente(Cliente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Clientes (Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP) VALUES (@Documento, @Nombre, @Apellido, @Email, @Direccion, @Ciudad, @CP)");

                datos.setearParametros("@Documento", nuevo.Documento);
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.setearParametros("@Apellido", nuevo.Apellido);
                datos.setearParametros("@Email", nuevo.Email);
                datos.setearParametros("@Direccion", nuevo.Direccion);
                datos.setearParametros("@Ciudad", nuevo.Ciudad);
                datos.setearParametros("@CP", nuevo.CodigoPostal);


                datos.ejecutarAccion();
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
        public Cliente existeCliente(string documento)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select Id, Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP from Clientes WHERE Documento = @documento");
                datos.setearParametros("@documento", documento);
                datos.ejecutarLectura();

                Cliente aux = new Cliente();
                while (datos.Lector.Read())
                {
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Documento = (string)datos.Lector["Documento"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Direccion = (string)datos.Lector["Direccion"];
                    aux.Ciudad = (string)datos.Lector["Ciudad"];
                    aux.CodigoPostal = (int)datos.Lector["CP"];
                    return aux;
                    
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
