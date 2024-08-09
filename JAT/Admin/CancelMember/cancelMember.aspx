<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cancelMember.aspx.cs" Inherits="JAT.Admin.CancelMember.cancelMember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <style>
        .form-control {
            height: 1.5em;
            padding: 7px 5px;
        }
        .Grid td {
            padding: 5px;
        }
        table{
            table-layout: fixed;
            width: auto;
        }
        td label  {
            /*width: 350px;*/
            margin-right:20px;
        }

        body {
            font-size: 17px;
        }

        h3 {
            font-weight: bold;
        }

        .form-group {
            margin-bottom: 10px;
            padding-right: 0px;
            top: -1px;
            left: 20px;
        }

        .box {
            display: block;
            margin: auto;
            /*width: 95%;*/
            align-self: center;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 20px;
            box-shadow: 3px 3px 20px rgba(0, 0, 0, 0.1);
        }
    </style>

    <br />
    <br />
    <br />
    <div style="display: block; margin: auto; width: 95%; align-self: center; padding: 20px; background-color: lightgray; border-radius: 20px; box-shadow: 3px 3px 20px rgba(0, 0, 0, 0.1);">
        <h3><%=Resources.Resources.cancel_member %></h3>
        <div class="row">
            <div class="form-group col-md-3">
                <label><%=Resources.Resources.register_status %></label>
                <span style="float: right;">:</span>
            </div>
            <div class="form-group col-md-2">
                <asp:DropDownList ID="DropDownList1" runat="server" >
                    <asp:ListItem Value="IN ('WT', 'AP','RJ')">-- Any --</asp:ListItem>
                    <asp:ListItem Selected="True" Value="= 'WT'">Waiting Admin</asp:ListItem>
                    <asp:ListItem Value="= 'AP'">Approved</asp:ListItem>
                    <asp:ListItem Value="= 'RJ'">Rejected</asp:ListItem>
                </asp:DropDownList>
            </div>

        </div>

        <div class="form-group">
            <div class="text-center">
                <asp:Button class="btn btn-primary" ID="Button1" runat="server" Text="View" onClick="Button1_Click"/>
            </div>
        </div>
    </div>
    <br />
      <asp:GridView ID="GridView1" CssClass="Grid" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" PageSize="20" Width="100%" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
        <AlternatingRowStyle BackColor="#DCDCDC" />

        <Columns>
            <asp:HyperLinkField
                DataNavigateUrlFields="cancel_id,cancel_member"
                DataNavigateUrlFormatString="checkCancelMember.aspx?cancelid={0}&firstmemberid={1}"
                DataTextField="cancel_id"
                HeaderText="Cancel No."
                SortExpression="cancel_id">
                <HeaderStyle HorizontalAlign="Center" Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Underline="true"></ItemStyle>
            </asp:HyperLinkField>
            <asp:BoundField DataField="canceldate" HeaderText="Cancel Date">
                <HeaderStyle HorizontalAlign="Center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="cancel_member" HeaderText="Member">
                <HeaderStyle HorizontalAlign="Center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
              <asp:BoundField DataField="cancel_type" HeaderText="Cancel Type">
                <HeaderStyle HorizontalAlign="Center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
              <asp:BoundField DataField="cancel_status" HeaderText="Status">
                <HeaderStyle HorizontalAlign="Center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
              <asp:BoundField DataField="staff_id" HeaderText="Staff">
                <HeaderStyle HorizontalAlign="Center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="staff_date" HeaderText="Date Cancel">
                <HeaderStyle HorizontalAlign="Center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
        </Columns>
        <PagerStyle HorizontalAlign="Right" />
    </asp:GridView>
</asp:Content>
