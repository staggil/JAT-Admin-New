<%@ Page Title="Company List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportCMList.aspx.cs" Inherits="JAT.CompanyReport.reportCMList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

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

        input#datepicker1Input, input#datepicker2Input {
            width: inherit;
        }

        .space {
            margin-left: 20px;
        }
    </style>

    <br />
    <br />
    <br />
    <div class="box" style="background-color: lightgray;">
        <h3><%=Resources.Resources.list%></h3>
        <form>
            <%--@* ************************************************* 1 ***************************************************** *@--%>
            <div class="row space">
                <div class="form-group col-md-3">
                    <label><%=Resources.Resources.pay_at%></label>
                    <span style="float: right;">:</span>
                </div>
                <div class="form-group col-md-2">
                    <%--<select>
                    <option selected>New Member</option>
                    <option>Quit</option>
                </select>--%>
                    <asp:DropDownList ID="cbViewType" runat="server">
                        <asp:ListItem Value="New Member">New Member</asp:ListItem>
                        <asp:ListItem Value="Quit">Quit</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row space">
                <div class="col-md-12">
                    <div class="row">
                        <div class="form-group col-md-3">
                            <label><%=Resources.Resources.date_from%></label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="form-group col-md-5">
                            <input type="text" id="txtFromDate" class="datepicker1Input" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
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
                            <span style="float: right;">:</span>
                        </div>
                        <div class="form-group col-md-5">
                            <input type="text" id="txtToDate" class="datepicker2Input" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
                            <span id="datepicker2" class="glyphicon glyphicon-calendar"></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="text-center">
                    <br />
                    <br />
                    <asp:Button ID="print" runat="server" Text="Print" OnClick="print_Click" class="btn btn-primary" />
                    <asp:Button ID="reset" runat="server" Text="Reset" OnClick="reset_Click" class="btn btn-primary" />
                </div>
            </div>
        </form>
    </div>
    <br />
    <br />
    <div cellspacing="0" cellpadding="0" width="80%" align="center" border="0">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" DocumentMapCollapsed="True" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="1000px" Height="800px" ZoomMode="PageWidth">
            <LocalReport ReportPath="">
            </LocalReport>
        </rsweb:ReportViewer>
    </div>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
    <%--@section scripts{--%>
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
            $("id*=datepicker1").click(function () {
                $("[id*=txtFromDate]").datepicker(options).datepicker("show")
            });

            $("id*=datepicker2").click(function () {
                $("[id*=txtToDate]").datepicker(options).datepicker("show")
            });

            //$("[id*=txtFromDate]").datepicker(options);
            //$("[id*=txtToDate]").datepicker(options);
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
    <%--}--%>
</asp:Content>
