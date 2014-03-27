<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="ITProject14.Details" %>

<%@ Register src="Shared/UserControls/ContactEdit.ascx" tagname="ContactEdit" tagprefix="my" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>
        Kunddetaljer</h1>
    <asp:ValidationSummary ID="CustomerValidationSummary" runat="server" HeaderText="Fel inträffade. Korrigera det som är fel och försök igen."
        CssClass="validation-summary-errors-icon" />
    <%-- Namn --%>
    <div class="editor-label">
        <asp:Label ID="Label1" runat="server" Text="Namn:" />
    </div>
    <div class="editor-field">
        <asp:Label ID="NameLabel" runat="server" />
    </div>
    <%-- Mail --%>
    <div class="editor-label">
        <asp:Label ID="Label3" runat="server" Text="Mail:" />
    </div>
    <div class="editor-field">
        <asp:Label ID="MailLabel" runat="server" />
    </div>
    <%-- Username --%>
    <div class="editor-label">
        <asp:Label ID="Label5" runat="server" Text="Username:" />
    </div>
    <div class="editor-field">
        <asp:Label ID="UsernameLabel" runat="server" />
    </div>
    <%-- Password --%>
    <div class="editor-label">
        <asp:Label ID="Label7" runat="server" Text="Password:" />
    </div>
    <div class="editor-field">
        <asp:Label ID="PasswordLabel" runat="server" />
    </div>
    <%-- Kontaktuppgifter --%>
    <my:ContactEdit ID="ContactEdit1" runat="server" />
    <p>
        <asp:LinkButton ID="EditButton" runat="server">Edit</asp:LinkButton>
        <asp:LinkButton ID="DeleteButton" runat="server" OnCommand="DeleteButton_Command">Delete</asp:LinkButton></p>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" Runat="Server">
    <script src="<%= ResolveClientUrl("~/Scripts/delete-confirm.js") %>" type="text/javascript"></script>
</asp:Content>
