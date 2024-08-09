<%@ Page Title="Print Club" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportPrivateClub.aspx.cs" Inherits="JAT.PrivateReport.reportPrivateClub" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    a {
        color: #54667a;
        text-decoration: none;
    }
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
    .frame{
        border-style:solid;
        border-width:2px;
        padding-top:15px;
    }
    .no-top-border{
        border-top:none;
    }
    .no-right-border{
        border-right:none;
    }
    .no-left-border{
        border-left:none;
    }
    .left-border{
        border-left:solid;
        border-width:2px;
    }
    .right-border{
        border-left:solid;
        border-width:2px;
    }
    /*update by tim*/
   /* label{
        margin-left: 5px;
        margin-right: 5px;
    }*/
</style>

<%--@*PRINTCLUB*@--%>
<br />
<div class="">
    <%--@*<h3>Print</h3>*@--%>
    <div class="box" style="background-color:lightgray;">
        <h3><%=Resources.Resources.print_club_header%></h3>
        <form>
            <br/>
            <div class="row">
                <div class="col-sm-12">
                    <div class="row space">
                        <div class="col-sm-11 frame frame" >
                            <div class="form-group col-md-3" style="margin-top:30px;">
                                <label for="inputState"><%=Resources.Resources._event %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-md-9">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="3" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="english_test" Text="<%$Resources:Resources,english_test%>" Selected></asp:ListItem>
                                    <asp:ListItem Value="online_event" Text="<%$Resources:Resources,online_event %>"></asp:ListItem>
                                    <asp:ListItem Value="softball" Text="<%$Resources:Resources,softball %>"></asp:ListItem>
                                    <asp:ListItem Value="yoga" Text="<%$Resources:Resources,yoga %>"></asp:ListItem>
                                    <asp:ListItem Value="ev_tmp1" Text=""></asp:ListItem>
                                    <asp:ListItem Value="ev_tmp2" Text=""></asp:ListItem>
                                    <asp:ListItem Value="ev_tmp3" Text=""></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-12">
                    <div class="row space">
                        <div class="col-sm-11 frame">
                            <div class="form-group col-md-3" style="margin-top:30px;">
                                <label for="inputState"><%=Resources.Resources.subcommittee %></label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-md-9">
                                <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatColumns="2" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="board" Text="<%$Resources:Resources,board%>"></asp:ListItem>
                                    <asp:ListItem Value="board_list" Text="<%$Resources:Resources,board_list %>"></asp:ListItem>
                                    <asp:ListItem Value="golf" Text="<%$Resources:Resources,golf %>"></asp:ListItem>
                                    <asp:ListItem Value="lady" Text="<%$Resources:Resources,lady %>"></asp:ListItem>
                                    <asp:ListItem Value="club_secretary" Text="<%$Resources:Resources,club_secretary %>"></asp:ListItem>
                                    <asp:ListItem Value="bazaar_volunteer" Text="<%$Resources:Resources,bazaar_volunteer %>"></asp:ListItem>
                                    <asp:ListItem Value="social_gathering_members" Text="<%$Resources:Resources,social_gathering_members %>"></asp:ListItem>
                                    <asp:ListItem Value="youth_circle_members" Text="<%$Resources:Resources,youth_circle_members %>"></asp:ListItem>
                                    <asp:ListItem Value="sub_tmp1" Text=""></asp:ListItem>
                                    <asp:ListItem Value="sub_tmp2" Text=""></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-12">
                    <div class="row space">
                        <div class="col-sm-11 frame">
                            <div class="form-group col-md-3" style="margin-top:30px;">
                                <label for="inputState">Others</label>
                                <span style="float:right;">:</span>
                            </div>
                            <div class="form-group col-md-9">
                                <asp:RadioButtonList ID="RadioButtonList3" runat="server" OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="sukusuku" Text="<%$Resources:Resources,sukusuku%>"></asp:ListItem>
                                    <asp:ListItem Value="children_library" Text="<%$Resources:Resources,children_library %>"></asp:ListItem>
                                    <asp:ListItem Value="overseas_resident_members" Text="<%$Resources:Resources,overseas_resident_members %>"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="text-center">
                    <br/>
                    <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Print" AutoPostBack="false" OnClick="Button1_Click"/>
                </div>
            </div>
        </form>

    </div>
        </br>
        <div style="overflow: scroll;" >
        <div cellspacing="0" cellpadding="0" width="80%" align="center" border="0">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" DocumentMapCollapsed="True" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="1000px" Height="800px" ZoomMode="PageWidth">
            <localreport reportpath="">
            </localreport>
            </rsweb:ReportViewer>
        </div>
        </div>
</div>

</asp:Content>
