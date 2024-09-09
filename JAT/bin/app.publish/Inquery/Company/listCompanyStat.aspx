<%@ Page Title="Statistic Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="listCompanyStat.aspx.cs" Inherits="JAT.Inquery.Company.listCompanyStat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<style>
    .Grid td{
        padding:3px
    }
    .Grid th{
        padding:3px
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
    <h3><%=Resources.Resources.view_company_statistic %></h3>
    <form>
        <div class="row space">
            <div class="col-md-3 form-group">
                <label><%=Resources.Resources.view_type %></label>
                <span style="float:right;">:</span>
            </div>
            <div class="col-md-4 form-group">
                <asp:RadioButtonList id="rblViewType" runat="server" 
					RepeatDirection=Vertical>
					<asp:ListItem Value="1" Selected=true Text="<%$Resources:Resources, total_fee %>"></asp:ListItem>
					<asp:ListItem Value="2" Text="<%$Resources:Resources, news_fee %>"></asp:ListItem>
					<asp:ListItem Value="3" Text="<%$Resources:Resources, member_fee %>"></asp:ListItem>
				</asp:RadioButtonList>
            </div>

        </div>
        <div class="row space">
            <div class="form-group col-md-3">
                <label><%=Resources.Resources.member_status %></label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-sm-2">
                <select id="cboStatus" runat="server">
                    <option value="ALL" Text="<%$Resources:Resources, all %>" selected></option>
                    <option value="A">A</option>
                    <option value="NA">NA</option>
                </select>
            </div>
        </div>
        <div class="form-group">
            <div class="text-center">
                <%--<button type="submit" class="btn btn-primary">View</button>--%>
                <asp:Button ID="view" runat="server" OnClick="view_Click" Text="View" />
            </div>
        </div>
    </form>
</div>
<br />
<br />
    <asp:GridView ID="grdCompany" CssClass="Grid" runat="server" AllowSorting="True" AutoGenerateColumns="False"
	    BackColor="White" BorderColor="InactiveCaptionText" BorderStyle="None" BorderWidth="2px" CellPadding="3"
	    CellSpacing="1" GridLines="None" SelectedIndex="0" PageSize="20" HorizontalAlign="Center" >
        <HeaderStyle HorizontalAlign="Center" Height="10px" BackColor="#24227A" ForeColor="White" />
        <Columns>
            <asp:BoundField HeaderText="<%$Resources:Resources,fee_id%>" ReadOnly="True" DataField="TotalPerMonth" HeaderStyle-Width="20%"/>
            <asp:BoundField HeaderText="Number Of Persons" ReadOnly="True" DataField="NumberOfPersons"  HeaderStyle-Width="30%" />
            <asp:BoundField HeaderText="<%$Resources:Resources,member_fee%>" ReadOnly="True" DataField="MemberFee" HeaderStyle-Width="25%" />
            <asp:BoundField HeaderText="Total Member Fee" ReadOnly="True" DataField="TotalMemberFee" HeaderStyle-Width="20%"/>
            <asp:BoundField HeaderText="<%$Resources:Resources,news_fee%>" ReadOnly="True" DataField="NewsFee" HeaderStyle-Width="20%"/>
            <asp:BoundField HeaderText="Total News Fee" ReadOnly="True" DataField="TotalNewsFee" HeaderStyle-Width="20%"/>
            <asp:BoundField HeaderText="<%$Resources:Resources,total_fee%>" ReadOnly="True" DataField="totalfee" HeaderStyle-Width="20%"/>
        </Columns>
        <RowStyle BackColor="#DEDFDE" ForeColor="Black"/>
        
    </asp:GridView>
</asp:Content>
