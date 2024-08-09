<%@ Page Title="Send Type" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewSendType.aspx.cs" Inherits="JAT.Maintenance.viewSendType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .Grid td {
            padding: 5px
        }

        .Grid th {
            padding: 5px
        }

        body {
            font-size: 17px;
        }

        h3 {
            font-weight: bold;
        }
    </style>

    <br />
    <br />
    <br />
    <div style="padding: 0px 50px;">
        <h3><%=Resources.Resources.send_type_st %></h3>
    </div>

    <asp:DataGrid ID="DataGrid1" CssClass="Grid" AutoGenerateColumns="false" runat="server" CellPadding="4" BackColor="White" BorderWidth="1px"
        BorderStyle="None" BorderColor="InactiveCaptionText" PageSize="20" Width="40%" HorizontalAlign="Center">
        <FooterStyle ForeColor="Desktop" BackColor="#B5C7DE"></FooterStyle>
        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
        <AlternatingItemStyle BackColor="#F7F7F7"></AlternatingItemStyle>
        <ItemStyle ForeColor="#24227A" BackColor="White"></ItemStyle>
        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#24227A"></HeaderStyle>
        <Columns>
            <asp:BoundColumn DataField="sendID" HeaderText="Send Id">
                <HeaderStyle HorizontalAlign="center" Width="20%"></HeaderStyle>
                <ItemStyle></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="sendtype" HeaderText="<%$Resources:Resources,send_type_st %>">
                <HeaderStyle HorizontalAlign="center" Width="20%"></HeaderStyle>
                <ItemStyle></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="description" HeaderText="<%$Resources:Resources,description %>">
                <HeaderStyle HorizontalAlign="center" Width="60%"></HeaderStyle>
                <ItemStyle></ItemStyle>
            </asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>
