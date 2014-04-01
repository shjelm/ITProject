<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberEdit.ascx.cs" Inherits="ITProject14.Shared.UserControls.MemberEdit" %>

<asp:ValidationSummary ID="MemberValidationSummary" runat="server" HeaderText="<%$ Resources:Strings, Validation_Header %>"
    CssClass="validation-summary-errors-icon" ValidationGroup="vgPost" />
<%-- Namn --%>
<div class="editor-label">
    <asp:Label ID="Label1" runat="server" Text="Namn:" />
</div>
<div class="editor-field">
    <asp:TextBox ID="NameTextBox" runat="server" MaxLength="30" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:Strings, Member_Name_Required %>"
        Display="Dynamic" Text="*" ControlToValidate="NameTextBox" SetFocusOnError="True"
        CssClass="field-validation-error" ValidationGroup="vgPost" /></div>
<%-- Username --%>
<div class="editor-label">
    <asp:Label ID="Label3" runat="server" Text="Adress:" />
</div>
<div class="editor-field">
    <asp:TextBox ID="UsernameTextBox" runat="server" MaxLength="30" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Resources:Strings, Member_Mail_Required %>"
        Display="Dynamic" Text="*" ControlToValidate="UsernameTextBox" SetFocusOnError="True"
        CssClass="field-validation-error" ValidationGroup="vgPost" /></div>
<%-- Postnummer --%>
<div class="editor-label">
    <asp:Label ID="Label5" runat="server" Text="Postnummer:" />
</div>
<div class="editor-field">
    <asp:TextBox ID="MailTextBox" runat="server" MaxLength="6" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Resources:Strings, Member_Mail_Required %>"
        Display="Dynamic" Text="*" ControlToValidate="MailTextBox" SetFocusOnError="True"
        CssClass="field-validation-error" ValidationGroup="vgPost" />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Resources:Strings, Member_Mail_Invalid_Format %>"
        Display="Dynamic" Text="*" ControlToValidate="MailTextBox" SetFocusOnError="True"
        ValidationExpression="<%$ Resources:Strings, Regular_Expression_Mail %>"
        CssClass="field-validation-error" ValidationGroup="vgPost" /></div>
<%-- Ort --%>
<div class="editor-label">
    <asp:Label ID="Label7" runat="server" Text="Ort:" />
</div>
<div class="editor-field">
    <asp:TextBox ID="PasswordTextBox" runat="server" MaxLength="30" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="<%$ Resources:Strings, Member_Password_Required %>"
        Display="Dynamic" Text="*" ControlToValidate="PasswordTextBox" SetFocusOnError="True"
        CssClass="field-validation-error" ValidationGroup="vgPost" />
</div>
<%-- Kontaktuppgifter --%>
    <my:PostEdit ID="PostEdit1" runat="server" Visible="false" ReadOnly="false" />
<%-- Kommandoknapp för att spara kunduppgift. --%>
<p>
    <asp:LinkButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" ValidationGroup="vgPost">Spara</asp:LinkButton>
    <asp:LinkButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="false">Avbryt</asp:LinkButton>
</p>