<%@ Page Title="Private Cancel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="privateCancel.aspx.cs" Inherits="JAT.Private.privateCancel" %>

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
                <asp:LinkButton ID="memberTab" Text="<%$Resources:Resources,member_information %>" runat="server" OnClick="memberTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="familyTab" Text="<%$Resources:Resources,family_member %>" runat="server" OnClick="familyTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="ChildrenTab" Text="<%$Resources:Resources,children %>" runat="server" OnClick="ChildrenTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="paymentTab" Text="<%$Resources:Resources,payment_management %>" runat="server" OnClick="paymentTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks active">
                <asp:LinkButton ID="cancelTab" Text="<%$Resources:Resources,cancel_member %>" runat="server"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="specialTab" Text="<%$Resources:Resources,special_feature %>" runat="server" OnClick="specialTab_Click"></asp:LinkButton>
            </button>
        </div>

        <%--@*CancelTab*@--%>
        <div>
            <div class="box">
                <div style="float: right;">
                    <label>Last Editor: </label>
                    <asp:Label ID="updateBy" Text="" runat="server" />
                </div>
                <br />
                <br />
                <div class="box" style="background-color: lightgray">
                    <h3 lang="eng"><%=Resources.Resources.cancel_member %></h3>
                    <%--@*<h3 lang="jpn">キャンセルメンバー</h3>*@--%>
                    <form>
                        <div class="row space">
                            <div class="col-md-12">
                                <div class="row ">
                                    <div class="form-group col-md-3">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.first_member_name %></label>
                                        <%--@*<label lang="jpn">主なメンバー名前</label>*@--%>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <b>
                                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                            &nbsp;&nbsp;[<asp:Label ID="Label2" runat="server" Text=""></asp:Label>]
                                        </b>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row space">
                            <div class="form-group col-md-3">
                                <label style="color: red; visibility: hidden">*</label>
                                <label lang="eng"><%=Resources.Resources.mode %></label>
                                <%--@*<label lang="jpn">モード</label>*@--%>
                                <span style="float: right;">:</span>
                            </div>
                            <div class="form-group col-md-3">
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                    <asp:ListItem Selected="True">-- Select --</asp:ListItem>
                                    <asp:ListItem Value="Cancel all member">Cancel all member</asp:ListItem>
                                    <asp:ListItem Value="Cancel first member">Cancel first member</asp:ListItem>
                                    <asp:ListItem Value="Cancel all family member">Cancel all family member</asp:ListItem>
                                    <asp:ListItem Value="Cancel some family member">Cancel some family member</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="text-center">
                                <asp:Button ID="processBtn" runat="server" Text="Process" type="submit" class="btn btn-primary" Style="text-align: center;" OnClick="processBtn_Click" />

                                <%--@*<button lang="jpn" type="submit" id="processBtnJP" class="btn btn-primary">プロセス</button>*@--%>
                            </div>
                            <div class="text-center">
                                </br>
                        <b>
                            <asp:Label ID="Label3" runat="server" Text="All members have been cancelled."></asp:Label></b>
                                <b>
                                    <asp:Label ID="Label4" runat="server" Text="All family members have been cancelled."></asp:Label></b>
                            </div>
                        </div>
                    </form>
                </div>
                </br>
            <b>
                <asp:Label ID="Label5" runat="server" Text="Select new memberId"></asp:Label></b>
                </br>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" CommandName="Edit" CausesValidation="False" ID="ibtn1" ImageUrl="~/img/xp_users.gif" OnClick="GridView_Button_ChnageFrist" OnClientClick="return fnConfirmChangeFirst();" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" CommandName="Edit2" CausesValidation="False" ID="ibtn2" ImageUrl="~/img/xp_users.gif" OnClick="GridView_Button_ChnageSome" OnClientClick="return fnConfirmChangeSome();" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="<%$Resources:Resources,member_id %>" DataField="memberid" HeaderStyle-CssClass="text-center">
                                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemStyle Height="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$Resources:Resources,name %>" DataField="nameJ" HeaderStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" HorizontalAlign="Center" Height="10px" />
                                <ItemStyle Height="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$Resources:Resources,name_eng %>" DataField="nameE" HeaderStyle-CssClass="text-center">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" Height="10px" />
                                <ItemStyle Height="40px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$Resources:Resources,member_type %>" DataField="memberType" HeaderStyle-CssClass="text-center">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                                <ItemStyle Height="40px" HorizontalAlign="Center" Width="10%" />
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
            </div>
        </div>

        <script language="javascript" type="text/javascript">     
            function fnConfirmChangeFirst() {
                return confirm("Would you like to select this member to be the new first member?");
            }
        </script>

        <script language="javascript" type="text/javascript">
            function fnConfirmChangeSome() {
                return confirm("Would you like to cancel this selected member?");
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
        <%--@* *********** Datepicker *********** *@--%>
        <script>
            var options = {
                format: 'dd-M-yyyy',
                todayHighlight: true,
                autoclose: true
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

            function countChar(val) {
                var len = val.value.length;
                if (len >= 501) {
                    val.value = val.value.substring(0, 500);
                } else {
                    $('#charNum').text(500 - len + '/500');
                }
            };

        </script>
    </div>
</asp:Content>
