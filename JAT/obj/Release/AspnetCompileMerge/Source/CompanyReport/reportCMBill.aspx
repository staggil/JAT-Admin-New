<%@ Page Title="Company Bill" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportCMBill.aspx.cs" Inherits="JAT.CompanyReport.reportCMBill" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
    body {
        font-size: 17px;
    }
    .Grid td {
            padding: 5px;
        }

    input#datepicker1Input, input#datepicker2Input {
        width: inherit;
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
<div class="box" style="background-color:lightgray;">
    <h3><%=Resources.Resources.bill%></h3>
    <form>
        <%--@* ************************************************* 1 ***************************************************** *@--%>
        <div class="row space">
            <div class="form-group col-md-3">
                <label><%=Resources.Resources.report_type%></label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-md-2">
                <asp:dropdownlist id="drplstType" runat="server" CssClass="listfr">
					<asp:ListItem>Accrued</asp:ListItem>
					<asp:ListItem>Deferred</asp:ListItem>
				</asp:dropdownlist>
            </div>
        </div>
        <div class="row space">
            <div class="form-group col-md-3">
                <label><%=Resources.Resources.payment_method%></label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-md-2">
                <asp:dropdownlist id="cboPayMethod" runat="server" CssClass="listfr">
					<asp:ListItem>J</asp:ListItem>
					<asp:ListItem>K</asp:ListItem>
					<asp:ListItem>P</asp:ListItem>
					<asp:ListItem>S</asp:ListItem>
					<asp:ListItem>T</asp:ListItem>
					<asp:ListItem>B</asp:ListItem>
				</asp:dropdownlist>
            </div>
        </div>
        <div class="row space">
            <div class="col-md-12">
                <div class="row">
                    <div class="form-group col-md-3">
                        <label><%=Resources.Resources.expired_date_from%></label>
                        <span style="float:right;">:</span>
                    </div>
                    <div class="form-group col-md-5">
                        <input type="text" id="txtFromDate" style="width: inherit;" autocomplete="off" placeholder="dd/mm/yyyy" runat="server" readonly="readonly">
                        <span id="datepicker1" class="glyphicon glyphicon-calendar"></span>
                    </div>
                </div>
            </div>
        </div>
        <div class="row space">
            <div class="col-md-12">
                <div class="row">
                    <div class="form-group col-md-3">
                        <label><%=Resources.Resources.to%></label>
                        <span style="float:right;">:</span>
                    </div>
                    <div class="form-group col-md-5">
                        <input type="text" id="txtToDate" style="width: inherit;" autocomplete="off" placeholder="dd/mm/yyyy" runat="server" runat="server" readonly="readonly">
                        <span id="datepicker2" class="glyphicon glyphicon-calendar"></span>
                    </div>
                </div>
            </div>
        </div>
        <div class="row space">
            <div class="col-md-12">
                <div class="row">
                    <div class="form-group col-md-3">
                        <label><%=Resources.Resources.member_id%></label>
                        <span style="float:right;">:</span>
                    </div>
                    <div class="form-group col-md-3">
                        <input type="text" id="txtCompanyId" runat="server" class="form-control" autocomplete="off" >
                    </div>
                </div>
            </div>
        </div>
        <div class="row space">
            <div class="col-md-12">
                <div class="row">
                    <div class="form-group col-md-3">
                        <label><%=Resources.Resources.name%></label>
                        <span style="float:right;">:</span>
                    </div>
                    <div class="form-group col-md-5">
                        <input type="text" id="txtCompanyNameJpn" runat="server" class="form-control" autocomplete="off" >
                    </div>
                </div>
            </div>
        </div>
        <div class="row space">
            <div class="col-md-12">
                <div class="row">
                    <div class="form-group col-md-3">
                        <label><%=Resources.Resources.name_eng%></label>
                        <span style="float:right;">:</span>
                    </div>
                    <div class="form-group col-md-5">
                        <input type="text" id="txtCompanyNameEng" runat="server" class="form-control" autocomplete="off">
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="text-center">
                <asp:Button ID="cmdView" Text="View" OnClick="cmdView_Click" runat="server" class="btn btn-primary"/>
                <asp:Button ID="cmdReset" Text="Reset" OnClick="cmdReset_Click" runat="server" class="btn btn-primary" />
            </div>
        </div>
    </form>
