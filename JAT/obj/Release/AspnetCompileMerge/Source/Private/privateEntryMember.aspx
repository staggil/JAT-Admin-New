<%@ Page Title="Private EntryMember" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="privateEntryMember.aspx.cs" Inherits="JAT.Private.privateEntryMember" %>

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

    <div class="col-sm-12">
        </br>
    <%--@*<h1>&nbsp;Private Entry</h1>*@--%>
        <div class="tab">
            <%--@*<button lang="eng" class="tablinks active" onclick="changeTAB(event, 'MemberTab')">@Html.ActionLink(JAT.Languages.Index_PrivateEntry_ENG.tab1)</button>*@--%>
            <%--@*<button lang="eng" class="tablinks active" onclick="changeTAB(event, 'MemberTab')">@Html.ActionLink(JAT.Languages.Index_PrivateEntry.tab1, "index_PrivateEntry")</button>*@--%>
            <button class="tablinks">
                <asp:LinkButton ID="memberTab" Text="<%$ Resources:Resources,member_information %>" runat="server" OnClick="memberTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks active">
                <asp:LinkButton ID="familyTab" Text="<%$ Resources:Resources,family_member %>" runat="server"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="ChildrenTab" Text="<%$ Resources:Resources,children %>" runat="server" OnClick="ChildrenTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="paymentTab" Text="<%$ Resources:Resources,payment_management %>" runat="server" OnClick="paymentTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="cancelTab" Text="<%$ Resources:Resources,cancel_member %>" runat="server" OnClick="cancelTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="specialTab" Text="<%$ Resources:Resources,special_feature %>" runat="server" OnClick="specialTab_Click"></asp:LinkButton>
            </button>
        </div>
        <div style="float: right;">
            <label>Last Editor: </label>
            <asp:Label ID="updateBy" Text="" runat="server" />
            <asp:Button ID="addBTN" runat="server" Text="Add" OnClick="addBTN_Click" />
            <br />
            <br />
        </div>



        <%--@*FamilyTab*@--%>
        <div>
            <div class="box">
                <br />
                <div class="box" style="background-color: lightgray">

                    <h3 lang="eng"><%=Resources.Resources.family_member_older_than_18 %></h3>

                    <%--@*<h3 lang="jpn">家族会員 (18 歳以下)</h3>*@--%>
                    <form>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.first_member_name %></label>
                                        <%--@*<label lang="jpn">主なメンバー名前</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-7">
                                        <b>
                                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label></b>
                                        <b>[<asp:Label ID="Label2" runat="server" Text=""></asp:Label>]</b>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color: red;">*</label>
                                        <label lang="eng"><%=Resources.Resources.member_id %></label>
                                        <%--@*<label lang="jpn">会員ID</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <input runat="server" type="text" onkeypress="numberOnly(event)" maxlength="10" id="Box1" placeholder="" autocomplete="off" >
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Box1" ErrorMessage="ID should be a number." ForeColor="Red" ValidationExpression="^[0-9]{1,10}"></asp:RegularExpressionValidator>
                                        <asp:Label ID="lbError" runat="server" ForeColor="Red"></asp:Label>
                                        </br>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Box1" ForeColor="Red" ErrorMessage="ID can't be empty." Visible="False"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label style="color: red;">*</label>
                                        <label lang="eng"><%=Resources.Resources.applied_date %></label>
                                        <%--@*<label lang="jpn">入会日</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-8">
                                        <%--<input required type="text" id="blockInput" disabled="disabled" class="datepicker3Input" placeholder="DD/MM/YYYY" autocomplete="off">--%>
                                        <input type="text" id="Box6" runat="server" class="datepicker3Input" placeholder="dd/mm/yyyy" autocomplete="off">
                                        <span id="datepicker3" class="glyphicon glyphicon-calendar"></span>
                                        </br>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Box6" ForeColor="Red" ErrorMessage="Applied date can't be empty." Visible="False"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <%--<asp:CheckBox ID="Box18" runat="server" />--%>
                                        <label style="color: red;">*</label>
                                        <label lang="eng"><%=Resources.Resources.name %></label>
                                        <%--@*<label lang="jpn">会員名</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>

                                    <div class="form-group col-md-5">
                                        <input runat="server" type="text" id="Box2" maxlength="50" autocomplete="off" ><br />
                                        <asp:CheckBox ID="Box17" runat="server" />
                                        <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.spouse %></label><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Box2" ForeColor="Red" ErrorMessage="Name can't be empty."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label style="color: red;">*</label>
                                        <label lang="eng"><%=Resources.Resources.name_eng %></label>
                                        <%--@*<label lang="jpn">会員名 (英文)</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <asp:DropDownList ID="Box3" runat="server">
                                            <asp:ListItem>-- ANY --</asp:ListItem>
                                            <asp:ListItem>--</asp:ListItem>
                                            <asp:ListItem>Ms.</asp:ListItem>
                                            <asp:ListItem Selected>Mrs.</asp:ListItem>
                                            <asp:ListItem>Mr.</asp:ListItem>
                                            <asp:ListItem>Dr.</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <input runat="server" style="margin-left: 0px;" type="text" id="Box4" maxlength="50" autocomplete="off" >
                                        </br>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Box4" ForeColor="Red" ErrorMessage="Name can't be empty."></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.home_phone %></label>
                                        <%--@*<label lang="jpn">電話番号</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <%-- <input type="tel" id="blockInput" disabled="disabled" style="width: 90%;"
                                       pattern="[0-9]{1}-[0-9]{4}-[0-9]{4}" placeholder="x-xxxx-xxxx">--%>
                                        <input id="Box9" runat="server" placeholder="x-xxxx-xxxx" maxlength="50" autocomplete="off" >
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.mobile %></label>
                                        <%--@*<label lang="jpn">携帯電話番号</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-8">
                                        <input type="tel" id="Box10" runat="server" style="margin-left: 0px; width: 70%" placeholder="(0XX)XXX-XXXX" maxlength="40" autocomplete="off" >
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.member_type %></label>
                                        <%--@*<label lang="jpn">メンバータイプ</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <asp:DropDownList ID="Box7" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.birth_date %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-8">
                                        <input type="text" id="Box5" runat="server" class="datepicker4Input" placeholder="dd/mm/yyyy" autocomplete="off">
                                        <span id="datepicker4" class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.e_mail %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <%--<input type="email" id="Box19" runat="server" maxlength="50" autocomplete="off" >--%>
                                        <input type="text" id="Box19" runat="server" maxlength="50" autocomplete="off" >
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.member_status %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-8">
                                        <asp:DropDownList ID="Box8" runat="server">
                                            <asp:ListItem Value="A" Selected>A</asp:ListItem>
                                            <asp:ListItem Value="NA">NA</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="dateNAtoA" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources._event %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-7">
                                        <div class="form-check" style="float: left; margin-right: 5px; font-size: 14px;">
                                            <asp:CheckBox ID="cbEngtest" runat="server" />
                                            <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.english_test %></label><br>
                                            <asp:CheckBox ID="cbOnevent" runat="server" />
                                            <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.online_event %></label><br>
                                            <asp:CheckBox ID="cbSoftball" runat="server" />
                                            <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.softball %></label><br>
                                            <asp:CheckBox ID="cbYoga" runat="server" />
                                            <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.yoga %></label><br>
                                        </div>
                                        <div class="form-check" style="float: left; margin-right: 5px; font-size: 14px;">
                                            <asp:CheckBox ID="cbValue1" runat="server" />
                                            <asp:Label ID="ev_tmp1" runat="server"></asp:Label><br>
                                            <asp:CheckBox ID="cbValue2" runat="server" />
                                            <asp:Label ID="ev_tmp2" runat="server"></asp:Label><br>
                                            <asp:CheckBox ID="cbValue3" runat="server" />
                                            <asp:Label ID="ev_tmp3" runat="server"></asp:Label><br>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.subcommittee %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-8">
                                        <div class="form-check" style="float: left; margin-right: 5px; font-size: 14px;">
                                            <asp:CheckBox ID="cbBoard" runat="server" />
                                            <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.board %></label><br>
                                            <asp:CheckBox ID="cbBoardlist" runat="server" />
                                            <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.board_list %></label><br>
                                            <asp:CheckBox ID="cbGolf" runat="server" />
                                            <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.golf %></label><br>
                                            <asp:CheckBox ID="cbLady" runat="server" />
                                            <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.lady %></label><br>
                                            <asp:CheckBox ID="cbClubSecre" runat="server" />
                                            <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.club_secretary %></label><br>
                                            <asp:CheckBox ID="cbBaVolun" runat="server" />
                                            <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.bazaar_volunteer %></label><br>
                                        </div>
                                        <div class="form-check" style="float: left; margin-right: 5px; font-size: 14px;">
                                            <asp:CheckBox ID="cbSocialMem" runat="server" />
                                            <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.social_gathering_members %></label><br>
                                            <asp:CheckBox ID="cbYouthMem" runat="server" />
                                            <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.youth_circle_members %></label><br>
                                            <asp:CheckBox ID="cbValue4" runat="server" />
                                            <asp:Label ID="sub_tmp1" runat="server"></asp:Label><br>
                                            <asp:CheckBox ID="cbValue5" runat="server" />
                                            <asp:Label ID="sub_tmp2" runat="server"></asp:Label><br>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.sukusuku %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-7">
                                        <div class="form-check" style="float: left; margin-right: 5px; font-size: 14px;">
                                            <asp:CheckBox ID="cbSukusukuMem" runat="server" />
                                            <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.member %></label><br>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.children_library %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-8">
                                        <div class="form-check" style="float: left; margin-right: 5px; font-size: 14px;">
                                            <asp:CheckBox ID="cbChildLibMem" runat="server" />
                                            <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.member %></label><br>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="row">
                                    <div class="form-group col-sm-5">
                                        <label style="color: red; visibility: hidden;">*</label>
                                        <label lang="eng"><%=Resources.Resources.overseas_resident_members %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-sm-7">
                                        <div class="form-check" style="float: left; margin-right: 5px; font-size: 14px;">
                                            <asp:CheckBox ID="cbOverseasMem" runat="server" />
                                            <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.overseas_resident_members %></label><br>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="text-align: center;">
                            <div class="text-center">
                                <asp:Button ID="saveBtn" runat="server" Text="Save" type="submit" class="btn btn-primary" Style="text-align: center;" OnClientClick="ConfirmSave()" OnClick="saveBtn_Click1" />
                                <asp:Button ID="updateBtn" runat="server" Text="Save" type="submit" class="btn btn-primary" Style="text-align: center;" OnClientClick="ConfirmEdit()" OnClick="updateBtn_Click" Visible="False" />
                                <asp:Button ID="cancelBtn" runat="server" Text="Cancel" type="submit" class="btn btn-primary" CausesValidation="false" OnClick="cancelBtn_Click" />
                            </div>
                        </div>
                    </form>
                </div>
                <br />
                <br />
                <div style="overflow: scroll;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" Width="100%" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundField HeaderText="<%$Resources:Resources,member_id %>" DataField="memberid" HeaderStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$Resources:Resources,name %>" DataField="nameJ" HeaderStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$Resources:Resources,name_eng %>" DataField="nameE" HeaderStyle-CssClass="text-center">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                                <ItemStyle />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$Resources:Resources,birth_date %>" DataField="birthDate" HeaderStyle-CssClass="text-center">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$Resources:Resources,applied_date %>" DataField="appliedDate" HeaderStyle-CssClass="text-center">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Cancelled Date" DataField="cancelledDate" HeaderStyle-CssClass="text-center">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$Resources:Resources,member_type %>" DataField="memberType" HeaderStyle-CssClass="text-center">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$Resources:Resources,member_status %>" DataField="memberStatus" HeaderStyle-CssClass="text-center">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$Resources:Resources,english_test %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Enabled="False" Checked='<%# Eval("ev_1") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Resources,online_event %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox2" runat="server" Enabled="False" Checked='<%# Eval("ev_2") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Resources,softball %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox3" runat="server" Enabled="False" Checked='<%# Eval("ev_3") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Resources,yoga %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox4" runat="server" Enabled="False" Checked='<%# Eval("ev_4") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="evt1" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox5" runat="server" Enabled="False" Checked='<%# Eval("ev_tmp1") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="evt2" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox6" runat="server" Enabled="False" Checked='<%# Eval("ev_tmp2") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="evt3" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox7" runat="server" Enabled="False" Checked='<%# Eval("ev_tmp3") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Resources,board %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox8" runat="server" Enabled="False" Checked='<%# Eval("board") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Resources,board_list %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox9" runat="server" Enabled="False" Checked='<%# Eval("sub_board_list") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Resources,golf %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox10" runat="server" Enabled="False" Checked='<%# Eval("golf") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Resources,lady %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox11" runat="server" Enabled="False" Checked='<%# Eval("lady") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Resources,club_secretary%>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox12" runat="server" Enabled="False" Checked='<%# Eval("sub_secretary") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Resources,bazaar_volunteer %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox13" runat="server" Enabled="False" Checked='<%# Eval("sub_volunteer") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Resources,social_gathering_members %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox14" runat="server" Enabled="False" Checked='<%# Eval("sub_social") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Resources,youth_circle_members %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox15" runat="server" Enabled="False" Checked='<%# Eval("sub_member") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="subt1" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox16" runat="server" Enabled="False" Checked='<%# Eval("sub_tmp1") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="subt2" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox17" runat="server" Enabled="False" Checked='<%# Eval("sub_tmp2") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Resources,sukusuku %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox18" runat="server" Enabled="False" Checked='<%# Eval("zukuzuku") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Resources,children_library %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox19" runat="server" Enabled="False" Checked='<%# Eval("children") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Resources,overseas_resident_members %>" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox20" runat="server" Enabled="False" Checked='<%# Eval("ov_member") %>' />
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" CausesValidation="False" ID="ibtn1" OnClick="GridView_EditButton_Click" ImageUrl="~/img/icon-pencil.gif" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" CommandName="Delete" CausesValidation="False" ID="ibtn2" OnClick="GridView_DeleteButton_Click" OnClientClick="return fnConfirmDelete();" ImageUrl="~/img/icon-delete.gif" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Print" HeaderStyle-CssClass="text-center">
                           <HeaderStyle  BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                            <ItemTemplate>
                           <asp:ImageButton runat="server" CommandName="Print" CausesValidation="False" ID="ibtn3" ImageUrl="~/img/xp_print.gif" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Print" HeaderStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text="Print"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <script language="javascript" type="text/javascript">
            function fnConfirmDelete() {
                return confirm("Are you sure you want to delete this member?");
            }
        </script>

        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
        <%--@section scripts{--%>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
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
        <%--@* *********** Datepicker *********** *@--%>
        <script>
            var options = {
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
                $("#datepicker3,.datepicker3Input").click(function () {
                    if (!$(".datepicker3Input").prop("disabled")) {
                        $('.datepicker3Input').datepicker(options).datepicker("show")
                    }
                });
                $("#datepicker4,.datepicker4Input").click(function () {
                    if (!$(".datepicker4Input").prop("disabled")) {
                        $('.datepicker4Input').datepicker(options).datepicker("show")
                    }
                });
                $("#datepicker5").click(function () {
                    $('.datepicker5Input').datepicker(options).datepicker("show")
                });
                $("#datepicker6").click(function () {
                    $('.datepicker6Input').datepicker(options).datepicker("show")
                });
                $("#EffectiveDate").click(function () {
                    $('#EffectiveDateInput').datepicker(options).datepicker("show")
                });
                $("#datepicker8").click(function () {
                    $('.datepicker8Input').datepicker(options).datepicker("show")
                });
                $("#datepicker9").click(function () {
                    $('.datepicker9Input').datepicker(options).datepicker("show")
                });
            });
        </script>
        <%--@* *********** /Datepicker *********** *@--%>
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

            //@* ************************ Auto expand TextArea ***************************** *@
            //var comfyText = (function () {
            //    var tag = document.querySelectorAll('textarea')
            //    for (var i = 0; i < tag.length; i++) {
            //        tag[i].addEventListener('paste', autoExpand)
            //        tag[i].addEventListener('input', autoExpand)
            //        tag[i].addEventListener('keyup', autoExpand)
            //    }
            //    function autoExpand(e, el) {
            //        var el = el || e.target
            //        el.style.height = 'inherit'
            //        el.style.height = el.scrollHeight + 'px'
            //    }
            //    window.addEventListener('load', expandAll)
            //    window.addEventListener('resize', expandAll)
            //    function expandAll() {
            //        var tag = document.querySelectorAll('textarea')
            //        for (var i = 0; i < tag.length; i++) {
            //            autoExpand(e, tag[i]);
            //        }
            //    }
            //})()
            //@* ************************500/500***************************** *@
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
            var hideEntrancePay = document.querySelectorAll("[id='entranceVal']");
            var hideAnnuaPay = document.querySelectorAll("[id='annualVal']");
            $(hideEntrancePay).prop("disabled", true);
            $(hideAnnuaPay).prop("disabled", true);
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
                    $("input[type=radio][name=paymentchoic]").each(function () { $(this).prop('checked', false); });
                    $("[name='NumberofMemberVal']").val("");

                    if (selectedmemTypeOption == '0' || selectedmemTypeOption == '1') {
                        memTypeValue = 200;
                        start_numMemVal = 400;

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

    </div>
</asp:Content>

