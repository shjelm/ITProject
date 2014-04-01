<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommentEdit.ascx.cs" Inherits="ITProject14.Shared.UserControls.CommentEdit" %>

<fieldset id="contact">
    <legend>Post</legend>
    <%-- Datakälla som  används för att filtrera ut en kunds kontaktuppgifter. --%>
    <asp:ObjectDataSource ID="PostDataSource" runat="server" SelectMethod="GetCommentsByPostId"
        TypeName="ITProject14.App_Code.BLL.Service" OnSelected="PostDataSource_Selected" DataObjectTypeName="Post"
        DeleteMethod="DeletePost" InsertMethod="SavePost" UpdateMethod="SavePost"
        OnUpdated="PostDataSource_Updated" OnInserted="PostDataSource_Inserted"
        OnInserting="PostDataSource_Inserting" OnUpdating="PostDataSource_Updating"
        OnDeleted="PostDataSource_Deleted">
        <SelectParameters>
            <asp:QueryStringParameter Name="postID" QueryStringField="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
  <%-- Datakälla som DropDownList-kontroller använder för att presentera kontakttyper.
    <asp:ObjectDataSource ID="PostTypeDataSource" runat="server" SelectMethod="GetPostTypes"
        TypeName="Service" OnSelected="PostTypeDataSource_Selected" />
    <asp:MultiView ID="PostMultiView" runat="server" ActiveViewIndex="1">
        <asp:View ID="EditView" runat="server">--%>
            <%-- ListView som presenterar en kunds kontaktuppgifter. --%>
            <asp:ListView ID="PostListView" runat="server" DataKeyNames="PostId, MemberId, PostTypeId"
                DataSourceID="PostDataSource" InsertItemPosition="LastItem" OnItemInserting="PostListView_ItemInserting">
                <LayoutTemplate>
                    <asp:ValidationSummary ID="CommentValidationSummary" runat="server" HeaderText="<%$ Resources:Strings, Validation_Header %>"
                        CssClass="validation-summary-errors-icon" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="<%$ Resources:Strings, Validation_Header %>"
                        CssClass="validation-summary-errors-icon" ValidationGroup="vgCommentInsert" />
                    <ul>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <p>
                        No comments.</p>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <li>
                        <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# Bind("Value") %>' MaxLength="50"
                            Enabled="false" />
                        <%-- "Kommandknappar" för att redigera och ta bort en kunduppgift . Kommandonamnen är VIKTIGA! --%>
                        <asp:LinkButton ID="EditLinkButton" runat="server" CommandName="Edit" Text="Redigera"
                            CausesValidation="false" />
                        <%-- Unobtrusive JavaScript ersätter OnClientClick='<%# String.Format("return confirm(\"Ta bort kunduppgiften &rsquo;{0}&rsquo; permanent?\" )", Eval("Value")) %>' --%>
                        <asp:LinkButton ID="DeleteLinkButton" runat="server" CommandName="Delete" Text="Ta bort"
                            CausesValidation="false" CssClass="delete-action" data-type="<%$ Resources:Strings, Data_Type_Post %>"
                            data-value='<%# Eval("Value") %>' />
                    </li>
                </ItemTemplate>
                <EditItemTemplate>
                    <li>
                        <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# Bind("Value") %>' MaxLength="50" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="<%$ Resources:Strings, Post_Value_Required %>"
                            ControlToValidate="ValueTextBox" CssClass="field-validation-error" Display="Dynamic">*</asp:RequiredFieldValidator>
                        <%-- "Kommandknappar" för att uppdatera en kontaktuppgift och avbryta. Kommandonamnen är VIKTIGA! --%>
                        <asp:LinkButton ID="UpdateLinkButton" runat="server" CommandName="Update" Text="Spara" />
                        <asp:LinkButton ID="CancelLinkButton" runat="server" CommandName="Cancel" Text="Avbryt"
                            CausesValidation="false" />
                    </li>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <li>
                        <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# Bind("Value") %>' MaxLength="50"
                            ValidationGroup="vgPostInsert" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="<%$ Resources:Strings, Post_Value_Required %>"
                            ControlToValidate="ValueTextBox" CssClass="field-validation-error" ValidationGroup="vgPostInsert"
                            Display="Dynamic">*</asp:RequiredFieldValidator>
                        <%-- "Kommandoknappar" för att uppdatera en kontaktuppgift och avbryta. Kommandonamnen är VIKTIGA! --%>
                        <asp:LinkButton ID="UpdateLinkButton" runat="server" CommandName="Insert" Text="Spara"
                            ValidationGroup="vgPostInsert" />
                    </li>
                </InsertItemTemplate>
            </asp:ListView>
        </asp:View>
        <asp:View ID="ReadOnlyView" runat="server">
            <%-- ListView som presenterar en kunds kontaktuppgifter. --%>
            <asp:ListView ID="PostReadOnlyListView" runat="server" DataKeyNames="PostId, MemberId, PostTypeId"
                DataSourceID="PostDataSource" 
                onselectedindexchanged="PostReadOnlyListView_SelectedIndexChanged">
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
                        <asp:Label ID="PostTypeNameLabel" runat="server" /><%# Eval("Value") %></li>
                </ItemTemplate>
            </asp:ListView>
        </asp:View>
    </asp:MultiView>
</fieldset>