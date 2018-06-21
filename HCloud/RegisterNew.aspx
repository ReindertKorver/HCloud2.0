<%@ Page Title="Registreer pagina" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterNew.aspx.cs" Inherits="HCloud.RegisterNew" %>

<%@ Register Assembly="Recaptcha" Namespace="Recaptcha" TagPrefix="recaptcha" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit"
        async defer></script>
    <script type="text/javascript">
        var onloadCallback = function () {
            grecaptcha.render('dvCaptcha', {
                'sitekey': '<%=ReCaptcha_Key %>',
                'callback': function (response) {
                    $("#<%=txtCaptchaFirst.ClientID%>").val(response);
                   
                }
            });
        };
    </script>
    <div>
        <script>
            function BSN() {
                var text = document.getElementById("<%=BsnNumberTB.ClientID%>").value;

                if (text.length >= 9) {
                    var newtext = text.substring(0, 8);
                    document.getElementById("<%=BsnNumberTB.ClientID%>").value = newtext;
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
        <asp:TextBox ID="RegisterNameTB" CssClass="form-control" runat="server"></asp:TextBox><asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="RegisterNameTB" ErrorMessage="Vul een voornaam in!" Style="color: red;" /><br />
        <asp:Label ID="RegisterLastName" runat="server" Text="Achternaam:"></asp:Label>
        <br />
        <asp:TextBox ID="RegisterLastNameTB" CssClass="form-control" runat="server"></asp:TextBox><asp:RequiredFieldValidator runat="server" ID="LastnameRequired" ControlToValidate="RegisterLastNameTB" ErrorMessage="Vul een achternaam in!" Style="color: red;" /><br />
        <asp:Label ID="RegisterEmail" runat="server" Text="Emailadres:"></asp:Label><br />
        <asp:TextBox ID="RegisterEmailTB" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox><asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="RegisterEmailTB" ErrorMessage="Vul een email in!" Style="color: red;" /><br />

        <asp:Label ID="RegisterPassword" runat="server" Text="Wachtwoord:"></asp:Label><br />
        <asp:TextBox ID="RegisterPasswordTB" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox><asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="RegisterPasswordTB" ErrorMessage="Vul een wachtwoord in!" Style="color: red;" /><br />
        <asp:Label ID="RegisterPasswordExtra" runat="server" Text="Wachtwoord herhalen:"></asp:Label><br />
        <asp:TextBox ID="RegisterPasswordExtraTB" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox><asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="RegisterPasswordExtraTB" ErrorMessage="Vul een wachtwoord in!" Style="color: red;" /><br />
        <asp:Label ID="BsnNumber" runat="server" Text="Burger Service Nummer:"></asp:Label><br />
        <asp:TextBox ID="BsnNumberTB" onkeydown="BSN();" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox><asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="BsnNumberTB" ErrorMessage="Vul een BSN nummer in!" Style="color: red;" /><br />
        <asp:Label ID="PhoneNumber" runat="server" Text="Telefoonnummer:"></asp:Label><br />
        <asp:TextBox ID="PhoneNumberTB" onkeydown="Phone();" CssClass="form-control" TextMode="Phone" runat="server"></asp:TextBox><asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="PhoneNumberTB" ErrorMessage="Vul een telefoonnummer in!" Style="color: red;" /><br />
        <div id="dvCaptcha">
        </div>
        <%--reCAPTCHA--%>
        <asp:TextBox ID="txtCaptchaFirst" runat="server" Style="display: none;visibility:hidden;" OnTextChanged="txtCaptchaFirst_TextChanged"/>
       
        <br />
        <br />
        <asp:Button runat="server" ID="RegisterButton" Text="Registreer" OnClick="Register_Click" CssClass="btn btn-default hcloudBtn" />

    </div>
    <asp:RegularExpressionValidator ID="valRegExEmail" runat="server" ControlToValidate="RegisterEmailTB"
        Display="None" ErrorMessage="Geef een geldig emailadress" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z\.][a-zA-Z]{1,3}$" Style="color: red;"></asp:RegularExpressionValidator>
    <div runat="server" id="MessageAlert" class="alert alert-info hcloudAlertPanel">
        <asp:Label runat="server" ID="RegisterControler" Text="info"></asp:Label>
    </div>
</asp:Content>

