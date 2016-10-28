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
                             <asp:GridView ID="GridViewDeltagare" runat="server" style="margin-left: 20%; margin-bottom: 2%;" CellPadding="4" ForeColor="#333333" GridLines="None">
                                 <AlternatingRowStyle BackColor="White" />
                                 <EditRowStyle BackColor="#2461BF" />
                                 <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                 <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                 <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                 <RowStyle BackColor="#EFF3FB" />
                                 <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                 <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                 <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                 <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                 <SortedDescendingHeaderStyle BackColor="#4870BE" />
                              </asp:GridView>
                             <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                        </div>
                       <div class="grid gridRight mobil hidden">
                        <p>höger meny</p>
                    </div>
            </div>
</asp:Content>
