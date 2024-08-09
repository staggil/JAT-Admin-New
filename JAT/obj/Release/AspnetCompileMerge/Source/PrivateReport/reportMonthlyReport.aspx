<%@ Page Title="Monthly Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportMonthlyReport.aspx.cs" Inherits="JAT.PrivateReport.reportMonthlyReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

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
            <%--<h3><%=Resources.Resources.print_detail_header %></h3>--%>
            <h3>Monthly Report</h3>
            <form>
                <br />
                <div class="row">
                    <div class="col-sm-12">
                        <div class="row space">
                            <div class="form-group col-md-3">
                                <%--<label for="inputState"><%=Resources.Resources.report_type %></label>--%>
                                <label for="inputState">Member Type</label>
                                <span style="float: right;">:</span>
                            </div>
                            <div class="form-group col-md-5">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Vertical" RepeatColumns="2" CellSpacing="4">
                                    <%--<asp:ListItem Value="New Member" Text="<%$Resources:Resources,new_member %>" Selected></asp:ListItem>--%>
                                    <asp:ListItem Value="A" Text="A" Selected></asp:ListItem>
                                    <asp:ListItem Value="NA" Text="NA"></asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="row space">
                            <div class="form-group col-md-3">
                                <label>Date From</label>
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
                        <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Export" AutoPostBack="false" OnClick="Button1_Click" />
                    </div>
                </div>
            </form>
        </div>
        </br>
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


