<%@ Page Title="Registreer pagina" Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true" CodeBehind="NewTherapy.aspx.cs" Inherits="HCloud.NewTherapy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Nieuwe behandeling</h1>
    Hier onder kunt u een nieuwe behandeling aanmaken:
                                <br />
    <div class="row">
        <div class="col-sm-6">
            <b>Behandelaar:</b><br />
            <asp:TextBox ID="NewTherapyTherapist" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>

        </div>
        <div class="col-sm-6">
            <b>Cliënt:</b><br />
            <asp:DropDownList ID="NewTherapyClient" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList>
        </div>
    </div>
    <b>Beschrijving:</b><br />
    <asp:TextBox ID="NewTherapyDescriptionTB" TextMode="MultiLine" CssClass="form-control" runat="server" Style="max-width: 100%;"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="NewTherapyDescriptionTB"  ValidationGroup='NewTherapyValidation'  ErrorMessage="Vul een beschrijving in!" Style="color: red;"  /><br />
    <b>Locatie:</b><br />
    <asp:TextBox ID="Location" TextMode="SingleLine" CssClass="form-control" MaxLength="50" runat="server" Style="max-width: 100%; "></asp:TextBox>
    <b>Kosten:</b><br />
    <div style="width:100%; display:inline-flex;">
    <span class="fas fa-euro-sign" style="font-size: 20px; margin-top: 6px; margin-left: 5px; margin-right: 0px;"></span><asp:TextBox  ID="Costs" TextMode="Number" step=".01"  CssClass="form-control" MaxLength="20" runat="server" Style="max-width: 100%; margin-left:2px; float:right; display:inline-block;"></asp:TextBox>
     </div>
<asp:RegularExpressionValidator ID="revNumber" runat="server" ControlToValidate="Costs"
           ErrorMessage="Vul een geldig geld bedrag in" ValidationExpression="^\d+(\.\d\d)?$"></asp:RegularExpressionValidator>
   
    <div class="row">
        <div class="col-xs-3">
            <b>Datum:</b><br />
            <asp:TextBox ID="NewTherapyDate" TextMode="DateTimeLocal" CssClass="form-control " runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="NewTherapyDate"  ValidationGroup='NewTherapyValidation' ErrorMessage="Vul een datum in!" Style="color: red;" /><br />
        </div>
        <div class="col-xs-3">
            <b>Tijdduur:</b><br />
            <asp:TextBox ID="NewTherapyEndTime" TextMode="Time" CssClass="form-control " runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="NewTherapyDate"  ValidationGroup='NewTherapyValidation' ErrorMessage="Vul een tijd in!" Style="color: red;" /><br />
        </div>
    </div>
    <b>Aandoening:</b><br />
    <div class="row">
    <div class="col-xs-6">
        <asp:DropDownList ID="NewDesease" runat="server" CssClass="form-control"></asp:DropDownList>
    </div>
    <a href="/PortalContent/NewDesease" style="font-size: 25px; color: #127a7b;" target="_blank"><span class="glyphicon glyphicon-plus"></span></a>
        </div>
     <b>Medicatie:</b><br />
    <div class="row">
        <div class="col-xs-6">
            <asp:DropDownList ID="NewMedicationDDL" runat="server" CssClass="form-control" ></asp:DropDownList>
        </div>
        <a href="/PortalContent/NewMedication" style="font-size: 25px; color: #127a7b;" target="_blank"><span class="glyphicon glyphicon-plus"></span></a>
    </div>
    <br />
    <asp:Button ID="SaveTherapy" OnClick="SaveTherapy_Click" ValidationGroup='NewTherapyValidation' CausesValidation="true" runat="server" Text="Behandeling opslaan" CssClass="btn hcloudBtn" />

    <asp:Label runat="server" ID="lbl"></asp:Label>
</asp:Content>
