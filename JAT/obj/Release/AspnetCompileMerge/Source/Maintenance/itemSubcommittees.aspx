<%@ Page Title="Subcommittees Variable Item" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="itemSubcommittees.aspx.cs" Inherits="JAT.Maintenance.itemSubcommittees" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<style>
    .Grid td{
        padding:5px
    }
    .Grid th{
        padding:5px
    }
    body {
        font-size: 17px;
    }

    h3 {
        font-weight: bold;
    }
    .form-control {
        height: 1.5em;
        padding: 7px 5px;
    }

    .form-group {
        margin-bottom: 10px;
        padding-right: 0px
    }

    .box {
        display: block;
        margin: auto;
        width: 95%;
        align-self: center;
        padding: 20px;
        background-color: #ffffff;
        border-radius: 20px;
        box-shadow: 3px 3px 20px rgba(0, 0, 0, 0.1);
    }

    .space {
        margin-left: 20px;
    }
</style>
<br />
<br />
<br />
<div style="margin-left: auto; margin-right: auto; text-align: center;">
    <div runat="server" id="div_datagrid1">
        <asp:GridView ID="DataGrid1" runat="server" AutoGenerateColumns="false" CssClass="Grid" DataKeyNames="itemNm" HorizontalAlign="Center">
            <Columns>
                <asp:BoundField DataField="itemNo" HeaderText="NO." />
                <asp:BoundField DataField="itemVal" HeaderText="<%$Resources:Resources,event %>" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button class="btn btn-primary" ID="chngbtn" runat="server" Text="Change" OnClick="chngbtn_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle BackColor="" ForeColor="DarkSlateBlue"
                    HorizontalAlign="Right" />
        </asp:GridView>
    </div>
    <div runat="server" id="div_datagrid1_edit">
        <asp:GridView ID="DataGrid1_Edit" runat="server" AutoGenerateColumns="false" CssClass="Grid" DataKeyNames="itemNm" HorizontalAlign="Center">
            <Columns>
                <asp:BoundField DataField="itemVal" HeaderText="Current Item Name" />
                <asp:TemplateField>
                    <HeaderTemplate>
                        New Item Name
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="itemname_new" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle BackColor="" ForeColor="DarkSlateBlue"
                    HorizontalAlign="Right" />
        </asp:GridView>
        <br />
        <div>
            <asp:Button class="btn btn-primary" ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" OnClientClick="return fnConfirmSave();"/>
            <asp:Button class="btn btn-primary" ID="btn_cancel" runat="server" Text="Cancel" OnClick="btn_cancel_Click" />
        </div>
    </div>
</div>
<script type = "text/javascript">
    function fnConfirmSave() {
        return confirm("Do you want to save this item?");
    }

</script>
</asp:Content>
