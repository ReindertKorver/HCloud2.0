<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HCloud._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center; width: 100%;">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/Webp.net-gifmaker (1).gif" ImageAlign="Middle" Style="max-width: 100%; max-height:250px;" />
    </div>
    <h1>H.Cloud het zorgportaal</h1>
    Welkom op de hoofdpagina van het zorgportaal van H.Cloud.<br />
    Het portaal waar cliënten, Huisartsen, ziekenhuizen, zorgverzekeraars en specialisten bij elkaar komen. Een uitgebreide registratie van behandelingen en aandoeningen.
    <br />
    <b>Wat betekent dit voor u?</b>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                Cliënten
            </h3>
        </div>
        <div class="panel-body">
            Cliënten hebben inzicht in de gedane behandelingen door doktoren, huisartsen, ziekenhuizen en specialisten.
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                Huisartsen
            </h3>
        </div>
        <div class="panel-body">
          Voeg behandelingen toe aan cliënten en krijg makkelijk inzicht in welke cliënt welke aandoening heeft.
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
               Ziekenhuizen
            </h3>
        </div>
        <div class="panel-body">
            Bekijk cliënt gegevens om zo sneller tot een goede behandeling te komen.
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
               Zorgverzekeraars
            </h3>
        </div>
        <div class="panel-body">
             Krijg inzicht in de behandelingen van uw cliënten, waardoor u makkelijker kunt factureren.
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
               Specialisten
            </h3>
        </div>
        <div class="panel-body">
             Behandel uw cliënten beter door de informatie die verschaft wordt door dit registratie systeem.
        </div>
    </div>
    <div style="text-align: center; width: 100%;">
        <a  ID="Register" class="btn btn-default hcloudBtn" href="RegisterNew.aspx">Registreer nu gratis</a>
    </div>
</asp:Content>
