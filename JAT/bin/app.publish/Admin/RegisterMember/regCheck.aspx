<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="regCheck.aspx.cs" Inherits="JAT.Admin.RegisterMember.regCheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-control {
            height: 1.5em;
            padding: 7px 5px;
        }

        .Grid td {
            padding: 5px;
        }

        table {
            table-layout: fixed;
            width: auto;
        }

        td label {
            /*width: 350px;*/
            margin-right: 20px;
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
        <h3><%=Resources.Resources.register_information %></h3>
        <div class="row">
            <div class="form-group col-md-3">
                <label><%=Resources.Resources.register_status %></label>
                <span style="float: right;">:</span>
            </div>
            <div class="form-group col-md-3">
                <asp:DropDownList ID="DropDownList1" runat="server" >
                    <asp:ListItem Value="IN ('WT', 'AP','RJ')">-- Any --</asp:ListItem>
                    <asp:ListItem Selected="True" Value="= 'WT'">Waiting Admin</asp:ListItem>
                    <asp:ListItem Value="= 'AP'">Approved</asp:ListItem>
                    <asp:ListItem Value="= 'RJ'">Rejected</asp:ListItem>
                </asp:DropDownList>
            </div>

        </div>
        <div class="row">
            <div class="form-group col-md-3">
                <label><%=Resources.Resources.name_member_id %></label>
                <span style="float: right;">:</span>
            </div>
            <div class="form-group col-md-2">
                <input id="Text1" type="text" runat="server" autocomplete="off" />
                
            </div>

        </div>
        <div class="form-group">
            <div class="text-center">
                <asp:Button class="btn btn-primary" ID="Button1" runat="server" Text="View" OnClick="Button1_Click" />
            </div>
        </div>

    </div>
    <br />
    <asp:GridView ID="GridView1" CssClass="Grid" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" PageSize="20" Width="100%" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
        <AlternatingRowStyle BackColor="#DCDCDC" />

        <Columns>
          
            <asp:HyperLinkField
                DataNavigateUrlFields="register_id,register_type"
                DataNavigateUrlFormatString="regEntryCheck.aspx?registerid={0}&type={1}"
                DataTextField="register_id"
                HeaderText="Register No."
                SortExpression="register_id">
                <HeaderStyle HorizontalAlign="Center" Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Underline="true"></ItemStyle>
            </asp:HyperLinkField>
            <asp:BoundField DataField="regisdate" HeaderText="<%$Resources:Resources, register_date %>">
                <HeaderStyle HorizontalAlign="Center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="register_member" HeaderText="<%$Resources:Resources,name_member_id %>">
                <HeaderStyle HorizontalAlign="Center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
             <asp:BoundField DataField="register_approve" HeaderText="Approve Date">
                <HeaderStyle HorizontalAlign="Center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="register_status" HeaderText="<%$Resources:Resources, register_status %>">
                <HeaderStyle HorizontalAlign="Center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
           
        </Columns>
        <PagerStyle HorizontalAlign="Right" />
    </asp:GridView>
</asp:Content>