<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDataControl.ascx.cs" Inherits="HCloud.UserDataControl" %>
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/jquery-ui.min.js" type="text/javascript"></script>
<script>  
    $(document).ready(function (event) {
        if (<%=(Page.IsPostBack).ToString().ToLower()%>) { }
        else {
            //$(".FilterGrid").hide();
        }
    });
    $(document).ready(function (event) {
        $("#<%=FilterGrid.ClientID %>").draggable();
    });

    function DraggermsDown() {
        $("#<%=FilterGrid.ClientID %>").draggable();
    }
    function ShowFilter() {
        if ($(".FilterGrid").is(":hidden"))
            $(".FilterGrid").fadeIn();
        else
            $(".FilterGrid").fadeOut();
    }
</script>
<div class="row">
    <div class="col-md-3">
        <b>Postcode</b>
    </div>
    <div class="col-md-5">
        <asp:Label runat="server" ID="PostCode" />
    </div>
    <div class="col-md-3">
<asp:TextBox runat="server" ID="PostCodeTXT" CssClass="form-control " ></asp:TextBox>
        <asp:RegularExpressionValidator runat="server" ControlToValidate="PostCodeTXT" ValidationExpression="^[1-9][0-9]{3}\s*(?:[a-zA-Z]{2})?$" ErrorMessage="Geef een geldige postcode op"></asp:RegularExpressionValidator>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <b>Straat</b>
    </div>
    <div class="col-md-5">
        <asp:Label runat="server" ID="Straat" />
    </div>
    <div class="col-md-3">
<asp:TextBox runat="server" ID="StraatTXT" CssClass="form-control " ></asp:TextBox>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <b>Huisnummer</b>
    </div>
    <div class="col-md-5">
        <asp:Label runat="server" ID="Huisnummer" />
    </div>
    <div class="col-md-3">
<asp:TextBox runat="server" ID="HuisnummerTXT" CssClass="form-control "  TextMode="Number" MaxLength="5"></asp:TextBox>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <b>Woonplaats</b>
    </div>
    <div class="col-md-5">
        <asp:Label runat="server" ID="Woonplaats" />
    </div>
    <div class="col-md-3">
<asp:TextBox runat="server" ID="WoonplaatsTXT" CssClass="form-control " ></asp:TextBox>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <b>Geboorteplaats</b>
    </div>
    <div class="col-md-5">
        <asp:Label runat="server" ID="Geboorteplaats" />
    </div>
    <div class="col-md-3">
<asp:TextBox runat="server" ID="GeboorteplaatsTXT" CssClass="form-control " ></asp:TextBox>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <b>Bloedgroep</b>
    </div>
    <div class="col-md-5">
        <asp:Label runat="server" ID="Bloedgroep" />
    </div>
    <div class="col-md-3">
<asp:TextBox runat="server" ID="BloedgroepTXT" CssClass="form-control " ></asp:TextBox>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <b>GeboorteDatum</b>
    </div>
    <div class="col-md-5">
        <asp:Label runat="server" ID="GeboorteDatum" />
    </div>
    <div class="col-md-3">
<asp:TextBox runat="server" ID="GeboorteDatumTXT" TextMode="Date" CssClass="form-control " ></asp:TextBox>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <b>Bankrekeningnummer</b>
    </div>
    <div class="col-md-5">
        <asp:Label runat="server" ID="Bankrekeningnummer" />
    </div>
    <div class="col-md-3">
<asp:TextBox runat="server" ID="BankrekeningnummerTXT" CssClass="form-control " ></asp:TextBox>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <b>Provincie</b>
    </div>
    <div class="col-md-5">
        <asp:Label runat="server" ID="Provincie" /></div>
    <div class="col-md-3"><asp:DropDownList runat="server" CssClass="form-control "  ID="ProvincieTXT">
            <asp:ListItem>
                Groningen
            </asp:ListItem>
            <asp:ListItem>
                Friesland
            </asp:ListItem>
            <asp:ListItem>
                Drenthe
            </asp:ListItem>
            <asp:ListItem>
                Overijssel
            </asp:ListItem>
            <asp:ListItem>
                Flevoland
            </asp:ListItem>
            <asp:ListItem>
                Gelderland
            </asp:ListItem>
            <asp:ListItem>
                Utrecht
            </asp:ListItem>
            <asp:ListItem>
                Noord-Holland
            </asp:ListItem>
            <asp:ListItem>
                Zuid-Holland
            </asp:ListItem>
            <asp:ListItem>
                Zeeland
            </asp:ListItem>
            <asp:ListItem>
                	Noord-Brabant
            </asp:ListItem>
            <asp:ListItem>
                Limburg

            </asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <b>Nationaliteit</b>
    </div>
    <div class="col-md-5">
        <asp:Label runat="server" ID="Nationaliteit" />
    </div>
    <div class="col-md-3">
        <asp:TextBox runat="server" ID="NationaliteitTXT" CssClass="form-control " ></asp:TextBox>
    </div>
</div>
<div class="FilterGrid" id="FilterGrid" style="display: inline-block;" runat="server">
    <div>
        <div id="Dragger" onmousedown="DraggermsDown();" style="font-size: 20px; padding: 10px; padding-bottom: 0px; margin-right: 0px;">
            Wijziging aanbrengen<div onclick="ShowFilter();" class="HCicon" style="float: right;"><i class="glyphicon glyphicon-remove"></i></div>
        </div>
    </div>
    <hr style="margin-top: 10px;" />
    <div style="padding: 10px; padding-top: 0px;">
        <asp:Panel runat="server" DefaultButton="SaveValue">
            <b runat="server" id="EditValueText"></b>
            <br />
            <asp:TextBox ID="EditValue" runat="server" CssClass="form-control"></asp:TextBox>
        </asp:Panel>
    </div>
    <hr style="margin: 10px;" />
    <div>
        <asp:Button ID="SaveValue" CausesValidation="false" runat="server" OnClick="SaveValue_Click" class="btn hcloudBtn" Style="float: right; margin-right: 10px; margin-bottom: 10px;" Text="Opslaan"></asp:Button>
    </div>

</div>
<div style="display: none;">
    <asp:Label runat="server" ID="CurrentValueEdit" />
</div>
