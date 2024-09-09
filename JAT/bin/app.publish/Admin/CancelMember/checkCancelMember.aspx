<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="checkCancelMember.aspx.cs" Inherits="JAT.Admin.CancelMember.checkCancelMember" %>
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
    <div class="box">

        <br />
        <div style="display: block; margin: auto; width: 95%; align-self: center; padding: 20px; background-color: lightgray; border-radius: 20px; box-shadow: 3px 3px 20px rgba(0, 0, 0, 0.1);">
            <h3 lang="eng"><%=Resources.Resources.cancel_member %></h3>
            <div class="row space">
                <div class="col-md-12">
                    <div class="row space">
                        <div class="col-md-12">
                            <div class="row ">
                                <div class="form-group col-md-3">
                                    <label style="color: red; visibility: hidden">*</label>
                                    <label lang="eng">First Member ID :</label>

                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-6">
                                    <b>
                                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>

                                    </b>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row space">
                        <div class="col-md-12">
                            <div class="row ">
                                <div class="form-group col-md-3">
                                    <label style="color: red; visibility: hidden">*</label>
                                    <label lang="eng"><%=Resources.Resources.first_member_name %></label>

                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-6">
                                    <b>
                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                        &nbsp;&nbsp;[<asp:Label ID="Label2" runat="server" Text=""></asp:Label>]
                                    </b>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row space">
                        <div class="form-group col-md-3">
                            <label style="color: red; visibility: hidden">*</label>
                            <label lang="eng"><%=Resources.Resources.mode %></label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="form-group col-md-3">
                            <asp:DropDownList ID="DropDownList1" runat="server" Enabled="false">
                                <asp:ListItem Selected="True">-- Select --</asp:ListItem>
                                <asp:ListItem Value="Cancel all member">Cancel all member</asp:ListItem>
                                <asp:ListItem Value="Cancel first member">Cancel first member</asp:ListItem>
                                <asp:ListItem Value="Cancel all family member">Cancel all family member</asp:ListItem>
                                <asp:ListItem Value="Cancel some family member">Cancel some family member</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">

                        <div class="text-center">
                            </br>
                        <b>
                            <asp:Label ID="Label4" runat="server" Text="All members have been cancelled."></asp:Label></b>
                            <b>
                                <asp:Label ID="Label5" runat="server" Text="All family members have been cancelled."></asp:Label></b>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <br />

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>

                <asp:BoundField HeaderText="<%$Resources:Resources,member_id %>" DataField="memberid" HeaderStyle-CssClass="text-center">
                    <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                    <ItemStyle Height="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="<%$Resources:Resources,name %>" DataField="nameJ" HeaderStyle-CssClass="text-center">
                    <HeaderStyle BackColor="#24227A" HorizontalAlign="Center" Height="10px" />
                    <ItemStyle Height="40px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="<%$Resources:Resources,name_eng %>" DataField="nameE" HeaderStyle-CssClass="text-center">
                    <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" Height="10px" />
                    <ItemStyle Height="40px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="<%$Resources:Resources,member_type %>" DataField="memberType" HeaderStyle-CssClass="text-center">
                    <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                    <ItemStyle Height="40px" HorizontalAlign="Center" Width="10%" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
        <br />
        <div class="form-group">
            <div class="text-center">
                <asp:Button ID="Button1" runat="server" Text="Approve/Process" type="submit" class="btn btn-success" Style="text-align: center;" OnClick="cancelMemberPressed" CommandArgument="approve"/>
                <asp:Button ID="Button2" runat="server" Text="Reject" type="submit" class="btn btn-danger" Style="text-align: center;" OnClick="cancelMemberPressed" CommandArgument="reject"/>
            </div>

        </div>
</asp:Content>
