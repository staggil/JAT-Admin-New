<%@ Page Title="privateFeature" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="privateFeature.aspx.cs" Inherits="JAT.Private.privateFeature" %>

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

        .auto-style1 {
            display: block;
            width: 350%;
            height: 1.5em;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555555;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            border: 1px solid #ccc;
            padding: 7px 5px;
            background-color: #fff;
            background-image: none;
        }
        .Space label
        {
           margin-left: 10px;
           margin-right:10px;
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
            <button class="tablinks">
                <asp:LinkButton ID="cancelTab" Text="<%$Resources:Resources,cancel_member %>" runat="server" OnClick="cancelTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks active">
                <asp:LinkButton ID="specialTab" Text="<%$Resources:Resources,special_feature %>" runat="server"></asp:LinkButton>
            </button>
        </div>
        <div style="float: right;">
            <%--<asp:Button ID="addBTN" runat="server" Text="Add" OnClick="addBTN_Click" />--%>
            <%--<button lang="eng" id="hideAddBtn" class="tablinks" onclick="addData()">ADD</button>--%>
            <%--@*<button lang="jpn" id="hideAddBtn" class="tablinks" onclick="addData()">新規</button>*@--%>
            <br />
            <br />
        </div>

        <%--@*SpecialTab*@--%>
        <div>
            <div class="box">
                <div style="float: right;">
                    <label>Last Editor: </label>
                    <asp:Label ID="updateBy" Text="" runat="server" />
                </div>
                <br />
                <br />
                <div class="box" style="background-color: lightgray">
                    <h3 lang="eng"><%=Resources.Resources.special_feature %></h3>
                    <%--@*<h3 lang="jpn">特徴</h3>*@--%>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="form-group col-md-3">
                                    <label style="color: red; visibility: hidden">*</label>
                                    <label lang="eng"><%=Resources.Resources.first_member_name %></label>
                                    <%--@*<label lang="jpn">主なメンバー名前</label>*@--%>
                                    <span style="float: right;">:</span>
                                </div>
                                <div class="form-group col-md-6">
                                    <b>
                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label></b>
                                    <b>&nbsp;&nbsp;[<asp:Label ID="Label2" runat="server" Text=""></asp:Label>]</b>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="form-group col-md-3">
                                    <label style="color: red; visibility: hidden">*</label>
                                    <label lang="eng"><%=Resources.Resources.mode %></label>
                                    <%--@*<label lang="jpn">モード</label>*@--%>
                                    <span style="float: right;">:</span>
                                </div>


                                <div class="form-check" style="float: left; margin-left: 20px;">
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" CssClass="Space" AutoPostBack="true">
                                        <asp:ListItem Value="Merge Family" Text="<%$Resources:Resources,merge_family %>" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="Divide Family" Text="<%$Resources:Resources,divide_family%>"></asp:ListItem>
                                        <asp:ListItem Value="Activate Member" Text="<%$Resources:Resources,activate_member %>"></asp:ListItem>
                                        <asp:ListItem Value="Change First Member" Text="<%$Resources:Resources,change_first_member %>"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="form-group col-md-3">
                                    <label style="color: red; visibility: hidden">*</label>
                                    <b>
                                        <asp:Label runat="server" ID="MergeFamilyLabel"><%=Resources.Resources.member_id %></asp:Label></b>
                                    <asp:Label runat="server" ID="MergeFamilySpan" Style="float: right;">:</asp:Label>
                                </div>
                                <div class="form-group col-md-6" style="vertical-align: middle;">
                                    <asp:TextBox runat="server" type="text" onkeypress="numberOnly(event)" MaxLength="10" ID="MergeFamilyInput" placeholder=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                            <Columns>
                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" CommandName="Edit" CausesValidation="False" ID="ibtn1" ImageUrl="~/img/xp_users.gif" OnClick="GridView_Button_Divide" OnClientClick="return fnConfirmDivide();" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center">
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" CommandName="Edit2" CausesValidation="False" ID="ibtn2" ImageUrl="~/img/xp_users.gif" OnClick="GridView_Button_ChangeFirst" OnClientClick="return fnConfirmChangeFirst();" />
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
                    <div class="form-group">
                        <div class="text-center">
                            <%--<asp:Button id="processBtn" runat="server" Text="Process" class="btn btn-primary" onclick="processBtn_Click1" type="submit"/>--%>
                            <asp:Button ID="processBtn" runat="server" class="btn btn-primary" OnClick="processBtn_Click" Text="Process" />
                            <%--@* <button lang="jpn" type='button' id="processBtnJP" class="btn btn-primary">プロセス</button>*@--%>
                        </div>
                    </div>
                </div>
                <br />
                <br />
            </div>
        </div>



        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
        <%--@section scripts{--%>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
        <%--@* *********** Calculation Date*********** *@--%>
        <script>
            function fnConfirmDivide() {
                return confirm("Would you like to select this member to be the new first member and divide old first member out?");
            }
            function fnConfirmChangeFirst() {
                return confirm("Would you like to select this member to be the new first member?");
            }
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

            var tititi;

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

                var radioModeSpecial = $("[id='RadioButtonList1']:checked").val();

                if (radioModeSpecial == "Merge Family") {
                    $(mergeInput).show();
                    $(mergeTable).hide();
                    $(changeFirstTable).hide();

                }
                else if (radioModeSpecial == "Divide Family") {
                    $(mergeTable).show();
                    $(mergeInput).hide();
                    $(changeFirstTable).hide();

                }
                else if (radioModeSpecial == "Activate Member") {
                    location.reload();
                    $(mergeInput).hide();
                    $(mergeTable).hide();
                    $(changeFirstTable).hide();

                }
                else if (radioModeSpecial == "Change First Member") {
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

