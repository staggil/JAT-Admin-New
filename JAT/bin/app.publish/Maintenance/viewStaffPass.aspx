<%@ Page Title="viewStaffPass" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="viewStaffPass.aspx.cs" Inherits="JAT.Maintenance.viewStaffPass" %>
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
    <h3><%=Resources.Resources.change_password %></h3>
    <form>
        <div class="row">
            <div class="form-group col-sm-3">
                <label><%=Resources.Resources.name %></label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-sm-4">
                <%--<label>Marubeni Staff</label>--%>
                <asp:Label ID="staffName" runat="server" Text="Label"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="form-group col-sm-3">
                <label><%=Resources.Resources.Spassword %></label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-sm-3">
                <input type="text" id="passOld" class="form-control" runat="server">
            </div>
        </div>
        <div class="row">
            <div class="form-group col-sm-3">
                <label><%=Resources.Resources.S_new_password %></label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-sm-3">
                <input type="text" id="password" class="form-control" runat="server" required>
            </div>
        </div>
        <div class="row">
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
                <asp:Button ID="save" class="btn btn-primary" runat="server" OnClientClick="ConfirmSave()" OnClick="save_Click" Text="Save" />
                <asp:Button ID="cancel" class="btn btn-primary" OnClick="cancel_Click" runat="server" Text="Cancel" formnovalidate/>
            </div>
        </div>
    </form>
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

        //$('[id*=save]').on('click', function () {
        //    if ($('[id*=password]').val() == $('[id*=staffPass]').val()) {
        //        $('[id*=staffPass]').setCustomValidity("");
        //    } else
        //        $('[id*=staffPass]').setCustomValidity("Passwords Don't Match");
        //});
        //$('[id*=password]').onchange()


        $('[id*=password],[id*=staffPass]').on('keyup', function () {
            if ($('[id*=password]').val() == $('[id*=staffPass]').val()) {
                $('#message').html('Matching').css('color', 'green');
            } else
                $('#message').html('Not Matching').css('color', 'red');
        });

        

        //var password = document.getElementById("password")
        //    , confirm_password = document.getElementById("staffPass");

        //function validatePassword() {
        //    if (password.value != confirm_password.value) {
        //        confirm_password.setCustomValidity("Passwords Don't Match");
        //    } else {
        //        confirm_password.setCustomValidity('');
        //    }
        //}
        //password.onchange = validatePassword;
        //confirm_password.onkeyup = validatePassword;
    </script>

</asp:Content>
