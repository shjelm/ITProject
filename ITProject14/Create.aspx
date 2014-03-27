<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="ITProject14.Create" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>
        Register member</h1>
    <%-- "User control" för att skapa nya kunduppgifter. --%>
    <my:MemberEdit ID="MemberEdit" runat="server" OnSaved="MemberEdit_Saved" OnCanceled="MemberEdit_Canceled" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" Runat="Server">
</asp:Content>
