using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPWeb_equipo_11A
{
    public partial class ProductoCanjeado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["articuloSeleccionado"] != null)
                {
                    int id = int.Parse(Session["articuloSeleccionado"].ToString());
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    Articulo articulo = negocio.obtenerPorId(id);

                    // Creamos una lista porque el Repeater espera una colección
                    List<Articulo> lista = new List<Articulo> { articulo };
                    RepeaterProducto.DataSource = lista;
                    RepeaterProducto.DataBind();
                }
            }
        }

        protected void RepeaterProducto_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Articulo articulo = (Articulo)e.Item.DataItem;
                Repeater repeaterImagen = (Repeater)e.Item.FindControl("RepeaterImagen");
                repeaterImagen.DataSource = articulo.Imagen;
                repeaterImagen.DataBind();
            }
        }
    }
    }
