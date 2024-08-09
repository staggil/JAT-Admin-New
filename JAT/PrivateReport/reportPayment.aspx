<%@ Page Title="Print Payment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportPayment.aspx.cs" Inherits="JAT.PrivateReport.reportPayment" %>

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

    input#datepicker1Input, input#datepicker2Input{
        width: inherit;
    }
</style>

<%--@*PRINTPAYMENT*@--%>
<br />
<div class="">
    <%--@*<h3>Payment Management</h3>*@--%>
    <div class="box" style="background-color:lightgray;">
        <h3><%=Resources.Resources.print_payment_header%></h3>
        <form>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="row space">
                        <div class="form-group col-md-3">
                            <label for="inputState"><%=Resources.Resources.payment_method %></label>
                            <span style="float:right;">:</span>
                        </div>
                        <div class="form-group col-md-3">
                            <asp:DropDownList ID="payMethod" runat="server">
                                <asp:ListItem Text="All" Value="A" />
                                <asp:ListItem Text="T not have short" Value="T" />
                                <asp:ListItem Text="T have short" Value="TS" />
                                <asp:ListItem Text="S not have short" Value="S" />
                                <asp:ListItem Text="S have short" Value="SS" />
                                <asp:ListItem Text="C" Value="C" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <div class="row space">
                        <div class="form-group col-md-3">
                            <label for="inputState"><%=Resources.Resources.pay_at %></label>
                            <span style="float:right;">:</span>
                        </div>
                        <div class="form-group col-md-3">
                            <%--<select id="blockInput" class="">
                                <option selected>Sathorn</option>
                                <option>Annex</option>
                                <option>Rec.</option>
                                <option>Transfer</option>
                            </select>--%>
                            <asp:DropDownList ID="PayAt" runat="server">
                                <asp:ListItem Text="Sathorn" Value="1" />
                                <asp:ListItem Text="Annex" Value="2" />
                                <asp:ListItem Text="Rec." Value="3" />
                                <asp:ListItem Text="Transfer" Value="4" />
                                <asp:ListItem Text="Credit card" Value="5" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>


                <div class="col-md-12">
                    <div class="row space">
                        <div class="form-group col-md-3">
                            <label><%=Resources.Resources.payment_date_from %></label>
                            <span style="float:right;">:</span>
                        </div>
                        <div class="form-group col-md-5">
                            <input required type="text" id="datepicker1Input" class="datepicker1Input" autocomplete="off" runat="server" placeholder="dd/mm/yyyy" readonly="readonly" >
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
                            <input required type="text" id="datepicker2Input" class="datepicker2Input" autocomplete="off" runat="server" placeholder="dd/mm/yyyy" readonly="readonly" >
                            <span id="datepicker2" class="glyphicon glyphicon-calendar"></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="text-center">
                    <br />
                    <%--<button id="blockInput" type="submit" class="btn btn-primary">Print</button>--%>
                    <asp:Button ID="Print" runat="server" Text="Print" OnClick="Print_Click" />
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
    <%--@* *********** /Selection Depentdent*********** *@--%>

<script>
        function setBNo() {
            CNo.value = BNo.value;
        }
</script>

    <%--@* *********** Calculation Date*********** *@--%>
<script>
        function setExpireDate() {
            var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            var myDate = new Date(EffectiveDate.value);
            month = myDate.getMonth();
            modifiedMonth = month + parseInt(PayDuration.value);
            modifiedDate = new Date(myDate.getFullYear(), modifiedMonth, 0);
            modifiedMonth = months[modifiedDate.getMonth() + 0];
            if (modifiedDate.getDate() + "-" + modifiedMonth + "-" + modifiedDate.getFullYear() != "NaN-undefined-NaN") {
                ExpiredDate.value = modifiedDate.getDate() + "-" + modifiedMonth + "-" + modifiedDate.getFullYear();
            } else {
                ExpiredDate.value = " ";
            }

        }
</script>
    <%--@* *********** /Calculation Date*********** *@--%>

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

    <%--@* ***********  Calculation Total Fee *********** *@--%>
<script>
    function cclt() {
        var tm = document.getElementById("TotalMonth").value;
        var pd = document.getElementById("PayDuration").value;
        var totalfee = tm * pd;
        var MemberFee = totalfee * 0.3
        var NewsFee = totalfee * 0.7
        if (tm == 1000) {
            document.getElementById("TotalFee").value = totalfee;
            document.getElementById("MemberFee").value = MemberFee;
            document.getElementById("NewsFee").value = NewsFee;
        } else if (tm == 1500) {
            document.getElementById("TotalFee").value = totalfee
        } else if (tm == 2000) {
            document.getElementById("TotalFee").value = totalfee
        } else if (tm == 2500) {
            document.getElementById("TotalFee").value = totalfee
        } else if (tm == 1300) {
            document.getElementById("TotalFee").value = totalfee
        }
    }
</script>
    <%--@* ***********  /Calculation Total Fee *********** *@--%>
<%--}--%>
</asp:Content>
