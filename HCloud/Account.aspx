<%@ Page Title="Account Pagina" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="HCloud.Account" %>
<%@ Register Src="~/UserDataControl.ascx" TagName="UserDataControl" TagPrefix="uc1" %> 

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Account</h1>
    <b style="color: #127a7b;">Ingelogd als:
        <asp:Label runat="server" ID="IngelogdAls"></asp:Label></b><asp:Button CssClass="form-control hcloudBtn" runat="server" ID="ToPortal" OnClick="ToPortal_Click" Text="Naar het portaal" />
    <div class="panel-group" id="accordion">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse0">Cliënt gegevens</a>
                </h4>
            </div>
            <div id="collapse0" class="panel-collapse collapse in">
                <div class="panel-body">Bekijk hier uw gegevens</div>
                <div id="UserDataDiv" class="UserDataControl" style="padding-left: 50px;padding-right: 50px;" runat="server">
                    <uc1:UserDataControl id="ListPicker1"  Runat="server" />
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">Instellingen</a>
                </h4>
            </div>
            <div id="collapse1" class="panel-collapse collapse">
                <div class="panel-body">Hier kunnen de instellingen worden ingevoerd</div>
                
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">Wachtwoord wijzigen</a>
                </h4>
            </div>
            <div id="collapse2" class="panel-collapse collapse">
                <div class="panel-body"></div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">Uitloggen</a>
                </h4>
            </div>
            <div id="collapse3" class="panel-collapse collapse">
                <div class="panel-body">
                    Weet u zeker dat u wilt uitloggen?<br />
                    <asp:Button runat="server" ID="LogOut" OnClick="LogOut_Click" CssClass="form-control hcloudBtn" Text="Uitloggen" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
