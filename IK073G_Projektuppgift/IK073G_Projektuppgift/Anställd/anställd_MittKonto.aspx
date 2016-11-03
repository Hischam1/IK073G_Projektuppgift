<%@ Page Title="" Language="C#" MasterPageFile="~/JE-Bank.Master" AutoEventWireup="true" CodeBehind="anställd_MittKonto.aspx.cs" Inherits="IK073G_Projektuppgift.Anställd.anställd_MittKonto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
                <div class="header" id="header">

                    <%--<a href ="Index.aspx">--%><img id="logotyp" src="/Images/logga.gif" alt="JE LOGO"/><%--</a>--%>

                <div class="nav">
				        <ul id="navRutor">
					        <li ><a href="anställd_Start.aspx">Nytt Prov</a></li>
                            <li ><a href="anställd_MittKonto.aspx" class="current">Mitt Prov</a></li>
                            <li id="loggaut" ><a href="../Index.aspx"" >Logga ut</a></li>
				        </ul>
		        </div>

            </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
     <div class="clearfix">
                    <div class="grid subnavigering mobil" >
                         <ul>
                              <li><div id="namnet" runat="server">Välj Person: <asp:DropDownList ID="anställningsLista" runat="server" OnSelectedIndexChanged="anställningsLista_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></div></li>
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
