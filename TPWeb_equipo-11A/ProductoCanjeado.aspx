<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ProductoCanjeado.aspx.cs" Inherits="TPWeb_equipo_11A.ProductoCanjeado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentplaceholder1" runat="server">

     <div class="d-flex justify-content-center">
    <div class="d-flex justify-content-center">
    <div class="row row-cols-1 g-4 text-center">
        <asp:Repeater runat="server" ID="RepeaterProducto" OnItemDataBound="RepeaterProducto_ItemDataBound">
            <ItemTemplate>
                <div class="col">
                    <div class="card mx-auto" style="width: 18rem;">
                        <div id="carouselExampleSlidesOnly" class="carousel slide" data-bs-ride="carousel">
                            <div class="carousel-inner">
                                <asp:Repeater runat="server" ID="RepeaterImagen">
                                    <ItemTemplate>
                                        <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                                            <asp:Image ID="Image" runat="server" CssClass="card-img-top" ImageUrl='<%#Eval("Url") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            <p class="card-text"><%# Eval("Descripcion") %></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
</div>
<div class="d-flex justify-content-center mt-4">
    <div class="card" style="width: 24rem;">
        <div class="card-body text-center">
            <h5 class="card-title">¡Felicitaciones! Obtuviste tu producto</h5>
            <h6 class="card-subtitle mb-3 text-muted">¿Querés canjear otro Voucher?</h6>
            <a href="Default.aspx" class="btn btn-success">¡Sí, quiero canjear otro!</a>
        </div>
    </div>
</div>
</asp:Content>
