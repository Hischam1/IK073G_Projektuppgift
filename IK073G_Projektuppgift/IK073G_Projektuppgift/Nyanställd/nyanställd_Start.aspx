<%@ Page Title="" Language="C#" MasterPageFile="~/JE-Bank.Master" AutoEventWireup="true" CodeBehind="nyanställd_Start.aspx.cs" Inherits="IK073G_Projektuppgift.nyanställd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

                <div class="header" id="header">

                    <%--<a href ="Index.aspx">--%><img id="logotyp" src="/Images/logga.gif" alt="JE LOGO"/><%--</a>--%>

                <div class="nav">
				        <ul id="navRutor">
					        <li ><a href="nyanställd_Start.aspx" class="current">Prov</a></li>
                            <li ><a href="nyanställd_MittKonto.aspx">Mitt konto</a></li>
                            <li id="loggaut" ><a href="../Index.aspx"" >Logga ut</a></li>
				        </ul>
		        </div>

            </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
        <div class="clearfix">
                    <div class="grid subnavigering mobil hidden" >
                         <ul>
                             <li>Sidomeny</li>
				         </ul>
                    </div>
				        <div id="gridMitten" class="grid gridMiddle mobil" runat="server">
                            <div id="namnet" runat="server" style="margin-left: 35%; margin-bottom: 2%;">Välj Person: <asp:DropDownList ID="nyanställningsLista" runat="server" AutoPostBack="True"></asp:DropDownList></div>
                            
                            <p id="provText" runat="server" style="text-align:center; font-size: 16px;">Du har ett licensieringsprov att göra.</p>
                            <asp:Button ID="startaNyttTest" runat="server" Text="Starta prov" OnClick="startaNyttTest_Click"  style="margin-left: 45%; margin-bottom: 2%;"/>
                            <div id="frågeform" runat="server" Visible="false">
                                <h4 id="frågenummer" runat="server" Visible="false"></h4>
                                <h2 id="status" runat="server" Visible="false"></h2>
                                <h4 id="kategori1" runat="server" Visible="false"></h4>
                                <h4 id="kategori2" runat="server" Visible="false"></h4>
                                <h4 id="kategori3" runat="server" Visible="false"></h4>
                                <div id="fråga" runat="server">

                                    </div>
                                <div id="svarsalternativ">
                                        <ul>
                                            <li><asp:CheckBox ID="CheckBox1" runat="server" Visible="false" /></li>
                                            <li><asp:CheckBox ID="CheckBox2" runat="server" Visible="false"/></li>
                                            <li><asp:CheckBox ID="CheckBox3" runat="server" Visible="false"/></li>
                                            <li><asp:CheckBox ID="CheckBox4" runat="server" Visible="false"/></li>
                                        </ul>
                                    </div>
                                                            </div>
                            <asp:Button ID="nästaSida1" runat="server" Text="Nästa Fråga" OnClick="nästaSida1_Click" Visible="false" style="margin-left: 45%; margin-bottom: 2%;"/>
                            <asp:Button ID="avslutaProv" runat="server" Text="Avsluta provet" OnClick="avslutaProv_Click" Visible="false" style="margin-left: 45%; margin-bottom: 2%;"/>
                            <asp:Button ID="seDetaljer" runat="server" Text="Se detaljer" OnClick="seDetaljer_Click" Visible="false" style="margin-left: 45%; margin-bottom: 2%;"/>                          
                            <asp:Button ID="görOm" runat="server" Text="Gör om test" OnClientClick="window.open('nyanställd_Start.aspx', 'nyanställd_Start');" Visible="false" style="margin-left: 45%; margin-bottom: 2%;"/>           
                            <asp:Button ID="avslutaAllt" runat="server" Text="Avsluta" OnClick="avslutaAllt_Click" OnClientClick="window.open('../Index.aspx', 'Index');" Visible="false" style="margin-left: 45%; margin-bottom: 2%;"/>
                        </div>
				    <div class="grid gridRight mobil hidden">
                        
                        <p>höger meny</p>
                    </div>
                </div>
</asp:Content>
