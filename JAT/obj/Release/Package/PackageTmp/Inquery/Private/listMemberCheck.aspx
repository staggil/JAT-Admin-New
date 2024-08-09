<%@ Page Title="Member Check List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="listMemberCheck.aspx.cs" Inherits="JAT.Inquery.Private.listMemberCheck" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="500" SizeToReportContent = "true">
    </rsweb:ReportViewer>
</asp:Content>
