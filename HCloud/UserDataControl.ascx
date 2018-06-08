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
    <div class="col-md-4">
        <b>Postcode</b>
    </div>
    <div class="col-md-8">
        <asp:Label runat="server" ID="PostCode" /><asp:LinkButton class="glyphicon glyphicon-pencil" id="PostCodeLink" myParam="PostCode" runat="server" onclick="ClickHandler_Click" OnClientClick="ShowFilter();" />
    </div>
</div>
<div class="row">
    <div class="col-md-4">
        <b>Straat</b>
    </div>
    <div class="col-md-8">
        <asp:Label runat="server" ID="Straat" /><asp:LinkButton class="glyphicon glyphicon-pencil"   id="StraatLink" myParam="Straat"  runat="server" onclick="ClickHandler_Click" OnClientClick="ShowFilter();"></asp:LinkButton>
    </div>
</div>
<div class="row">
    <div class="col-md-4">
        <b>Huisnummer</b>
    </div>
    <div class="col-md-8">
        <asp:Label runat="server" ID="Huisnummer" /><asp:LinkButton class="glyphicon glyphicon-pencil"  id="HuisnummerLink" myParam="Huisnummer"  runat="server" onclick="ClickHandler_Click" OnClientClick="ShowFilter();"></asp:LinkButton>
    </div>
</div>
<div class="row">
    <div class="col-md-4">
        <b>Woonplaats</b>
    </div>
    <div class="col-md-8">
        <asp:Label runat="server" ID="Woonplaats" /><asp:LinkButton class="glyphicon glyphicon-pencil"  id="WoonplaatsLink" myParam="Woonplaats"  runat="server" onclick="ClickHandler_Click" OnClientClick="ShowFilter();"></asp:LinkButton>
    </div>
</div>
<div class="row">
    <div class="col-md-4">
        <b>Geboorteplaats</b>
    </div>
    <div class="col-md-8">
        <asp:Label runat="server" ID="Geboorteplaats" /><asp:LinkButton class="glyphicon glyphicon-pencil"  id="GeboorteplaatsLink" myParam="Geboorteplaats"  runat="server" onclick="ClickHandler_Click" OnClientClick="ShowFilter();"></asp:LinkButton>
    </div>
</div>
<div class="row">
    <div class="col-md-4">
        <b>Bloedgroep</b>
    </div>
    <div class="col-md-8">
        <asp:Label runat="server" ID="Bloedgroep" /><asp:LinkButton class="glyphicon glyphicon-pencil"   id="BloedgroepLink" myParam="Bloedgroep"  runat="server" onclick="ClickHandler_Click" OnClientClick="ShowFilter();"></asp:LinkButton>
    </div>
</div>
<div class="row">
    <div class="col-md-4">
        <b>GeboorteDatum</b>
    </div>
    <div class="col-md-8">
        <asp:Label runat="server" ID="GeboorteDatum" /><asp:LinkButton class="glyphicon glyphicon-pencil"  id="GeboorteDatumLink" myParam="GeboorteDatum"   runat="server" onclick="ClickHandler_Click" OnClientClick="ShowFilter();"></asp:LinkButton>
    </div>
</div>
<div class="row">
    <div class="col-md-4">
        <b>Bankrekeningnummer</b>
    </div>
    <div class="col-md-8">
        <asp:Label runat="server" ID="Bankrekeningnummer" /><asp:LinkButton class="glyphicon glyphicon-pencil"  id="BankrekeningnummerLink" myParam="Bankrekeningnummer"  runat="server" onclick="ClickHandler_Click" OnClientClick="ShowFilter();"></asp:LinkButton>
    </div>
</div>
<div class="row">
    <div class="col-md-4">
        <b>Provincie</b>
    </div>
    <div class="col-md-8">
        <asp:Label runat="server" ID="Provincie" /><asp:LinkButton class="glyphicon glyphicon-pencil"  id="ProvincieLink" myParam="Provincie"   runat="server" onclick="ClickHandler_Click" OnClientClick="ShowFilter();"></asp:LinkButton>
    </div>
</div>
<div class="row">
    <div class="col-md-4">
        <b>Nationaliteit</b>
    </div>
    <div class="col-md-8">
        <asp:Label runat="server" ID="Nationaliteit" /><asp:LinkButton class="glyphicon glyphicon-pencil"  id="NationaliteitLink" myParam="Nationaliteit"   runat="server" onclick="ClickHandler_Click" OnClientClick="ShowFilter();"></asp:LinkButton>
    </div>
</div>
<div class="FilterGrid" id="FilterGrid" style="display:inline-block; " runat="server">
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
