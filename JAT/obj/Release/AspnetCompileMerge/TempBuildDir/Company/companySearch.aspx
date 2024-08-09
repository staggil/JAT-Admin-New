<%@ Page Title="Company Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="companySearch.aspx.cs" Inherits="JAT.Company.companySearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link href="~/Content/bootstrap.css" rel="stylesheet" />

    <link href="https://cdn.datatables.net/1.10.15/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/responsive/2.1.1/css/responsive.bootstrap.min.css" rel="stylesheet" />

    <script src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.15/js/dataTables.bootstrap4.min.js "></script>
    <style type="text/css">
        .Grid td {
            padding: 10px
        }

        .Grid th {
            text-align: center;
        }

        input[type=text] {
            /*height:100px;*/
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
    <div style="display: block; margin: auto; width: 95%; align-self: center; padding: 20px; background-color: lightgray; border-radius: 20px; box-shadow: 3px 3px 20px rgba(0, 0, 0, 0.1);">
        <h3><%=Resources.Resources.retrieve_information %></h3>

        <%--@* ************************************************* 1 ***************************************************** *@--%>
        <div class="row space">
            <div class="form-group col-sm-4 col-md-3 col-lg-3">
                <label for="inputState"><%=Resources.Resources.member_status %></label>
                <span style="float: right;">:</span>
            </div>
            <div class="form-group col-sm-5 col-md-4 col-lg-3">
                <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem Value="IN ('A', 'NA')">-- Any --</asp:ListItem>
                    <asp:ListItem Selected="True" Value="= 'A'">A</asp:ListItem>
                    <asp:ListItem Value="= 'NA'">NA</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row space">
            <div class="col-md-12">
                <div class="row">
                    <div class="form-group col-sm-4 col-md-3 col-lg-3">
                        <label><%=Resources.Resources.company_name %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-sm-5 col-md-4 col-lg-3">
                        <asp:TextBox ID="companyNmJ" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="row ">
            <div class="col-md-12">
                <div class="row space">
                    <div class="form-group col-sm-4 col-md-3 col-lg-3">
                        <label for="inputState"><%=Resources.Resources.name_eng %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-sm-5 col-md-4 col-lg-3">
                        <asp:TextBox ID="companyNmE" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="text-center">
                <asp:Button ID="Button1" class="btn btn-primary" OnClick="Button1_Click" runat="server" Text="View" />
            </div>
        </div>

    </div>
    <br />
    <br />

    <br />
    <asp:GridView ID="GridView1" CssClass="Grid" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" PageSize="20" Width="100%" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
        <AlternatingRowStyle BackColor="#DCDCDC" />

        <Columns>
            <asp:HyperLinkField
                DataNavigateUrlFields="companyId"
                DataNavigateUrlFormatString="companyEntry.aspx?companyId={0}"
                DataTextField="companyId"
                HeaderText="ID"
                SortExpression="companyId">
                <HeaderStyle HorizontalAlign="Center" Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Underline="true"></ItemStyle>
            </asp:HyperLinkField>
            <asp:BoundField DataField="companyNmJ" HeaderText="<%$Resources:Resources, name_japanese %>">
                <HeaderStyle HorizontalAlign="Center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="companyNmE" HeaderText="<%$Resources:Resources, name_english %>">
                <HeaderStyle HorizontalAlign="Center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
        </Columns>
        <PagerStyle HorizontalAlign="Right" />
    </asp:GridView>
    <br />

</asp:Content>
