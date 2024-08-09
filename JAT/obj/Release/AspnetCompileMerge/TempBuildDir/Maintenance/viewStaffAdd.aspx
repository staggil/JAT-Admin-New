<%@ Page Title="viewStaffAdd" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewStaffAdd.aspx.cs" Inherits="JAT.Maintenance.viewStaffAdd" %>
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
</style>
<br />
<br />
<br />
<div class="box" style="background-color: lightgray;">
    <h3>Staff</h3>
        <div class="row space">
            <asp:HiddenField ID="staffID" runat="server" />
            <div class="form-group col-sm-3">
                <label>Name</label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-sm-3">
                <input type="text" id="staffFName" class="form-control" runat="server" required>
                <%--<asp:TextBox ID="staffFName" runat="server"></asp:TextBox>--%>

            </div>
        </div>
        <div class="row space">
            <div class="form-group col-sm-3">
                <label>E-mail</label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-sm-3">
                <input type="email" id="staffEmail" class="form-control" runat="server" required>
            </div>
        </div>
        <div class="row space">
            <div class="col-md-12">
                <div class="row">
                    <div class="form-group col-sm-3">
                        <label>Branch</label>
                        <span style="float:right;">:</span>
                    </div>
                    <div class=" form-group col-sm-9">
                        <asp:RadioButtonList ID="staffBranch" runat="server" RepeatDirection="Horizontal" required>
                                <asp:ListItem>Sathorn</asp:ListItem>
                                <asp:ListItem>Sukhumvit</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
        </div>
    <div class="row space">
            <div class="col-md-12">
                <div class="row">
                    <div class="form-group col-sm-3">
                        <label>Authority</label>
                        <span style="float:right;">:</span>
                    </div>
                    <div class=" form-group col-sm-9">
                        <asp:RadioButtonList ID="rdbAuthority" runat="server" RepeatDirection="Horizontal" required>
                                <asp:ListItem Value="Administrator">Administrator</asp:ListItem>
                                <asp:ListItem Value="Normal">Normal</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
        </div>
        <div class="row space">
            <div class="form-group col-sm-3">
                <label for="inputState">Login</label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-sm-2">
                <input type="text" id="staffName" class="form-control" runat="server">
            </div>
        </div>
        <div class="row space">
            <div class="form-group col-sm-3">
                <label>Password</label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-sm-3">
                <input type="text" id="password" class="form-control" runat="server" required>
            </div>
        </div>
        <div class="row space">
            <div class="form-group col-sm-3">
                <label>Confirm Password</label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-sm-3">
                <input type="text" id="staffPass" class="form-control" runat="server" required>
                <span id='message'></span>
            </div>
        </div>
        <div class="form-group">
            <div class="text-center">
                <asp:Button ID="save" class="btn btn-primary" OnClick="save_Click" OnClientClick="ConfirmSave()" runat="server" Text="Save" />
                <asp:Button ID="cancel" class="btn btn-primary" OnClick="cancel_Click" runat="server" Text="Cancel" formnovalidate/>
            </div>
        </div>
</div>
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
    <script>
        $('[id*=password],[id*=staffPass]').on('keyup', function () {
            if ($('[id*=password]').val() == $('[id*=staffPass]').val()) {
                $('#message').html('Matching').css('color', 'green');
            } else
                $('#message').html('Not Matching').css('color', 'red');
        });
    </script>   
</asp:Content>
