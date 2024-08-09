<%@ Page Title="Company Label" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportCMLabel.aspx.cs" Inherits="JAT.CompanyReport.reportCMLabel" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
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

        .mycheckbox input[type="checkbox"] {
            margin: 20px;
        }
    </style>

    <br />
    <br />
    <br />
    <div class="box" style="background-color: lightgray;">
        <h3><%=Resources.Resources.label%></h3>
        <form>
            <div class="row space">
                <div class="col-md-12">
                    <div class="row">
                        <div class="form-group col-md-3">
                            <label><%=Resources.Resources.company_id%></label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="form-group col-md-3">
                            <input type="text" id="txtCompanyId" runat="server" class="form-control" autocomplete="off">
                        </div>
                    </div>
                </div>
            </div>
            <div class="row space">
                <div class="col-md-12">
                    <div class="row">
                        <div class="form-group col-md-3">
                            <label><%=Resources.Resources.name%></label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="form-group col-md-5">
                            <input type="text" id="txtCompanyNameJpn" runat="server" class="form-control" autocomplete="off">
                        </div>
                    </div>
                </div>
            </div>
            <div class="row space">
                <div class="col-md-12">
                    <div class="row">
                        <div class="form-group col-md-3">
                            <label><%=Resources.Resources.name_eng%></label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="form-group col-md-5">
                            <input type="text" id="txtCompanyNameEng" runat="server" class="form-control" autocomplete="off">
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="text-center">
                    <asp:Button ID="cmdView" Text="View" OnClick="cmdView_Click" runat="server" class="btn btn-primary" />
                    <asp:Button ID="cmdReset" Text="Reset" OnClick="cmdReset_Click" runat="server" class="btn btn-primary" />
                </div>
            </div>
        </form>
    </div>
    <br />
    <div class="form-group">
        <div class="text-right">
            <asp:Button ID="cmdPrintAll" runat="server" Text="Print" class="btn btn-primary" OnClick="cmdPrintAll_Click" />
            <asp:Button ID="cmdPrintSel" runat="server" Text="Print Selected" class="btn btn-primary" OnClick="cmdPrintSel_Click" />
        </div>
    </div>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <br />
    <asp:GridView ID="DataGrid1" runat="server" AutoGenerateColumns="False" PageSize="20" Width="100%" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3">
        <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="mycheckbox" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="companyNmJ" HeaderText="<%$Resources:Resources,name_japanese%>" />
            <asp:BoundField DataField="companyNmE" HeaderText="<%$Resources:Resources,name_english%>" />
            <asp:BoundField DataField="represNmE" HeaderText="<%$Resources:Resources,represent_name_eng%>" />
            <asp:BoundField DataField="represPosition" HeaderText="<%$Resources:Resources,represent_position%>"  />
            <asp:BoundField DataField="address" HeaderText="<%$Resources:Resources,address%>" />
        </Columns>
        <PagerStyle BackColor="" ForeColor="DarkSlateBlue"
            HorizontalAlign="Right" />
    </asp:GridView>
    <div cellspacing="0" cellpadding="0" width="80%" align="center" border="0">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" DocumentMapCollapsed="True" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="1000px" Height="800px" ZoomMode="PageWidth">
            <LocalReport ReportPath="">
            </LocalReport>
        </rsweb:ReportViewer>
    </div>
</asp:Content>
