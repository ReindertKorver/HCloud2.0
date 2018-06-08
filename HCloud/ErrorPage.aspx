﻿<%@ Page Title="ErrorPagina" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="ErrorPage.aspx.cs" Inherits="HCloud.ErrorPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Fout</h1>
    <h4>Er is iet fout gegaan met het laden van de website, probeer later opnieuw.</h4>
    <h2>Error:</h2>
    <p></p>
    <asp:Label ID="FriendlyErrorMsg" runat="server" Text="Hier wordt de error weergegeven als er een error is" Font-Size="Large" style="color: red"></asp:Label>

    <asp:Panel ID="DetailedErrorPanel" runat="server" Visible="false">
        <p>&nbsp;</p>
        <h4>Detailed Error:</h4>
        <p>
            <asp:Label ID="ErrorDetailedMsg" runat="server" Font-Size="Small" /><br />
        </p>

        <h4>Error Handler:</h4>
        <p>
            <asp:Label ID="ErrorHandler" runat="server" Font-Size="Small" /><br />
        </p>

        <h4>Detailed Error Message:</h4>
        <p>
            <asp:Label ID="InnerMessage" runat="server" Font-Size="Small" /><br />
        </p>
        <p>
            <asp:Label ID="InnerTrace" runat="server"  />
        </p>
    </asp:Panel>
    </asp:Content>