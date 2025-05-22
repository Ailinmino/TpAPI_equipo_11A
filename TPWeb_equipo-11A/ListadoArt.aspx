<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ListadoArt.aspx.cs" Inherits="TPWeb_equipo_11A.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<% if (codigoVoucher != null)
    {%>
    <div class="row row-cols-1 row-cols-md-3 g-4 text-center">
        <asp:Repeater runat="server" ID="RepeaterProducto" OnItemDataBound="RepeaterProducto_ItemDataBound">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                        <div id="carouselExampleSlidesOnly" class="carousel slide" data-bs-ride="carousel" style="height:400px; align-content:center">
                            <div class="carousel-inner">
                                <asp:Repeater runat="server" ID="RepeaterImagen">
                                    <ItemTemplate>
                                        <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>" >
                                            <asp:Image ID="Image" runat="server" ImageAlign="Middle" Width="200" CssClass="card-img-top" ImageUrl='<%#Eval ("Url") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre") %></h5>
                            <p class="card-text"><%#Eval ("Descripcion")%></p>
                            <asp:Button Text="CANJEAR" ID="btnCanjear" runat="server" CssClass="btn btn-primary" CommandArgument='<%#Eval ("Id")%>' CommandName="ArticuloId" OnClick="btnCanjear_Click" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
<%}
    else
    {%>
    <div class="text-center">
        <h2 >Aún no deberias estar acá</h2>
        <p>Para volver al inicio <a href="/Default.aspx">click aqui</a></p>
    </div>
    
    <%} %>

    <%--<div class="row" >
        <asp:Repeater runat="server" ID="RepeaterProducto" OnItemDataBound="RepeaterProducto_ItemDataBound">
            <ItemTemplate>
                <div class="col justify-content-md-center">
                    <div class="card" style="width: 18rem;">
                        <div id="carouselExampleSlidesOnly" class="carousel slide" data-bs-ride="carousel">
                            <div class="carousel-inner">
                                <asp:Repeater runat="server" ID="RepeaterImagen">
                                    <ItemTemplate>
                                        <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                                            <asp:Image ID="Image" runat="server" ImageUrl='<%#Eval ("Url") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre") %></h5>
                            <p class="card-text"><%#Eval ("Descripcion")%></p>
                            <asp:Button Text="CANJEAR" ID="btnCanjear" runat="server" CssClass="btn btn-primary" CommandArgument='<%#Eval ("Id")%>' CommandName="ArticuloId" OnClick="btnCanjear_Click" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <div class="modal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">¿Desea canjear este artículo?</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:Label Text="text" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary">Save changes</button>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>


