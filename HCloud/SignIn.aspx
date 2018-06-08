<%@ Page Title="Inlog pagina" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="HCloud.SignIn" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Inloggen</h1>
        Hieronder kunt u inloggen met een e-mail adres en wachtwoord:<br />
        <asp:Label ID="SignInEmail" runat="server" Text="Emailadres:"></asp:Label><br />
        <asp:TextBox ID="SignInEmailTB" CssClass="form-control" runat="server"></asp:TextBox>
        <asp:Label ID="SignInPassword" runat="server" Text="Wachtwoord:"></asp:Label><br />
        <asp:TextBox ID="SignInPasswordTB" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
        <asp:Button runat="server" ID="SignInButton" Text="Inloggen" OnClick="SignInButton_Click" CssClass="btn btn-default hcloudBtn" />
        <div runat="server" id="MessageAlert" class="alert alert-info hcloudAlertPanel" >
            <asp:Label runat="server" ID="Messager" Text="info"></asp:Label>
        </div>
    </div>
</asp:Content>
