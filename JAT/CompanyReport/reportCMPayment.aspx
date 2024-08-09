<%@ Page Title="Company Payment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportCMPayment.aspx.cs" Inherits="JAT.CompanyReport.reportCMPayment" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
    body {
        font-size: 17px;
    }

    h3 {
        font-weight: bold;
    }

    input#datepicker1Input, input#datepicker2Input {
        width: inherit;
    }
    /*.datepicker-dropdown {
        top:30em;
    }*/

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
    <h3><%=Resources.Resources.payment_management%></h3>
        <%--@* ************************************************* 1 ***************************************************** *@--%>
        <div class="row space">
            <div class="form-group col-md-3">
                <label><%=Resources.Resources.pay_at%></label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-md-2">
                <select id="payAt" runat="server">
                    <option value="JAT" selected>JAT</option>
                    <option value="Annex">Annex</option>
                </select>
            </div>
        </div>
        <div class="row space">
            <div class="form-group col-md-3">
                <label><%=Resources.Resources.report_type%></label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-md-2">
                <asp:DropDownList ID="reportType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="reportType_SelectedIndexChanged">
                    <asp:ListItem Text="History" Value="History" />
                    <asp:ListItem Text="Accrued" Value="Accrued" />
                    <asp:ListItem Text="Book" Value="Book" />
                    <asp:ListItem Text="List Receiver" Value="List Receiver" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="row space">
            <div class="form-group col-md-3">
                <label><%=Resources.Resources.payment_method%></label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-md-6">
                <asp:DropDownList ID="payMethod" runat="server">
                    <asp:ListItem Text="C" Value="C" />
                    <asp:ListItem Text="T" Value="T" />
                    <asp:ListItem Text="S" Value="S" />
                    <asp:ListItem Text="B" Value="B" />
                </asp:DropDownList>
                <%--<asp:DropDownList ID="paymethodacc" runat="server">
                    <asp:ListItem Text="K" Value="K" />
                    <asp:ListItem Text="J" Value="J" />
                    <asp:ListItem Text="P" Value="P" />
                    <asp:ListItem Text="T" Value="T" />
                    <asp:ListItem Text="S" Value="S" />
                    <asp:ListItem Text="B" Value="B" />
                </asp:DropDownList>--%>
                <asp:Label ID="Months" runat="server" Text="Months"></asp:Label>
                <%--<select id="month" class="1-50" runat="server">
                    
                </select>--%>
                <asp:dropdownlist id="month" runat="server">
			    </asp:dropdownlist>
                <%--<asp:DropDownList ID="DropDownList1" runat="server">

                </asp:DropDownList>--%>
            </div>
        </div>
        <div class="row space">
            <div class="col-md-12">
                <div class="row">
                    <div class="form-group col-md-3">
                        <label><%=Resources.Resources.report_payment03%></label>
                        <span style="float:right;">:</span>
                    </div>
                    <div class="form-group col-md-5">
                        <input required type="text" id="expdateFrom" class="datepicker1Input" autocomplete="off"  placeholder="dd/mm/yyyy" runat="server">
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
                        <input required type="text" id="expdateTo" class="datepicker2Input" autocomplete="off"  placeholder="dd/mm/yyyy" runat="server">
                        <span id="datepicker2" class="glyphicon glyphicon-calendar"></span>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="text-center">
                <br />
                <br />
                <asp:Button ID="print" OnClick="print_Click" runat="server" Text="Print" class="btn btn-primary" />
                <asp:Button ID="reset" OnClick="reset_Click" runat="server" Text="Reset" class="btn btn-primary" />
            </div>
        </div>
</div>
<br />
<br />
    <div cellspacing="0" cellpadding="0" width="80%" align="center" border="0">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" DocumentMapCollapsed="True" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="1000px" Height="800px" ZoomMode="PageWidth">
                <localreport reportpath="">
                </localreport>
                </rsweb:ReportViewer>
    </div>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    
    <%--@* *********** Datepicker *********** *@--%>
    <script>
        var options = {
            //format: 'dd-M-yyyy',
            format: 'dd/mm/yyyy',
            todayHighlight: true,
            autoSize: true,
            autoclose: true,

        }

        $(function () {
            $("#datepicker1,.datepicker1Input").click(function () {
                $('.datepicker1Input').datepicker(options).datepicker("show")
            });

            $("#datepicker2,.datepicker2Input").click(function () {
                $('.datepicker2Input').datepicker(options).datepicker("show")
            });
        });

        //$(document).ready(function () {
        //    $("[id*=reportType]").change(function () {
        //        var val = $(this).val();
        //        if (val == "history") {
        //            $("[id*=paymethod]").html("<option value=''>-- Any --</option><option selected value='Z'>Z</option><option value='#'>#</option>");
        //        } else if (val == "accrued") {
        //            $("[id*=paymethod]").html("<option value=''>-- Any --</option><option selected value='#'>#</option><option value='Z'>Z</option>");
        //        }
        //    });
        //});
    </script>
    <%--@* *********** /Datepicker *********** *@--%>
    <%-- @* *********** Period 1-12 *********** *@ --%>
    <script>
        $(function () {
            var $select = $(".1-50");
            for (i = 1; i <= 50; i++) {
                $select.append($('<option value="' + i + '"></option>').val(i).html(i))
            }
        });
    </script>
   <%-- @* *********** /Period 1-12*********** *@--%>
</asp:Content>
