<%@ Page Title="Payment Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="listCompanyPayment.aspx.cs" Inherits="JAT.Inquery.Company.listCompanyPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body {
            font-size: 17px;
        }
        .Grid td {
            padding: 5px;
        }
        table{
            table-layout: fixed;
            width: auto;
        }

        th, td {
            width: 200px;
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
            width: 95%;
            align-self: center;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 20px;
            box-shadow: 3px 3px 20px rgba(0, 0, 0, 0.1);
        }

        input#datepicker1Input, input#datepicker2Input, input#datepicker3Input, input#datepicker4Input, input#datepicker5Input, input#datepicker6Input, input#datepicker7Input, input#datepicker8Input {
            width: inherit;
        }
    </style>
    <br />
    <br />
    <br />
    <div class="box" style="background-color: lightgray;">
        <h3><%=Resources.Resources.view_member_payment %></h3>
        <%--@* ************************************************* 1 ***************************************************** *@--%>
        <div class="row">
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-5">
                        <label><%=Resources.Resources.member_status %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-sm-7">
                        <asp:RadioButtonList ID="memberStatus" runat="server" RepeatDirection="horizontal" CellPadding="4" AutoPostBack="True" OnSelectedIndexChanged="rblMemberStatus_SelectedIndexChanged">
                            <asp:ListItem Value="A" Text="A"></asp:ListItem>
                            <asp:ListItem Value="NA" Text="NA"></asp:ListItem>
                            <asp:ListItem Value="Both" Text="<%$Resources:Resources, both %>" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-5">
                        <label><%=Resources.Resources.payment_type %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-5">
                        <select id="payMethod" runat="server">
                            <option selected>-- Any --</option>
                            <option value="Cash">Cash[K,J,P]</option>
                            <option value="Bank">Bank[T,S,B]</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-4">
                        <label><%=Resources.Resources.pay_overdue %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-5">
                        <asp:DropDownList ID="drplstOverdue" runat="server">
                            <asp:ListItem>-- Any --</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-5">
                        <label><%=Resources.Resources.pay_duration %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-5">
                        <asp:DropDownList ID="drplstDuration" runat="server" CssClass="listfr">
                            <asp:ListItem>-- Any --</asp:ListItem>
                            <asp:ListItem Value="1">= 1 year</asp:ListItem>
                            <asp:ListItem Value="2">&lt; 1 year</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-5">
                        <label><%=Resources.Resources.applied_date_from %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-7">
                        <%--<input type="text" id="datepicker1Input" autocomplete="off" placeholder="DD/MM/YYYY">
                        <span id="datepicker1" class="glyphicon glyphicon-calendar"></span>--%>
                        <input type="text" id="appliedDateFrom" style="width: inherit;" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
                        <span id="datepicker1" class="glyphicon glyphicon-calendar"></span>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-4">
                        <label>To</label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-7">
                        <%--<input type="text" id="datepicker2Input" autocomplete="off" placeholder="DD/MM/YYYY">
                        <span id="datepicker2" class="glyphicon glyphicon-calendar"></span>--%>
                        <input type="text" id="appliedDateTo" style="width: inherit;" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
                        <span id="datepicker2" class="glyphicon glyphicon-calendar"></span>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-5">
                        <label><%=Resources.Resources.quit_date_from %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-7">
                        <%--<input type="text" id="datepicker3Input" autocomplete="off" placeholder="DD/MM/YYYY">
                        <span id="datepicker3" class="glyphicon glyphicon-calendar"></span>--%>
                        <input type="text" id="txtQuitDateFrom" style="width: inherit;" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
                        <span id="datepicker3" class="glyphicon glyphicon-calendar"></span>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-4">
                        <label>To</label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-7">
                        <%--<input type="text" id="datepicker4Input" autocomplete="off" placeholder="DD/MM/YYYY">
                        <span id="datepicker4" class="glyphicon glyphicon-calendar"></span>--%>
                        <input type="text" id="txtQuitDateTo" style="width: inherit;" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
                        <span id="datepicker4" class="glyphicon glyphicon-calendar"></span>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-5">
                        <label><%=Resources.Resources.payment_date_from %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-7">
                        <%--<input type="text" id="payMethodFrom" autocomplete="off" placeholder="DD/MM/YYYY">
                        <span id="datepicker5" class="glyphicon glyphicon-calendar"></span>--%>
                        <input type="text" id="payMethodFrom" style="width: inherit;" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
                        <span id="datepicker5" class="glyphicon glyphicon-calendar"></span>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-4">
                        <label>To</label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-7">
                        <%--<input type="text" id="payMethodTo" autocomplete="off" placeholder="DD/MM/YYYY">
                        <span id="datepicker6" class="glyphicon glyphicon-calendar"></span>--%>
                        <input type="text" id="payMethodTo" style="width: inherit;" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
                        <span id="datepicker6" class="glyphicon glyphicon-calendar"></span>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-5">
                        <label><%=Resources.Resources.expired_date_from %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-7">
                        <%--<input type="text" id="expiredDateFrom" autocomplete="off" placeholder="DD/MM/YYYY">
                        <span id="datepicker7" class="glyphicon glyphicon-calendar"></span>--%>
                        <input type="text" id="expiredDateFrom" style="width: inherit;" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
                        <span id="datepicker7" class="glyphicon glyphicon-calendar"></span>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-4">
                        <label>To</label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-7">
                        <%--<input type="text" id="expiredDateTo" autocomplete="off" placeholder="DD/MM/YYYY">
                        <span id="datepicker8" class="glyphicon glyphicon-calendar"></span>--%>
                        <input type="text" id="expiredDateTo" style="width: inherit;" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
                        <span id="datepicker8" class="glyphicon glyphicon-calendar"></span>
                    </div>
                </div>
            </div>
        </div>
        <%--@* ************************************************* 2 ***************************************************** *@--%>
        <div class="row">
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-5">
                        <label><%=Resources.Resources.show_field %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="col-sm-7">
                        <asp:RadioButtonList ID="rblFieldFormat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblFieldFormat_SelectedIndexChanged" RepeatDirection="Horizontal">
                            <asp:ListItem Value="All" Selected="True" Text="<%$Resources:Resources, all %>"></asp:ListItem>
                            <asp:ListItem Value="Selection" Text="<%$Resources:Resources, selection %>"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <label>&nbsp</label>
            </div>
            <div class="col-md-10">
                <div class="form-group col-md-12">
                    <div class="form-check" style="margin-left: 30px;">
                        <asp:CheckBoxList ID="CheckBoxList1" RepeatColumns="3" AutoPostBack="true" CssClass="chkBoxList" Enabled="false" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" runat="server">
                            <asp:ListItem Value="Member Id" Text="<%$Resources:Resources, member_id %>"></asp:ListItem>
                            <asp:ListItem Value="Japanese Name" Text="<%$Resources:Resources, japanese_name %>"></asp:ListItem>
                            <asp:ListItem Value="English Name" Text="<%$Resources:Resources, english_name %>"></asp:ListItem>
                            <asp:ListItem Value="Payment Method	" Text="<%$Resources:Resources, payment_method %>"></asp:ListItem>
                            <asp:ListItem Value="Account No." Text="<%$Resources:Resources, account_no %>"></asp:ListItem>
                            <asp:ListItem Value="Bank Code" Text="<%$Resources:Resources, bank_code %>"></asp:ListItem>
                            <asp:ListItem Value="Total Per Month" Text="<%$Resources:Resources, total_per_month %>"></asp:ListItem>
                            <asp:ListItem Value="Payment Date" Text="<%$Resources:Resources, payment_date %>"></asp:ListItem>
                            <asp:ListItem Value="Address" Text="<%$Resources:Resources, address %>"></asp:ListItem>
                            <asp:ListItem Value="Payment Remark	" Text="<%$Resources:Resources, payment_remark %>"></asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="text-center">
                <asp:Button ID="view" runat="server" class="btn btn-primary" OnClick="view_Click" Text="View" />
                <asp:Button ID="reset" runat="server" class="btn btn-primary" OnClick="reset_Click" Text="Reset" />
            </div>
        </div>
    </div>
    <br />
    <br />
    <div style="max-height: 500px; overflow-y: scroll;">
        <asp:DataGrid ID="DataGrid1" CssClass="Grid" runat="server" CellPadding="4" BackColor="White"
        BorderWidth="1px" BorderStyle="None" BorderColor="InactiveCaptionText" PageSize="20"
        AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="DataGrid1_PageIndexChanged">
        <FooterStyle ForeColor="Desktop" BackColor="#B5C7DE"></FooterStyle>
        <%--<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>--%>
        <AlternatingItemStyle BackColor="#F7F7F7"></AlternatingItemStyle>
        <ItemStyle ForeColor="#24227A" BackColor="White"></ItemStyle>
        <%--<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#24227A"></HeaderStyle>--%>
        <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="#F7F7F7"></HeaderStyle>
        <Columns>
            <asp:HyperLinkColumn DataTextField="companyId" HeaderText="companyId" DataNavigateUrlField="companyId" DataNavigateUrlFormatString="../../Company/companyEntry.aspx?companyId={0}">
                <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>
                <ItemStyle HorizontalAlign="center" Width="10%" Font-Underline="true"></ItemStyle>
            </asp:HyperLinkColumn>
            <asp:BoundColumn DataField="companyNmJ" HeaderText="<%$Resources:Resources, name_japanese %>">
                <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="companyNmE" HeaderText="<%$Resources:Resources, name_english %>">
                <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="payMethod" HeaderText="<%$Resources:Resources, payment_method %>">
                <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="accNumber" HeaderText="<%$Resources:Resources, account_no %>">
                <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="bankCode" HeaderText="<%$Resources:Resources, bank_code %>">
                <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="totalPerMonth" HeaderText="<%$Resources:Resources, total_per_month %>"><%--DataFormatString="{0:N}"--%>
                <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="paymentDate" HeaderText="<%$Resources:Resources, payment_date %>" DataFormatString="{0:dd/MM/yyyy}">
                <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="address" HeaderText="<%$Resources:Resources, address %>">
                <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="payRemark" HeaderText="<%$Resources:Resources, payment_remark %>">
                <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
            </asp:BoundColumn>
        </Columns>
        <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="tbBody" Mode="NumericPages"></PagerStyle>
    </asp:DataGrid>
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
    <script type="text/javascript">
        $(function () {
            var options = {
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                autoclose: true
            }
            $("#datepicker1,[id*=appliedDateFrom]").click(function () {
                if (!$("[id*=appliedDateFrom]").prop("disabled")) {
                    $('[id*=appliedDateFrom]').datepicker(options).datepicker("show")
                }
            });
            $("#datepicker2,[id*=appliedDateTo]").click(function () {
                if (!$("[id*=appliedDateTo]").prop("disabled")) {
                    $('[id*=appliedDateTo]').datepicker(options).datepicker("show")
                }
            });
            $("#datepicker3,[id*=txtQuitDateFrom]").click(function () {
                if (!$("[id*=txtQuitDateFrom]").prop("disabled")) {
                    $('[id*=txtQuitDateFrom]').datepicker(options).datepicker("show")
                }
            });
            $("#datepicker4,[id*=txtQuitDateTo]").click(function () {
                if (!$("[id*=txtQuitDateTo]").prop("disabled")) {
                    $('[id*=txtQuitDateTo]').datepicker(options).datepicker("show")
                }
            });
            $("#datepicker5,[id*=payMethodFrom]").click(function () {
                if (!$("[id*=payMethodFrom]").prop("disabled")) {
                    $('[id*=payMethodFrom]').datepicker(options).datepicker("show")
                }
            });
            $("#datepicker6,[id*=payMethodTo]").click(function () {
                if (!$("[id*=payMethodTo]").prop("disabled")) {
                    $('[id*=payMethodTo]').datepicker(options).datepicker("show")
                }
            });
            $("#datepicker7,[id*=expiredDateFrom]").click(function () {
                if (!$("[id*=expiredDateFrom]").prop("disabled")) {
                    $('[id*=expiredDateFrom]').datepicker(options).datepicker("show")
                }
            });
            $("#datepicker8,[id*=expiredDateTo]").click(function () {
                if (!$("[id*=expiredDateTo]").prop("disabled")) {
                    $('[id*=expiredDateTo]').datepicker(options).datepicker("show")
                }
            });
        });
    </script>
    <%--<script>

    var options = {
        format: 'dd-M-yyyy',
        todayHighlight: true,
        autoclose: true
    }
    $(function () {
        $("#datepicker1").click(function () {
            $('#datepicker1Input').datepicker(options).datepicker("show")
        });

        $("#datepicker2").click(function () {
            $('#datepicker2Input').datepicker(options).datepicker("show")
        });

        $("#datepicker3").click(function () {
            $('#datepicker3Input').datepicker(options).datepicker("show")
        });

        $("#datepicker4").click(function () {
            $('#datepicker4Input').datepicker(options).datepicker("show")
        });

        $("#datepicker5").click(function () {
            $('#datepicker5Input').datepicker(options).datepicker("show")
        });

        $("#datepicker6").click(function () {
            $('#datepicker6Input').datepicker(options).datepicker("show")
        });

        $("#datepicker7").click(function () {
            $('#datepicker7Input').datepicker(options).datepicker("show")
        });

        $("#datepicker8").click(function () {
            $('#datepicker8Input').datepicker(options).datepicker("show")
        });
    });
</script>--%>
    <%--@* *********** /Datepicker *********** *@--%>
</asp:Content>
