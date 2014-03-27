<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainMenu.ascx.cs" Inherits="ITProject14.Shared.UserControls.MainMenu" %>
<li id="HomeLi" runat="server">
    <asp:HyperLink ID="HomeHyperLink" runat="server" NavigateUrl="~/Default.aspx">Kundlista</asp:HyperLink></li>
<li id="CreateLi" runat="server">
    <asp:HyperLink ID="CreateHyperLink" runat="server" NavigateUrl="~/Create.aspx">Skapa ny kund</asp:HyperLink></li>