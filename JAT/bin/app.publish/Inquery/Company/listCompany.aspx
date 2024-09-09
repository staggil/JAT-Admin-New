<%@ Page Title="Company Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="listCompany.aspx.cs" Inherits="JAT.Inquery.Company.listCompany" %>

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
        td {
            width: 250px;
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

        .space {
            margin-left: 20px;
        }

        input#datepicker1Input, input#datepicker2Input, input#datepicker3Input, input#datepicker4Input {
            width: inherit;
        }
    </style>
    <br />
    <br />
    <br />
    <div class="box" style="background-color: lightgray;">
        <h3><%=Resources.Resources.view_company_payment %></h3>
        <%--@* ************************************************* 1 ***************************************************** *@--%>
        <div class="row">
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-5">
                        <label><%=Resources.Resources.company_name %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-7">
                        <input type="text" id="companyNmE" class="form-control" runat="server" autocomplete="off">
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-5">
                        <label><%=Resources.Resources.member_status %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="col-sm-7">
                        <asp:RadioButtonList ID="memberStatus" runat="server" RepeatDirection="horizontal" CellPadding="4">
                            <asp:ListItem Value="A" Text="A"></asp:ListItem>
                            <asp:ListItem Value="NA" Text="NA"></asp:ListItem>
                            <asp:ListItem Value="Both" Text="<%$Resources:Resources, both %>" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-4">
                        <label><%=Resources.Resources.telephone %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-5">
                        <select id="drplstHasPhone" runat="server">
                            <option value="" selected>-- Any --</option>
                            <option value="1">Yes</option>
                            <option value="2">No</option>
                        </select>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-5">
                        <label><%=Resources.Resources.company_in_bangkok %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-5">
                        <select id="drplstInBkk" runat="server">
                            <option value="" selected>-- Any --</option>
                            <option value="1">Yes</option>
                            <option value="2">No</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-4">
                        <label><%=Resources.Resources.send_method %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-5">
                        <select id="sendType" runat="server">
                            <option selected>-- Any --</option>
                            <option>#</option>
                            <option>Z</option>
                        </select>
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
                        <%--<input required type="text" id="datepicker1Input" autocomplete="off"   placeholder="yyyy/mm/dd" readonly="readonly" readonly="readonly">
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
                        <%--<input required type="text" id="datepicker2Input" autocomplete="off"   placeholder="yyyy/mm/dd" readonly="readonly" readonly="readonly">
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
                        <%--<input required type="text" id="datepicker3Input" autocomplete="off"   placeholder="yyyy/mm/dd" readonly="readonly" readonly="readonly">
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
                        <%--<input required type="text" id="datepicker4Input" autocomplete="off"   placeholder="yyyy/mm/dd" readonly="readonly" readonly="readonly">
                        <span id="datepicker4" class="glyphicon glyphicon-calendar"></span>--%>
                        <input type="text" id="txtQuitDateTo" style="width: inherit;" autocomplete="off"  placeholder="dd/mm/yyyy" readonly="readonly" runat="server">
                        <span id="datepicker4" class="glyphicon glyphicon-calendar"></span>
                    </div>
                </div>
            </div>
        </div>
        <%--@* ************************************************* 2 ***************************************************** *@--%>

        <div class="row">
            <div class="col-md-6">
                <div class="row">
                    <div class="form-group col-md-5">
                        <label><%=Resources.Resources.address %></label>
                        <span style="float: right;">:</span>
                    </div>
                    <div class="form-group col-md-7">
                        <input id="address" type="text" class="form-control" runat="server">
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
                        <asp:RadioButtonList ID="rblFieldFormat" runat="server" OnSelectedIndexChanged="rblFieldFormat_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                            <asp:ListItem Value="All" Selected="True" Text="<%$Resources:Resources, all %>"></asp:ListItem>
                            <asp:ListItem Value="Selection" Text="<%$Resources:Resources, selection %>"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <label>&nbsp</label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <label>&nbsp</label>
            </div>
            <div class="col-md-10">
                <div class="form-group col-md-12">
                    <div class="form-check" style="margin-left: 20px">
                        <asp:CheckBoxList ID="CheckBoxList1" RepeatColumns="3" AutoPostBack="true" CssClass="chkBoxList" Enabled="false" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged" runat="server">
                            <asp:ListItem Value="Id"></asp:ListItem>
                            <asp:ListItem Value="Name (Japanese)" Text="<%$Resources:Resources, name_japanese %>"></asp:ListItem>
                            <asp:ListItem Value="Name (English)" Text="<%$Resources:Resources, name_english %>"></asp:ListItem>
                            <asp:ListItem Value="Name (Remark)" Text="<%$Resources:Resources, name_remark %>"></asp:ListItem>
                            <asp:ListItem Value="Business Type" Text="<%$Resources:Resources, business_type %>"></asp:ListItem>
                            <asp:ListItem Value="Applied Date" Text="<%$Resources:Resources, applied_date %>"></asp:ListItem>
                            <asp:ListItem Value="Established Date" Text="<%$Resources:Resources, established _date %>"></asp:ListItem>
                            <asp:ListItem Value="Member Status" Text="<%$Resources:Resources, member_status %>"></asp:ListItem>
                            <asp:ListItem Value="Send Type" Text="<%$Resources:Resources, send_type %>"></asp:ListItem>
                            <asp:ListItem Value="Address" Text="<%$Resources:Resources, address %>"></asp:ListItem>
                            <asp:ListItem Value="Phone" Text="<%$Resources:Resources, phone %>"></asp:ListItem>
                            <asp:ListItem Value="Fax" Text="<%$Resources:Resources, fax %>"></asp:ListItem>
                            <asp:ListItem Value="E-Mail" Text="<%$Resources:Resources, e_mail %>"></asp:ListItem>
                            <asp:ListItem Value="Represent Name (Jpn)" Text="<%$Resources:Resources, represent_name_jpn %>"></asp:ListItem>
                            <asp:ListItem Value="Represent Name (Eng)" Text="<%$Resources:Resources, represent_name_eng %>"></asp:ListItem>
                            <asp:ListItem Value="Represent Position" Text="<%$Resources:Resources, represent_position %>"></asp:ListItem>
                            <asp:ListItem Value="Contract Name" Text="<%$Resources:Resources, contract_name %>"></asp:ListItem>
                            <asp:ListItem Value="Contract Position" Text="<%$Resources:Resources, contract_position %>"></asp:ListItem>
                            <asp:ListItem Value=" Remark" Text="<%$Resources:Resources, remark %>"></asp:ListItem>
                            <asp:ListItem Value="Payment Method" Text="<%$Resources:Resources, payment_method %>"></asp:ListItem>
                            <asp:ListItem Value=" Payment Period" Text="<%$Resources:Resources, payment_period %>"></asp:ListItem>
                            <asp:ListItem Value="Payment Duration" Text="<%$Resources:Resources, payment_duration %>"></asp:ListItem>
                            <asp:ListItem Value="Get Invoice" Text="<%$Resources:Resources, get_invoice %>"></asp:ListItem>
                            <asp:ListItem Value="Get WHT" Text="<%$Resources:Resources, get_wht %>"></asp:ListItem>
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
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center" Font-Underline="true"></ItemStyle>
                </asp:HyperLinkColumn>
                <asp:BoundColumn DataField="companyNmJ" HeaderText="<%$Resources:Resources, name_japanese %>" >
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="companyNmE" HeaderText="<%$Resources:Resources, name_english %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="companyNmE" HeaderText="<%$Resources:Resources, name_remark %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="busType" HeaderText="<%$Resources:Resources, business_type %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="appliedDate" HeaderText="<%$Resources:Resources, applied_date %>" DataFormatString="{0:dd/MM/yyyy}">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="establishedDate" HeaderText="<%$Resources:Resources, established _date %>" DataFormatString="{0:dd/MM/yyyy}">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="memberStatus" HeaderText="<%$Resources:Resources, member_status %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>

                <asp:BoundColumn DataField="sendType" HeaderText="<%$Resources:Resources, send_type %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="address" HeaderText="<%$Resources:Resources, address %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="phone" HeaderText="<%$Resources:Resources, phone %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="fax" HeaderText="<%$Resources:Resources, fax %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="email" HeaderText="<%$Resources:Resources, e_mail %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="represNm" HeaderText="<%$Resources:Resources, represent_name_jpn %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="represNmE" HeaderText="<%$Resources:Resources, represent_name_eng %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="represPosition" HeaderText="<%$Resources:Resources, represent_position %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="companyNmE" HeaderText="<%$Resources:Resources, contract_name %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="companyNmE" HeaderText="<%$Resources:Resources, contract_position %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="remark" HeaderText="<%$Resources:Resources, remark %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="payMethod" HeaderText="<%$Resources:Resources, payment_method %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="payPeriod" HeaderText="<%$Resources:Resources, payment_period %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="payDuration" HeaderText="<%$Resources:Resources, payment_duration %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="getInvoice" HeaderText="<%$Resources:Resources, get_invoice %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="withHolding" HeaderText="<%$Resources:Resources, get_wht %>">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="updatedDate" HeaderText="<%$Resources:Resources, update_date %>" DataFormatString="{0:dd-MMM-yyyy}">
                    <%--<HeaderStyle HorizontalAlign="center" Height="50px" BackColor="#24227A" ForeColor="White"></HeaderStyle>--%>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF" CssClass="tbBody" Mode="NumericPages"></PagerStyle>
        </asp:DataGrid>
    </div>



    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />

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
    });
</script>--%>
    <%--@* *********** /Datepicker *********** *@--%>
</asp:Content>
