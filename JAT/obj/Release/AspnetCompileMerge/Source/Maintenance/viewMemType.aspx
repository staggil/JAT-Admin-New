<%@ Page Title="Member Type" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewMemType.aspx.cs" Inherits="JAT.Maintenance.viewMemType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .Grid td {
            padding: 5px
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
            width: 80%;
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
    <div class="box" style="background-color: lightgray;">
        <h3><%=Resources.Resources.member_type %></h3>
        <form>
            <div class="row space">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-3">
                            <label><%=Resources.Resources.member_status %></label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="col-sm-7">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Radio_Change" RepeatDirection="Horizontal">
                                <asp:ListItem Value="All" Selected="Ture" Text="<%$Resources:Resources, all %>"> ></asp:ListItem>
                                <asp:ListItem Value="Selection" Text="<%$Resources:Resources, selection %>">></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row space">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-3">
                            <label>&nbsp</label>
                        </div>
                        <div class="col-sm-7">
                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                RepeatColumns="3" OnSelectedIndexChanged="Checkbox_Change" Enabled="False">
                                <asp:ListItem Value="Member Type" Text="<%$Resources:Resources, member_type %>">></asp:ListItem>
                                <asp:ListItem Value="Description" Text="<%$Resources:Resources, description %>">></asp:ListItem>
                                <asp:ListItem Value="Newsletter" Text="<%$Resources:Resources, newsletter %>">></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <br />
    <br />

    <asp:DataGrid ID="DataGrid1" CssClass="Grid" runat="server" CellPadding="4" BackColor="White"
        BorderWidth="1px" BorderStyle="None" BorderColor="InactiveCaptionText" PageSize="20"
        AutoGenerateColumns="False" HorizontalAlign="Center">
        <AlternatingItemStyle BackColor="#F7F7F7"></AlternatingItemStyle>
        <ItemStyle ForeColor="#24227A" BackColor="White"></ItemStyle>
        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#24227A"></HeaderStyle>
        <Columns>
            <asp:BoundColumn DataField="memberType" HeaderText="<%$Resources:Resources,member_type %>">
                <HeaderStyle CssClass="tbHeader"></HeaderStyle>
                <ItemStyle HorizontalAlign="center"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="description" HeaderText="<%$Resources:Resources,description %>">
                <HeaderStyle CssClass="tbHeader"></HeaderStyle>
                <ItemStyle></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="newsletter" HeaderText="<%$Resources:Resources,news_letter %>">
                <HeaderStyle CssClass="tbHeader"></HeaderStyle>
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
        </Columns>
        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="tbBody" Mode="NumericPages"></PagerStyle>
    </asp:DataGrid>

</asp:Content>
