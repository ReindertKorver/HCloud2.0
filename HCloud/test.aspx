<%@  Title="Registreer pagina" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="HCloud.test" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox runat="server" ID="Button" OnTextChanged="Button_TextChanged"></asp:TextBox>
    <asp:Label ID="hoi" runat="server"></asp:Label>
</asp:Content>
