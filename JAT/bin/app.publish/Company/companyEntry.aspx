<%@ Page Title="Company Entry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="companyEntry.aspx.cs" Inherits="JAT.Company.companyEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
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

        /* Style the tab content */
        .tabcontent {
            display: none;
            padding: 6px 12px;
            /*border: 1px solid #ccc;*/
            border-top: none;
        }
    </style>
<div class="col-sm-12">
<%--    @*<h1>&nbsp;Private Entry</h1>*@--%>
    <div class="tab">
        <%--<button class="tablinks active" onclick="changeTAB(event, 'MemberTab')">Member Information</button>
        <button class="tablinks" onclick="changeTAB(event, 'PaymentTab')">Payment Management</button>--%>
        <button class="tablinks active">
             <asp:LinkButton ID="LinkButton1" Text="<%$Resources:Resources,member_information %>" runat="server" ></asp:LinkButton>
        </button>
        <button class="tablinks">
             <asp:LinkButton ID="PaymentTab" Text="<%$Resources:Resources,company_payment %>" runat="server" OnClick="PaymentTab_Click" ></asp:LinkButton>
        </button>
    </div>
</div>
<br />
<br />
<br />
<div style="float: right;">
    <label>Last Editor: </label>
    <asp:Label ID="updateBy" Text="" runat="server" />
    <asp:Button ID="addBTN" runat="server" Text="Edit" Visible="false" OnClick="addBTN_Click" />
