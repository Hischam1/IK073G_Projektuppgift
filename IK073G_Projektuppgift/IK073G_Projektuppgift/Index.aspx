<%@ Page Title="" Language="C#" MasterPageFile="~/JE-Bank.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="IK073G_Projektuppgift.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

                <div class="header" id="header">

                    <%--<a href ="Index.aspx">--%><img id="logotyp" src="/Images/logga.gif" alt="JE LOGO"/><%--</a>--%>

            <div class="nav">
				    <ul id="navRutor">
					    <li ><a href="Index.aspx" class="current">Start</a></li>
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
				        <div id="väljPerson" class="grid gridMiddle mobil" runat="server" >

                            <a href="Nyanställd/nyanställd_Start.aspx">Nyanställd</a>
                            <a href="Anställd/anställd_Start.aspx">Anställd</a>
                            <a href="Admin/admin_Start.aspx">Admin</a>

                            <div id="bild" runat="server"></div>
                            

                        </div>
				    <div class="grid gridRight mobil hidden">
                        <p>höger meny</p>
                    </div>
                </div>
</asp:Content>
