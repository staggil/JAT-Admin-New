<%@ Page Title="Private Change MemberID" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="privateChangeMemberID.aspx.cs" Inherits="JAT.Private.privateChangeMemberID" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
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

        input#datepicker1, input#datepicker2 {
            width: inherit;
        }
    </style>

    <%--@*PRINTLABEL*@--%>
    <br />
    <div class="">
        <%--@*<h3>Label</h3>*@--%>
        <div class="box" style="background-color: lightgray;">
            <h2><%=Resources.Resources.change_member_id %></h2>
            <form>
                <br />
                <div class="row">

                    <div class="col-md-12">
                        <div class="row space">
                            <div class="form-group col-md-4">
                                <label for="inputState"><%=Resources.Resources.old_member_id_from %></label>
                                <span style="float: right;">:</span>
                            </div>
                            <div class="form-group col-md-3">
                                <input type="text" id="oldMemID" runat="server">
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="row space">
                            <div class="form-group col-md-4">
                                <label for="inputState"><%=Resources.Resources.new_member_id %></label>
                                <span style="float: right;">:</span>
                            </div>
                            <div class="form-group col-md-3">
                                <input type="text" id="newMemID" runat="server">
                            </div>
                        </div>
                    </div>

                </div>
                <div class="form-group">
                    <div class="text-center">
                        <br />
                        <asp:Button class="btn btn-primary" ID="SaveBtn" runat="server" OnClick="Button1_Click" Text="Save" />
                        <asp:Button class="btn btn-primary" ID="Button1" runat="server" Text="Reset" OnClick="Button1_Click1" />
                    </div>
                </div>
            </form>
        </div>
    </div>


</asp:Content>
