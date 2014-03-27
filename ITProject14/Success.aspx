<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="ITProject14.Success" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>
        <asp:Literal ID="MessageLiteral" runat="server" /></h1>
    <p class="redirect">
        Om 5 sekunder förflyttas du automatiskt till denna
        <asp:HyperLink ID="RedirectHyperLink" runat="server" NavigateUrl='<%# ReturnUrl %>'
            Text="sida" />.</p>
</asp:Content>
