<%@ Page Title="Print Age" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportPrivateAge.aspx.cs" Inherits="JAT.PrivateReport.reportPrivateAge" %>

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
</style>

<%--@*PRINTAGE*@--%>
<br />
<div class="">
<%--    @*<h3>Print</h3>*@--%>
    <div class="box" style="background-color:lightgray; margin:10px 50px 20px 50px; padding:30px;">
        <h3><%=Resources.Resources.print_age_header %></h3>
        <form>
            <br/>
            <div class="row">
                <div class="col-md-12">
                    <div class="row space">
                        <div class="form-group col-md-2">
                            <label><%=Resources.Resources.age %></label>
                            <span style="float:right;">:</span>
                        </div>
                        <div class="form-group col-md-8">
                            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                            &nbsp year old &nbsp &nbsp
                            <label for="inputState">~</label>
                            &nbsp &nbsp
                            <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
                            &nbsp year old
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="text-center">
                    <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Print" AutoPostBack="false" OnClick="Button1_Click"/>
                </div>
            </div>
        </form>
    </div>
    </br>
        <div style="overflow: scroll;" >
        <div cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" DocumentMapCollapsed="True" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="1000px" Height="800px" ZoomMode="PageWidth">
            <localreport reportpath="">
            </localreport>
            </rsweb:ReportViewer>
        </div>
    </div>
</div>

<script>
    $(function () {
        var $select = $(".1-100");
        for (i = 1; i <= 100; i++) {
            $select.append($('<option></option>').val(i).html(i))
        }
    });
</script>

</asp:Content>
