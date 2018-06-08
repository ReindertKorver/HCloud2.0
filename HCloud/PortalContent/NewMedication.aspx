<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true" CodeBehind="NewMedication.aspx.cs" Inherits="HCloud.PortalContent.NewMedication" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server" ID="MedicationUpdatePanel">
        <ContentTemplate>
            <h1>Nieuwe medicatie
    </h1>
    Hieronder kunt u een nieuwe medicatie aanmaken<br />
    <div class="row">
        <div class="col-sm-6">
            <b>Behandelaar:</b><br />
            <asp:TextBox ID="NewMedicationTherapist" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>

        </div>
        <div class="col-sm-6">
            <b>Cliënt:</b><br />
            <asp:DropDownList ID="NewMedicationClient" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    <b>Beschrijving:</b><br />
    <asp:TextBox ID="NewMedicationDescriptionTB" TextMode="MultiLine" CssClass="form-control" runat="server" Style="max-width: 100%;"></asp:TextBox>
    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="NewMedicationDescriptionTB" ValidationGroup='NewTherapyValidation' ErrorMessage="Vul een beschrijving in!" Style="color: red;" /><br />


    <div class="row">
        <div class="col-xs-3">
            <b>Datum:</b><br />
            <asp:TextBox ID="NewMedicationDate" TextMode="Date" CssClass="form-control " runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="NewMedicationDate" ValidationGroup='NewTherapyValidation' ErrorMessage="Vul een datum in!" Style="color: red;" /><br />
        </div>
    </div>
   
    <br />
    <asp:Button ID="SaveMedication" OnClick="SaveMedication_Click" ValidationGroup='NewTherapyValidation' CausesValidation="true" runat="server" Text="Medicatie opslaan" CssClass="btn hcloudBtn" />

    <asp:Label runat="server" ID="Label1"></asp:Label>
            </ContentTemplate>

        </asp:UpdatePanel>
</asp:Content>
