<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true" CodeBehind="NewDesease.aspx.cs" Inherits="HCloud.PortalContent.NewDesease" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Nieuwe aandoening
    </h1>
    Hieronder kunt u een nieuwe aandoening aanmaken<br />
    <div class="row">
        <div class="col-sm-6">
            <b>Behandelaar:</b><br />
            <asp:TextBox ID="NewDeseaseTherapist" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>

        </div>
        <div class="col-sm-6">
            <b>Cliënt:</b><br />
            <asp:DropDownList ID="NewDeseaseClient" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    <b>Beschrijving:</b><br />
    <asp:TextBox ID="NewDeseaseDescriptionTB" TextMode="MultiLine" CssClass="form-control" runat="server" Style="max-width: 100%;"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="NewDeseaseDescriptionTB" ValidationGroup='NewTherapyValidation' ErrorMessage="Vul een beschrijving in!" Style="color: red;" /><br />
     <div class="row">
        <div class="col-xs-3">
            <b>Datum:</b><br />
            <asp:TextBox ID="NewDeseaseDate" TextMode="Date" CssClass="form-control " runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="NewDeseaseDate"  ValidationGroup='NewTherapyValidation' ErrorMessage="Vul een datum in!" Style="color: red;" /><br />
        </div>
    </div>
   
    <br />
    <asp:Button ID="SaveTherapy" ValidationGroup='NewTherapyValidation' CausesValidation="true" runat="server" Text="Aandoening opslaan" OnClick="SaveTherapy_Click" CssClass="btn hcloudBtn" />

    <asp:Label runat="server" ID="lbl"></asp:Label>
</asp:Content>
