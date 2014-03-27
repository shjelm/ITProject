<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ITProject14.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <%-- Hämtar alla kunduppgifter som finns i tabellen Customer i databasen via affärslogikklassen Service och
         metoden GetCustomers, som i sin tur använder klassen CustomerDAL och metoden GetCustomers, som skapar en
         lista med referenser till Customer-objekt; ett Customer-objekt för varje post i tabellen. --%>
    <asp:ObjectDataSource ID="ServiceObjectDataSource" runat="server" SelectMethod="GetMembers"
        TypeName="ITProject14.App_Code.BLL.Service" />
    <h1>
        Posts</h1>
    <asp:ListView ID="CustomerListView" runat="server" DataSourceID="ServiceObjectDataSource">
        <LayoutTemplate>
            <%-- Platshållare för kunder --%>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            <%-- "Paging" --%>
            <div id="pager">
                <asp:DataPager ID="DataPager1" runat="server" PageSize="6" QueryStringField="page">
                    <Fields>
                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                            ShowPreviousPageButton="False" FirstPageText="First" />
                        <asp:NumericPagerField />
                        <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="False"
                            ShowPreviousPageButton="False" LastPageText="Last" />
                    </Fields>
                </asp:DataPager>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <dl class="customer-card">
                <dt>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("CustomerId", "~/Details.aspx?id={0}") %>'><%# Eval("Name") %></asp:HyperLink></dt>
                <dd>
                    <%# Eval("Username") %></dd>
                <dd>
                    <%# Eval("Mail") %>
                </dd>
            </dl>
        </ItemTemplate>
    </asp:ListView>
    <script type="text/javascript">
        var message = sprintf("ZZZZTa bort %s '%s' permanent?", "1234", "abcd");
        alert(message);
    </script>
</asp:Content>