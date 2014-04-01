<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostEdit.ascx.cs" Inherits="ITProject14.Shared.UserControls.PostEdit" %>

<asp:ValidationSummary ID="PostValidationSummary" runat="server" HeaderText="<%$ Resources:Strings, Validation_Header %>"
    CssClass="validation-summary-errors-icon" ValidationGroup="vgPost" />
<%-- value --%>
<div class="editor-label">
    <asp:Label ID="Label1" runat="server" Text="Post:" />
</div>
<div class="editor-field">
    <asp:TextBox ID="ValueTextBox" runat="server" MaxLength="500" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:Strings, Post_Value_Required %>"
        Display="Dynamic" Text="*" ControlToValidate="ValueTextBox" SetFocusOnError="True"
        CssClass="field-validation-error" ValidationGroup="vgPost" /></div>
<%-- Kontaktuppgifter --%>
    <%--<my:CommentEdit ID="CommentEdit1" runat="server" Visible="false" ReadOnly="false" />--%>
<%-- Kommandoknapp för att spara kunduppgift. --%>
<p>
    <asp:LinkButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" ValidationGroup="vgPost">Save</asp:LinkButton>
    <asp:LinkButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="false">Cancel</asp:LinkButton>
</p>