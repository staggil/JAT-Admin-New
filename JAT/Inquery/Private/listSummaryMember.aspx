<%@ Page Title="Summary Member" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="listSummaryMember.aspx.cs" Inherits="JAT.Inquery.Private.listSummaryMember" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .Grid td{
        padding:5px
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
<div class="box" style="background-color:lightgray;">
    <h3><%=Resources.Resources.summary_member %></h3>
        <div class="row space">
            <div class="form-group">
                <div class="col-sm-4">
                    <asp:RadioButtonList ID="PrintType" runat="server"  CssClass="txtLabel" AutoPostBack="true" OnSelectedIndexChanged="PrintType_SelectedIndexChanged" RepeatDirection="vertical">
                        <asp:ListItem Selected="True" Value="0"  Text="<%$Resources:Resources, all_expired_date %>"></asp:ListItem>
                        <asp:ListItem Value="1"  Text="<%$Resources:Resources, assign_expired_date %>"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
        </div>
        <div class="row space dbl">
            <div class="form-group">
                <div class="col-sm-3">
                    <asp:label id="lblFromDate" runat="server" Text="<%$Resources:Resources, expire_month_from %> "></asp:label>
                </div>
                <div class="col-sm-9">
                    <asp:DropDownList ID="drpYear" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpMonth" runat="server">
                        <asp:ListItem Selected="True" Value="01">January</asp:ListItem>
                        <asp:ListItem Value="02">Febuary</asp:ListItem>
                        <asp:ListItem Value="03">March</asp:ListItem>
                        <asp:ListItem Value="04">April</asp:ListItem>
                        <asp:ListItem Value="05">May</asp:ListItem>
                        <asp:ListItem Value="06">June</asp:ListItem>
                        <asp:ListItem Value="07">July</asp:ListItem>
                        <asp:ListItem Value="08">August</asp:ListItem>
                        <asp:ListItem Value="09">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row space dbl">
            <div class="form-group">
                <div class="col-sm-3">
                    <asp:label id="lblToDate" runat="server" Text="<%$Resources:Resources, expire_month_to %> "></asp:label>
                </div>
                <div class="col-sm-9">
                    <asp:DropDownList ID="drpYearTo" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="drpMonthTo" runat="server">
                        <asp:ListItem Selected="True" Value="01">January</asp:ListItem>
                        <asp:ListItem Value="02">Febuary</asp:ListItem>
                        <asp:ListItem Value="03">March</asp:ListItem>
                        <asp:ListItem Value="04">April</asp:ListItem>
                        <asp:ListItem Value="05">May</asp:ListItem>
                        <asp:ListItem Value="06">June</asp:ListItem>
                        <asp:ListItem Value="07">July</asp:ListItem>
                        <asp:ListItem Value="08">August</asp:ListItem>
                        <asp:ListItem Value="09">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="form-group">
            <div class="text-center">
                <asp:Button ID="view" runat="server" Text="View" OnClick="view_Click" />
            </div>
        </div>
</div>
<br />
<br />
<div>
    <asp:Button id="btnPrintGrid" runat="server" Text="Print" OnClick="btnPrintGrid_Click"></asp:Button>
</div>
	<br /><br />
    <asp:label id="lblCurrentMonth" runat="server" CssClass="txtLabel"></asp:label>

    <asp:datagrid id="DataGrid1" CssClass="Grid" OnItemDataBound="DataGrid1_ItemDataBound" HorizontalAlign="Center" runat="server" cellpadding="4" BackColor="White" BorderWidth="1px"
	    BorderStyle="None" BorderColor="InactiveCaptionText" PageSize="20" Visible="false">
	    <ItemStyle BackColor="#DEDFDE" ForeColor="Black" CssClass="tbBody"/>
	    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" CssClass="tbBody" />
	    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" CssClass="tbBody" />
	    <HeaderStyle BackColor="#24227A" Font-Bold="True" ForeColor="#F7F7F7" CssClass="tbHeader"/>
    </asp:datagrid>

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="500" SizeToReportContent = "true">
    </rsweb:ReportViewer>

    <script language="javascript">		    
        function ShowFromTo() {
            FromToBox.style.display = "";
            //if (listSummaryMemberFrm.rblViewType_1.checked == true)
            if (listSummaryMemberFrm.PrintType_1.checked == true)
                listSummaryMemberFrm.btnPrintGrid.style.display = "";
            else
                listSummaryMemberFrm.btnPrintGrid.style.display = "none"
        }
        function HideFromTo() {
            FromToBox.style.display = "none";
        }

        function FillDate(obj) {
            if (obj.value != "") {
                ddd = new Date(obj.value);
                if (obj.name == "txtToDate") {
                    var MonthDays = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
                    theYear = ddd.getYear();
                    if (theYear < 1000)
                        theYear += 1900;
                    LastMonth = ddd.getMonth();
                    DaysInLastMonth = MonthDays[LastMonth];
                    if (LastMonth == 1)
                        if (theYear % 400 == 0 || (theYear % 4 == 0 && theYear % 100 != 0))
                            DaysInLastMonth += 1;
                    obj.value = ddd.getYear() + "/" + ((eval(ddd.getMonth() + 1) < 10) ? "0" : "") + eval(ddd.getMonth() + 1) + "/" + DaysInLastMonth;
                }
                else
                    obj.value = ddd.getYear() + "/" + ((eval(ddd.getMonth() + 1) < 10) ? "0" : "") + eval(ddd.getMonth() + 1) + "/" + "01";
            }
        }
    </script>
</asp:Content>
