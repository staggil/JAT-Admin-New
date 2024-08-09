<%@ Page Title="Payment Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="listPrivatePayment.aspx.cs" Inherits="JAT.Inquery.Private.listPrivatePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        table {
            table-layout: fixed;
            width: auto;
        }

        td {
            width: 250px;
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
    <div class="box" style="background-color: lightgray">
        <h3><%=Resources.Resources.view_member_payment %></h3>
        <form>
            <div class="row">
                <div class="col-md-6">
                    <div class="row">
                        <div class="form-group col-md-5">
                            <label><%=Resources.Resources.member_status %></label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="col-sm-7">
                            <asp:RadioButtonList ID="memberStatus" runat="server" RepeatDirection="horizontal" CellPadding="4" AutoPostBack="True" OnSelectedIndexChanged="memberStatus_SelectedIndexChanged">
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
                            <label><%=Resources.Resources.member_type %></label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="form-group col-md-5">
                            <asp:DropDownList ID="drplstMemberType" runat="server">
                                <asp:ListItem Text="--Any--" Value="0" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="form-group col-md-5">
                            <label><%=Resources.Resources.has_family_member %></label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="form-group col-md-5">
                            <asp:DropDownList ID="drplstHasFamily" runat="server">
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
                            <label><%=Resources.Resources.payment_type %></label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="form-group col-md-5">
                            <asp:DropDownList ID="drplstPayMethod" runat="server">
                                <asp:ListItem>-- Any --</asp:ListItem>
                                <asp:ListItem Value="Cash">Cash [K, J, P]</asp:ListItem>
                                <asp:ListItem Value="Bank">Bank [T, S, B]</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="form-group col-md-5">
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
                            <asp:DropDownList ID="drplstDuration" runat="server">
                                <asp:ListItem>-- Any --</asp:ListItem>
                                <asp:ListItem Value="1">= 6 months</asp:ListItem>
                                <asp:ListItem Value="2">&gt; 6 months</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="form-group col-md-5">
                            <label><%=Resources.Resources.member_status %></label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="col-sm-7">
                            <asp:RadioButtonList ID="rblPayAt" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="JAT" Selected="true"></asp:ListItem>
                                <asp:ListItem Value="Annex"></asp:ListItem>
                            </asp:RadioButtonList>
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
                            <input type="text" id="appliedDateFrom" style="width: inherit;" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
                            <span id="datepicker1" class="glyphicon glyphicon-calendar"></span>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="form-group col-md-5">
                            <label>To</label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="form-group col-md-7">
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
                            <input type="text" id="txtQuitDateFrom" style="width: inherit;" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
                            <span id="datepicker3" class="glyphicon glyphicon-calendar"></span>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="form-group col-md-5">
                            <label>To</label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="form-group col-md-7">
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
                            <input type="text" id="payMethodFrom" style="width: inherit;" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
                            <span id="datepicker5" class="glyphicon glyphicon-calendar"></span>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="form-group col-md-5">
                            <label>To</label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="form-group col-md-7">
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
                            <input type="text" id="expiredDateFrom" style="width: inherit;" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
                            <span id="datepicker7" class="glyphicon glyphicon-calendar"></span>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="form-group col-md-5">
                            <label>To</label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="form-group col-md-7">
                            <input type="text" id="expiredDateTo" style="width: inherit;" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
                            <span id="datepicker8" class="glyphicon glyphicon-calendar"></span>
                        </div>
                    </div>
                </div>
            </div>
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
                            <asp:CheckBoxList ID="CheckBoxList1" RepeatColumns="3" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" runat="server">
                                <asp:ListItem Value="Member Id" Text="<%$Resources:Resources, member_id %>"></asp:ListItem>
                                <asp:ListItem Value="Company Name" Text="<%$Resources:Resources, company_name %>"></asp:ListItem>
                                <asp:ListItem Value="Prefix Name" Text="<%$Resources:Resources, prefix_name %>"></asp:ListItem>
                                <asp:ListItem Value="Member Name" Text="<%$Resources:Resources, member_name %>"></asp:ListItem>
                                <asp:ListItem Value="Member Type" Text="<%$Resources:Resources, member_type %>"></asp:ListItem>
                                <asp:ListItem Value="Payment Method" Text="<%$Resources:Resources, payment_method %>"></asp:ListItem>
                                <asp:ListItem Value="Account No." Text="<%$Resources:Resources, account_no %>"></asp:ListItem>
                                <asp:ListItem Value="Bank Code" Text="<%$Resources:Resources, bank_code %>"></asp:ListItem>
                                <asp:ListItem Value="Payment Date" Text="<%$Resources:Resources, payment_date %>"></asp:ListItem>
                                <asp:ListItem Value="Pay Duration" Text="<%$Resources:Resources, pay_duration %>"></asp:ListItem>
                                <asp:ListItem Value="Send Type" Text="<%$Resources:Resources, send_type %>"></asp:ListItem>
                                <asp:ListItem Value="Company Address" Text="<%$Resources:Resources, company_address %>"></asp:ListItem>
                                <asp:ListItem Value="Company Phone" Text="<%$Resources:Resources, company_phone %>"></asp:ListItem>
                                <asp:ListItem Value="Company Fax" Text="<%$Resources:Resources, company_fax %>"></asp:ListItem>
                                <asp:ListItem Value="Home Address" Text="<%$Resources:Resources, home_address %>"></asp:ListItem>
                                <asp:ListItem Value="Home Phone" Text="<%$Resources:Resources, home_phone %>"></asp:ListItem>
                                <asp:ListItem Value="Mobile Phone" Text="<%$Resources:Resources, mobile_phone %>"></asp:ListItem>
                                <asp:ListItem Value="Member Status" Text="<%$Resources:Resources, member_status %>"></asp:ListItem>
                                <asp:ListItem Value="Family Name" Text="<%$Resources:Resources, family_name %>"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="text-center">
                    <asp:Button ID="view" OnClick="view_Click" class="btn btn-primary" runat="server" Text="View" />
                    <asp:Button ID="reset" OnClick="reset_Click" runat="server" class="btn btn-primary" Text="Reset" />
                </div>
            </div>
        </form>
    </div>
    <br />
    <br />
    <div style="max-height: 500px; overflow-y: scroll;">
        <asp:DataGrid ID="grdCompany" runat="server" CellPadding="4" BackColor="White"
            BorderWidth="1px" BorderStyle="None" BorderColor="InactiveCaptionText" PageSize="20"
            AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="grdCompany_PageIndexChanged">
            <FooterStyle ForeColor="Desktop" BackColor="#B5C7DE"></FooterStyle>
            <AlternatingItemStyle BackColor="#F7F7F7"></AlternatingItemStyle>
            <ItemStyle ForeColor="#24227A" BackColor="White"></ItemStyle>
            <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="#F7F7F7"></HeaderStyle>
            <Columns>
                <asp:TemplateColumn HeaderText="<%$Resources:Resources, member_id %>">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Font-Underline="true"></ItemStyle>
                    <ItemTemplate>
                        <asp:HyperLink runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Member Id")%>' NavigateUrl='<%# "../../Private/privateEntry.aspx?memberid=" + DataBinder.Eval(Container.DataItem,"Member Id") + "&firstMemberid=" + DataBinder.Eval(Container.DataItem,"firstMemberid")%>' ID="Hyperlink1" NAME="Hyperlink1" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, company_name %>" ReadOnly="True" DataField="Company Name">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, prefix_name %>" ReadOnly="True" DataField="Prefix Name">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, member_name %>" ReadOnly="True" DataField="Member Name">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, member_type %>" ReadOnly="True" DataField="Member Type">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, payment_method %>" ReadOnly="True" DataField="Payment Method">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, account_no %>" ReadOnly="True" DataField="Account No.">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, bank_code %>" ReadOnly="True" DataField="Bank Code">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, payment_date %>" ReadOnly="True" DataField="Payment Date" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, pay_duration %>" ReadOnly="True" DataField="Pay Duration">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, send_type %>" ReadOnly="True" DataField="Send Type">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, company_address %>" ReadOnly="True" DataField="Company Address">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, company_phone %>" ReadOnly="True" DataField="Company Phone">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, company_fax %>" ReadOnly="True" DataField="Company Fax">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, home_address %>" ReadOnly="True" DataField="Home Address">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, home_phone %>" ReadOnly="True" DataField="Home Phone">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, mobile_phone %>" ReadOnly="True" DataField="Mobile Phone">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, member_status %>" ReadOnly="True" DataField="Member Status">
                    <HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White" />
                    <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="<%$Resources:Resources, family_name %>" ReadOnly="True" DataField="Family Name">
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
</asp:Content>
