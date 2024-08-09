<%@ Page Title="Fee Maintenance" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="mnEffective.aspx.cs" Inherits="JAT.Maintenance.mnEffective" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<style>
    .Grid td{
        padding:5px
    }
    .Grid th{
        padding:5px
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
        width: 95%;
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
    <h3><%=Resources.Resources.effective_fee_maintenance %></h3>
    <form>
        <div class="row space">
            <div class="form-group col-md-2">
                <label><%=Resources.Resources.effective_id %></label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-md-2">
                <%--<select>
                    <option selected>1</option>
                    <option>2</option>
                    <option>3</option>
                    <option>4</option>
                    <option>5</option>
                    <option>6</option>
                    <option>7</option>
                </select>--%>
                <asp:DropDownList ID="drpEffID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpEffID_SelectedIndexChanged"
                    Width="81px" CssClass="listfr">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row space">
            <div class="form-group col-md-2">
                <label><%=Resources.Resources.effective_date %></label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-md-2">
                <input id="effdate" type="text" autocomplete="off" disabled class="form-control" runat="server">
                <%--<asp:TextBox ID="effdate" ReadOnly runat="server"></asp:TextBox>--%>
            </div>
        </div>
        <div class="row space">
            <div class="form-group col-md-2">
                <label><%=Resources.Resources.expired_date1 %></label>
                <span style="float:right;">:</span>
            </div>
            <div class="form-group col-md-2">
                <input id="expdate" type="text" autocomplete="off" disabled class="form-control" runat="server">
                <%--<asp:TextBox ID="expdate" ReadOnly runat="server"></asp:TextBox>--%>
            </div>
        </div>
    </form>
</div>
<div class="form-group">
    <div class="text-right">
        <asp:Button ID="add" class="btn btn-primary" runat="server" OnClick="add_Click" Text="<%$Resources:Resources, add %>" />
        <asp:Button ID="save" class="btn btn-primary" runat="server" OnClick="save_Click" Text="<%$Resources:Resources, save %>" />
        <%--<asp:Button ID="Button1" class="btn btn-primary" runat="server" OnClick="Button1_Click" Text="Button1" />--%>

        <asp:Button ID="cancel" class="btn btn-primary" runat="server" OnClick="cancel_Click" Text="<%$Resources:Resources, cancel %>" />
    </div>
</div>
<div>
    <h3 style="margin-left: 9em;">
        <%=Resources.Resources.entrance_fee_title %>
    </h3>
    <asp:GridView ID="gvEntr" CssClass="Grid" HorizontalAlign="Center" runat="server" AutoGenerateColumns="false" ShowFooter="true" DataKeyNames="EffectiveID"
        ShowHeaderWhenEmpty="true" OnRowDataBound="gvEntr_RowDataBound" 

        OnRowEditing="gvEntr_RowEditing" OnRowCancelingEdit="gvEntr_RowCancelingEdit"
        OnRowUpdating="gvEntr_RowUpdating"

        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
        <%--<FooterStyle BackColor="White" ForeColor="#000066" />--%>
        <HeaderStyle BackColor="#24227A" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
                
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:Resources, effective_id %>">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("EffectiveID") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEffectiveID" Text='<%# Eval("EffectiveID") %>' runat="server" />
                </EditItemTemplate>
                <%--<FooterTemplate>
                    <asp:TextBox ID="txtEffectiveIDFooter" runat="server" />
                </FooterTemplate>--%>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="<%$Resources:Resources, first_member %>">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("FirstMember") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtFirstMember" Text='<%# Eval("FirstMember") %>' runat="server" />
                </EditItemTemplate>
                <%--<FooterTemplate>
                    <asp:TextBox ID="txtFirstMemberFooter" runat="server" />
                </FooterTemplate>--%>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="<%$Resources:Resources, family_member1 %>">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("SecondMember") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtSecondMember" Text='<%# Eval("SecondMember") %>' runat="server" />
                </EditItemTemplate>
                <%--<FooterTemplate>
                    <asp:TextBox ID="txtSecondMemberFooter" runat="server" />
                </FooterTemplate>--%>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="<%$Resources:Resources, edit %>">
                <ItemTemplate>
                    <asp:ImageButton ImageUrl="~/images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px"/>
                    <%--<asp:ImageButton ImageUrl="~/images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px"/>--%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ImageUrl="~/images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px"/>
                    <asp:ImageButton ImageUrl="~/images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                </EditItemTemplate>
                <%--<FooterTemplate>
                    <asp:ImageButton ImageUrl="~/images/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="20px" Height="20px"/>
                </FooterTemplate>--%>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <h3 style="margin-left: 9em;">
        <%=Resources.Resources.member_fee_description %>
    </h3>
    <asp:GridView ID="memfeeDes" CssClass="Grid" HorizontalAlign="Center" runat="server" AutoGenerateColumns="false" ShowFooter="true" DataKeyNames="MemberType"
        ShowHeaderWhenEmpty="true" 

        OnRowEditing="memfeeDes_RowEditing" OnRowCancelingEdit="memfeeDes_RowCancelingEdit" OnRowUpdating="memfeeDes_RowUpdating"

        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
        <%--<FooterStyle BackColor="White" ForeColor="#000066" />--%>
        <HeaderStyle BackColor="#24227A" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
                
        <Columns>

            <asp:TemplateField HeaderText="<%$Resources:Resources, member_type1 %>">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("MemberType") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtMemberType" Text='<%# Eval("MemberType") %>' runat="server" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="<%$Resources:Resources, description1 %>">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("Description") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDescription" Text='<%# Eval("Description") %>' runat="server" />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="<%$Resources:Resources, news_letter %>">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("Newsletter") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNewsletter" Text='<%# Eval("Newsletter") %>' runat="server" />
                </EditItemTemplate>
                <%--<FooterTemplate>
                    <asp:TextBox ID="txtSecondMemberFooter" runat="server" />
                </FooterTemplate>--%>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="<%$Resources:Resources, edit %>">
                <ItemTemplate>
                    <asp:ImageButton ImageUrl="~/images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px"/>
                    <%--<asp:ImageButton ImageUrl="~/images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px"/>--%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ImageUrl="~/images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px"/>
                    <asp:ImageButton ImageUrl="~/images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                </EditItemTemplate>
                <%--<FooterTemplate>
                    <asp:ImageButton ImageUrl="~/images/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="20px" Height="20px"/>
                </FooterTemplate>--%>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <h3 style="margin-left: 9em;">
        <%=Resources.Resources.company_fee_description %>
    </h3>
    <asp:GridView ID="gvPhoneBook" CssClass="Grid" HorizontalAlign="Center" runat="server" AutoGenerateColumns="false" ShowFooter="true" DataKeyNames="CFeeID"
        ShowHeaderWhenEmpty="true"

        OnRowEditing="gvPhoneBook_RowEditing" OnRowCancelingEdit="gvPhoneBook_RowCancelingEdit"
        OnRowUpdating="gvPhoneBook_RowUpdating"

        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
        <HeaderStyle BackColor="#24227A" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
                
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:Resources, fee_id %>">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("CFeeID") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtCFeeID" Text='<%# Eval("CFeeID") %>' runat="server" />
                </EditItemTemplate>
                <%--<FooterTemplate>
                    <asp:TextBox ID="txtCFeeIDFooter" runat="server" />
                </FooterTemplate>--%>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="<%$Resources:Resources, company_entrance_fee %>">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("mem_fee") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtmem_fee" Text='<%# Eval("mem_fee") %>' runat="server" />
                </EditItemTemplate>
                <%--<FooterTemplate>
                    <asp:TextBox ID="txtmem_feeFooter" runat="server" />
                </FooterTemplate>--%>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="<%$Resources:Resources, company_news_fee %>">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("news_fee") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtnews_fee" Text='<%# Eval("news_fee") %>' runat="server" />
                </EditItemTemplate>
                <%--<FooterTemplate>
                    <asp:TextBox ID="txtnews_feeFooter" runat="server" />
                </FooterTemplate>--%>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="<%$Resources:Resources, total %>">
                <ItemTemplate>
                    <asp:Label Text='<%# Eval("CTotalPerMonth") %>' runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtCTotalPerMonth" Text='<%# Eval("CTotalPerMonth") %>' runat="server" />
                </EditItemTemplate>
                <%--<FooterTemplate>
                    <asp:TextBox ID="txtCTotalPerMonthFooter" runat="server" />
                </FooterTemplate>--%>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Resources, edit %>">
                <ItemTemplate>
                    <asp:ImageButton ImageUrl="~/images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px"/>
                    <%--<asp:ImageButton ImageUrl="~/images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px"/>--%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ImageUrl="~/images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px"/>
                    <asp:ImageButton ImageUrl="~/images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                </EditItemTemplate>
                <%--<FooterTemplate>
                    <asp:ImageButton ImageUrl="~/images/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="20px" Height="20px"/>
                </FooterTemplate>--%>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>

    <%--@* *********** Datepicker *********** *@--%>
    <script type="text/javascript">
        $(function () {
            var options = {
                format: 'dd/mm/yyyy',
                //format: 'dd/mm/yyyy',
                todayHighlight: true,
                autoclose: true
            }
            $("[id*=effdate]").datepicker(options);
            $("[id*=expdate]").datepicker(options);

        });
    </script>

    
    <script>  


</script> 

</asp:Content>
