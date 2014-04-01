<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainMenu.ascx.cs" Inherits="ITProject14.Shared.UserControls.MainMenu" %>
<li id="HomeLi" runat="server">
    <asp:HyperLink ID="HomeHyperLink" runat="server" NavigateUrl="~/Default.aspx">All posts</asp:HyperLink></li>
<li id="CreateLi" runat="server">
    <asp:HyperLink ID="CreateHyperLink" runat="server" NavigateUrl="~/Create.aspx">Create post</asp:HyperLink></li>