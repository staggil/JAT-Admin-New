<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="regApprove.aspx.cs" Inherits="JAT.Admin.RegisterMember.regApprove" %>

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
    <div class="col-sm-12">
         <asp:Label ID="Label3" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>
        <asp:Label ID="Label4" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red"></asp:Label>
        <br/>

        <div class="tab">
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
                <asp:LinkButton ID="cancelTab" Text="✔" runat="server"></asp:LinkButton>
            </button>
            <button class="tablinks">
                <asp:LinkButton ID="specialTab" Text="✘" runat="server" OnClientClick="ConfirmReject()"></asp:LinkButton>
            </button>
        </div>
        <div>
            <div class="box">
                <div class="box" style="background-color: lightgray">

                    <h3 lang="eng"><%=Resources.Resources.member_information %></h3>
                    <br />
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" Width="100%" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundField HeaderText="<%$Resources:Resources,member_id %>" DataField="firstmember_id" HeaderStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$Resources:Resources,name %>" DataField="nameJp" HeaderStyle-CssClass="text-center">
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

                            <asp:BoundField HeaderText="<%$Resources:Resources,member_type %>" DataField="memberType" HeaderStyle-CssClass="text-center">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$Resources:Resources,member_status %>" DataField="memberStatus" HeaderStyle-CssClass="text-center">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                                <ItemStyle HorizontalAlign="Center" />
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
                    <h3><%=Resources.Resources.family_member %></h3>
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" Width="100%" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundField HeaderText="<%$Resources:Resources,member_id %>" DataField="familymember_id" HeaderStyle-CssClass="text-center">
                                <HeaderStyle BackColor="#24227A" ForeColor="White" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$Resources:Resources,name %>" DataField="nameJp" HeaderStyle-CssClass="text-center">
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

                            <asp:BoundField HeaderText="<%$Resources:Resources,member_type %>" DataField="memberType" HeaderStyle-CssClass="text-center">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="<%$Resources:Resources,member_status %>" DataField="memberStatus" HeaderStyle-CssClass="text-center">
                                <HeaderStyle HorizontalAlign="Center" BackColor="#24227A" />
                                <ItemStyle HorizontalAlign="Center" />
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
                    <br />
                    <h3 lang="eng"><%=Resources.Resources.family_member_younger_than_18 %></h3>
                    <br />
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>

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
                    <br />
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
                                        <asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" />
                                        <label style="font-weight: 100;" lang="eng">Personal payment</label><br>
                                        <asp:CheckBox ID="CheckBox2" runat="server"  Enabled="false"/>
                                        <label style="font-weight: 100;" lang="eng">Company payment</label><br>
                                        <textarea class="form-control" readonly="readonly" id="TextareaPayment" runat="server" style="margin: 0px -30.75px 0px 0px; width: 250px; height: 93px;" maxlength="125" autocomplete="off"></textarea>
                                    </div>
                                    
                                </div>

                            </div>
                        </div>
                         </div>
                      <div class="form-group" style="text-align: center;">
                            <div class="text-center">
                                <asp:Button ID="saveBtn" runat="server" Text="Approve" type="submit" class="btn btn-primary" Style="text-align: center;" OnClientClick="ConfirmApprove()" OnClick="saveBtn_Click"/>
                            </div>
                        </div>
                </div>

            </div>
        </div>


    </div>

    <script type="text/javascript">
        function insertFID() {
            alert("Please insert firstmember id!!");
        }
        function insertFamID() {
            alert("Please insert Family Member id!!");
        }
        function DupicateFamID() {
            alert("Please check your Family Member id!!");
        }
        function DupicateFamINDB() {
            alert("Your Family Member id already use");
        }
        function DupicateFINDB() {
            alert("Your First Member id already use");
        }
    </script>
</asp:Content>

