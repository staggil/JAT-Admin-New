<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="regEntryCheck.aspx.cs" Inherits="JAT.Admin.RegisterMember.regEntryCheck" %>
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
        <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>
         <br />
        <div class="tab">
           <button class="tablinks active">
                <asp:LinkButton ID="memberTab" Text="<%$Resources:Resources,member_information %>" runat="server"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="familyTab" Text="<%$Resources:Resources,family_member %>" runat="server" OnClick="familyTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="ChildrenTab" Text="<%$Resources:Resources,children %>" runat="server" OnClick="ChildrenTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks">
               <%-- <asp:LinkButton ID="cancelTab" Text="✔" runat="server" OnClientClick="ConfirmApprove()" OnClick="Approve_Click"></asp:LinkButton>--%>
                <asp:LinkButton ID="cancelTab" Text="✔" runat="server"  OnClick="Approve_Click"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="specialTab" Text="✘" runat="server" OnClientClick="ConfirmReject()" OnClick="Reject_Click"></asp:LinkButton>
            </button>
        </div>
        <div style="float: right;">
            <asp:Button ID="editBTN" runat="server" Text="Edit" OnClick="editBTN_Click" />
        </div>
     <!-- The Modal -->
        <div id="SendHistoryBox" class="modal-1">

            <!-- Modal content -->
            <div class="modal-content-1">
               
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField HeaderText="<%$Resources:Resources,send_method %>" DataField="sendType" HeaderStyle-CssClass="text-center">
                            <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                            <ItemStyle Height="40px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Start Date" DataField="startDate" HeaderStyle-CssClass="text-center">
                            <HeaderStyle BackColor="#24227A" HorizontalAlign="Center" Height="10px" />
                            <ItemStyle Height="40px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="End Date" DataField="endDate" HeaderStyle-CssClass="text-center">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" Height="10px" />
                            <ItemStyle Height="40px" />
                        </asp:BoundField>
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
                <%--</div>--%>
                <br />
                <asp:Button ID="Button1" runat="server" Text="close" Style="display: block; margin: auto;" OnClientClick="closeModal(); return false;" />
            </div>
        </div>


        <%--@*Member Information*@--%>
        <div>
            <div class="box">
                <br />
                <div class="box" style="background-color: lightgray">

                    <h3 lang="eng"><%=Resources.Resources.member_information %></h3>
                    <form>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color: red;">*</label>
                                        <label lang="eng"><%=Resources.Resources.member_id %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <input type="text" maxlength="10" id="Box2" runat="server" placeholder="" autocomplete="off" style="width: 220px" pattern="[0-9]{0,10}" title="number only">
                                        <br/>                                
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Id can't be empty." ControlToValidate="Box2" ForeColor="Red" Enabled="True"></asp:RequiredFieldValidator>
                                        <br/>                                
                                <asp:Label ID="lbError" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label style="color: red;">*</label>
                                        <label lang="eng"><%=Resources.Resources.applied_date %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <input runat="server" type="text" id="Box3" class="datepicker1Input" placeholder="dd/mm/yyyy" autocomplete="off">
                                        <span id="datepicker1" class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color: red;">*</label>
                                        <label lang="eng"><%=Resources.Resources.name %></label>
                                         <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <input id="Box5" runat="server" type="text" style="width: 220px" maxlength="50" autocomplete="off" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name can't be empty." ControlToValidate="Box5" ForeColor="Red" Enabled="True"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label style="color: red;">*</label>
                                        <label lang="eng"><%=Resources.Resources.name_eng %></label>
                                        <span style="float: right;">:</span>
                                    </div>

                                    <div class="form-group col-md-6">
                                        <asp:DropDownList ID="preFixSel" runat="server">
                                            <asp:ListItem>-- ANY --</asp:ListItem>
                                            <asp:ListItem>--</asp:ListItem>
                                            <asp:ListItem>Ms.</asp:ListItem>
                                            <asp:ListItem>Mrs.</asp:ListItem>
                                            <asp:ListItem Selected>Mr.</asp:ListItem>
                                            <asp:ListItem>Dr.</asp:ListItem>
                                        </asp:DropDownList>
                                        <input type="text" style="width: 220px" id="Box6" runat="server" placeholder="" maxlength="50" autocomplete="off">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Name can't be empty." ControlToValidate="Box6" ForeColor="Red" Enabled="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color: red; visibility: hidden;">*</label>
                                        <label lang="eng"><%=Resources.Resources.birth_place %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <input type="text" style="width: 220px" id="Box8" runat="server" placeholder="" maxlength="50" autocomplete="off">
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label style="color: red; visibility: hidden;">*</label>
                                        <label lang="eng"><%=Resources.Resources.birth_date %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <input runat="server" type="text" id="Box9" class="datepicker2Input" placeholder="dd/mm/yyyy" autocomplete="off">
                                        <span id="datepicker2" class="glyphicon glyphicon-calendar"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                         <div class="row">
                             <div class="col-sm-6">
                            <div class="row">
                                <div class="form-group col-sm-5">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng">Remark Payment</label>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-sm-7">
                                    <div class="form-check" style="float: left; margin-right: 5px; font-size: 14px;">
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                        <label style="font-weight: 100;" lang="eng">Personal payment</label><br>
                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                        <label style="font-weight: 100;" lang="eng">Company payment</label><br>
                                        <textarea class="form-control" id="TextareaPayment" runat="server" style="margin: 0px -30.75px 0px 0px; width: 250px; height: 93px;" maxlength="125" autocomplete="off"></textarea>
                                    </div>
                                    
                                </div>

                            </div>
                        </div>
                         </div>
                </div>

                <br />
                <%--@*Company Information*@--%>

                <div class="box" style="background-color: lightgray">
                    <h3 lang="eng"><%=Resources.Resources.company_information %></h3>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="form-group col-md-5">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng"><%=Resources.Resources.company_name %></label>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-6">
                                    <input type="text" style="width: 600px" id="Box11" runat="server" placeholder="" maxlength="100" autocomplete="off">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="form-group col-md-5">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng"><%=Resources.Resources.address %></label>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-6">
                                    <textarea class="form-control" id="Box13" runat="server" style="margin: 0px -30.75px 0px 0px; width: 250px; height: 93px;" maxlength="125" autocomplete="off"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng"><%=Resources.Resources.telephone %></label>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-4">
                                    <input id="Box15" runat="server" style="width: 90%;" placeholder="x-xxxx-xxxx" maxlength="50" autocomplete="off">
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <%--<asp:CheckBox ID="Box16" runat="server" />--%>
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng"><%=Resources.Resources.fax %></label>
                                    <%--@*<label lang="jpn">ファクス番号</label>*@--%>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-4">
                                    <input type="tel" id="Box17" runat="server" style="width: 90%;" placeholder="x-xxxx-xxxx" maxlength="40" autocomplete="off">
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

                <br />
                <%--@*Information*@--%>
                <div class="box" style="background-color: lightgray">
                    <h3 lang="eng"><%=Resources.Resources.information %></h3>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="row">
                                <div class="form-group col-md-5">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng"><%=Resources.Resources.zip_code %></label>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-6">
                                    <input type="text" maxlength="10" style="width: 250px;" id="Box18" runat="server" placeholder="" autocomplete="off">
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng"><%=Resources.Resources.e_mail %></label>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-6">
                                    <input id="email" runat="server" type="text" maxlength="50" style="width: 90%;" autocomplete="off" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="form-group col-md-5">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng"><%=Resources.Resources.address %></label>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-6">
                                    <textarea class="form-control" id="Box20" runat="server" style="margin: 0px -30.75px 0px 0px; width: 250px; height: 93px;" maxlength="125" autocomplete="off"></textarea>
                                    <asp:Label Font-Bold="true" ID="changeAddressDate" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng">
                                        <%=Resources.Resources.home_phone %>
                                    </label>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-4">
                                    <input id="Box22" runat="server" style="width: 90%;" placeholder="x-xxxx-xxxx" maxlength="50" autocomplete="off">
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng"><%=Resources.Resources.mobile %></label>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-4">
                                    <input type="tel" id="Box24" runat="server" style="width: 90%;" placeholder="(0XX)XXX-XXXX" maxlength="40" autocomplete="off">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="row">
                                <div class="form-group col-sm-5">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng"><%=Resources.Resources._event %></label>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-sm-7">
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
                        <%--@*unsure to use*@--%>
                        <div class="col-sm-6">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label lang="eng"><%=Resources.Resources.subcommittee %></label>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-8">
                                    <div class="form-check" style="float: left; margin-right: 5px; font-size: 14px;">
                                        <asp:CheckBox ID="cbBoard" runat="server" />
                                        <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.board %></label><br>
                                        <asp:CheckBox ID="cbBoardlist" runat="server" />
                                        <label style="font-weight: 100;" lang="eng">Board[List]</label><br>
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
                                <div class="form-group col-sm-5">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng"><%=Resources.Resources.sukusuku %></label>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-sm-7">
                                    <div class="form-check" style="float: left; margin-right: 5px; font-size: 14px;">
                                        <asp:CheckBox ID="cbSukusukuMem" runat="server" />
                                        <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.member %></label><br>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--@*unsure to use*@--%>
                        <div class="col-sm-6">
                            <div class="row">
                                <div class="form-group col-md-4">
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
                                    <div class="form-check" style="margin-right: 5px; font-size: 14px;">
                                        <asp:CheckBox ID="cbOverseasMem" runat="server" />
                                        <label style="font-weight: 100;" lang="eng"><%=Resources.Resources.overseas_resident_members %></label><br>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="form-group col-md-5">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng"><%=Resources.Resources.member_status %></label>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-3">
                                    <asp:DropDownList ID="Box32" runat="server">
                                        <asp:ListItem Value="A" Selected>A</asp:ListItem>
                                        <asp:ListItem Value="NA">NA</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="statusdate" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label style="color: red;">*</label>
                                    <label lang="eng"><%=Resources.Resources.send_method %></label>
                                    <%--@*<label lang="jpn">会報送</label>*@--%>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-3">
                                    <asp:DropDownList ID="Box33" runat="server">
                                        <asp:ListItem Value="any">-- Any --</asp:ListItem>
                                        <asp:ListItem Value="#">#</asp:ListItem>
                                        <asp:ListItem Value="$">$</asp:ListItem>
                                        <asp:ListItem Value="T">T</asp:ListItem>
                                        <asp:ListItem Value="Z">Z</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="form-group col-md-5">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng"><%=Resources.Resources.member_type %></label>
                                    <%--@*<label lang="jpn">メンバータイプ</label>*@--%>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-3">
                                    <asp:DropDownList ID="Box34" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng"><%=Resources.Resources.sort_board %></label>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-6">
                                    <input type="number" style="width: 85px" id="Box35" runat="server" placeholder="" max="32767" autocomplete="off">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="row">
                                <div class="form-group col-md-5">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng"><%=Resources.Resources.sort_lady %></label>
                                    <%--@*<label lang="jpn">ソート婦人</label>*@--%>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-3">
                                    <input type="number" style="width: 220px" id="Box36" runat="server" placeholder="" max="32767" autocomplete="off">
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label lang="eng"><%=Resources.Resources.position %></label>
                                    <%--@*<label lang="jpn">役職</label>*@--%>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-4">
                                    <input type="text" style="width: 90%" id="Box37" runat="server" placeholder="" maxlength="100" autocomplete="off">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <%--@*<div class="row">
                                <div class="form-group col-md-5">
                                    <label style="color:red;visibility:hidden;">*</label>
                                    <label lang="eng" for="">Remark</label>
                                    <label lang="jpn" for="">備考</label>
                                    <span style="float:right;">:</span>
                                </div>
                                <div class="form-group col-md-5">
                                    <div id="charNum" style="text-align:right;width:400px;font-size:10px;">500/500</div>
                                    <textarea id="test" onkeyup="countChar(this)" style="font-family: monospace;width:395px; font-size:14px; overflow: hidden; resize: none;" maxlength="500" name="Text1" rows="1"></textarea>
                                </div>
                            </div>*@--%>
                            <div class="row">
                                <div class="form-group col-md-5">
                                    <label style="color: red; visibility: hidden;">*</label>
                                    <label for=""><%=Resources.Resources.remark %></label>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-5">
                                    <div id="charNum" style="text-align: right; width: 400px; font-size: 10px;">500/500</div>
                                    <textarea id="Box38" runat="server" onkeyup="countChar(this)" style="font-family: monospace; width: 395px; font-size: 14px; overflow: hidden; resize: none;" maxlength="500" name="Text1" rows="10"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="text-center">
                            <asp:CheckBox ID="Box39" runat="server" />
                            <label><%=Resources.Resources.payment_split %></label>
                        </div>
                    </div>
                    <div class="form-group" style="text-align: center;">
                        <div class="text-center">
                            <asp:Button ID="updateBtn" runat="server" Text="Save" type="submit" class="btn btn-primary" Style="text-align: center;" CausesValidation="false" OnClientClick="ConfirmEdit()" OnClick="updateBtn_Click" />                           
                            <asp:Button ID="cancelBtnMem" runat="server" Text="Cancel" type="submit" class="btn btn-primary" CausesValidation="false" OnClick="cancelBtnMem_Click" UseSubmitBehavior="false" />                            
                        </div>
                    </div>
                </div>
                </form>
        <br/>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField HeaderText=Number DataField="run_num" HeaderStyle-CssClass="text-center">
                            <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                            <ItemStyle Height="40px" HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="<%$Resources:Resources,member_id%>" DataField="familymember_id" HeaderStyle-CssClass="text-center">
                            <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                            <ItemStyle Height="40px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="<%$Resources:Resources,name%>" DataField="nameJp" HeaderStyle-CssClass="text-center">
                            <HeaderStyle BackColor="#24227A" HorizontalAlign="Center" Height="10px" />
                            <ItemStyle Height="40px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="<%$Resources:Resources,name_eng%>" DataField="nameE" HeaderStyle-CssClass="text-center">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" Height="10px" />
                            <ItemStyle Height="40px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="<%$Resources:Resources,birth_date%>" DataField="birthDate" HeaderStyle-CssClass="text-center">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" Height="10px" />
                            <ItemStyle Height="40px" HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                            <ItemTemplate>
                                <asp:ImageButton runat="server" CommandName="Edit" OnClick="GridView_Button_Click" CausesValidation="False" ID="ibtn1" ImageUrl="~/img/icon-pencil.gif" />
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
            if (confirm("Do you want to save data?")) {
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
            if (confirm("Do you want to edit data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <script type="text/javascript">
        function ConfirmApprove() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to approve this member?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

        <script type="text/javascript">
            function ConfirmReject() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to reject this member?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        </script>


        <script type="text/javascript">
            function insertID() {
                alert("Please insert firstmember id!!");
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
            //$('.datepicker1Input').datepicker().datepicker("setDate", new Date());
            //$('.datepicker1Input').datepicker().datepicker("setDate", null);
            $("#datepicker1,.datepicker1Input").click(function () {
                if (!$(".datepicker1Input").prop("disabled")) {
                    $('.datepicker1Input').datepicker(options).datepicker("show")
                }
            });
            $("#datepicker2,.datepicker2Input").click(function () {
                if (!$(".datepicker2Input").prop("disabled")) {
                    $('.datepicker2Input').datepicker(options).datepicker("show")
                }
            });
            $("#datepicker3").click(function () {
                $('.datepicker3Input').datepicker(options).datepicker("show")
            });
            $("#datepicker4").click(function () {
                $('.datepicker4Input').datepicker(options).datepicker("show")
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

        var canType = document.querySelectorAll("[id='blockInput']");
        var hideAdd = document.querySelectorAll("[id='hideAddBtn']");

        //$('.tabcontent').find('input, textarea, button, select').prop('disabled', 'disabled');

        function changeTAB(evt, tabName) {
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }
            document.getElementById(tabName).style.display = "block";
            evt.currentTarget.className += " active";
        }


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

        }

    </script>

</asp:Content>
