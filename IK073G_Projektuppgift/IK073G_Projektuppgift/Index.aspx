﻿<%@ Page Title="" Language="C#" MasterPageFile="~/JE-Bank.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="IK073G_Projektuppgift.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
        <div class="clearfix">
                    <div class="grid subnavigering mobil">
                         <ul>
                             <li>Sidomeny</li>
				         </ul>
                    </div>
				        <div class="grid gridMiddle mobil" runat="server">

                            <a href="nyanställd.aspx">Nyanställd</a>

                            <div id="bild" runat="server"></div>
                            <div id="frågeform" runat="server"></div>
                            

                        </div>
				    <div class="grid gridRight mobil">
                        <p>höger meny</p>
                    </div>
                </div>
</asp:Content>
