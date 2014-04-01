<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPost.aspx.cs" Inherits="ITProject14.EditPost" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>
        Edit post</h1>
    <%-- "User control" för redigering av kunduppgifter. --%>
    <my:PostEdit ID="PostEdit" runat="server" PostVisible="true" OnSaved="PostEdit_Saved" OnCanceled="PostEdit_Canceled" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" Runat="Server">
    <script src="<%= ResolveClientUrl("~/Scripts/delete-confirm.js") %>" type="text/javascript"></script>
</asp:Content>
