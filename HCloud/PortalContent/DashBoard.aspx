<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="HCloud.PortalContent.DashBoard" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>DashBoard</h1>
    <b>Planning van komende 7 dagen:</b>
     <DayPilot:DayPilotCalendar ID="ActivityAgendaTherapist" runat="server" />
     <div style="width: 100%; text-align: center; display: inline-block;">
        <asp:Label runat="server" Text="Aantal dagen:"></asp:Label><asp:TextBox ID="countdays" Style="width: 60px; display: inline-block;" min="1" max="15" runat="server" MaxLength="2" onkeydown="countdaysKeyDown(event);" AutoPostBack="true" OnTextChanged="countdays_TextChanged" TextMode="Number" CssClass="form-control" Text="7"></asp:TextBox>
        <asp:Button Style="display: inline-block;" runat="server" ID="BackDay" OnClick="BackDay_Click" Text="Vorige dag" CssClass="btn btn-default hcloudBtn"></asp:Button>
        <asp:Button Style="display: inline-block;" runat="server" ID="Today" OnClick="Today_Click" Text="Vandaag" CssClass="btn btn-default hcloudBtn"></asp:Button>
        <asp:Button Style="display: inline-block;" runat="server" ID="NextDay" OnClick="NextDay_Click" Text="Volgende dag" CssClass="btn btn-default hcloudBtn"></asp:Button>
    </div>
</asp:Content>
