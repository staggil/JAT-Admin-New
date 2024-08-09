<%@ Page Title="Print Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportPrivateDetail.aspx.cs" Inherits="JAT.PrivateReport.reportPrivateDetail" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        a {
            color: #54667a;
            text-decoration: none;
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

        input#datepicker1Input, input#datepicker2Input {
            width: inherit;
        }
    </style>

    <%--<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../aspnet_client/system_web/4_0_30319/crystalreportviewers13/css/default.css" rel="stylesheet" />--%>


    <%--@section scripts{--%>
    <br />
    <div class="">
        <%--@* *********** Selection Depentdent*********** *@--%>
        <div class="box" style="background-color: lightgray;">
            <h3><%=Resources.Resources.print_detail_header %></h3>
            <form>
                <br />
                <div class="row">
                    <div class="col-sm-12">
                        <div class="row space">
                            <div class="form-group col-md-3" style="margin-top: 45px;">
                                <label for="inputState"><%=Resources.Resources.report_type %></label>
                                <span style="float: right;">:</span>
                            </div>
                            <div class="form-group col-md-8">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Vertical" RepeatColumns="2" CellSpacing="4">
                                    <asp:ListItem Value="New Member" Text="<%$Resources:Resources,new_member %>" Selected></asp:ListItem>
                                    <asp:ListItem Value="Cancel Member" Text="<%$Resources:Resources,cancel_member %>"></asp:ListItem>
                                    <asp:ListItem Value="Honor Member" Text="<%$Resources:Resources,honor_member %>"></asp:ListItem>
                                    <asp:ListItem Value="KrungThep Magazine" Text="<%$Resources:Resources,krung_thep_magazine %>"></asp:ListItem>
                                    <asp:ListItem Value="Change Address" Text="<%$Resources:Resources,change_address %>"></asp:ListItem>
                                    <asp:ListItem Value="Family Member" Text="<%$Resources:Resources,family_member %>"></asp:ListItem>
                                    <asp:ListItem Value="Election Label" Text="<%$Resources:Resources,election_label %>"></asp:ListItem>
                                    <asp:ListItem Value="List Receiver" Text="<%$Resources:Resources,list_receiver %>"></asp:ListItem>
                                    <asp:ListItem Value="Withdrawal=>Re-enrollment" Text="NA => A"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="row space">
                            <div class="form-group col-md-3">
                                <label><%=Resources.Resources.date_from %></label>
                                <span style="float: right;">:</span>
                            </div>
                            <div class="form-group col-md-5">
                                <input runat="server" type="text" id="Box1" class="datepicker1Input"  placeholder="dd/mm/yyyy" autocomplete="off" style="width: 40%;" readonly="readonly" >
                                <span id="datepicker1" class="glyphicon glyphicon-calendar"></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="row space">
                            <div class="form-group col-md-3">
                                <label><%=Resources.Resources.to %></label>
                                <span style="float: right;">:</span>
                            </div>
                            <div class="form-group col-md-5">
                                <input runat="server" type="text" id="Box2" class="datepicker2Input"  placeholder="dd/mm/yyyy" autocomplete="off" style="width: 40%;" readonly="readonly"  >
                                <span id="datepicker2" class="glyphicon glyphicon-calendar"></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="text-center">
                        <br />
                        <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Print" OnClick="Button1_Click" AutoPostBack="false" />
                    </div>
                </div>
            </form>
        </div>
        </br>
        <div style="overflow: scroll;">
            <div cellspacing="0" cellpadding="0" width="80%" align="center" border="0">
                <%--            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <Report FileName="PrivateReport\CrystalReports\test2.rpt">
                </Report>
                </CR:CrystalReportSource>     --%>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" DocumentMapCollapsed="True" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="1000px" Height="800px" ZoomMode="PageWidth">
                    <LocalReport ReportPath="">
                    </LocalReport>
                </rsweb:ReportViewer>
            </div>
        </div>
    </div>
    <%--@* *********** Datepicker *********** *@--%>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
    <%--@* *********** /Datepicker *********** *@--%>

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <%--}--%>
    <script>
        $(document).ready(function () {
            $("#type").change(function () {
                var val = $(this).val();
                if (val == "item1") {
                    $("#size").html("<option value=''>-- Any --</option><option selected value='Z'>Z</option><option value='#'>#</option>");
                } else if (val == "item2") {
                    $("#size").html("<option value=''>-- Any --</option><option selected value='#'>#</option><option value='Z'>Z</option>");
                }
            });
        });
    </script>

    <%--@* *********** Datepicker *********** *@--%>
    <script>

        var options = {
            format: 'dd/mm/yyyy',
            todayHighlight: true,
            autoclose: true
        }
        $(function () {

            $("#datepicker1,.datepicker1Input").click(function () {
                $('.datepicker1Input').datepicker(options).datepicker("show")
            });

            $("#datepicker2,.datepicker2Input").click(function () {
                $('.datepicker2Input').datepicker(options).datepicker("show")
            });


        });

    </script>
    <%--@* *********** /Datepicker *********** *@--%>
    <%--}--%>
</asp:Content>
