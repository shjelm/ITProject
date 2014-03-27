<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberEdit.ascx.cs" Inherits="ITProject14.Shared.UserControls.MemberEdit" %>

<%@ Register Src="ContactEdit.ascx" TagName="ContactEdit" TagPrefix="my" %>
<asp:ValidationSummary ID="CustomerValidationSummary" runat="server" HeaderText="<%$ Resources:Strings, Validation_Header %>"
    CssClass="validation-summary-errors-icon" ValidationGroup="vgContact" />
<%-- Namn --%>
<div class="editor-label">
    <asp:Label ID="Label1" runat="server" Text="Namn:" />
</div>
<div class="editor-field">
    <asp:TextBox ID="NameTextBox" runat="server" MaxLength="30" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:Strings, Customer_Name_Required %>"
        Display="Dynamic" Text="*" ControlToValidate="NameTextBox" SetFocusOnError="True"
        CssClass="field-validation-error" ValidationGroup="vgContact" /></div>
<%-- Address --%>
<div class="editor-label">
    <asp:Label ID="Label3" runat="server" Text="Adress:" />
</div>
<div class="editor-field">
    <asp:TextBox ID="AddressTextBox" runat="server" MaxLength="30" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Resources:Strings, Customer_Address_Required %>"
        Display="Dynamic" Text="*" ControlToValidate="AddressTextBox" SetFocusOnError="True"
        CssClass="field-validation-error" ValidationGroup="vgContact" /></div>
<%-- Postnummer --%>
<div class="editor-label">
    <asp:Label ID="Label5" runat="server" Text="Postnummer:" />
</div>
<div class="editor-field">
    <asp:TextBox ID="PostalCodeTextBox" runat="server" MaxLength="6" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$ Resources:Strings, Customer_PostalCode_Required %>"
        Display="Dynamic" Text="*" ControlToValidate="PostalCodeTextBox" SetFocusOnError="True"
        CssClass="field-validation-error" ValidationGroup="vgContact" />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Resources:Strings, Customer_PostalCode_Invalid_Format %>"
        Display="Dynamic" Text="*" ControlToValidate="PostalCodeTextBox" SetFocusOnError="True"
        ValidationExpression="<%$ Resources:Strings, Regular_Expression_PostalCode %>"
        CssClass="field-validation-error" ValidationGroup="vgContact" /></div>
<%-- Ort --%>
<div class="editor-label">
    <asp:Label ID="Label7" runat="server" Text="Ort:" />
</div>
<div class="editor-field">
    <asp:TextBox ID="CityTextBox" runat="server" MaxLength="30" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="<%$ Resources:Strings, Customer_City_Required %>"
        Display="Dynamic" Text="*" ControlToValidate="CityTextBox" SetFocusOnError="True"
        CssClass="field-validation-error" ValidationGroup="vgContact" />
</div>
<%-- Kontaktuppgifter --%>
    <my:ContactEdit ID="ContactEdit1" runat="server" Visible="false" ReadOnly="false" />
<%-- Kommandoknapp för att spara kunduppgift. --%>
<p>
    <asp:LinkButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" ValidationGroup="vgContact">Spara</asp:LinkButton>
    <asp:LinkButton ID="CancelButton" runat="server" OnClick="CancelButton_Click" CausesValidation="false">Avbryt</asp:LinkButton>
</p>