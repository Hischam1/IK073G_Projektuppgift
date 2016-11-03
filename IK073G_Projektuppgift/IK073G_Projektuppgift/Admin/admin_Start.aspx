<%@ Page Title="" Language="C#" MasterPageFile="~/JE-Bank.Master" AutoEventWireup="true" CodeBehind="admin_Start.aspx.cs" Inherits="IK073G_Projektuppgift.Admin.admin_Start" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

                    <div class="header" id="header">

                    <%--<a href ="Index.aspx">--%><img id="logotyp" src="/Images/logga.gif" alt="JE LOGO"/><%--</a>--%>

                <div class="nav">
				        <ul id="navRutor">
					        <li ><a href="admin_Start.aspx" class="current">Provdeltagare</a></li>
                            <li ><a href="#">Provresultat</a></li>
                            <li ><a href="#">Mitt Konto</a></li>
                            <li id="loggaut" ><a href="../Index.aspx"" >Logga ut</a></li>
				        </ul>
		        </div>

            </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
          <div class="clearfix">
                    <div class="grid subnavigering mobil">
                         <ul>
                            <li><div id="namnet" runat="server" style="margin-left: 35%; margin-bottom: 2%;">Välj Person: <asp:DropDownList ID="AdminLista" OnSelectedIndexChanged="AdminLista_SelectedIndexChanged"  AutoPostBack="True" runat="server"></asp:DropDownList></div></li>
                            <li><div id="lista1" runat="server"  Visible="false">Inte klarat licens: <asp:DropDownList ID="LicensInteKlarat" AutoPostBack="True" OnSelectedIndexChanged="LicensInteKlarat_SelectedIndexChanged" runat="server"></asp:DropDownList></div></li>
                            <li><div id="lista3" runat="server"  Visible="false">Inte klarat ÅKU: <asp:DropDownList ID="ÅKUinteKlarat" AutoPostBack="True" OnSelectedIndexChanged="ÅKUinteKlarat_SelectedIndexChanged" runat="server"></asp:DropDownList></div></li>
                            <li><div id="lista4" runat="server"  Visible="false">Klarat Prov: <asp:DropDownList ID="PROVKlarat" AutoPostBack="True" OnSelectedIndexChanged="PROVKlarat_SelectedIndexChanged" runat="server"></asp:DropDownList></div></li>
				         </ul>
                    </div>
                         <div id="gridMitten" class="grid gridMiddle mobil" runat="server">
                                <div id="frågeform" runat="server">
                                <h4 id="frågenummer" runat="server" Visible="false"></h4>
                                <h3 id="provTyp" runat="server" Visible="false"></h3>
                                <h2 id="status" runat="server" Visible="false"></h2>
                                <h4 id="kategori1" runat="server" Visible="false"></h4>
                                <h4 id="kategori2" runat="server" Visible="false"></h4>
                                <h4 id="kategori3" runat="server" Visible="false"></h4>
                                <div id="fråga" runat="server">
 
                                    </div>

                         </div>
                        </div>
                       <div class="grid gridRight mobil hidden">
                        <p>höger meny</p>
                    </div>
            </div>
</asp:Content>
