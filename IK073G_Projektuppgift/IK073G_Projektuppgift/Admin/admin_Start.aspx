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
</asp:Content>
