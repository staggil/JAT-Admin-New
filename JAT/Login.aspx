<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="JAT.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
            width: fit-content;
            align-self: center;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 20px;
            box-shadow: 3px 3px 20px rgba(0, 0, 0, 0.1);
        }
        input {
            margin: 5px;    
        }
        .space {
            margin-left: 20px;
        }

        table#MainContent_Login1 tbody tr td table tbody tr td {
            text-align: center;
        }
    </style>
    <br />
    <div class="box" style="background-color: lightgray;">
        <div class="row">
            <div class="form-group col-sm-12">
                <asp:Login ID="Login1" runat="server" DisplayRememberMe="false" OnAuthenticate="LoginControl_Authenticate">
                    <LabelStyle Font-Bold="False" />
                    <TitleTextStyle Font-Bold="True" Font-Size="X-Large" />
                </asp:Login>
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

</asp:Content>
