<%@ Page Title="Private EntryPayment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="privateEntryPayment.aspx.cs" Inherits="JAT.Private.privateEntryPayment" EnableViewState="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        h3 {
            font-weight: bold;
        }

        textarea {
            border-radius: 5px;
            border: 1px solid #556677;
        }

        input.datepicker1Input, input.datepicker2Input, input.datepicker3Input, input.datepicker4Input, input.datepicker5Input, input.datepicker6Input,
        input#EffectiveDateInput, input.datepicker8Input, input.datepicker9Input {
            width: inherit;
        }

        .form-control {
            height: 1.5em;
            padding: 7px 5px;
        }

        .form-group {
            margin-bottom: 10px;
            padding-right: 0px
        }

        .space {
            margin-left: 20px;
        }

        .box {
            display: block;
            margin: auto;
            /*width: 95%;*/
            align-self: center;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 20px;
            box-shadow: 3px 3px 20px rgba(0, 0, 0, 0.1);
        }

        .tab {
            /*overflow: hidden;*/
            border: 0px solid #ccc;
            border-top: 1px solid #ccc;
            background-color: #f1f1f1;
        }

            /* Style the buttons inside the tab */
            .tab button {
                background-color: inherit;
                /*float: inherit;*/
                border: none;
                outline: none;
                cursor: pointer;
                /*padding: 10px 22.4px;*/
                padding: 15px;
                transition: 0.3s;
                font-size: 16px;
            }

                /* Change background color of buttons on hover */
                .tab button:hover {
                    background-color: #ddd;
                }

                /* Create an active/current tablink class */
                .tab button.active {
                    background-color: #ffffff;
                }

        /* Style the tab content */
        .tabcontent {
            display: none;
            padding: 6px 12px;
            /*border: 1px solid #ccc;*/
            border-top: none;
        }

        /*Hide Input number arrow*/
        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            /* display: none; <- Crashes Chrome on hover */
            -webkit-appearance: none;
            margin: 0; /* <-- Apparently some margin are still there even though it's hidden */
        }

        input[type=number] {
            -moz-appearance: textfield; /* Firefox */
        }

        /*pop up send history*/
        .modal-1 {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }

        /* Modal Content */
        .modal-content-1 {
            background-color: #fefefe;
            margin-left: auto;
            margin-right: 12px;
            padding: 20px;
            border: 1px solid #888;
            width: 80%;
        }
    </style>

    <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>


    <div class="col-sm-12">
        </br>
    <%--@*<h1>&nbsp;Private Entry</h1>*@--%>
        <div class="tab">
            <%--@*<button lang="eng" class="tablinks active" onclick="changeTAB(event, 'MemberTab')">@Html.ActionLink(JAT.Languages.Index_PrivateEntry_ENG.tab1)</button>*@--%>
            <%--@*<button lang="eng" class="tablinks active" onclick="changeTAB(event, 'MemberTab')">@Html.ActionLink(JAT.Languages.Index_PrivateEntry.tab1, "index_PrivateEntry")</button>*@--%>
            <button class="tablinks">
                <asp:LinkButton ID="memberTab" Text="<%$Resources:Resources,member_information %>" runat="server" OnClick="memberTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="familyTab" Text="<%$Resources:Resources,family_member %>" runat="server" OnClick="familyTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="ChildrenTab" Text="<%$Resources:Resources,children %>" runat="server" OnClick="ChildrenTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks active">
                <asp:LinkButton ID="paymentTab" Text="<%$Resources:Resources,payment_management %>" runat="server"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="cancelTab" Text="<%$Resources:Resources, cancel_member %>" runat="server" OnClick="cancelTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="specialTab" Text="<%$Resources:Resources,special_feature %>" runat="server" OnClick="specialTab_Click"></asp:LinkButton>
            </button>
        </div>
        <div style="float: right;">
            <%--<asp:Button ID="addBTN" runat="server" Text="Add" OnClick="addBTN_Click" />--%>
            <%--<button lang="eng" id="hideAddBtn" class="tablinks" onclick="addData()">ADD</button>--%>
            <%--@*<button lang="jpn" id="hideAddBtn" class="tablinks" onclick="addData()">新規</button>*@--%>
            <label>Last Editor: </label>
            <asp:Label ID="updateBy" Text="" runat="server" />
            <asp:Button ID="addBTN" runat="server" Text="Add" OnClick="addBTN_Click" />
            <br />
            <br />
        </div>


        <%--@*PaymentTab*@--%>
        <div>
            <div class="box">
                <br />
                <div class="box" style="background-color: lightgray">
                    <h3 lang="eng"><%=Resources.Resources.payment_management %></h3>
                    <%--@*<h3 lang="jpn">支払い管理</h3>*@--%>
                    <form>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group col-md-3">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.first_member_name%></label>
                                        <%--@*<label lang="jpn">主なメンバー名前</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-8">
                                        <b>
                                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label></b>
                                        <b>[<asp:Label ID="Label2" runat="server" Text=""></asp:Label>]</b>
                                        <b>&nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text=""></asp:Label></b>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <br>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red;">*</label>
                                        <label lang="eng"><%=Resources.Resources.pay_by%></label>
                                        <%--@*<label lang="jpn">Pay By</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <asp:DropDownList ID="DropDownList1" runat="server">
                                            <%--<asp:ListItem>-- Any --</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red;">*</label>
                                        <label lang="eng"><%=Resources.Resources.member_type%></label>
                                        <%--@*<label lang="jpn">メンバータイプ</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <asp:DropDownList ID="DropDownList2" runat="server" class="memTypeOption" AutoPostBack="true" OnSelectedIndexChanged="calculateFee">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red;">*</label>
                                        <label lang="eng"><%=Resources.Resources.number_of_member%></label>
                                        <%--@*<label lang="jpn">メンバーの数</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <asp:TextBox ID="numMemBox" ClientIDMode="Static" CssClass="form-control" OnTextChanged="calculateFee" AutoPostBack="true" runat="server" AutoCompleteType="Disabled" ></asp:TextBox>
                                        <%--<input runat="server" name="NumberofMemberVal"  type="number" class="form-control" AutoPostBack="false">--%>
                                        <%--<input id="numMemBox" runat="server" name="NumberofMemberVal"  type="number" class="form-control" onblur="testOnClick()"/>--%>
                                    </div>
                                </div>
                                <asp:RangeValidator runat="server" Type="Integer" MinimumValue="0" MaximumValue="32767" ControlToValidate="numMemBox" ErrorMessage="Value must be a whole number between 0 and 32,767" />
                            </div>
                            <div class="col-md-5">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red;">*</label>
                                        <label lang="eng" class=""><%=Resources.Resources.payment_type%></label>
                                        <%--@*<label lang="jpn" for="inputEmail3" class="">支払いタイプ</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>

                                    <div class="form-group col-md-5">
                                        <asp:RadioButtonList ID="PaymentType" OnSelectedIndexChanged="calculateFee" runat="server" AutoPostBack="true">
                                            <asp:ListItem Value="Entrance Fee">&amp;nbsp;Entrance Fee</asp:ListItem>
                                            <asp:ListItem Value="Annual Fee">&amp;nbsp;Annual Fee</asp:ListItem>
                                            <asp:ListItem Value="Both" Selected="True">&amp;nbsp;Both</asp:ListItem>
                                            <asp:ListItem Value="No Pay">&amp;nbsp;No Pay</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red;">*</label>
                                        <label lang="eng"><%=Resources.Resources.pay_duration%></label>
                                        <%--@*<label lang="jpn">月数</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <asp:TextBox ID="PayDuration" runat="server" ClientIDMode="Static" AutoCompleteType="Disabled" CssClass="form-control" OnTextChanged="calculateFee" AutoPostBack="true" onchange="setExpireDate();"></asp:TextBox>
                                        <%--<input id="PayDuration" runat="server" name="PayDurationVal" type="number" class="form-control">--%>
                                        <%--<asp:TextBox ID="PayDuration" runat="server"></asp:TextBox>--%>
                                    </div>
                                    <label lang="eng"><%=Resources.Resources.months%></label>
                                    <%--@*<label lang="jpn">&nbsp ヶ月</label>*@--%>
                                </div>
                                <asp:RangeValidator runat="server" Type="Integer" MinimumValue="0" MaximumValue="32767" ControlToValidate="PayDuration" ErrorMessage="Value must be a whole number between 0 and 32,767" />
                            </div>
                            <div class="col-md-5">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.entrance_fee%></label>
                                        <%-- @*<label lang="jpn">入学金</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <asp:TextBox ReadOnly="true" ID="entranceVal" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                        <%--<input id="entranceVal" runat="server" type="number" class="form-control" placeholder="">--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red;">*</label>
                                        <label lang="eng"><%=Resources.Resources.payment_date %></label>
                                        <%--@*<label lang="jpn">支払い日</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <input type="text" id="paymentDateBox" runat="server" class="datepicker6Input" placeholder="dd/mm/yyyy" autocomplete="off" readonly="readonly">
                                        <span id="datepicker6" class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.annual_fee%></label>
                                        <%--@*<label lang="jpn">年会費</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <asp:TextBox ReadOnly="true" ID="annualVal" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                        <%--<input id="annualVal" runat="server" type="number" class="form-control" placeholder="">--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red;">*</label>
                                        <label lang="eng"><%=Resources.Resources.effective_date %></label>
                                        <%--@*<label lang="jpn">有効日</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <%--<input type="text" id="EffectiveDateInput" onChange="setExpireDate();" class="" placeholder="dd-mm-yyyy" autocomplete="off">
                                <span id="EffectiveDate" class="glyphicon glyphicon-calendar"></span>--%>
                                        <input type="text" id="EffectiveDateInput" runat="server" class="datepicker4Input" placeholder="dd/mm/yyyy" autocomplete="off" onchange="setExpireDate();" readonly="readonly">
                                        <span id="datepicker4" class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.total_fee%></label>
                                        <%--@*<label lang="jpn">有効日</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <asp:TextBox ReadOnly="true" ID="totolSum" runat="server" ClientIDMode="Static" CssClass="form-control" AutoPostBack="false" AutoCompleteType="Disabled"></asp:TextBox>
                                        <%--<input readonly id="totolSum" runat="server" class="form-control" placeholder="">--%>
                                    </div>
                                    <label lang="eng">&nbsp Baht</label>
                                    <%--@*<label lang="jpn">&nbsp バーツ</label>*@--%>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.expired_date_from %></label>
                                        <%--@*<label lang="jpn">満期の期日</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <%--<input readonly type="text" id="ExpiredDate" class="form-control" name="" placeholder="">--%>
                                        <%--<input readonly runat="server" id="ExpiredDate" class="form-control" AutoPostBack="false" >--%>
                                        <asp:TextBox ReadOnly="true" ID="ExpiredDate" runat="server" CssClass="datepicker2Input" placeholder="dd/mm/yyyy" autocomplete="off" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenExpiredDate" runat="server" />
                                        <asp:HiddenField ID="HiddenTranId" runat="server" />
                                        <%--<input type="text" id="ExpiredDate" class="datepicker2Input" placeholder="dd/mm/yyyy" autocomplete="off" disabled="disabled">--%>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.remark %></label>
                                        <%--@*<label lang="jpn">備考</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <input id="remarkBox" runat="server" type="text" class="form-control" name="" placeholder="" maxlength="50">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red;">*</label>
                                        <label lang="eng"><%=Resources.Resources.payment_method %></label>
                                        <%--@*<label lang="jpn">支払い方法</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <asp:DropDownList ID="DropDownList3" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem>-- Any --</asp:ListItem>
                                            <asp:ListItem>B</asp:ListItem>
                                            <asp:ListItem>F</asp:ListItem>
                                            <asp:ListItem>J</asp:ListItem>
                                            <asp:ListItem>K</asp:ListItem>
                                            <asp:ListItem>P</asp:ListItem>
                                            <asp:ListItem>S</asp:ListItem>
                                            <asp:ListItem>T</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <fieldset>

                                            <div class="1">
                                                <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select..." Value="No selection made"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="1">
                                                <asp:DropDownList ID="DropDownList6" runat="server">
                                                    <asp:ListItem Text="Select..." Value="No selection made"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </fieldset>
                                    </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.bank_account %></label>
                                        <%--@*<label lang="jpn">支払い方法</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <asp:DropDownList ID="DropDownList4" runat="server" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged1">
                                            <%--<asp:ListItem>-- Any --</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <input id="bankPanel" runat="server" type="text" class="form-control" name="" placeholder="" style="width: 145%">
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label>&nbsp</label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <div class="form-check">
                                            <asp:CheckBox ID="GetReceiptChk" runat="server" OnCheckedChanged="GetReceiptChk_CheckedChanged" AutoPostBack="true" />
                                            <label lang="eng" for="vehicle1"><%=Resources.Resources.get_receipt %></label><br>
                                            <%--@*<label lang="jpn" for="vehicle1">領収書</label><br>*@--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.bank_code %></label>
                                        <%--@*<label lang="jpn">銀行 コード</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="col-md-4">
                                        <input id="bankCodeBox" runat="server" type="text" class="form-control" disabled="disabled">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label><%=Resources.Resources.c_no %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <input id="cNoBox" runat="server" name="receiptOption" type="text" class="form-control" disabled="disabled">
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng" for="inputEmail3" class=""><%=Resources.Resources.pay_period %></label>
                                        <%-- @*<label lang="jpn" for="inputEmail3" class="">Pay期間</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>

                                    <div class=" form-group col-sm-5">
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                            <asp:ListItem Value="1" Text="<%$Resources:Resources,1_month %>"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="<%$Resources:Resources,6_month %>"></asp:ListItem>
                                            <asp:ListItem Value="12" Text="<%$Resources:Resources,1_year %>"></asp:ListItem>
                                        </asp:RadioButtonList>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label>&nbsp</label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <div class="form-check">
                                            <%--<input id="blockInput"  type="checkbox" name="shortChk" value="" onclick="shortChoose()">--%>
                                            <asp:CheckBox ID="CheckShort" runat="server" OnCheckedChanged="CheckShort_CheckedChanged" AutoPostBack="true" />
                                            <label lang="eng" for="vehicle1"><%=Resources.Resources.get_short %></label><br>
                                            <%--@*<label lang="jpn" for="vehicle1">短い</label><br>*@--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng">Pay At</label>
                                        <%--@*<label lang="jpn">備考</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <asp:DropDownList ID="ddpayat" runat="server">
                                            <asp:ListItem>-- Any --</asp:ListItem>
                                            <asp:ListItem>Sathorn</asp:ListItem>
                                            <asp:ListItem>Annex</asp:ListItem>
                                            <asp:ListItem>Rec.</asp:ListItem>
                                            <asp:ListItem>Transfer</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label><%=Resources.Resources.short_from %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <input type="text" id="ShortFromBox" runat="server" class="datepicker8Input" placeholder="dd/mm/yyyy" autocomplete="off" disabled="disabled">
                                        <span id="datepicker8" class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label><%=Resources.Resources.short_to %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <input type="text" id="ShortToBox" runat="server" class="datepicker9Input" placeholder="dd/mm/yyyy" autocomplete="off" disabled="disabled">
                                        <span id="datepicker9" class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="margin-top: 20px">
                            <div class="text-center">
                                <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
                                <asp:Button ID="update" runat="server" Text="update" type="submit" class="btn btn-primary" OnClientClick="ConfirmEdit()" OnClick="update_Click" />
                                <asp:Button ID="saveBtn" runat="server" Text="Save" type="submit" class="btn btn-primary" OnClientClick="ConfirmSave()" OnClick="saveBtn_Click" />
                                <asp:Button ID="cancelBtn" runat="server" Text="Cancel" type="submit" class="btn btn-primary" CausesValidation="false" OnClick="cancelBtn_Click" />
                            </div>
                        </div>
                    </form>
                </div>

                <br />
                <br />
                <div style="max-height: 500px; overflow-y: scroll;">
                    <asp:DataGrid ID="GridView1" CssClass="Grid" AutoGenerateColumns="false" OnItemCommand="GridView1_ItemCommand" runat="server" CellPadding="4" BackColor="White" BorderWidth="1px"
                        DataKeyField="" GridLines="Vertical"
                        BorderStyle="None" BorderColor="InactiveCaptionText" PageSize="20" Width="100%">
                        <FooterStyle ForeColor="Desktop" BackColor="#B5C7DE"></FooterStyle>
                        <SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
                        <AlternatingItemStyle BackColor="#DCDCDC"></AlternatingItemStyle>
                        <ItemStyle ForeColor="Desktop" BackColor="White"></ItemStyle>
                        <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#24227A"></HeaderStyle>
                        <Columns>
                            <%--<asp:BoundColumn DataField="tranId" Visible="true" HeaderText="">
                <HeaderStyle HorizontalAlign="center" ></HeaderStyle>
                <ItemStyle HorizontalAlign="center"></ItemStyle>
            </asp:BoundColumn>--%>
                            <asp:BoundColumn DataField="payBy" HeaderText="<%$Resources:Resources,pay_by %>">
                                <HeaderStyle HorizontalAlign="center" Width="30px"></HeaderStyle>
                                <ItemStyle ForeColor="#24227A"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="nameE" HeaderText="<%$Resources:Resources,name_eng %>">
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" ForeColor="#24227A"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="paymentDate" HeaderText="<%$Resources:Resources,payment_date %>" DataFormatString="{0:dd-MMM-yyyy}">
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="effectiveDate" HeaderText="<%$Resources:Resources,effective_date %>" DataFormatString="{0:dd-MMM-yyyy}">
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="expireDate" HeaderText="<%$Resources:Resources,expired_date %>" DataFormatString="{0:dd-MMM-yyyy}">
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="payMethod" HeaderText="<%$Resources:Resources,payment_method %>">
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" ForeColor="#24227A"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="bankcode" HeaderText="<%$Resources:Resources,bank_code %>">
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="accno" HeaderText="<%$Resources:Resources,bank_account %>">
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="receiptNo" HeaderText="<%$Resources:Resources,c_no %>">
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="payNoMember" HeaderText="<%$Resources:Resources,number_of_member %>">
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="entranceFee" HeaderText="<%$Resources:Resources,entrance_fee %>">
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="payDuration" HeaderText="<%$Resources:Resources,pay_duration %>">
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="newsletterFee" HeaderText="<%$Resources:Resources,news_fee %>">
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="payRemark" HeaderText="<%$Resources:Resources,remark %>">
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
                            </asp:BoundColumn>
