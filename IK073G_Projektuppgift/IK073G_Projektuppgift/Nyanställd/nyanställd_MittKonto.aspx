<%@ Page Title="" Language="C#" MasterPageFile="~/JE-Bank.Master" AutoEventWireup="true" CodeBehind="nyanställd_MittKonto.aspx.cs" Inherits="IK073G_Projektuppgift.Nyanställd.nyanställd_MittKonto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

                <div class="header" id="header">

                    <%--<a href ="Index.aspx">--%><img id="logotyp" src="/Images/logga.gif" alt="JE LOGO"/><%--</a>--%>

                <div class="nav">
				        <ul id="navRutor">
					        <li ><a href="nyanställd_Start.aspx">Prov</a></li>
                            <li ><a href="nyanställd_MittKonto.aspx" class="current">Mitt konto</a></li>
                            <li id="loggaut" ><a href="../Index.aspx"" >Logga ut</a></li>
				        </ul>
		        </div>

            </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
     <div class="clearfix">
                    <div class="grid subnavigering mobil" >
                         <ul>
                             <li><div id="namnet" runat="server" style="margin-left: 35%; margin-bottom: 2%;">Välj Person: <asp:DropDownList ID="nyanställningsLista" runat="server" OnSelectedIndexChanged="nyanställningsLista_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></div></li>
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
