<%@ Page Title="" Language="C#" MasterPageFile="~/JE-Bank.Master" AutoEventWireup="true" CodeBehind="admin_Start.aspx.cs" Inherits="IK073G_Projektuppgift.Admin.admin_Start" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

                    <div class="header" id="header">

                    <%--<a href ="Index.aspx">--%><img id="logotyp" src="/Images/logga.gif" alt="JE LOGO"/><%--</a>--%>

                <div class="nav">
				        <ul id="navRutor">
					        <li ><a href="#" class="current">Prov</a></li>
                            <li ><a href="#">Mitt konto</a></li>
                            <li id="loggaut" ><a href="../Index.aspx"" >Logga ut</a></li>
				        </ul>
		        </div>

            </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
          <div class="clearfix">
                    <div class="grid subnavigering mobil hidden">
                         <ul>
                             <li>Sidomeny</li>
				         </ul>
                    </div>
                         <div id="gridMitten" class="grid gridMiddle mobil" runat="server">
                              <div id="namnet" runat="server" style="margin-left: 35%; margin-bottom: 2%;">Välj Person: <asp:DropDownList ID="AdminLista" runat="server"></asp:DropDownList></div>

                        </div>
                       <div class="grid gridRight mobil hidden">
                        <p>höger meny</p>
                    </div>
            </div>
</asp:Content>
