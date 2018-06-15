<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDataControl.ascx.cs"   Inherits="HCloud.UserDataControl" %>
<div class="panel-group" id="accordion1">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a data-toggle="collapse" data-parent="#accordion1" href="#collapse10">Persoonsgegevens</a>
            </h4>
        </div>
        <div id="collapse10" class="panel-collapse collapse in">
            <div class="panel-body">


                <asp:RegularExpressionValidator runat="server" ValidationGroup="PostCodeVal" ControlToValidate="PostCodeTXT" ForeColor="Red" ValidationExpression="^[1-9][0-9]{3}\s*(?:[a-zA-Z]{2})?$" ErrorMessage="Geef een geldige postcode op"></asp:RegularExpressionValidator><br />
                <div class="row">
                    <div class="col-md-3">
                        <b>Postcode</b>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="PostCode" />
                    </div>
                    <div class="col-md-3">
                        <div style="width: 100%; display: inline-flex;">
                            <asp:TextBox runat="server" ID="PostCodeTXT" CssClass="form-control " ValidationGroup="PostCodeVal"></asp:TextBox><asp:Button runat="server" ID="PostCodeSave" Style="float: right;" CssClass="btn hcloudBtn" ValidationGroup="PostCodeVal" Text="Opslaan" OnClick="PostCodeSave_Click" />
                        </div>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <b>Straat</b>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="Straat" />
                    </div>
                    <div class="col-md-3">
                        <div style="width: 100%; display: inline-flex;">
                            <asp:TextBox runat="server" ID="StraatTXT" CssClass="form-control "></asp:TextBox><asp:Button runat="server" ID="StraatSave" Style="float: right;" CssClass="btn hcloudBtn" Text="Opslaan" OnClick="StraatSave_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <b>Huisnummer</b>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="Huisnummer" />
                    </div>
                    <div class="col-md-3">
                        <div style="width: 100%; display: inline-flex;">
                            <asp:TextBox runat="server" ID="HuisnummerTXT" CssClass="form-control " TextMode="Number" MaxLength="3"></asp:TextBox><asp:Button runat="server" ID="HuisnummerSave" Style="float: right;" CssClass="btn hcloudBtn" Text="Opslaan" OnClick="HuisnummerSave_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <b>Woonplaats</b>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="Woonplaats" />
                    </div>
                    <div class="col-md-3">
                        <div style="width: 100%; display: inline-flex;">
                            <asp:TextBox runat="server" ID="WoonplaatsTXT" CssClass="form-control "></asp:TextBox><asp:Button runat="server" ID="WoonplaatsSave" Style="float: right;" CssClass="btn hcloudBtn" Text="Opslaan" OnClick="WoonplaatsSave_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <b>Geboorteplaats</b>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="Geboorteplaats" />
                    </div>
                    <div class="col-md-3">
                        <div style="width: 100%; display: inline-flex;">
                            <asp:TextBox runat="server" ID="GeboorteplaatsTXT" CssClass="form-control "></asp:TextBox><asp:Button runat="server" ID="GeboorteplaatsSave" Style="float: right;" CssClass="btn hcloudBtn" Text="Opslaan" OnClick="GeboorteplaatsSave_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <b>Bloedgroep</b>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="Bloedgroep" />
                    </div>
                    <div class="col-md-3">
                        <div style="width: 100%; display: inline-flex;">
                            <asp:TextBox runat="server" ID="BloedgroepTXT" CssClass="form-control "></asp:TextBox><asp:Button runat="server" ID="BloedgroepSave" Style="float: right;" CssClass="btn hcloudBtn" Text="Opslaan" OnClick="BloedgroepSave_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <b>GeboorteDatum</b>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="GeboorteDatum" />
                    </div>
                    <div class="col-md-3">
                        <div style="width: 100%; display: inline-flex;">
                            <asp:TextBox runat="server" ID="GeboorteDatumTXT" TextMode="Date" CssClass="form-control "></asp:TextBox><asp:Button runat="server" ID="GeboorteDatumSave" Style="float: right;" CssClass="btn hcloudBtn" Text="Opslaan" OnClick="GeboorteDatumSave_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <b>Bankrekeningnummer</b>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="Bankrekeningnummer" />
                    </div>
                    <div class="col-md-3">
                        <div style="width: 100%; display: inline-flex;">
                            <asp:TextBox runat="server" ID="BankrekeningnummerTXT" CssClass="form-control "></asp:TextBox><asp:Button runat="server" ID="BankrekeningSave" Style="float: right;" CssClass="btn hcloudBtn" Text="Opslaan" OnClick="BankrekeningSave_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <b>Provincie</b>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="Provincie" />
                    </div>

                    <div class="col-md-3">
                        <div style="width: 100%; display: inline-flex;">
                            <asp:DropDownList runat="server" CssClass="form-control " ID="ProvincieTXT">
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
                            </asp:DropDownList><asp:Button runat="server" ID="ProvincieSave" Style="float: right;" CssClass="btn hcloudBtn" Text="Opslaan" OnClick="ProvincieSave_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <b>Nationaliteit</b>
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" ID="Nationaliteit" />
                    </div>
                    <div class="col-md-3">
                        <div style="width: 100%; display: inline-flex;">
                            <asp:TextBox runat="server" ID="NationaliteitTXT" CssClass="form-control "></asp:TextBox><asp:Button runat="server" ID="NationaliteitSave" Style="float: right;" CssClass="btn hcloudBtn" Text="Opslaan" OnClick="NationaliteitSave_Click" />
                        </div>
                    </div>
                </div>
                <div runat="server" id="messager">
                </div>
            </div>

        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a data-toggle="collapse" data-parent="#accordion1" href="#collapse11">Zorg metingen</a>
            </h4>
        </div>
        <div id="collapse11" class="panel-collapse collapse">
            <div class="panel-body">

                <asp:ListBox runat="server" ID="CareControlMeasures" CssClass="form-control" Style="margin-right: 50px; max-height: 200px; overflow-y: auto; overflow-x: hidden;"></asp:ListBox>
                <div style="max-width: 100%; width: 100%; overflow-x: auto; overflow-y: hidden; height: auto;">
                    <ajaxToolkit:LineChart runat="server" ID="CareControlMeasuresLineChart" Style="border: none;" ChartWidth="720" Width="800px" ChartTitle="Temperatuur overzicht" CssClass="ChartLine" CategoryAxisLineColor="#127a7b" ValueAxisLineColor="#127a7b"></ajaxToolkit:LineChart>
                </div>
            </div>

        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a data-toggle="collapse" data-parent="#accordion1" href="#collapse12">QR code</a>
            </h4>
        </div>
        <div id="collapse12" class="panel-collapse collapse">
            <div class="panel-body">
                <script type="text/javascript" src="/Scripts/qrcode.js"></script>
                <div id="qrcode"></div>
                <script type="text/javascript">
                    $(document).ready(function () {
                        var qrcode = new QRCode(document.getElementById("qrcode"), {
                            width: 100,
                            height: 100
                        });

                        function makeCode() {
                            qrcode.makeCode('<%=qrBSN%>');
                        }
                        makeCode();
                    });


                </script>



            </div>
        </div>
    </div>

</div>
<br />
