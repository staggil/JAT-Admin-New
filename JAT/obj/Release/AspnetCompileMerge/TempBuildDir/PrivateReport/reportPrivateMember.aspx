<%@ Page Title="Print Member" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportPrivateMember.aspx.cs" Inherits="JAT.PrivateReport.reportPrivateMember" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
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
</style>

<%--@*PRINTMEMBER*@--%>
<br />
<div class="">
    <%--@*<h3>Print</h3>*@--%>
    <div class="box" style="background-color:lightgray;">
        <h3><%=Resources.Resources.print_member_header %></h3>
        <form>
            <br/>
            <div class="row">
                <div class="col-sm-12">
                    <div class="row space">
                        <div class="form-group col-md-3" style="margin-top:10px;">
                            <label for="inputState"><%=Resources.Resources.member_type %></label>
                            <span style="float:right;">:</span>
                        </div>
                        <div class="form-group col-md-8">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Vertical" RepeatColumns="2" cellspacing="2" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="Honor Member" Text="<%$Resources:Resources,honor_member %>" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="Thai Main Member" Text="<%$Resources:Resources,thai_main_member %>"></asp:ListItem>
                                <asp:ListItem Value="Japanese Member List" Text="<%$Resources:Resources,japanese_member_list %>"></asp:ListItem>
                                <asp:ListItem Value="Company List" Text="<%$Resources:Resources,company_list %>"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="inputdiv1" runat="server">
                <div class="col-sm-12">
                    <div class="row space">
                        <div class="form-group col-md-3"></div>
                        <div class="form-group col-md-8">
                            <asp:CheckBox ID="chkBox" runat="server"/>
                            <asp:Label ID="chkBoxLabel" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <%-- Member Check/Company Check_Applied Date From :/Applied Date To : --%>
            <div class="row" id="inputdiv2" runat="server">
                <div class="col-sm-12">
                    <div class="row space">
                        <div class="form-group col-md-3">
                            <asp:Label runat="server" Text="<%$Resources:Resources,applied_date_from %>" ></asp:Label>
                            <span style="float:right;" runat="server">:</span>
                            <br />
                            <asp:Label runat="server" Text="<%$Resources:Resources,applied_date_to_print %>"></asp:Label>
                            <span style="float:right;" runat="server">:</span>
                        </div>
                        <div class="form-group col-md-8">
                            <asp:TextBox ID="date_fr" runat="server" ClientIDMode="Static" class="datepicker1Input" placeholder="dd/mm/yyyy"></asp:TextBox>
                            <span id="datepicker1" class="glyphicon glyphicon-calendar"></span>
                            <br />
                            <asp:TextBox ID="date_to" runat="server" ClientIDMode="Static" class="datepicker2Input" placeholder="dd/mm/yyyy"></asp:TextBox>
                            <span id="datepicker2" class="glyphicon glyphicon-calendar"></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="text-center">
                    <br/>
                    <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Print" AutoPostBack="false" OnClick="Button1_Click"/>
                </div>
            </div>
        </form>
    </div>
    </br>
    <div style="overflow: scroll;" >
        <div cellspacing="0" cellpadding="0" width="80%" align="center" border="0">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" DocumentMapCollapsed="True" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="1000px" Height="800px" ZoomMode="PageWidth">
            <localreport reportpath="">
            </localreport>
            </rsweb:ReportViewer>
        </div>
</div>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
<%--@section scripts{--%>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
<script>
    var options = {
        format: 'dd/mm/yyyy',
        todayHighlight: true,
        autoclose: true
    }

    var tititi;

    $(function () {
        $("#datepicker1,.datepicker1Input").click(function () {
            $('.datepicker1Input').datepicker(options).datepicker("show")
        });

        $("#datepicker2,.datepicker2Input").click(function () {
            $('.datepicker2Input').datepicker(options).datepicker("show")
        });
    });
</script>
</asp:Content>