</div>
<br />
<div class="form-group">
    <div class="text-right">
        <asp:Button ID="cmdPrintAll" runat="server" Text="PrintAll" OnClick="cmdPrintAll_Click" class="btn btn-primary"/>
        <asp:Button ID="cmdPrintSel" runat="server" Text="PrintSelected" class="btn btn-primary" OnClick="cmdPrintSel_Click"/>
    </div>
</div>
<asp:GridView ID="DataGrid1" runat="server" AutoGenerateColumns="False" PageSize="20" Width="100%" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3">
    <%--<AlternatingRowStyle BackColor="#DCDCDC" />--%>    
    <HeaderStyle CssClass="GridViewRowHeader" />
    
    <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />
                </ItemTemplate>
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:TemplateField>
            <asp:BoundField DataField="companyNmJ" HeaderText="<%$Resources:Resources,name_japanese%>">
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="companyNmE" HeaderText="<%$Resources:Resources,name_english%>" >
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="expiredDate" HeaderText="<%$Resources:Resources,expired_date%>" DataFormatString="{0:dd/MM/yyyy}">
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
        <asp:BoundField DataField="startDate" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}">
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
        <asp:BoundField DataField="termDate" HeaderText="Term Date" DataFormatString="{0:dd/MM/yyyy}">
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
        <asp:BoundField DataField="noPayMonth" HeaderText="No. Pay Month">
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
        <asp:BoundField DataField="total" HeaderText="<%$Resources:Resources,total%>" DataFormatString = "{0:N2}">
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White"/>
                <ItemStyle Height="40px" />
            </asp:BoundField>
        <asp:BoundField DataField="address" HeaderText="<%$Resources:Resources,address%>">
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
        <asp:BoundField DataField="fax" HeaderText="<%$Resources:Resources,fax%>">
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField><asp:BoundField DataField="phone" HeaderText="<%$Resources:Resources,phone%>">
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
        <asp:BoundField DataField="contNm" HeaderText="<%$Resources:Resources,contract_name%>">
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
        <asp:BoundField DataField="contPosition" HeaderText="<%$Resources:Resources,position%>">
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
            <%--<asp:BoundField DataField="expiredDate" HeaderText="<%$Resources:Resources,expired_date%>" />
            <asp:BoundField DataField="startDate" HeaderText="Start Date" />
            <asp:BoundField DataField="termDate" HeaderText="Term Date" />
            <asp:BoundField DataField="noPayMonth" HeaderText="No. Pay Month" />
            <asp:BoundField DataField="total" HeaderText="Total" />
            <asp:BoundField DataField="address" HeaderText="<%$Resources:Resources,address%>" />
            <asp:BoundField DataField="fax" HeaderText="<%$Resources:Resources,fax%>" />
            <asp:BoundField DataField="phone" HeaderText="Phone" />
            <asp:BoundField DataField="contNm" HeaderText="Contract Name" />
            <asp:BoundField DataField="contPosition" HeaderText="<%$Resources:Resources,position%>" />--%>
        </Columns>
        <PagerStyle BackColor="" ForeColor="DarkSlateBlue"
                HorizontalAlign="Right" />
    </asp:GridView>
    <div cellspacing="0" cellpadding="0" width="80%" align="center" border="0">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" DocumentMapCollapsed="True" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="1000px" Height="800px" ZoomMode="PageWidth">
                <localreport reportpath="">
                </localreport>
                </rsweb:ReportViewer>
    </div>

<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <%--@* *********** Datepicker *********** *@--%>
    <%--<script>
        var options = {
            format: 'dd-M-yyyy',
            todayHighlight: true,
            autoSize: true,
            autoclose: true,

        }

        $(function () {
            $("#datepicker1").click(function () {
                $('#datepicker1Input').datepicker(options).datepicker("show")
            });

            $("#datepicker2").click(function () {
                $('#datepicker2Input').datepicker(options).datepicker("show")
            });
        });
    </script>--%>
    <script type="text/javascript">
        $(function () {
            var options = {
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                autoclose: true
            }
            $("[id*=txtFromDate],#datepicker1").click(function () {
                $('[id*=txtFromDate]').datepicker(options).datepicker("show")
            });

            $("[id*=txtToDate],#datepicker2").click(function () {
                $('[id*=txtToDate]').datepicker(options).datepicker("show")
            });
        });
    </script>
    <%--@* *********** /Datepicker *********** *@--%>
</asp:Content>
