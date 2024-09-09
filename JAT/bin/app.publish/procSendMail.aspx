<%@ Page Title="Send Mail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="procSendMail.aspx.cs" Inherits="JAT.procSendMail" %>
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
    .frame{
        border-style:solid;
        border-width:2px;
    }
    .no-top-border{
        border-top:none;
    }
    .no-right-border{
        border-right:none;
    }
    .left-border{
        border-left:solid;
        border-width:2px;
    }
    .right-border{
        border-left:solid;
        border-width:2px;
    }
</style>
<br />
<div class="box" style="background-color:lightgray;">
    <h3><%=Resources.Resources.e_mail%></h3>
    <div class="row">
        <div class="col-sm-11">
            <div class="row space">
                <div class="form-group col-sm-3">
                    <label><%=Resources.Resources.transmission_type%></label>
                    <span style="float:right;">:</span>
                </div>
                <div class="form-group col-sm-7">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Vertical" RepeatColumns="2" cellspacing="4">
                        <asp:ListItem Value="1" Selected>&amp;nbsp; User mail &amp;nbsp;</asp:ListItem>
                        <asp:ListItem Value="info@jat.or.jp">&amp;nbsp; info@jat.or.jp &amp;nbsp;</asp:ListItem>
                        <asp:ListItem Value="no-reply@jat.or.jp">&amp;nbsp; no-reply@jat.or.jp &amp;nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div class="form-group col-sm-2" align="right">
                    <asp:LinkButton ID="openLog" runat="server" OnClick="openLog_Click">Log file</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-11" style="font-size:12px;">
            <div class="row space">
                <div class="form-group col-sm-4 frame">
                    <asp:CheckBox ID="head_ev" Text="&nbsp;<%$Resources:Resources,event%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                    <br />
                    <div class="form-group col-sm-8">
                        <asp:CheckBox ID="ev_1" Text="&nbsp;<%$Resources:Resources,english_test%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                        <asp:CheckBox ID="ev_2" Text="&nbsp;<%$Resources:Resources,online_event%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                        <asp:CheckBox ID="ev_3" Text="&nbsp;<%$Resources:Resources,softball%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                        <asp:CheckBox ID="ev_4" Text="&nbsp;<%$Resources:Resources,yoga%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                    </div>
                    <div class="form-group col-sm-4">
                        <asp:CheckBox ID="ev_tmp1" Text="&nbsp;text" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                        <asp:CheckBox ID="ev_tmp2" Text="&nbsp;text" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                        <asp:CheckBox ID="ev_tmp3" Text="&nbsp;text" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                    </div>
                </div>
                <div class="form-group col-sm-1"></div>
                <div class="form-group col-sm-7 frame">
                    <asp:CheckBox ID="head_sub" Text="&nbsp;<%$Resources:Resources,subcommittee%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                    <br />
                    <div class="form-group col-sm-5">
                        <asp:CheckBox ID="sub_1" Text="&nbsp;<%$Resources:Resources,board%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                        <asp:CheckBox ID="sub_2" Text="&nbsp;<%$Resources:Resources,board_list%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                        <asp:CheckBox ID="sub_3" Text="&nbsp;<%$Resources:Resources,golf%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                        <asp:CheckBox ID="sub_4" Text="&nbsp;<%$Resources:Resources,lady%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                        <asp:CheckBox ID="sub_5" Text="&nbsp;<%$Resources:Resources,club_secretary%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                    </div>
                    <div class="form-group col-sm-7">
                        <asp:CheckBox ID="sub_6" Text="&nbsp;<%$Resources:Resources,bazaar_volunteer%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                        <asp:CheckBox ID="sub_7" Text="&nbsp;<%$Resources:Resources,social_gathering_members%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                        <asp:CheckBox ID="sub_8" Text="&nbsp;<%$Resources:Resources,youth_circle_members%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                        <asp:CheckBox ID="sub_tmp1" Text="&nbsp;text" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                        <asp:CheckBox ID="sub_tmp2" Text="&nbsp;text" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-11" style="font-size:12px;">
            <div class="row space">
                <div class="form-group col-sm-2 frame">
                    <asp:CheckBox ID="head_sk" Text="&nbsp;<%$Resources:Resources,sukusuku%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                    <br />
                    <div class="form-group col-sm-12">
                        <asp:CheckBox ID="sk_1" Text="&nbsp;<%$Resources:Resources,member%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                    </div>
                </div>
                <div class="form-group col-sm-1"></div>
                <div class="form-group col-sm-3 frame">
                    <asp:CheckBox ID="head_ch" Text="&nbsp;<%$Resources:Resources,children_library%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                    <br />
                    <div class="form-group col-sm-12">
                        <asp:CheckBox ID="ch_1" Text="&nbsp;<%$Resources:Resources,member%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                    </div>
                </div>
                <div class="form-group col-sm-1"></div>
                <div class="form-group col-sm-5 frame">
                    <asp:CheckBox ID="head_ov" Text="&nbsp;<%$Resources:Resources,overseas_resident_members%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                    <br />
                    <div class="form-group col-sm-12">
                        <asp:CheckBox ID="ov_1" Text="&nbsp;<%$Resources:Resources,overseas_resident_members%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-11" style="font-size:12px;">
            <div class="row space">
                <div class="form-group col-md-12 frame">
                    <asp:CheckBox ID="head_sup" Text="&nbsp;<%$Resources:Resources,supporting_member_companies%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                    <br />
                    <div class="form-group col-md-3">
                        <asp:CheckBox ID="sup_1" Text="&nbsp;<%$Resources:Resources,representative%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                    </div>
                    <div class="form-group col-md-3">
                        <asp:CheckBox ID="sup_3" Text="&nbsp;<%$Resources:Resources,person_in_charge%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                    </div>
                    <div class="form-group col-md-3" style="margin-left:50px;">
                        <asp:CheckBox ID="sup_2" Text="&nbsp;<%$Resources:Resources,treasurer%>" runat="server" OnCheckedChanged="groupboxCheckedChanged" AutoPostBack="true"/>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-11">
            <div class="row space">
                <div class="form-group col-md-12">
                    <label>Subject</label>
                    <br />
                    <input type="text" id="txtSubject" style="width: inherit;max-width: inherit;margin-bottom:10px" runat="server"/>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-11">
            <div class="row space">
                <div class="form-group col-md-12">
                    <label>Content</label>
                    <br />
                    <textarea id="txtBody" style="width: inherit;max-width: inherit;height:7em;" runat="server"></textarea>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12" style="text-align:center;">
            <asp:Button CssClass="btn btn-primary" ID="Button1" OnClick="sendEmail_Click" runat="server" Text="Send"/>
            <br />
            <asp:Label id="lblText" runat="server"></asp:Label>
        </div>
    </div>
</div>
<br />

<!--script>
    var checker = document.getElementById("[id='allAge']");
    var all1 = document.getElementById("[id='DropDownList1']");
    var all2 = document.getElementById("[id='DropDownList2']");
    checker.onchange = function () {
        all1.disabled = !!this.checked;
        all2.disabled = !!this.checked;
    };

</script-->

</asp:Content>