</div>
<%--@*Member Information*@--%>
<div id="MemberTab" class="tabcontent">
    <br />
    <div class="">
            <div class="box" style="background-color:lightgray">
                <h3><%=Resources.Resources.member_information %> &nbsp; <asp:Label ID="id" runat="server"></asp:Label> &nbsp; <asp:Label ID="lbupdate" runat="server"></asp:Label></h3>
                <%--<asp:Label ID="lbupdate" runat="server" Text="update"></asp:Label>--%>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="form-group col-lg-3">
                                <label style="color:red">*</label>
                                <label><%=Resources.Resources.company_name %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-7">
                                <asp:Label ID="lbl" style="display: none;" runat="server"></asp:Label>
                                <input id="companyNmJ" class="form-control" type="text" runat="server" maxlength="80" required/>
                                
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="form-group col-lg-3">
                                <label style="color:red">*</label>
                                <label><%=Resources.Resources.name_eng %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-7">
                                <input id="companyNmE" class="form-control" type="text" runat="server" maxlength="80" required/>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="form-group col-lg-3">
                                <label style="color:red">*</label>
                                <label><%=Resources.Resources.comBill %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-7">
                                <%--<input type="text" class="form-control" placeholder="">--%>
                                <input id="companyNmEE" class="form-control" type="text" runat="server" maxlength="80"/>
                                <%--<asp:TextBox ID="companyNmEE" runat="server"></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="form-group col-lg-6">
                                <label style="color:red">*</label>
                                <label><%=Resources.Resources.business_type %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-6">
                                <input id="busType" class="form-control" type="text" runat="server" maxlength="100" required/>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="form-group col-lg-5">
                                <label style="color:red">*</label>
                                <label><%=Resources.Resources.applied_date %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-6">
                                <%--<input  type="text" id="datepicker1Input" autocomplete="off" placeholder="dd/MM/yyyy">--%>
                                <%--<asp:TextBox ID="appliedDate" runat="server" Width="50%" ReadOnly = "true" ></asp:TextBox>--%>
                                <input  type="text" id="appliedDate" style="width: inherit;" autocomplete="off" placeholder="dd/mm/yyyy" runat="server" required>
                                <span id="datepicker1" class="glyphicon glyphicon-calendar"></span>
                                <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />--%>
                                <%--<span id="datepicker1" class="glyphicon glyphicon-calendar"></span>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="form-group col-lg-3">
                                <label style="color:red">&nbsp;&nbsp;</label>
                                <label>Tax ID</label>
                                <label></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-7">
                                <input type="text" runat="server" id="taxID" class="form-control" maxlength="100"/>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="form-group col-lg-3">
                                <label style="color:red">*</label>
                                <label><%=Resources.Resources.address %></label>

                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-7">
                                <textarea id="address" style="resize:none; height:140px;" class="form-control" runat="server" maxlength="500"></textarea>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="form-group col-lg-3">
                                <label style="color:red">*</label>
                                <label><%=Resources.Resources.telephone %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-3">
                                    <input type="text" id="phone" style="width: 90%;" runat="server" maxlength="20" required>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="form-group col-lg-3">
                                <label style="color:red">*</label>
                                <label><%=Resources.Resources.fax %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-3">
                                    <input type="text" id="fax" style="width: 90%;" placeholder="xxx-xxx-xxxx" runat="server" maxlength="20">
                            </div>
                        </div>
                    </div>
                </div>





                <div class="row">
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="form-group col-lg-6">
                                <label style="color:red;visibility: hidden;">*</label>
                                <label><%=Resources.Resources.e_mail %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-6">
                                <input type="email" id="email" class="form-control" placeholder="" runat="server" maxlength="40">
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="form-group col-lg-5">
                                <label style="color:red;">*</label>
                                <label><%=Resources.Resources.established_date %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-6">
                                <%--<input type="text" id="datepicker2Input" autocomplete="off" class="" placeholder="dd/MM/yyyy">--%>
                                <%--<asp:TextBox ID="establishedDate" runat="server"></asp:TextBox>--%>
                                <%--<asp:TextBox ID="establishedDate" runat="server" Width="50%" ReadOnly = "true" ></asp:TextBox>--%>
                                <input  type="text" id="establishedDate" style="width: inherit;" autocomplete="off" placeholder="dd/mm/yyyy" runat="server" required>
                                <span id="datepicker2" class="glyphicon glyphicon-calendar"></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="form-group col-lg-6">
                                <label style="color:red;">*</label>
                                <label><%=Resources.Resources.send_method %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-3">
                                <select id="sendType" runat="server" required>
                                    <option value="">-- Any --</option>
                                    <option selected value="Z">Z</option>
                                    <option value="#">#</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="form-group col-lg-5">
                                <label style="color:red;">*</label>
                                <label><%=Resources.Resources.member_status %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-3">
                                <select id="memberStatus" runat="server" required>
                                    <option value="A">A</option>
                                    <option value="NA">NA</option>
                                </select>
                                <asp:Label ID="statusdate" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="form-group col-lg-6">
                                <label style="color:red;">*</label>
                                <label><%=Resources.Resources.represent_person_id %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-3">
                                <input type="text" id="represID" class="form-control" runat="server" maxlength="10">
                            </div>
                            <div class="form-group col-lg-2">
                                <asp:Button ID="Retrieve" Height="25px" runat="server" OnClick="Retrieve_Click" Text="Retrieve" formnovalidate/>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="form-group col-lg-5">
                                <label style="color:red;visibility: hidden;">*</label>
                                <label><%=Resources.Resources.member_status %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-6">
                                <asp:Label ID="represMem" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="form-group col-lg-12">
                                <label style="color:red;">*</label>
                                <label><%=Resources.Resources.represent_person %></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="col-lg-6 form-group">
                                <label style="color:red;visibility:hidden;">****</label>
                                <label><%=Resources.Resources.name1 %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-lg-3 form-group">
                                <input  type="text" id="represNm" class="form-control" runat="server" maxlength="50"/>
                            </div>
                            <div class="col-lg-3 form-group">
                                <input  type="text" id="represNmE" class="form-control" name="" value="" runat="server" maxlength="40"/>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-lg-5 form-group">
                                <label style="color:red;visibility:hidden;">*</label>
                                <label><%=Resources.Resources.e_mail_address %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="col-lg-4 form-group">
                                <input  type="email" id="represEm" class="form-control" runat="server" maxlength="50"/>
                                <%--<asp:TextBox ID="represEm" runat="server" ></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="col-lg-6 form-group">
                                <label style="color:red;visibility:hidden;">****</label>
                                <label><%=Resources.Resources.telephone1 %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="col-lg-4 form-group">
                                <input type="text" id="represTp" class="form-control" runat="server" maxlength="50"/>
                                <%--<asp:TextBox ID="represTp" runat="server" ></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="col-lg-5 form-group">
                                <label style="color:red;visibility:hidden;">*</label>
                                <label><%=Resources.Resources.position %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="col-lg-4 form-group">
                                <input  type="text" id="represPosition" class="form-control" runat="server" maxlength="50"/>
                                <%--<asp:TextBox ID="represPosition" runat="server" ></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="form-group col-lg-12">
                                <label style="color:red;visibility:hidden;">*</label>
                                <label><%=Resources.Resources.person_in_charge %></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="col-lg-6 form-group">
                                <label style="color:red;visibility:hidden;">****</label>
                                <label><%=Resources.Resources.name1 %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="col-lg-3 form-group">
                                <input type="text" id="personinchargeNm" class="form-control" runat="server" maxlength="50"/>
                                <%--<asp:TextBox ID="personinchargeNm" runat="server" Width="100%"></asp:TextBox>--%>
                            </div>
                            <div class="col-lg-3 form-group">
                                <input  type="text" id="personinchargeNmE" class="form-control" runat="server" maxlength="40"/>
                                <%--<asp:TextBox ID="personinchargeNmE" runat="server" Width="100%"></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="col-lg-5 form-group">
                                <label style="color:red;visibility:hidden;">*</label>
                                <label><%=Resources.Resources.e_mail_address %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="col-lg-4 form-group">
                                <input  type="email" id="personinchargeEm" class="form-control" runat="server" maxlength="50"/>
                                <%--<asp:TextBox ID="personinchargeEm" runat="server"></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6">
                        <div class="row">
                            <div class="col-lg-6 form-group">
                                <label style="color:red;visibility:hidden;">****</label>
                                <label><%=Resources.Resources.telephone1 %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="col-md-4 form-group">
                                <input  type="text" id="personinchargeTp" class="form-control" runat="server" maxlength="50"/>
                                <%--<asp:TextBox ID="personinchargeTp" runat="server"></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-5 form-group">
                                <label style="color:red;visibility:hidden;">*</label>
                                <label><%=Resources.Resources.position %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="col-md-4 form-group">
                                <input  type="text" id="personinchargePosition" class="form-control" runat="server" maxlength="50"/>
                                <%--<asp:TextBox ID="personinchargePosition" runat="server"></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="form-group col-md-12">
                                <label style="color:red;visibility:hidden;">*</label>
                                <label><%=Resources.Resources.accounting %></label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-6 form-group">
                                <label style="color:red;visibility:hidden;">****</label>
                                <label><%=Resources.Resources.name1 %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="col-md-3 form-group">
                                <input  type="text" id="AccNm" class="form-control" runat="server" maxlength="50"/>
                                <%--<asp:TextBox ID="AccNm" runat="server" Width="100%"></asp:TextBox>--%>
                            </div>
                            <div class="col-md-3 form-group">
                                <input  type="text" id="AccNmE" class="form-control" runat="server" maxlength="40"/>
                                <%--<asp:TextBox ID="AccNmE" runat="server" Width="100%"></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-5 form-group">
                                <label style="color:red;visibility:hidden;">*</label>
                                <label><%=Resources.Resources.e_mail_address %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="col-md-4 form-group">
                                <input  type="email" id="AccEm" class="form-control" runat="server" maxlength="50"/>
                                <%--<asp:TextBox ID="AccEm" runat="server"></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-6 form-group">
                                <label style="color:red;visibility:hidden;">****</label>
                                <label><%=Resources.Resources.telephone1 %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="col-md-4 form-group">
                                <input  type="text" id="AccTp" class="form-control" runat="server" maxlength="50"/>
                                <%--<asp:TextBox ID="AccTp" runat="server"></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-5 form-group">
                                <label style="color:red;visibility:hidden;">*</label>
                                <label><%=Resources.Resources.position %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="col-md-4 form-group">
                                <input  type="text" id="AccPosition" class="form-control" runat="server" maxlength="50"/>
                                <%--<asp:TextBox ID="AccPosition" runat="server"></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="row">
                            <div class="form-group col-md-6">
                                <label style="color:red;visibility: hidden;">*</label>
                                <label for=""><%=Resources.Resources.remark %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-md-5">
                                <div id="charNum" style="text-align:right;width:400px;font-size:10px;">500/500</div>
                                <%--<textarea onkeyup="countChar(this)" style="font-family: monospace;width:395px; font-size:14px; overflow: hidden; resize: none;" maxlength="500" rows="10"></textarea>--%>
                                <asp:TextBox id="remark" TextMode="multiline" onkeyup="countChar(this)" CssClass="textarea" width="395px"  Rows="10" runat="server" MaxLength="500" />
                                <%--<asp:TextBox ID="remark" runat="server"></asp:TextBox>--%>                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="box" style="background-color:lightgray">
                <h3><%=Resources.Resources.payment_information %></h3>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="form-group col-md-6">
                                <label>&nbsp</label>
                                <span style="float:right;"></span>
                            </div>
                            <div class="form-group col-md-6">
                                <%--<input type="checkbox" id="getInvoice" runat="server" required>--%>
                                <asp:CheckBox ID="getInvoice" runat="server" required/>
                                <label style=""><%=Resources.Resources.get_receipt %></label>
                                <%--<input type="checkbox" id="withHolding" runat="server">--%>
                                <asp:CheckBox ID="withHolding" runat="server"/>
                                <label>W/H Tax</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="form-group col-md-6">
                                <label style="color:red;">*</label>
                                <label><%=Resources.Resources.payment_type %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-md-3">
                                <select id="payMethod" runat="server" required>
                                    <option selected value="">--Any--</option>
                                    <option value="B">B</option>
                                    <option value="F">F</option>
                                    <option value="J">J</option>
                                    <option value="K">K</option>
                                    <option value="P">P</option>
                                    <option value="S">S</option>
                                    <option value="T">T</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="form-group col-md-5">
                                <label style="color:red;">*</label>
                                <label><%=Resources.Resources.period %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-md-3">
                                <select id="payPeriod"  runat="server" required>
                                    <option value="93">9-3</option>
                                    <option value="612">6-12</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                    <option value="6">6</option>
                                    <option value="7">7</option>
                                    <option value="8">8</option>
                                    <option value="9">9</option>
                                    <option value="10">10</option>
                                    <option value="11">11</option>
                                    <option value="12">12</option>
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="form-group col-md-6">
                                <label style="color:red;">*</label>
                                <label><%=Resources.Resources.pay_duration %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <asp:RadioButtonList ID="payDuration" CssClass=""  runat="server" RepeatDirection="Horizontal" required>
                                <asp:ListItem Selected>1</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="text-center">
                        <asp:Button ID="Button1" class="btn btn-primary" OnClick="Button1_Click" runat="server" Text="Save" />
                        <asp:Button ID="update" class="btn btn-primary" OnClick="update_Click" runat="server" Text="Update" />                        
                        <asp:Button ID="cancel" class="btn btn-primary" OnClick="cancel_Click" runat="server" Text="Cancel" UseSubmitBehavior="false" />
                    </div>
                </div>
            </div>
        
    </div>
