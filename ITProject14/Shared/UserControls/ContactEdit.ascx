<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactEdit.ascx.cs" Inherits="ITProject14.Shared.UserControls.ContactEdit" %>

<fieldset id="contact">
    <legend>Kontaktuppgifter</legend>
    <%-- Datakälla som  används för att filtrera ut en kunds kontaktuppgifter. --%>
    <asp:ObjectDataSource ID="ContactDataSource" runat="server" SelectMethod="GetContactsByCustomerId"
        TypeName="ITProject14.App_Code.BLL.Service" OnSelected="ContactDataSource_Selected" DataObjectTypeName="Contact"
        DeleteMethod="DeleteContact" InsertMethod="SaveContact" UpdateMethod="SaveContact"
        OnUpdated="ContactDataSource_Updated" OnInserted="ContactDataSource_Inserted"
        OnInserting="ContactDataSource_Inserting" OnUpdating="ContactDataSource_Updating"
        OnDeleted="ContactDataSource_Deleted">
        <SelectParameters>
            <asp:QueryStringParameter Name="customerID" QueryStringField="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
  <%-- Datakälla som DropDownList-kontroller använder för att presentera kontakttyper.
    <asp:ObjectDataSource ID="ContactTypeDataSource" runat="server" SelectMethod="GetContactTypes"
        TypeName="Service" OnSelected="ContactTypeDataSource_Selected" />
    <asp:MultiView ID="ContactMultiView" runat="server" ActiveViewIndex="1">
        <asp:View ID="EditView" runat="server">--%>
            <%-- ListView som presenterar en kunds kontaktuppgifter. --%>
            <asp:ListView ID="ContactListView" runat="server" DataKeyNames="ContactId, CustomerId, ContactTypeId"
                DataSourceID="ContactDataSource" InsertItemPosition="LastItem" OnItemInserting="ContactListView_ItemInserting">
                <LayoutTemplate>
                    <asp:ValidationSummary ID="ContactValidationSummary" runat="server" HeaderText="<%$ Resources:Strings, Validation_Header %>"
                        CssClass="validation-summary-errors-icon" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="<%$ Resources:Strings, Validation_Header %>"
                        CssClass="validation-summary-errors-icon" ValidationGroup="vgContactInsert" />
                    <ul>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <p>
                        Kontaktuppgifter saknas.</p>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <li>
                        <asp:DropDownList ID="ContactTypeDropDownList" runat="server" DataSourceID="ContactTypeDataSource"
                            DataTextField="Name" DataValueField="ContactTypeId" Enabled="false" SelectedValue='<%# Bind("ContactTypeId") %>' />
                        <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# Bind("Value") %>' MaxLength="50"
                            Enabled="false" />
                        <%-- "Kommandknappar" för att redigera och ta bort en kunduppgift . Kommandonamnen är VIKTIGA! --%>
                        <asp:LinkButton ID="EditLinkButton" runat="server" CommandName="Edit" Text="Redigera"
                            CausesValidation="false" />
                        <%-- Unobtrusive JavaScript ersätter OnClientClick='<%# String.Format("return confirm(\"Ta bort kunduppgiften &rsquo;{0}&rsquo; permanent?\" )", Eval("Value")) %>' --%>
                        <asp:LinkButton ID="DeleteLinkButton" runat="server" CommandName="Delete" Text="Ta bort"
                            CausesValidation="false" CssClass="delete-action" data-type="<%$ Resources:Strings, Data_Type_Contact %>"
                            data-value='<%# Eval("Value") %>' />
                    </li>
                </ItemTemplate>
                <EditItemTemplate>
                    <li>
                        <asp:DropDownList ID="ContactTypeDropDownList" runat="server" DataSourceID="ContactTypeDataSource"
                            DataTextField="Name" DataValueField="ContactTypeId"  SelectedValue='<%# Bind("ContactTypeId") %>'/>
                        <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# Bind("Value") %>' MaxLength="50" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="<%$ Resources:Strings, Contact_Value_Required %>"
                            ControlToValidate="ValueTextBox" CssClass="field-validation-error" Display="Dynamic">*</asp:RequiredFieldValidator>
                        <%-- "Kommandknappar" för att uppdatera en kontaktuppgift och avbryta. Kommandonamnen är VIKTIGA! --%>
                        <asp:LinkButton ID="UpdateLinkButton" runat="server" CommandName="Update" Text="Spara" />
                        <asp:LinkButton ID="CancelLinkButton" runat="server" CommandName="Cancel" Text="Avbryt"
                            CausesValidation="false" />
                    </li>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <li>
                        <asp:DropDownList ID="ContactTypeDropDownList" runat="server" DataSourceID="ContactTypeDataSource"
                            DataTextField="Name" DataValueField="ContactTypeId"  SelectedValue='<%# Bind("ContactTypeId") %>' />
                        <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# Bind("Value") %>' MaxLength="50"
                            ValidationGroup="vgContactInsert" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="<%$ Resources:Strings, Contact_Value_Required %>"
                            ControlToValidate="ValueTextBox" CssClass="field-validation-error" ValidationGroup="vgContactInsert"
                            Display="Dynamic">*</asp:RequiredFieldValidator>
                        <%-- "Kommandoknappar" för att uppdatera en kontaktuppgift och avbryta. Kommandonamnen är VIKTIGA! --%>
                        <asp:LinkButton ID="UpdateLinkButton" runat="server" CommandName="Insert" Text="Spara"
                            ValidationGroup="vgContactInsert" />
                    </li>
                </InsertItemTemplate>
            </asp:ListView>
        </asp:View>
        <asp:View ID="ReadOnlyView" runat="server">
            <%-- ListView som presenterar en kunds kontaktuppgifter. --%>
            <asp:ListView ID="ContactReadOnlyListView" runat="server" DataKeyNames="ContactId, CustomerId, ContactTypeId"
                DataSourceID="ContactDataSource" 
                OnItemDataBound="ContactReadOnlyListView_ItemDataBound" 
                onselectedindexchanged="ContactReadOnlyListView_SelectedIndexChanged">
                <LayoutTemplate>
                    <ul>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </ul>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <p>
                        Kontaktuppgifter saknas.</p>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <li>
                        <%-- Label-kontrollen uppgift är att visa vilken typ kontaktinformationen är av. --%>
                        <asp:Label ID="ContactTypeNameLabel" runat="server" /><%# Eval("Value") %></li>
                </ItemTemplate>
            </asp:ListView>
        </asp:View>
    </asp:MultiView>
</fieldset>