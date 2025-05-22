using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPWeb_equipo_11A
{
    public partial class Default : System.Web.UI.Page
    {
        public ArticuloNegocio negocio = new ArticuloNegocio();
        public string codigoVoucher; 
        public List<Articulo> lista;
        protected void Page_Load(object sender, EventArgs e)
        {
            codigoVoucher = Session["codigoVoucher"] != null ? Session["codigoVoucher"].ToString() : null;
            if (!IsPostBack)
            {
                cargar();
                RepeaterProducto.ItemDataBound += RepeaterProducto_ItemDataBound;
            }
        }

        public void cargar()
        {
            lista = negocio.listar();
            RepeaterProducto.DataSource = lista;
            RepeaterProducto.DataBind();
        }

        protected void RepeaterProducto_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Articulo articulo = (Articulo)e.Item.DataItem;

                Repeater rptImagenes = (Repeater)e.Item.FindControl("RepeaterImagen");
                if (rptImagenes != null && articulo.Imagen != null)
                {
                    rptImagenes.DataSource = articulo.Imagen;
                    rptImagenes.DataBind();
                }
            }
        }

        //El sender es el elemento que envía el evento

        protected void btnCanjear_Click(object sender, EventArgs e)
        {
            //en valor traigo el argumento que capture en el botón dentro del repeater 
            string valor = ((Button)sender).CommandArgument;
            Session.Add("articuloSeleccionado",valor);
            Response.Redirect("Clientes.aspx");
        }

    }
}