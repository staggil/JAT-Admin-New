<%@ Page Title="Private Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="privateSearch.aspx.cs" Inherits="JAT.Private.privateSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .form-control {
            height: 1.5em;
            padding: 7px 5px;
        }
        .Grid td {
            padding: 5px;
        }
        table{
            table-layout: fixed;
            width: auto;
        }
        td label  {
            /*width: 350px;*/
            margin-right:20px;
        }

        body {
            font-size: 17px;
        }

        h3 {
            font-weight: bold;
        }

        .form-group {
            margin-bottom: 10px;
            padding-right: 0px;
            top: -1px;
            left: 20px;
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
    </style>

    <br />
    <br />
    <br />
    <div style="display: block; margin: auto; width: 95%; align-self: center; padding: 20px; background-color: lightgray; border-radius: 20px; box-shadow: 3px 3px 20px rgba(0, 0, 0, 0.1);">
        <h3><%=Resources.Resources.retrieve_information %></h3>
        <form>
            <div class="row">
                <div class="form-group col-md-3">
                    <label><%=Resources.Resources.member_status %></label>
                    <%--<asp:Label runat="server" Text="<%$Resources:Resources, String1 %>"></asp:Label>--%>
                    <span style="float: right;">:</span>
                </div>
                <div class="form-group col-md-2">
                    <%--<select id="inputState" class="">
                    <option>-- Any --</option>
                    <option selected>A</option>
                    <option>NA</option>
                </select>--%>
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        <asp:ListItem Value="">-- Any --</asp:ListItem>
                        <asp:ListItem Value="A" Selected>A</asp:ListItem>
                        <asp:ListItem Value="NA">NA</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-md-3">
                    <label for="inputState"><%=Resources.Resources.member_type %></label>
                    <span style="float: right;">:</span>
                </div>
                <div class="form-group col-sm-8">
                    <div class="form-check" style="float: left; margin-right: 20px;">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                            <asp:ListItem Selected Value="1" Text="<%$Resources:Resources, member_information %>"></asp:ListItem>
                            <asp:ListItem Value="2" Text="<%$Resources:Resources, family_member %>"></asp:ListItem>
                            <asp:ListItem Value="3" Text="<%$Resources:Resources, children %>"></asp:ListItem>
                            <asp:ListItem Value="4" Text="<%$Resources:Resources, all %>"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-md-3">
                    <label><%=Resources.Resources.retrieve_key %></label>
                    <span style="float: right;">:</span>
                </div>
                <div class="form-group col-md-3">
                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                        <asp:ListItem Value="">-- Any --</asp:ListItem>
                        <asp:ListItem Value="1">Member ID</asp:ListItem>
                        <asp:ListItem Value="2">Name (Japanese)</asp:ListItem>
                        <asp:ListItem Value="3">Name (English)</asp:ListItem>
                        <asp:ListItem Value="4">Company Name (English)</asp:ListItem>
                        <asp:ListItem Value="5">Family Name (Japanese)</asp:ListItem>
                        <asp:ListItem Value="6">Family Name (English)</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="form-group col-md-3">
                            <label for="inputState"><%=Resources.Resources.name_member_id %></label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="form-group col-md-6">
                            <%--<input type="text" class="form-control" id="inputEmail4" placeholder="">--%>
                            <input id="Text1" type="text" runat="server" autocomplete="off" />
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Invalid memberId format." OnServerValidate="checkKeyValue" ControlToValidate="Text1"></asp:CustomValidator>
                            <asp:Label ID="Label3" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="form-group col-md-3">
                            <label for="inputState"><%=Resources.Resources.sort_by %></label>
                            <span style="float: right;">:</span>
                        </div>
                        <div class="form-group col-md-5">
                            <asp:DropDownList ID="DropDownList3" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                <asp:ListItem Value="memberid">MemberID</asp:ListItem>
                                <asp:ListItem Value="nameJ">Name (Japanese)</asp:ListItem>
                                <asp:ListItem Value="nameE">Name (English)</asp:ListItem>
                                <asp:ListItem Value="companyNm">Company Name (English)</asp:ListItem>
                                <asp:ListItem Value="familyNmJ">Family Name (Japanese)</asp:ListItem>
                                <asp:ListItem Value="familyNmE">Family Name (English)</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList4" runat="server" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
                                <asp:ListItem>ASC</asp:ListItem>
                                <asp:ListItem>DESC</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="text-center">
                    <%--<button type="submit" class="btn btn-primary">View</button>--%>
                    <asp:Button class="btn btn-primary" ID="Button1" runat="server" OnClick="Button1_Click" Text="View" />
                </div>
            </div>
        </form>
    </div>

    <br />
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <br />


    <asp:Label ID="Label2" runat="server"></asp:Label>


    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="20" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <%--<asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="No.">
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
               <ItemTemplate>
                 <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Container.DataItemIndex + 1 %>' 
                    NavigateUrl= '<%# "privateEntry.aspx?firstmemberid=" + Eval("firstmemberid")+"&memberid=" + Eval("memberid") %>'>
                 </asp:HyperLink>  
               </ItemTemplate>
               <ItemStyle HorizontalAlign="Center" Width="2%" Font-Underline="true"></ItemStyle>
            </asp:TemplateField>
            <%--<asp:HyperLinkField
                DataNavigateUrlFields="Row#, firstmemberid, memberid"
                DataNavigateUrlFormatString="privateEntry.aspx?firstmemberid={1}&memberid={2}"
                DataTextField="Row#"
                HeaderText="No."
                SortExpression="memberID">
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Underline="true"></ItemStyle>
            </asp:HyperLinkField>--%>

            <asp:BoundField DataField="memberid" HeaderText="<%$Resources:Resources,member_id %>">
                <HeaderStyle Height="10px" BackColor="#24227A" ForeColor="White" />
                <ItemStyle Height="40px" />
            </asp:BoundField>
            <asp:BoundField DataField="nameJ" HeaderText="<%$Resources:Resources,name %>">
                <HeaderStyle Height="50px" BackColor="#24227A" ForeColor="White" />
            </asp:BoundField>
            <asp:BoundField DataField="NameEng" HeaderText="<%$Resources:Resources,name_eng %>">
                <HeaderStyle Height="50px" BackColor="#24227A" ForeColor="White" />
            </asp:BoundField>
            <asp:BoundField DataField="companyNm" HeaderText="<%$Resources:Resources,company_name %>">
                <HeaderStyle Height="50px" BackColor="#24227A" ForeColor="White" />
            </asp:BoundField>
            <asp:BoundField DataField="firstmemberid" HeaderText="first">
                <HeaderStyle Height="50px" BackColor="#24227A" ForeColor="White" />
            </asp:BoundField>
            <asp:BoundField DataField="nameKidJ" HeaderText="Child Name">
                <HeaderStyle Height="50px" BackColor="#24227A" ForeColor="White" />
            </asp:BoundField>
            <asp:BoundField DataField="nameKidE" HeaderText="Child Name English">
                <HeaderStyle Height="50px" BackColor="#24227A" ForeColor="White" />
            </asp:BoundField>
            <asp:BoundField DataField="familyNameJ" HeaderText="Family Name (Jpn.)">
                <HeaderStyle Height="50px" BackColor="#24227A" ForeColor="White" />
            </asp:BoundField>
            <asp:BoundField DataField="familyNameE" HeaderText="Family Name (Eng.)">
                <HeaderStyle Height="50px" BackColor="#24227A" ForeColor="White" />
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerSettings Visible="False" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>
    <asp:ImageButton ID="ImageButton1" runat="server" Style="float: left;" OnClick="ImageButton1_Click" ImageUrl="~/img/prev.gif" />
    <asp:ImageButton ID="ImageButton2" runat="server" Style="float: right;" OnClick="ImageButton2_Click" ImageUrl="~/img/next.gif" />
    <br />
</asp:Content>