<%--                            <asp:BoundColumn DataField="accId" HeaderText="accId">
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                            </asp:BoundColumn>--%>
                            <asp:BoundColumn DataField="payat" HeaderText="Pay At">
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn ItemStyle-HorizontalAlign="center" HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btntrandedit" ImageUrl="../images/icon-pencil.gif" CommandArgument='<%#Eval("tranId")+","+ Eval("payMethod")+","+ Eval("accId")%>' CommandName="edit" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn ItemStyle-HorizontalAlign="center" HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btntranddelete" ImageUrl="../images/icon-delete.gif" CommandArgument='<%#Eval("tranId")+","+ Eval("payMethod")+","+ Eval("accId")%>' CommandName="delete" runat="server" OnClientClick="ConfirmDelete()"/>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>


            </div>
        </div>

        <%--<script language="javascript" type="text/javascript">
    function fnConfirmDelete() {
        return confirm("Are you sure you want to delete this child?");
    }
</script>--%>

        <script type="text/javascript" src="../Scripts/dateInputProc.js"></script>
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
        <%--@section scripts{--%>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
        <%--@* *********** Calculation Date*********** *@--%>
        <script>
            function setExpireDate() {
                //var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
                var months = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
                const months3 = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
                var effectiveDate = $("[id*=EffectiveDateInput]").val();
                var myDate = new Date(effectiveDate.toString().split('/')[1] + "/" + effectiveDate.toString().split('/')[0] + "/" + effectiveDate.toString().split('/')[2]);//tostring?
                var month = myDate.getMonth();
               var modifiedMonth = month + parseInt(PayDuration.value);
                var modifiedDate = new Date(myDate.getFullYear(), modifiedMonth, 0);
                modifiedMonth = months[modifiedDate.getMonth() + 0];
                 
                var fulldate = modifiedDate.getDate() + "/" + modifiedMonth + "/" + modifiedDate.getFullYear();
                var curmonth = months[modifiedDate.getMonth() + 0];
                if (modifiedDate.getDate() + "-" + modifiedMonth + "-" + modifiedDate.getFullYear() != "NaN-undefined-NaN") {
                    $("[id*=<%=ExpiredDate.ClientID%>]").val(modifiedDate.getDate() + "/" + curmonth + "/" + modifiedDate.getFullYear());
                    $("[id*=<%=HiddenExpiredDate.ClientID%>]").val(modifiedDate.getDate() + "/" + curmonth + "/" + modifiedDate.getFullYear());
                }

            }
        </script>
        <%--@* *********** /Calculation Date*********** *@--%>

        <script type="text/javascript">
            function ConfirmSave() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to save your changes?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        </script>
        <script type="text/javascript">
            function ConfirmEdit() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to save your changes?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        </script>
        <script type="text/javascript">
            function ConfirmDelete() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to delete this payment?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        </script>
        <%--@* *********** Datepicker *********** *@--%>
        <script>
            //$(document).ready(function () {
            //    $(".datepicker4Input").change(function () {
            //        var months = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
            //        var effectiveDate = $(this).val();
            //        var myDate = new Date(effectiveDate.toString().split('/')[2] + "/" + effectiveDate.toString().split('/')[1] + "/" + effectiveDate.toString().split('/')[0]);
            //        var lastDay = new Date(myDate.getFullYear(), myDate.getMonth() + 1, 0);
            //        var modifiedMonth = months[lastDay.getMonth()];
            //        var lastDayWithSlashes = (lastDay.getDate()) + '/' + modifiedMonth + '/' + lastDay.getFullYear();
            //        $("[id*=EffectiveDateInput]").val(lastDayWithSlashes);
            //    });
            //});

        </script>
        <script>
            var options = {

            /*format: 'yyyy/mm/dd',*/
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                autoclose: true,
                endDate: new Date('9999-12-31')
            }
            $(function () {

                $("#datepicker1").click(function () {
                    $('.datepicker1Input').datepicker(options).datepicker("show")
                });
                $("#datepicker2").click(function () {
                    $('.datepicker2Input').datepicker(options).datepicker("show")
                });
                $("#datepicker3").click(function () {
                    $('.datepicker3Input').datepicker(options).datepicker("show")
                });
                $("#datepicker4,.datepicker4Input").click(function () {
                    //$('.datepicker4Input').datepicker(options).datepicker("show")
                    if (!$(".datepicker4Input").prop("disabled")) {
                        $('.datepicker4Input').datepicker(options).datepicker("show")
                    }
                });
                $("#datepicker5").click(function () {
                    $('.datepicker5Input').datepicker(options).datepicker("show")
                });
                $("#datepicker6,.datepicker6Input").click(function () {
                    //$('.datepicker6Input').datepicker(options).datepicker("show")
                    if (!$(".datepicker6Input").prop("disabled")) {
                        $('.datepicker6Input').datepicker(options).datepicker("show")
                    }
                });
                $("#EffectiveDate").click(function () {
                    $('#EffectiveDateInput').datepicker(options).datepicker("show")
                });
                $("#datepicker8,.datepicker8Input").click(function () {
                    //$('.datepicker8Input').datepicker(options).datepicker("show")
                    if (!$(".datepicker8Input").prop("disabled")) {
                        $('.datepicker8Input').datepicker(options).datepicker("show")
                    }
                });
                $("#datepicker9,.datepicker9Input").click(function () {
                    //$('.datepicker9Input').datepicker(options).datepicker("show")
                    if (!$(".datepicker9Input").prop("disabled")) {
                        $('.datepicker9Input').datepicker(options).datepicker("show")
                    }
                });
                $(".datepicker4Input").on('input', dateInputProc);
                $(".datepicker6Input").on('input', dateInputProc);
                $(".datepicker8Input").on('input', dateInputProc);
                $(".datepicker9Input").on('input', dateInputProc);
            });
        </script>
        <%--@* *********** /Datepicker *********** *@--%>
        <%--<script>
            $(document).ready(function () {
                $("[id*=paymentDateBox]").change(function () {
                    var paymentDateBox = $(this).val();
                    var myDate = new Date(paymentDateBox);
                    month = myDate.getMonth();
                    alert(month);
                }
            });

        </script>--%>
        <%--}--%>
        <script>

            // input type text only number
            function numberOnly(evt) {
                var theEvent = evt || window.event;

                // Handle paste
                if (theEvent.type === 'paste') {
                    key = event.clipboardData.getData('text/plain');
                } else {
                    // Handle key press
                    var key = theEvent.keyCode || theEvent.which;
                    key = String.fromCharCode(key);
                }
                var regex = /[0-9]|\./;
                if (!regex.test(key)) {
                    theEvent.returnValue = false;
                    if (theEvent.preventDefault) theEvent.preventDefault();
                }
            }

            function countChar(val) {
                var len = val.value.length;
                if (len >= 501) {
                    val.value = val.value.substring(0, 500);
                } else {
                    $('#charNum').text(500 - len + '/500');
                }
            };



            var hideSaveBTN = document.querySelectorAll("[id='saveBtn']");
            $(hideSaveBTN).removeAttr("style").hide();
            var hideSaveBTNJP = document.querySelectorAll("[id='saveBtnJP']");

            var hideCancelBTN = document.querySelectorAll("[id='cancelBtn']");
            $(hideCancelBTN).removeAttr("style").hide();
            var hideCancelBTNJP = document.querySelectorAll("[id='cancelBtnJP']");

            var hideProcessBTN = document.querySelectorAll("[id='processBtn']");
            $(hideProcessBTN).removeAttr("style").hide();
            var hideProcessBTNJP = document.querySelectorAll("[id='processBtnJP']");


            function addData() {


                //$("#blockInput").prop("disabled", false);
                //$('#x').find('input, textarea, button, select').prop("disabled", false);

                //$(".trigger").click(function (e) {
                //    console.log(this.title);
                //     e.stopPropagation();
                //});

                //this time
                localStorage.setItem("selectedLang", "eng");


                $(canType).prop("disabled", false);
                $(hideAdd).hide();
                //document.getElementById("hideAddBtn").style.display = "none";

                var payDurationBOX = document.querySelectorAll("[id='PayDuration']");
                $(payDurationBOX).prop("disabled", false);
                var effectiveDateBOX = document.querySelectorAll("[id='EffectiveDate']");
                $(effectiveDateBOX).prop("disabled", false);


                if (localStorage.getItem('selectedLang') == 'jp') {
                    $(hideSaveBTNJP).removeAttr("style").show();
                    $(hideCancelBTNJP).removeAttr("style").show();
                    $(hideProcessBTNJP).removeAttr("style").show();

                }
                else if (localStorage.getItem('selectedLang') == 'eng') {
                    $(hideSaveBTN).removeAttr("style").show();
                    $(hideCancelBTN).removeAttr("style").show();
                    $(hideProcessBTN).removeAttr("style").show();
                }

            }

            function cancelInput() {
                location.reload();
            }

            // Get the modal
            var modal = document.getElementById("SendHistoryBox");

            // Get the button that opens the modal
            var btn = document.getElementById("openSendHistory");


            function openModal() {
                modal.style.display = "block";
            }


            function closeModal() {
                modal.style.display = "none";
            }

            //Payment choice
            //var hideEntrancePay = document.querySelectorAll("[id='entranceVal']");
            //var hideAnnuaPay = document.querySelectorAll("[id='annualVal']");
            //$(hideEntrancePay).prop("disabled", true);
            //$(hideAnnuaPay).prop("disabled", true);
            var paymentchoic = document.getElementsByName('paymentchoic');



            // for multiply member family to entrance fee control by member type
            var entranceTotal;
            var annualTotal;

            var start_numMemVal;

            var memTypeValue;

            // Number of Member order by Member Type
            $(document).ready(function () {
                $("select.memTypeOption").on("change keyup paste", function () {
                    var selectedmemTypeOption = $(this).children("option:selected").val();
                    $("input[type = radio][name = paymentchoic]").each(function () { $(this).prop('checked', false); });
                    $("[name='NumberofMemberVal']").val("");

                    if (selectedmemTypeOption == '0' || selectedmemTypeOption == '1') {
                        memTypeValue = 200;
                        start_numMemVal = 400;

                        $("input[type = radio][name = paymentchoic][value = bothPayment]").prop("checked", true);

                    }
                    else if (selectedmemTypeOption == '3A' || selectedmemTypeOption == '3B') {
                        memTypeValue = 200;
                        start_numMemVal = 400;

                    }
                    else if (selectedmemTypeOption == '4' || selectedmemTypeOption == '6') {
                        memTypeValue = 200;
                        start_numMemVal = -200;

                    }
                    else if (selectedmemTypeOption == '7') {
                        memTypeValue = 200;
                        start_numMemVal = 0;
                    }
                });
            });

            $("[name='NumberofMemberVal']").on("change keyup paste", function () {

                //var start_numMemVal = 400;
                //alert(start_numMemVal);
                //alert(memTypeValue);
                //let upEntrance = memTypeValue;
                let numMemValMultiply = $(this).val() * memTypeValue;
                entranceTotal = (numMemValMultiply + + start_numMemVal);
                //entranceTotal = ($(this).val());
            });

            $("[name='PayDurationVal']").on("change keyup paste", function () {

                let numMemVal = $("[name='NumberofMemberVal']").val();
                let upPayDuration = 100;
                let PayDurationValUp = $(this).val() * upPayDuration;
                let PayDurationValMulnumMemVal = PayDurationValUp * numMemVal;
                annualTotal = (PayDurationValMulnumMemVal + + PayDurationValUp);

                // Calculator Payment Month
                //datepicker7
                //var payDuraValToCal = $(this).val();
                //tititi

            });

            // show value from Payment Type
            function paymentChoice() {
                paymentchoic.forEach((paymentchoic) => {
                    if (paymentchoic.checked) {
                        //alert(`You rated: ${paymentchoic.value}`);
                        var paymentChose = paymentchoic.value;
                    }

                    if (paymentChose == "bothPayment") {
                        $(hideEntrancePay).prop("disabled", false);
                        $(hideAnnuaPay).prop("disabled", false);
                        $("#annualVal").val(annualTotal);
                        $("#entranceVal").val(entranceTotal);
                        $("#totolSum").val(entranceTotal + + annualTotal);

                    }
                    else if (paymentChose == "annualPayment") {
                        $(hideAnnuaPay).prop("disabled", false);
                        $(hideEntrancePay).prop("disabled", true);
                        $("[id='entranceVal']").val(null);
                        $("#annualVal").val(annualTotal);
                        $("#totolSum").val(annualTotal);

                    }
                    else if (paymentChose == "entrancePayment") {
                        $(hideEntrancePay).prop("disabled", false);
                        $(hideAnnuaPay).prop("disabled", true);
                        $("[id='annualVal']").val(null);
                        $("#entranceVal").val(entranceTotal);
                        $("#totolSum").val(entranceTotal);

                    }
                })
            }

            //Get Receipt
            var receiptBtn = $("[name='receiptChk']");
            var hidereceipt = $("[name='receiptOption']");

            function receiptChoose() {
                if ($(receiptBtn).is(':checked')) {
                    $(hidereceipt).prop("disabled", false);
                }
                else {
                    $(hidereceipt).prop("disabled", true);
                }
            }

            //Get Short
            var shortBtn = $("[name='shortChk']");
            var hideshortFrom = $("[id='shortOption']");
            var hideshortTo = $("[id='shortToOption']");
            function shortChoose() {
                if ($(shortBtn).is(':checked')) {
                    $(hideshortFrom).prop("disabled", false);
                    $(hideshortTo).prop("disabled", false);
                }
                else {
                    $(hideshortFrom).prop("disabled", true);
                    $(hideshortTo).prop("disabled", true);
                }
            }

            //Payment Method Selected option
            //bankPanel
            var bankInput = $("[id='bankPanel']");
            function paymentMethodSel() {
                $(document).ready(function () {
                    $("select.paymentMetOption").change(function () {
                        var selectedCountry = $(this).children("option:selected").val();
                        //alert("You have selected the country - " + selectedCountry);

                        if (selectedCountry == "B") {
                            $(bankInput).prop("disabled", false);
                        }
                        else if (selectedCountry == "P") {
                            $(bankInput).prop("disabled", false);
                        }
                        else if (selectedCountry == "S") {
                            $(bankInput).prop("disabled", false);
                        }
                        else if (selectedCountry == "T") {
                            $(bankInput).prop("disabled", false);
                        }
                        else {
                            $(bankInput).prop("disabled", true);
                        }
                    });
                });
            }

            //Processing Sepecial Page
            var mergeInput = $("[id='MergeFamilyInput']");
            var mergeTable = $("[id='DivideFamilyTable']");
            var changeFirstTable = $("[id='ChangeFirstMemberTable']");
            //var mergeTable = document.getElementById("DivideFamilyTable");
            //mergeTable.style.display = "none";

            $(mergeInput).hide();
            $(mergeTable).hide();
            $(changeFirstTable).hide();


            function processingSpecialPage() {

                var radioModeSpecial = $("[name='SpecialFeatureRadio']:checked").val();

                if (radioModeSpecial == "MergeMode") {
                    $(mergeInput).show();
                    $(mergeTable).hide();
                    $(changeFirstTable).hide();

                }
                else if (radioModeSpecial == "DivineMode") {
                    $(mergeTable).show();
                    $(mergeInput).hide();
                    $(changeFirstTable).hide();

                }
                else if (radioModeSpecial == "ActivateMode") {
                    location.reload();
                    $(mergeInput).hide();
                    $(mergeTable).hide();
                    $(changeFirstTable).hide();

                }
                else if (radioModeSpecial == "ChangeFirstMode") {
                    $(mergeInput).hide();
                    $(mergeTable).hide();
                    $(changeFirstTable).show();
                }


                //$(this).click(function () {
                //    //var radioValue = $("input[name='gender']:checked").val();
                //    var radioModeSpecial = $("[name='SpecialFeatureRadio']:checked").val();

                //    if (radioModeSpecial) {
                //        alert("Your are a - " + radioModeSpecial);
                //    }
                //});

            }

        </script>
        <script>
            $(document).ready(function () {
                $("[id*=DropDownList4]").change(function () {
                    var selectValue = $('#<%=DropDownList4.ClientID %> option:selected').text();
                    var arr = selectValue.split(":");
                    alert(arr)
                    $("[id*=bankPanel],[id*=bankCodeBox],[id*=RadioButtonList1").each(function (index) {
                        $(this).val(arr[index]);
                    //$('#RadioButtonList1 input:checked').val(arr[index]);
                    //$("input[name='<%=RadioButtonList1.UniqueID %>']:checked").val(arr[index])
                    });

                <%--var selectValue2 = $('#<%=DropDownList4.ClientID %> option:selected').text();
                alert(selectValue2)--%>
                });
            });
        </script>

    </div>
</asp:Content>
