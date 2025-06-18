<%@ Page Title="Data Extraction" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="dataExtraction.aspx.cs" Inherits="JAT.PrivateReport.dataExtraction" %>

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

<div class="box" style="background-color:lightgray;">
        <h3><%=Resources.Resources.print_dataextraction_header %></h3>
        <form>
            <br/>
            <div class="row">
                <div class="col-sm-12">
                    <div class="row space">
                        <div class="form-group col-md-3">
                            <label><%=Resources.Resources.member_status %></label>
                            <span style="float:right;">:</span>
                        </div>
                        <div class="form-group col-md-8">
                            <asp:RadioButtonList ID="MembershipStatus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="A" Selected="True" Text="<%$ Resources:Resources, member%>"></asp:ListItem>
                                <asp:ListItem Value="NA" Text="<%$ Resources:Resources, no_member%>"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="inputdiv1">
                <div class="col-sm-12">
                    <div class="row space">
                        <div class="form-group col-md-3">
                            <label><%=Resources.Resources.birth_place %></label>
                            <span style="float:right;" runat="server">:</span>
                        </div>
                        <div class="form-group col-md-8">
                            <%--<asp:TextBox ID="birthplace" runat="server"></asp:TextBox>--%>
                            <asp:DropDownList ID="birthplace1" runat="server">
                                <asp:ListItem Value="" Text="" />
                                <asp:ListItem Value="北海道" Text="北海道" />
                                <asp:ListItem Value="青森" Text="青森" />
                                <asp:ListItem Value="岩手" Text="岩手" />
                                <asp:ListItem Value="宮城" Text="宮城" />
                                <asp:ListItem Value="秋田" Text="秋田" />
                                <asp:ListItem Value="山形" Text="山形" />
                                <asp:ListItem Value="福島" Text="福島" />
                                <asp:ListItem Value="茨城" Text="茨城" />
                                <asp:ListItem Value="栃木" Text="栃木" />
                                <asp:ListItem Value="群馬" Text="群馬" />
                                <asp:ListItem Value="埼玉" Text="埼玉" />
                                <asp:ListItem Value="千葉" Text="千葉" />
                                <asp:ListItem Value="東京" Text="東京" />
                                <asp:ListItem Value="神奈川" Text="神奈川" />
                                <asp:ListItem Value="新潟" Text="新潟" />
                                <asp:ListItem Value="富山" Text="富山" />
                                <asp:ListItem Value="石川" Text="石川" />
                                <asp:ListItem Value="福井" Text="福井" />
                                <asp:ListItem Value="山梨" Text="山梨" />
                                <asp:ListItem Value="長野" Text="長野" />
                                <asp:ListItem Value="岐阜" Text="岐阜" />
                                <asp:ListItem Value="静岡" Text="静岡" />
                                <asp:ListItem Value="愛知" Text="愛知" />
                                <asp:ListItem Value="三重" Text="三重" />
                                <asp:ListItem Value="滋賀" Text="滋賀" />
                                <asp:ListItem Value="京都" Text="京都" />
                                <asp:ListItem Value="大阪" Text="大阪" />
                                <asp:ListItem Value="兵庫" Text="兵庫" />
                                <asp:ListItem Value="奈良" Text="奈良" />
                                <asp:ListItem Value="和歌山" Text="和歌山" />
                                <asp:ListItem Value="鳥取" Text="鳥取" />
                                <asp:ListItem Value="島根" Text="島根" />
                                <asp:ListItem Value="岡山" Text="岡山" />
                                <asp:ListItem Value="広島" Text="広島" />
                                <asp:ListItem Value="山口" Text="山口" />
                                <asp:ListItem Value="徳島" Text="徳島" />
                                <asp:ListItem Value="香川" Text="香川" />
                                <asp:ListItem Value="愛媛" Text="愛媛" />
                                <asp:ListItem Value="高知" Text="高知" />
                                <asp:ListItem Value="福岡" Text="福岡" />
                                <asp:ListItem Value="佐賀" Text="佐賀" />
                                <asp:ListItem Value="長崎" Text="長崎" />
                                <asp:ListItem Value="熊本" Text="熊本" />
                                <asp:ListItem Value="大分" Text="大分" />
                                <asp:ListItem Value="宮崎" Text="宮崎" />
                                <asp:ListItem Value="鹿児島" Text="鹿児島" />
                                <asp:ListItem Value="沖縄" Text="沖縄" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="row space">
                        <div class="form-group col-md-3">
                            <label><%=Resources.Resources.Extraction_Period %></label>
                            <span style="float:right;">:</span>
                        </div>
                        <div class="form-group col-md-8">
                            <asp:RadioButtonList ID="ExtractionPeriod" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="ExtractionPeriod_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="Age" Selected="True" Text="<%$ Resources:Resources, age%>"></asp:ListItem>
                                <%--<asp:ListItem Value="Applied Date">&amp;nbsp; Applied Date &amp;nbsp;</asp:ListItem>--%>
                                <asp:ListItem Value="Applied Date" Text="<%$ Resources:Resources, applied_date%>"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="inputdiv2" runat="server">
                <div class="col-sm-12">
                    <div class="row space">
                        <div class="form-group col-md-3">
                            <label><%=Resources.Resources.age %></label>
                            <span style="float:right;" runat="server">:</span>
                        </div>
                        <div class="form-group col-md-8">
                            <asp:TextBox ID="age_fr" runat="server"></asp:TextBox>
                            <span> ~ </span>
                            <asp:TextBox ID="age_to" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="inputdiv3" runat="server">
                <div class="col-sm-12">
                    <div class="row space">
                        <div class="form-group col-md-3">
                            <label>Applied Date</label>
                            <span style="float:right;" runat="server">:</span>
                        </div>
                        <div class="form-group col-md-8">
                            <asp:TextBox ID="date_fr" runat="server" ClientIDMode="Static" class="datepicker1Input" placeholder="yyyy-M-dd"></asp:TextBox>
                            <span id="datepicker1" class="glyphicon glyphicon-calendar"></span>
                            <span> ~ </span>
                            <asp:TextBox ID="date_to" runat="server" ClientIDMode="Static" class="datepicker2Input" placeholder="yyyy-M-dd"></asp:TextBox>
                            <span id="datepicker2" class="glyphicon glyphicon-calendar"></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="outputdiv1" runat="server">
                <div class="col-sm-12">
                    <div class="row space">
                        <div class="form-group col-md-12">
                            <asp:CustomValidator runat="server" ID="CustomValidate1" ControlToValidate="ExtractionPeriod" OnServerValidate="CustomValidate1_ServerValidate" ErrorMessage="Please check input again"></asp:CustomValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="text-center">
                    <br/>
                    <asp:Button ID="DataExtraction" runat="server" class="btn btn-primary" Text="Data Extraction" OnClick="DataExtraction_Click"/>
                </div>
            </div>
        </form>
    </div>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
<%--@section scripts--%>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
<script>
    var options = {
        format: 'dd-mm-yyyy',
        todayHighlight: true,
        autoclose: true
    }

    var tititi;

    $(function () {
        $("#datepicker1").click(function () {
            $('.datepicker1Input').datepicker(options).datepicker("show")
        });

        $("#datepicker2").click(function () {
            $('.datepicker2Input').datepicker(options).datepicker("show")
        });
    });
</script>
</asp:Content>
