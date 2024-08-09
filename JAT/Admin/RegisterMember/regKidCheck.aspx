<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="regKidCheck.aspx.cs" Inherits="JAT.Admin.RegisterMember.regKidCheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        h3 {
            font-weight: bold;options
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
                <asp:LinkButton ID="memberTab" Text="<%$ Resources:Resources,member_information %>" runat="server" OnClick="memberTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="familyTab" Text="<%$ Resources:Resources,family_member %>" runat="server" OnClick="familyTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks active">
                <asp:LinkButton ID="ChildrenTab" Text="<%$ Resources:Resources,children %>" runat="server"></asp:LinkButton>
            </button>
            <button class="tablinks">
              <asp:LinkButton ID="cancelTab" Text="✔" runat="server"  OnClick="Approve_Click"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="specialTab" Text="✘" runat="server" OnClientClick="ConfirmReject()" OnClick="Reject_Click"></asp:LinkButton>
            </button>
        </div>
        <div style="float: right;">
            <%--<label>Last Editor: </label>--%>
            <%--<asp:Label ID="updateBy" Text="" runat="server" />--%>
            <asp:Button ID="addBTN" runat="server" Text="Add" OnClick="addBTN_Click" />
            <%--<asp:Button ID="addBTN" runat="server" Text="Add" OnClick="addBTN_Click" />--%>
            <%--<button lang="eng" id="hideAddBtn" class="tablinks" onclick="addData()">ADD</button>--%>
            <%--@*<button lang="jpn" id="hideAddBtn" class="tablinks" onclick="addData()">新規</button>*@--%>
            <br />
            <br />
        </div>


        <%--%--@*ChildrenTab*@--%>
        <div>
            <div class="box">
                <br />
                <div class="box" style="background-color: lightgray">
                    <h3 lang="eng"><%=Resources.Resources.family_member_younger_than_18 %>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                    </h3>
                    <%--@*<h3 lang="jpn">家族会員 (18 歳未満)</h3>*@--%>
                    <form>
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
                                        <b>[<asp:Label ID="Label2" runat="server" Text=""></asp:Label>]</b>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group col-md-3">
                                    </div>
                                    <div class="form-group col-md-3">
                                        <div class="form-check" style="float: left; margin-right: 20px;">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Boy">&nbsp Boy &nbsp</asp:ListItem>
                                                <asp:ListItem Value="Girl">&nbsp Girl &nbsp</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
 

                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group col-md-3">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.name %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <input runat="server" type="text" id="Box1" maxlength="50" autocomplete="off" >
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Box1" ForeColor="Red"
                                            ErrorMessage="Name can't be empty." Visible="false"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group col-md-3">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.name_eng %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <input runat="server" type="text" id="Box2" maxlength="50" autocomplete="off" >
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Box2" ForeColor="Red"
                                            ErrorMessage="English Name can't be empty." Visible="false"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group col-md-3">
                                        <label style="color: red; visibility: hidden">*</label>
                                        <label lang="eng"><%=Resources.Resources.birth_date %></label>
                                        <span style="float: right;">:</span>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <input runat="server" type="text" id="Box3" class="datepicker5Input" placeholder="dd/mm/yyyy" autocomplete="off">
                                        <span id="datepicker5" class="glyphicon glyphicon-calendar"></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Box2" ForeColor="Red"
                                            ErrorMessage="Member Id can't be empty." Visible="false"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="text-align: center;">
                            <div class="text-center">
                                <asp:Button ID="saveBtn" runat="server" Text="Save" type="submit" class="btn btn-primary" Style="text-align: center;" OnClientClick="ConfirmSave()" OnClick="saveBtn_Click" />
                                <asp:Button ID="updateBtn" runat="server" Text="Save" type="submit" class="btn btn-primary" Style="text-align: center;" OnClientClick="ConfirmEdit()" OnClick="updateBtn_Click" />
                                <asp:Button ID="cancelBtn" runat="server" Text="Cancel" type="submit" class="btn btn-primary" CausesValidation="false" OnClick="cancelBtn_Click" />
                             </div>
                        </div>
                    </form>
                </div>
                <br />
                <br />

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>

                        <asp:BoundField HeaderText="" DataField="run_num" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                            <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                            <ItemStyle Width="80pt" />
                        </asp:BoundField>

                        <asp:BoundField HeaderText="" DataField="gender" HeaderStyle-CssClass="text-center">
                            <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                            <ItemStyle Width="80pt" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="<%$Resources:Resources,name %>" DataField="nameJp" HeaderStyle-CssClass="text-center">
                            <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="<%$Resources:Resources,name_eng %>" DataField="nameEn" HeaderStyle-CssClass="text-center">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                            <ItemStyle />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="<%$Resources:Resources,birth_date %>" DataField="birthdate" HeaderStyle-CssClass="text-center">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center">
                            <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton runat="server" CausesValidation="False" ID="ibtn1" ImageUrl="~/img/icon-pencil.gif" OnClick="GridView_Button_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center">
                            <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton runat="server" CommandName="Delete" CausesValidation="False" ID="ibtn2" ImageUrl="~/img/icon-delete.gif" OnClick="GridView_Delete_Click" OnClientClick="return fnConfirmDelete();" />
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

        <script language="javascript" type="text/javascript">
        function fnConfirmDelete() {
                   return confirm("Are you sure you want to delete this child?");
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
                   $("#datepicker5,.datepicker5Input").click(function () {
                       if (!$(".datepicker5Input").prop("disabled")) {
                           $('.datepicker5Input').datepicker(options).datepicker("show")
                       }
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