<%@ Page Title="Staff" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewStaff.aspx.cs" Inherits="JAT.Maintenance.viewStaff" %>
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
</style>
<br />
<div style="padding:0px 50px;">
    <h3><%=Resources.Resources.staff %></h3>
</div>
<div class="form-group">
    <div class="text-right">
        <asp:Button ID="add" class="btn btn-primary" runat="server" OnClick="add_Click" Text="Add" />
    </div>
</div>
    <asp:datagrid id="DataGrid1" CssClass="Grid" AutoGenerateColumns="false" runat="server" CellPadding="4" BackColor="White" BorderWidth="1px"
		DataKeyField="staffID" 
		BorderStyle="None" BorderColor="InactiveCaptionText" PageSize="20" Width="100%">
		<FooterStyle ForeColor="Desktop" BackColor="#B5C7DE"></FooterStyle>
		<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="#F7F7F7"></AlternatingItemStyle>
		<ItemStyle ForeColor="#24227A" BackColor="White"></ItemStyle>
		<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#24227A"></HeaderStyle>
		<Columns>
			<asp:HyperLinkColumn  DataTextField="staffID" HeaderText="staffID" DataNavigateUrlField="staffID" DataNavigateUrlFormatString="viewStaffEntry.aspx?staffID={0}">
				<HeaderStyle HorizontalAlign="center"></HeaderStyle>
				<ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
			</asp:HyperLinkColumn>
			<asp:BoundColumn DataField="staffFName" HeaderText="<%$Resources:Resources,name %>">
				<HeaderStyle HorizontalAlign="center" Width="35%"></HeaderStyle>
				<ItemStyle></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="staffEmail"  HeaderText="<%$Resources:Resources,e_mail %>">
				<HeaderStyle HorizontalAlign="center" Width="25%"></HeaderStyle>
				<ItemStyle></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="branchNm" HeaderText="Branch">
				<HeaderStyle HorizontalAlign="center" Width="15%"></HeaderStyle>
				<ItemStyle></ItemStyle>
			</asp:BoundColumn>
			<asp:TemplateColumn  HeaderText="<%$Resources:Resources,change_password %>" HeaderStyle-HorizontalAlign="center">
                <HeaderStyle CssClass="tbHeader"></HeaderStyle>
				<ItemStyle CssClass="tBody" HorizontalAlign="center"></ItemStyle>
				<ItemTemplate>
					<a href='<%# "viewStaffPass.aspx?staffId="+ DataBinder.Eval(Container.DataItem, "staffId")%>' class="tBody">Change</a>
				</ItemTemplate>
            </asp:TemplateColumn>
		</Columns>
	</asp:datagrid>
</asp:Content>
