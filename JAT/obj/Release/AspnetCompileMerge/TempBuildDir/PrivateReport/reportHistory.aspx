<%@ Page Title="Print History" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportHistory.aspx.cs" Inherits="JAT.PrivateReport.reportHistory" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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

    input#datepicker1Input, input#datepicker2Input {
        width: inherit;
    }
</style>

<%--@*PRINTHISTORY*@--%>
<br />
<div class="">
    <%--@*<h3>History</h3>*@--%>
    <div class="box" style="background-color:lightgray;">
        <h3><%=Resources.Resources.print_history_header%></h3>
        <form>
            <br />
            <div class="row">
                <div class="col-sm-12">
                    <div class="row space">
                        <div class="form-group col-md-3">
                            <label for="inputState"><%=Resources.Resources.member_type%></label>
                            <span style="float:right;">:</span>
                        </div>
                        <div class="form-group col-md-7">
                            <asp:RadioButtonList ID="MemberType" runat="server" RepeatDirection="Vertical" RepeatColumns="2" cellspacing="4">
                                <asp:ListItem Value="Japan" Text="<%$Resources:Resources,japan %>" Selected></asp:ListItem>
                                <asp:ListItem Value="Thai" Text="<%$Resources:Resources,thai %>"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                    </div>
                </div>

                <div class="col-md-12">
                    <div class="row space">
                        <div class="form-group col-md-3">
                            <label><%=Resources.Resources.applied_date_from %></label>
                            <span style="float:right;">:</span>
                        </div>
                        <div class="form-group col-md-5">
                            <input required type="text" class="datepicker1Input" id="datepicker1Input" autocomplete="off" placeholder="dd/mm/yyyy" readonly="readonly"  runat="server">
                            <span id="datepicker1" class="glyphicon glyphicon-calendar"></span>
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="row space">
                        <div class="form-group col-md-3">
                            <label><%=Resources.Resources.to %></label>
                            <span style="float:right;">:</span>
                        </div>
                        <div class="form-group col-md-5">
                            <input required type="text" class="datepicker2Input" id="datepicker2Input" autocomplete="off" placeholder="dd/mm/yyyy" readonly="readonly"  runat="server">
                            <span id="datepicker2" class="glyphicon glyphicon-calendar"></span>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="text-center">
                        <br />
                        <asp:Button ID="Print" runat="server" Text="Print" OnClick="Print_Click" />
                    </div>
                </div>

            </div>
        </form>


    </div>
</div>
    <div align="center">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" DocumentMapCollapsed="True" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="1000px" Height="800px" ZoomMode="PageWidth">
            <localreport reportpath="">
            </localreport>
        </rsweb:ReportViewer>
    </div>

<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
<%--@section scripts{--%>

<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <%--@* *********** Selection Depentdent*********** *@--%>
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

</asp:Content>
