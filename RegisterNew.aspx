<%@ Page  Title="Registreer pagina" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="RegisterNew.aspx.cs" Inherits="HCloud.RegisterNew" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div>
      <script>
          function BSN() {
              var text = document.getElementById("<%=BsnNumberTB.ClientID%>").value;
               
              if (text.length >= 9) {
                  var newtext = text.substring(0, 8);
                  document.getElementById("<%=BsnNumberTB.ClientID%>").value=newtext;
              }
          }
          function Phone() {
                
              var text = document.getElementById("<%=PhoneNumberTB.ClientID%>").value;
              
              var text1 = text.replace(/\D/g, '');
              var newtext = text1;
              if (text1.length >= 10) {
                  newtext = newtext.substring(0, 9);
                 
              }
              document.getElementById("<%=PhoneNumberTB.ClientID%>").value = newtext;
              
          }
      </script>
      <h1>Registreren</h1>
      <asp:Label ID="RegisterName" runat="server" Text="Voornaam:"></asp:Label><br />
      <asp:TextBox ID="RegisterNameTB" CssClass="form-control" runat="server"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator6" controltovalidate="RegisterNameTB" errormessage="Vul een voornaam in!"  style="color:red;" /><br />
      <asp:Label ID="RegisterLastName" runat="server" Text="Achternaam:"></asp:Label> <br />
      <asp:TextBox ID="RegisterLastNameTB" CssClass="form-control"  runat="server"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="LastnameRequired" controltovalidate="RegisterLastNameTB" errormessage="Vul een achternaam in!"  style="color:red;"/><br />
      <asp:Label ID="RegisterEmail" runat="server"  Text="Emailadres:"></asp:Label><br />
      <asp:TextBox ID="RegisterEmailTB" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="RegisterEmailTB" errormessage="Vul een email in!"  style="color:red;"/><br />
     
      <asp:Label ID="RegisterPassword" runat="server" Text="Wachtwoord:"></asp:Label><br />
      <asp:TextBox ID="RegisterPasswordTB" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="RegisterPasswordTB" errormessage="Vul een wachtwoord in!"  style="color:red;"/><br />
      <asp:Label ID="RegisterPasswordExtra" runat="server" Text="Wachtwoord herhalen:"></asp:Label><br />
      <asp:TextBox ID="RegisterPasswordExtraTB" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" controltovalidate="RegisterPasswordExtraTB" errormessage="Vul een wachtwoord in!"  style="color:red;"/><br />
      <asp:Label ID="BsnNumber" runat="server" Text="Burger Service Nummer:"></asp:Label><br />
      <asp:TextBox ID="BsnNumberTB" onkeydown="BSN();" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox ><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator4" controltovalidate="BsnNumberTB" errormessage="Vul een BSN nummer in!"  style="color:red;" /><br />
      <asp:Label ID="PhoneNumber" runat="server" Text="Telefoonnummer:"></asp:Label><br />
      <asp:TextBox ID="PhoneNumberTB"  onkeydown="Phone();" CssClass="form-control" TextMode="Phone" runat="server"></asp:TextBox ><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" controltovalidate="PhoneNumberTB"  errormessage="Vul een telefoonnummer in!" style="color:red;" /><br />
      <asp:Button runat="server" ID="RegisterButton" Text="Registreer" OnClick="Register_Click" CssClass="btn btn-default hcloudBtn"/>
  </div> 
    <asp:RegularExpressionValidator ID="valRegExEmail" runat="server" ControlToValidate="RegisterEmailTB"
                            Display="None" ErrorMessage="Geef een geldig emailadress"  ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z\.][a-zA-Z]{1,3}$" style="color:red;"></asp:RegularExpressionValidator> 
    <div runat="server" id="MessageAlert"  class="alert alert-info hcloudAlertPanel" >
            <asp:Label runat="server" ID="RegisterControler" Text="info"></asp:Label>
    </div>
</asp:Content>

