<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="ITProject14.Create" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Create post</h1>
    <%-- "User control" för att skapa nya kunduppgifter. --%>
    <my:PostEdit ID="PostEdit1" runat="server" OnSaved="PostEdit_Saved" OnCanceled="PostEdit_Canceled" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" Runat="Server">
</asp:Content>
