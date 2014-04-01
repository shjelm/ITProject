<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="ITProject14.Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>
        Edit member</h1>
    <%-- "User control" för redigering av kunduppgifter. --%>
    <my:MemberEdit ID="MemberEdit" runat="server" PostVisible="true" OnSaved="MemberEdit_Saved" OnCanceled="MemberEdit_Canceled" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" Runat="Server">
    <script src="<%= ResolveClientUrl("~/Scripts/delete-confirm.js") %>" type="text/javascript"></script>
</asp:Content>
