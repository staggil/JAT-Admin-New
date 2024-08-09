<%@ Page Title="Company Payment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="companyPayment.aspx.cs" Inherits="JAT.Company.companyPayment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .Grid td{
        padding:3px
    }
        .textarea
        {
            font-family:monospace;
            font-size:14px;
            resize: none;
        }
        body {
            font-size: 17px;
        }
        h3 {
            font-weight: bold;
        }
        textarea {
            border-radius: 5px;
            border: 1px solid #556677;
        }

        input#datepicker1Input, input#datepicker2Input, input#datepicker3Input, input#EffectiveDateInput {
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

        textareak {
            font-family: monospace;
        }
        .tab {
        overflow: hidden;
        border: 0px solid #ccc;
        border-top: 1px solid #ccc;
        background-color: #f1f1f1;
    }

        /* Style the buttons inside the tab */
        .tab button {
            background-color: inherit;
            float: left;
            border: none;
            outline: none;
            cursor: pointer;
            padding: 14px 22.4px;
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
    </style>
    <div class="col-sm-12">
        <div class="tab">
            <button class="tablinks">
                 <asp:LinkButton ID="MemberTab" Text="<%$Resources:Resources,member_information %>" runat="server" OnClick="MemberTab_Click"></asp:LinkButton>
            </button>
            <button class="tablinks active">
                 <asp:LinkButton ID="LinkButton2" Text="<%$Resources:Resources,company_payment %>" runat="server"></asp:LinkButton>
            </button>
        </div>
        <br />
    </div>
    <div style="float: right;">
        <label>Last Editor: </label>
        <asp:Label ID="updateBy" Text="" runat="server" />
        <asp:Button ID="add" runat="server" Text="Add" OnClick="add_Click"/>
        <asp:Button ID="edit" runat="server" Text="Edit" OnClick="edit_Click"/>
    </div>
    <br />
    <br />
    <div id="PaymentTab" class="tabcontent">
    <div class="">
            <div class="row">
                <div class="col-md-6">
                    <div class="box" style="background-color:lightgray">
                        <h3><%=Resources.Resources.payment_management %> &nbsp; <asp:Label ID="id" runat="server"></asp:Label></h3>
                        <%--<asp:Label ID="lbupdate" runat="server" Text="update"></asp:Label>--%>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color:red;visibility:hidden;">*</label>
                                        <label><%=Resources.Resources.company_name %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-7">
                                        <asp:Label ID="lbl" runat="server"></asp:Label>
                                        <asp:Label ID="Label1" style="display: none;" runat="server"></asp:Label>
                                        <asp:Label ID="Label2" style="display: none;" runat="server"></asp:Label>
                                        <asp:Label ID="PcompanyNmJ" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color:red;visibility:hidden;">*</label>
                                        <label><%=Resources.Resources.name_eng %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <%--<label>&nbsp</label>--%>
                                        <asp:Label ID="PcompanyNmE" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color:red;visibility:hidden;">*</label>
                                        <label><%=Resources.Resources.period %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <%--<label>&nbsp</label>--%>
                                        <asp:Label ID="PpayPeriod" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color:red;visibility:hidden;">*</label>
                                        <label><%=Resources.Resources.pay_duration %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <%--<label>&nbsp</label>--%>
                                        <asp:Label ID="PpayDuration" runat="server" Text=""></asp:Label>
                                        <label><%=Resources.Resources.months %></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color:red;visibility:hidden;">*</label>
                                        <label><%=Resources.Resources.send_method %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <%--<label>&nbsp</label>--%>
                                        <asp:Label ID="PsendType" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color:red;visibility:hidden;">*</label>
                                        <label><%=Resources.Resources.payment_method %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <asp:Label ID="payMethod" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color:red;visibility:hidden;">*</label>
                                        <label><%=Resources.Resources.total_month %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <asp:dropdownlist id="totalPerMonth" runat="server">
						                </asp:dropdownlist>
                                        <label><%=Resources.Resources.baht %></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label>&nbsp</label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <input id="checkShort" type="checkbox" runat="server"/>
                                        <label><%=Resources.Resources.get_short %></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color:red;">*</label>
                                        <label><%=Resources.Resources.b_no %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <input type="text" id="bNo" class="form-control" runat="server" maxlength="20" required>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color:red;">*</label>
                                        <label><%=Resources.Resources.c_no %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <input type="text" id="cNo" class="form-control" placeholder="" runat="server" maxlength="20" required>
                                        <%--<asp:TextBox ID="cNo" runat="server" Width="50%"></asp:TextBox>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color:red;">*</label>
                                        <label><%=Resources.Resources.pay_duration %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <input id="noPayMonth" type="number" min="0" class="form-control" runat="server" max="9999" required>
                                        <%--<input id="noPayMonth" type="number" onchange="setexpiredate();" min="0" class="form-control" runat="server">--%>
                                        <%--<asp:TextBox ID="TextBox3" runat="server" Width="80%"></asp:TextBox>--%>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label><%=Resources.Resources.months %></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color:red;">*</label>
                                        <label><%=Resources.Resources.payment_date %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-7">
                                        <input type="text" id="paymentDate" style="width: inherit;" autocomplete="off" placeholder="dd/MM/yyyy" runat="server" required readonly="readonly" disabled="disabled" >
                                        <%--<asp:TextBox ID="paymentDate" runat="server" Width="50%" ReadOnly = "true"></asp:TextBox>--%>
                                        <span id="datepicker3" class="glyphicon glyphicon-calendar"></span>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color:red;">*</label>
                                        <label><%=Resources.Resources.effective_date %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-7">
                                        <input type="text" id="effectiveDate" style="width: inherit;" oncuechange="setExpireDate();" autocomplete="off" placeholder="dd/MM/yyyy" runat="server" required readonly="readonly" disabled="disabled">
                                        <span id="EffectiveDate" class="glyphicon glyphicon-calendar"></span>
                                        <%--<asp:TextBox ID="effectiveDate" runat="server" Width="50%" ReadOnly = "true"></asp:TextBox>
                                        <span id="EffectiveDate" class="glyphicon glyphicon-calendar"></span>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-md-5">
                                        <label style="color:red;visibility:hidden">*</label>
                                        <label><%=Resources.Resources.expired_date %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <input type="text" id="expiredDate" autocomplete="off" class="form-control" runat="server">
                                        <asp:HiddenField ID="HiddenExpiredDate" />
                                        <%--<asp:TextBox ID="TextBox6" runat="server" Width="90%" ReadOnly = "true"></asp:TextBox>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-5" style="margin-bottom: 0;">
                                <label style="color:red;visibility:hidden">*</label>
                                <label for=""><%=Resources.Resources.remark %></label>
                                <span style="float:right;">:</span>
                            </div>
                        </div>
                        <div id="charNum2" style="text-align:right;width:400px;font-size:10px;">500/500</div>    
                        <asp:TextBox id="Remark1" TextMode="multiline" onkeyup="countChar2(this)" CssClass="textarea" width="395px"  Rows="10" runat="server" maxlength="500" />
                        
                        <%--<div id="charNum2" style="text-align:right;width:400px;font-size:10px;">500/500</div>
                        <textarea onkeyup="countChar2(this)" style="font-family: monospace;width:395px; font-size:14px; overflow: hidden; resize: none;" maxlength="500" rows="10"></textarea>--%>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box" style="background-color:lightgray">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label style="color:red;">*</label>
                                        <label><%=Resources.Resources.payment_type %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <input type="radio" id="pay70" value="pay70" runat="server" checked>
                                        <label for="html">70% 30%</label><br>
                                        <input type="radio" id="pay100" value="pay100" runat="server">
                                        <label for="html">100%</label><br>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label style="color:red;">*</label>
                                        <label><%=Resources.Resources.member_fee %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <input type="text" id="memberFee" class="form-control" runat="server">
                                        <%--<asp:TextBox ID="memberfee" runat="server" Width="100%"></asp:TextBox>--%>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label><%=Resources.Resources.baht %></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label style="color:red;">*</label>
                                        <label><%=Resources.Resources.news_fee %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <input type="text" id="newsFee" class="form-control" runat="server">
                                        <%--<asp:TextBox ID="newsFee" runat="server" Width="100%"></asp:TextBox>--%>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label><%=Resources.Resources.baht %></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label style="color:red;">*</label>
                                        <label><%=Resources.Resources.total_fee %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <input type="text" id="totalFee" class="form-control" runat="server">
                                        <%--<asp:TextBox ID="totalFee" runat="server" Width="100%"></asp:TextBox>--%>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label><%=Resources.Resources.baht %></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-4" style="margin-bottom:0">
                                <label style="color:red;visibility:hidden">*</label>
                                <label for=""><%=Resources.Resources.remark %></label>
                                <span style="float:right;">:</span>
                            </div>
                        </div>
                        <div id="charNum3" style="text-align:right;width:400px;font-size:10px;">500/500</div>    
                        <asp:TextBox id="payRemark" TextMode="multiline" onkeyup="countChar3(this)" CssClass="textarea" width="395px"  Rows="10" runat="server" maxlength="500" />
                        <%--<div id="charNum3" style="text-align:right;width:400px;font-size:10px;">500/500</div>
                        <textarea onkeyup="countChar3(this)" style="font-family: monospace;width:395px; font-size:14px; overflow: hidden; resize: none;" maxlength="500" rows="10"></textarea>--%>
                        <div class="form-group">
                            <div class="text-center">
                                <button type="button" id="cclt" runat="server">Calculation</button>
                                <%--<asp:Button ID="cclt" runat="server" Text="Button" OnClick="cclt_Click" />--%>
                                <%--<asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />--%>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="box" style="background-color:lightgray">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-md-4" ">
                                        <label style="color:red;visibility:hidden">*</label>
                                        <label><%=Resources.Resources.bank_account %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <asp:dropdownlist id="cboBankAcc" runat="server">
                                            <asp:ListItem Text="--Any--" Value="0" />
						                </asp:dropdownlist>
                                        <input type="text" class="form-control" id="accNumber" runat="server">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <label style="color:red;visibility:hidden">*</label>
                                        <label><%=Resources.Resources.bank_code %></label>
                                        <span style="float:right;">:</span>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <input type="text" class="form-control" id="bankCode" runat="server">
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="text-center">
                                <%--<button type="submit" class="btn btn-primary">Save</button>--%>
                                <asp:Button ID="save" class="btn btn-primary" OnClick="save_Click" runat="server" Text="save" />
                                <asp:Button ID="update" class="btn btn-primary" OnClick="update_Click" runat="server" Text="update" />
                                <asp:Button ID="cancel" class="btn btn-primary" OnClick="cancel_Click" runat="server" Text="cancel" UseSubmitBehavior="false"/>                                
                                <%--<asp:Button ID="open" class="btn btn-primary" OnClick="open_Click" runat="server" Text="open" />
                                <asp:Button ID="close" class="btn btn-primary" OnClick="close_Click" runat="server" Text="close" />                                --%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
    </div>
    <asp:datagrid id="DataGrid1" CssClass="Grid" AutoGenerateColumns="false" OnItemCommand="DataGrid1_ItemCommand" runat="server" CellPadding="4" BackColor="White" BorderWidth="1px"
		DataKeyField="PcompanyNmJ" 
		BorderStyle="None" BorderColor="InactiveCaptionText" PageSize="20" Width="100%">
		<FooterStyle ForeColor="Desktop" BackColor="#B5C7DE"></FooterStyle>
		<SelectedItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#738A9C"></SelectedItemStyle>
		<AlternatingItemStyle BackColor="#DCDCDC"></AlternatingItemStyle>
		<ItemStyle ForeColor="Desktop" BackColor="White"></ItemStyle>
		<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#24227A"></HeaderStyle>
		<Columns>
            <asp:BoundColumn DataField="tranId" Visible="false" HeaderText="">
                <HeaderStyle HorizontalAlign="center" Width="5%"></HeaderStyle>
                <ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
            </asp:BoundColumn>
			<asp:BoundColumn DataField="PcompanyNmJ" HeaderText="<%$Resources:Resources,company_name %>">
				<HeaderStyle HorizontalAlign="center" Width="20%"></HeaderStyle>
				<ItemStyle ForeColor="#24227A"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="totalFee" HeaderText="<%$Resources:Resources,total_fee %>" DataFormatString="{0:N}">
				<HeaderStyle HorizontalAlign="center" Width="10%"></HeaderStyle>
				<ItemStyle HorizontalAlign="Right" ForeColor="#24227A"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="paymentDate" HeaderText="<%$Resources:Resources,payment_date %>" DataFormatString="{0:dd-MMM-yyyy}">
				<HeaderStyle HorizontalAlign="center" Width="10%"></HeaderStyle>
				<ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
			</asp:BoundColumn>
            <asp:BoundColumn DataField="effectiveDate" HeaderText="<%$Resources:Resources,effective_date %>" DataFormatString="{0:dd-MMM-yyyy}">
				<HeaderStyle HorizontalAlign="center" Width="10%"></HeaderStyle>
				<ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
			</asp:BoundColumn>
            <asp:BoundColumn DataField="expiredDate" HeaderText="<%$Resources:Resources,expired_date %>" DataFormatString="{0:dd-MMM-yyyy}">
				<HeaderStyle HorizontalAlign="center" Width="10%"></HeaderStyle>
				<ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
			</asp:BoundColumn>
            <asp:BoundColumn DataField="payMethod" HeaderText="<%$Resources:Resources,payment_method %>">
				<HeaderStyle HorizontalAlign="center" Width="10%"></HeaderStyle>
				<ItemStyle HorizontalAlign="Center" ForeColor="#24227A"></ItemStyle>
			</asp:BoundColumn>
            <asp:BoundColumn DataField="bNo" HeaderText="<%$Resources:Resources,b_no %>">
				<HeaderStyle HorizontalAlign="center" Width="5%"></HeaderStyle>
				<ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
			</asp:BoundColumn>
            <asp:BoundColumn DataField="cNo" HeaderText="<%$Resources:Resources,c_no %>">
				<HeaderStyle HorizontalAlign="center" Width="5%"></HeaderStyle>
				<ItemStyle HorizontalAlign="center" ForeColor="#24227A"></ItemStyle>
			</asp:BoundColumn>
            <asp:BoundColumn DataField="payRemark" HeaderText="Pay Remark">
                <ItemStyle HorizontalAlign="left" Width="10%" ForeColor="#24227A"></ItemStyle>
            </asp:BoundColumn>
            <asp:templatecolumn itemstyle-horizontalalign="center" headertext="Edit">
                <itemtemplate>
                    <asp:imagebutton id="btntrandedit" imageurl="../images/icon-pencil.gif" CommandArgument='<%#Eval("tranId")+","+ Eval("accId")%>' commandname="edit" runat="server" />
                </itemtemplate>
            </asp:templatecolumn>

            <asp:templatecolumn itemstyle-horizontalalign="center" headertext="Delete"> 
                <itemtemplate>
                    <asp:imagebutton id="btntranddelete" imageurl="../images/icon-delete.gif" OnClientClick="ConfirmDelete()" CommandArgument='<%#Eval("tranId")+","+ Eval("accId")%>' commandname="delete" runat="server" />
                </itemtemplate>
            </asp:templatecolumn>
		</Columns>
	</asp:datagrid>
</div>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
<%--@section scripts{--%>

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    
    <%--Alert confirm delete--%>
    <script type="text/javascript">
        function ConfirmDelete() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete this company payment data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
            console.log(confirm_value);
            
        }
    </script>

    <script type="text/javascript">
        $(function () {
            var options = {
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                autoclose: true
            }
            /*$("[id*=appliedDate]").datepicker(options);*/
            //$("[id*=establishedDate]").datepicker(options);
            $("[id*=paymentDate]").datepicker(options);
            $("[id*=effectiveDate]").datepicker(options);
            $("#datepicker3").click(function () {
                if (!$("[id*=paymentDate]").prop("disabled")) {
                    $("[id*=paymentDate]").datepicker(options).datepicker("show")
                }
            });
            $("#EffectiveDate").click(function () {
                if (!$("[id*=effectiveDate]").prop("disabled")) {
                    $("[id*=effectiveDate]").datepicker(options).datepicker("show")
                }
            });
        });
    </script>

    <script type="text/javascript">

</script>
    <%--@* *********** Selection Depentdent*********** *@--%>
    <script>
        $(document).ready(function () {
            $("[id*=memberStatus]").change(function () {
                var val = $(this).val();
                if (val == "A") {
                    $("[id*=sendType]").html("<option value=''>-- Any --</option><option selected value='Z'>Z</option><option value='#'>#</option>");
                } else if (val == "NA") {
                    $("[id*=sendType]").html("<option value=''>-- Any --</option><option selected value='#'>#</option><option value='Z'>Z</option>");
                }
            });
        });
    </script>
    <%--@* *********** /Selection Depentdent*********** *@--%>

    <script>
        $(document).ready(function () {
            $("[id*=bNo]").change(function () {
                var v = $(this).val();
                $("[id*=cNo]").val(v);
            });
        });
    </script>

    <%-- @* *********** Period 1-12 *********** *@ --%>
    <script>
        $(function () {
            var $select = $(".1-12");
            for (i = 1; i <= 12; i++) {
                $select.append($('<option value="' + i + '"></option>').val(i).html(i))
            }
        });
    </script>
   <%-- @* *********** /Period 1-12*********** *@--%>

    <%--@* *********** Calculation Date*********** *@--%>
    <script>
        $(document).ready(function () {
            $("[id*=effectiveDate],[id*=noPayMonth]").change(function () {
                //var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
                var months = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
                var effectiveDate = $("[id*=effectiveDate]").val();
                //alert();
                var myDate = new Date(effectiveDate.toString().split('/')[1] + "/" + effectiveDate.toString().split('/')[0] + "/" + effectiveDate.toString().split('/')[2]);
                month = myDate.getMonth();
                modifiedMonth = month + parseInt($("[id*=noPayMonth]").val());
                //alert(modifiedMonth);
                modifiedDate = new Date(myDate.getFullYear(), modifiedMonth, 0);
                modifiedMonth = months[modifiedDate.getMonth() + 0];
                //alert(modifiedMonth);
                if (modifiedDate.getDate() + "-" + modifiedMonth + "-" + modifiedDate.getFullYear() != "NaN-undefined-NaN") {
                    //expiredDate.value = modifiedDate.getDate() + "-" + modifiedMonth + "-" + modifiedDate.getFullYear();
                    var r = modifiedDate.getDate() + "/" + modifiedMonth + "/" + modifiedDate.getFullYear();
                    $("[id*=expiredDate]").val(r);
                } else {
                    //expiredDate.value = " ";
                    $("[id*=expiredDate]").val(" ");
                }
            });
        });

    </script>
    <%--@* *********** /Calculation Date*********** *@--%>

    <%--@* *********** Datepicker *********** *@--%>
    <script>

        //var options = {
        //    format: 'dd-M-yyyy',
        //    todayHighlight: true,
        //    autoclose: true
        //}
        //$(function () {
        //    $("#datepicker1").click(function () {
        //        $('#datepicker1Input').datepicker(options).datepicker("show")
        //    });

        //    $("#datepicker2").click(function () {
        //        $('#datepicker2Input').datepicker(options).datepicker("show")
        //    });
        //    $("#datepicker3").click(function () {
        //        $('#datepicker3Input').datepicker(options).datepicker("show")
        //    });
        //    $("#EffectiveDate").click(function () {
        //        $('#EffectiveDateInput').datepicker(options).datepicker("show")
        //    });
        //});
    </script>
    <%--@* *********** /Datepicker *********** *@--%>

    <%--@* ***********  Calculation Total Fee *********** *@--%>
    <script>
        $(document).ready(function () {
            $("[id*=cclt]").click(function () {
                var tm = $("[id*=totalPerMonth]").val(); 
                var pd = $("[id*=noPayMonth]").val();
                var totalfee;
                var MemberFee;
                var NewsFee;

                if ($('[id*=pay70]').is(':checked'))
                {
                    //console.log("it's checked");
                    totalfee = tm * pd;
                    MemberFee = totalfee * 0.3;
                    NewsFee = totalfee * 0.7;
                }

                if ($('[id*=pay100]').is(':checked')) {
                    //console.log("it's checked");
                    MemberFee = tm * pd;
                    NewsFee = 0;
                    var vat = (MemberFee * 7) / 100;
                    totalfee = MemberFee + vat;
                }

                //var totalfee = tm * pd;
                //var MemberFee = totalfee * 0.3
                //var NewsFee = totalfee * 0.7

                if (tm == 1000) {
                    $("[id*=newsFee]").val(NewsFee);
                    $("[id*=memberFee]").val(MemberFee);
                    $("[id*=totalFee]").val(totalfee);
                } else if (tm == 1500){
                    $("[id*=newsFee]").val(NewsFee);
                    $("[id*=memberFee]").val(MemberFee);
                    $("[id*=totalFee]").val(totalfee);
                } else if (tm == 2000) {
                    $("[id*=newsFee]").val(NewsFee);
                    $("[id*=memberFee]").val(MemberFee);
                    $("[id*=totalFee]").val(totalfee);
                } else if (tm == 2500) {
                    $("[id*=newsFee]").val(NewsFee);
                    $("[id*=memberFee]").val(MemberFee);
                    $("[id*=totalFee]").val(totalfee);
                } else if (tm == 1300) {
                    $("[id*=newsFee]").val(NewsFee);
                    $("[id*=memberFee]").val(MemberFee);
                    $("[id*=totalFee]").val(totalfee);
                }
            });
        });
    </script>
    <%--@* ***********  /Calculation Total Fee *********** *@--%>
    
    <script>
        $(document).ready(function () {
            $("[id*=cboBankAcc]").change(function () {
                var selectValue = $(this).val();
                var arr = selectValue.split(":");

                $("[id*=accNumber],[id*=bankCode]").each(function (index) {
                    $(this).val(arr[index]);
                });
            });
        });
    </script>

<script>
    //@* *********** Auto Expand Area Text *********** *@
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
    //            autoExpand(e, tag[i])
    //        }
    //    }
    //})()
    //@* *********** /Auto Expand Area Text *********** *@

    //@* *********** count number 500 / 500 *********** *@
    function countChar(val) {
        var len = val.value.length;
        if (len >= 501) {
            val.value = val.value.substring(0, 500);
        } else {
            $('#charNum').text(500 - len + '/500');
        }
    };
    function countChar2(val) {
        var len = val.value.length;
        if (len >= 501) {
            val.value = val.value.substring(0, 500);
        } else {
            $('#charNum2').text(500 - len + '/500');
        }
    };
    function countChar3(val) {
        var len = val.value.length;
        if (len >= 501) {
            val.value = val.value.substring(0, 500);
        } else {
            $('#charNum3').text(500 - len + '/500');
        }
    };
    //@* *********** /count number 500 / 500 *********** *@

    document.getElementById("MemberTab").style.display = "block";
    var canType = document.querySelectorAll("[id='blockInput']");
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

    function addData() {
        $(canType).prop("disabled", false);
        document.getElementById("hideAddBtn").style.display = "none";

    }

</script>
</asp:Content>