</div>


<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    
    <script type="text/javascript">
        $(function () {
            var options = {
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                autoclose: true,
                endDate: new Date('9999-12-31')
            }
            $("[id*=appliedDate]").datepicker(options);
            $("[id*=establishedDate]").datepicker(options); 
            $("[id*=paymentDate]").datepicker(options);
            $("[id*=effectiveDate]").datepicker(options);
            $("#datepicker1").click(function () {
                if (!$("[id*=appliedDate]").prop("disabled")) {
                    $("[id*=appliedDate]").datepicker(options).datepicker("show")
                }
            });
            $("#datepicker2").click(function () {
                if (!$("[id*=establishedDate]").prop("disabled")) {
                    $("[id*=establishedDate]").datepicker(options).datepicker("show")
                }
            });
        });
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
                $select.append($('<option value="'+i+'"></option>').val(i).html(i))
            }
        });
    </script>
   <%-- @* *********** /Period 1-12*********** *@--%>

    <%--@* *********** Calculation Date*********** *@--%>
    <script>
        $(document).ready(function () {
            $("[id*=effectiveDate]").change(function () {
                var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
                var effectiveDate = $(this).val();
                //alert();
                var myDate = new Date(effectiveDate);
                month = myDate.getMonth();
                modifiedMonth = month + parseInt($("[id*=noPayMonth]").val());
                //alert(modifiedMonth);
                modifiedDate = new Date(myDate.getFullYear(), modifiedMonth, 0);
                modifiedMonth = months[modifiedDate.getMonth() + 0];
                //alert(modifiedMonth);
                if (modifiedDate.getDate() + "-" + modifiedMonth + "-" + modifiedDate.getFullYear() != "NaN-undefined-NaN") {
                    //expiredDate.value = modifiedDate.getDate() + "-" + modifiedMonth + "-" + modifiedDate.getFullYear();
                    var r = modifiedDate.getDate() + "-" + modifiedMonth + "-" + modifiedDate.getFullYear();
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
                var totalfee = tm * pd;
                var MemberFee = totalfee * 0.3
                var NewsFee = totalfee * 0.7

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
<%--}--%>

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