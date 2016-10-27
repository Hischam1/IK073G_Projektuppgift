<%@ Page Title="" Language="C#" MasterPageFile="~/JE-Bank.Master" AutoEventWireup="true" CodeBehind="nyanställd_Start.aspx.cs" Inherits="IK073G_Projektuppgift.nyanställd" %>
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

                            <p id="provText" runat="server" style="text-align:center; font-size: 16px;">Du har ett licensieringsprov att göra.</p>
                            <asp:Button ID="startaNyttTest" runat="server" Text="Starta prov" OnClick="startaNyttTest_Click" style="margin-left: 45%; margin-bottom: 2%;"/>
                            <div id="frågeform" runat="server"></div>
                            <asp:Button ID="nästaSida1" runat="server" Text="Nästa Kategori" OnClick="nästaSida1_Click" Visible="false" style="margin-left: 45%; margin-bottom: 2%;"/>
                            <asp:Button ID="nästaSida2" runat="server" Text="Nästa Kategori" OnClick="nästaSida2_Click" Visible="false" style="margin-left: 45%; margin-bottom: 2%;"/>
                            <asp:Button ID="avslutaProv" runat="server" Text="Avsluta provet" OnClick="avslutaProv_Click" Visible="false" style="margin-left: 45%; margin-bottom: 2%;"/>

                        </div>
				    <div class="grid gridRight mobil hidden">
                        <p>höger meny</p>
                    </div>
                </div>
</asp:Content>
