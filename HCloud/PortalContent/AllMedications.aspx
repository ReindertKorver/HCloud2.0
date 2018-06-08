<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true" CodeBehind="AllMedications.aspx.cs" Inherits="HCloud.PortalContent.AllMedications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function SearchList(a, b) {
            var input = document.getElementById(a).value.toLowerCase();
            var output = document.getElementById(b).options;
            for (var i = 0; i < output.length; i++) {
                if (output[i].text.toLowerCase().includes(input) == true) {
                    output[i].selected = true;
                }
                if (document.getElementById(a).value == '') {
                    output[0].selected = true;
                }
            }
        }
    </script>
    <h1>Alle medicaties</h1>
    Hier onder kunt u alle medicaties vinden, en afhandelen:
   
    <div>
        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" Height="150px" ActiveTabIndex="0" Width="100%" TabStripPlacement="Top" CssClass="MyTabStyle" CssTheme="none">
            <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Medicaties">
                <ContentTemplate>
                    <h3>Medicaties:</h3>
                    <div id="MedicationCards" runat="server">
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Afhandelen">
                <ContentTemplate>
                    <h3>Afhandelen:</h3>
                    <b>Afgehandeld door:</b>
                        <asp:TextBox ID="MedicationDoneBY" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                     <b>Datum afgifte:</b><br />
            <asp:TextBox ID="NewMedicationDate" TextMode="Date" CssClass="form-control " runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="NewMedicationDate"  ValidationGroup='NewTherapyValidation' ErrorMessage="Vul een datum in!" Style="color: red;" /><br />
                     <b>Verloopdatum medicatie:</b><br />
            <asp:TextBox ID="ExpirationDate" TextMode="Date" CssClass="form-control " runat="server"></asp:TextBox>
               <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ExpirationDate"  ValidationGroup='NewTherapyValidation' ErrorMessage="Vul een datum in!" Style="color: red;" /><br />
                    <b>Medicatie</b>
                    <input type="text" id="SearchMedication" class="form-control" placeholder="Zoeken" style="width: 100%;" onkeyup="SearchList(this.id,'<%=ListBoxMedication.ClientID%>');" /><br />
                    <asp:ListBox ID="ListBoxMedication" runat="server" CssClass="form-control" Width="100%"></asp:ListBox>
                      <asp:Button ID="SaveMedication" ValidationGroup='NewTherapyValidation' CausesValidation="true" runat="server" Text="Medicatie opslaan" OnClick="SaveMedication_Click" CssClass="btn hcloudBtn" />

                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>

    </div>

    <asp:Label ID="Messager" runat="server" Text=""></asp:Label>
</asp:Content>
