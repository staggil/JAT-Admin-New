<%@ Page Title="Statistic Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="listPrivateStat.aspx.cs" Inherits="JAT.Inquery.Private.listPrivateStat" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .Grid td{
        padding:5px
    }
    body {
        font-size: 17px;
    }
/*    table{
            table-layout: fixed;
            width: 550px;
        }
    td {
            width: 300px;
        }*/
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
<div class="box" style="background-color: lightgray">
    <h3><%=Resources.Resources.view_member_statistic %></h3>
        <div class="row space">
            <div class="form-group">
                <div class="col-sm-2">
                    <label for="inputstate"><%=Resources.Resources.view_type %></label>
                    <span style="float:right;">:</span>
                </div>
                <div class="col-sm-10">
                    <asp:radiobuttonlist id="rblViewType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblViewType_SelectedIndexChanged" repeatdirection="horizontal"
						repeatcolumns="2" >
						<asp:listitem value="1" Text="<%$Resources:Resources, member_summary %>" selected=true></asp:listitem>
						<asp:listitem value="2" Text="<%$Resources:Resources, group by_prefix_name %>"></asp:listitem>
						<asp:listitem value="3" Text="<%$Resources:Resources, group_by_age %>"></asp:listitem>
						<asp:listitem value="4" Text="<%$Resources:Resources, payment_summary %>"></asp:listitem>
						<asp:listitem value="5" Text="<%$Resources:Resources, payment_group_by_duration %>"></asp:listitem>
					</asp:radiobuttonlist>

                </div>
            </div>
        </div>

	    <div class="row space ">
            <div class="form-group">
                <div class="col-sm-2">
                    <asp:label style="float:right;" id="lblfromdate" runat="server" Text="<%$Resources:Resources, from_date %>"></asp:label>
                </div>
                <div class="col-sm-10">
                    <input required id="txtFromDate" type="text" runat="server" autocomplete="off" placeholder="dd/mm/yyyy" />
                    <span id="glyphicon1" runat="server" class="glyphicon glyphicon-calendar"></span>
                </div>
            </div>
        </div>
        <div class="row space">
            <div class="form-group">
                <div class="col-sm-2">
                    <asp:label style="float:right;" id="lbltodate" runat="server" Text="<%$Resources:Resources, to_date %>"></asp:label>
                </div>
                <div class="col-sm-10">
                    <input required id="txtToDate" type="text" runat="server" autocomplete="off" placeholder="dd/mm/yyyy"/>
                    <span id="glyphicon2" runat="server" class="glyphicon glyphicon-calendar"></span>
                    <%--<asp:label id="Label2" runat="server"></asp:label>--%>
                </div>
            </div>
        </div>
    <br />
        <div class="form-group">
            <div class="text-center">
                <asp:button id="view" class="btn btn-primary" onclick="view_Click" runat="server" text="view" />
            </div>
        </div>
</div>
<br />
<br />
    <div class="row">
        <div class="form-group">
            <div class="col-sm-1">
                &nbsp;
            </div>
            <div class="col-sm-10">
                <asp:label id="Label1" runat="server"></asp:label><br />
            </div>
        </div>
    </div>
    <br />
    <div class="row space">
        <div class="form-group">
            <div class="col-sm-3">
                &nbsp;
            </div>
            <div class="col-sm-9">
                <asp:button id="btnprintgrid" OnClick="btnprintgrid_Click" runat="server" text="print"></asp:button>
            </div>
        </div>
    </div>

    

    <asp:datagrid id="DataGrid2" CssClass="Grid" runat="server" cellpadding="4" backcolor="white" borderwidth="1px"
		borderstyle="none" bordercolor="inactivecaptiontext" pagesize="20" HorizontalAlign="Center">
		<itemstyle backcolor="#dedfde" forecolor="black"/>
		<headerstyle BackColor="#24227A" font-bold="true" forecolor="#f7f7f7" cssclass="tbheader"/>
		<footerstyle forecolor="desktop" backcolor="#b5c7de"></footerstyle>
	</asp:datagrid>

    <br /><br />

	<asp:datagrid id="DataGrid1" CssClass="Grid" runat="server" cellpadding="4" backcolor="white" borderwidth="1px"
		borderstyle="none" bordercolor="inactivecaptiontext" pagesize="20" visible="false" HorizontalAlign="Center">
		<itemstyle backcolor="#dedfde" forecolor="black"/>
		<headerstyle BackColor="#24227A" font-bold="true" forecolor="#f7f7f7" cssclass="tbheader"/>
		<footerstyle forecolor="desktop" backcolor="#b5c7de"></footerstyle>
	</asp:datagrid>

    <%--<rsweb:reportviewer runat="server"></rsweb:reportviewer>--%>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="500" SizeToReportContent = "true">
    </rsweb:ReportViewer>

    <script language="javascript">		    
        function showfromto() {
            fromtobox.style.display = "";
            txtFromDate.focus();
            if (rblviewtype_4.checked == true)
                listprivatestatfrm.btnprintgrid.style.display = "";
            else
                btnprintgrid.style.display = "none"
        }
        function hidefromto() {
            txtFromDate.value = "";
            txtToDate.value = "";
            fromtobox.style.display = "none";
            btnprintgrid.style.display = "none";
        }

        function checkdate() {
            // filldate(document.all.txtfromdate);
            //filldate(document.all.txtToDate);
            if (txtFromDate.value == "" && txtToDate.value != "") {
                alert("please! input from date");
                txtFromDate.focus();
                return false;
            }
            if (txtFromDate.value != "" && txtToDate.value == "") {
                alert("please! input to date");
                txtToDate.focus();
                return false;
            }
            return true;
        }

        function filldate(obj) {
            if (obj.value != "") {
                ddd = new date(obj.value);
                if (obj.name == "txtToDate") {
                    var monthdays = new array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
                    theyear = ddd.getyear();
                    if (theyear < 1000)
                        theyear += 1900;
                    lastmonth = ddd.getmonth();
                    daysinlastmonth = monthdays[lastmonth];
                    if (lastmonth == 1)
                        if (theyear % 400 == 0 || (theyear % 4 == 0 && theyear % 100 != 0))
                            daysinlastmonth += 1;
                    obj.value = ddd.getyear() + "/" + ((eval(ddd.getmonth() + 1) < 10) ? "0" : "") + eval(ddd.getmonth() + 1) + "/" + daysinlastmonth;
                }
                else
                    obj.value = ddd.getyear() + "/" + ((eval(ddd.getmonth() + 1) < 10) ? "0" : "") + eval(ddd.getmonth() + 1) + "/" + "01";
            }
        }
    </script>
    <%--@* *********** Datepicker *********** *@--%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var options = {
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                autoclose: true
            }
            $("[id*=glyphicon1],[id*=txtFromDate]").click(function () {
                if (!$("[id*=txtFromDate]").prop("disabled")) {
                    $('[id*=txtFromDate]').datepicker(options).datepicker("show")
                }
            });
            $("[id*=glyphicon2],[id*=txtToDate]").click(function () {
                if (!$("[id*=txtToDate]").prop("disabled")) {
                    $('[id*=txtToDate]').datepicker(options).datepicker("show")
                }
            });
        });
    </script>

</asp:Content>
