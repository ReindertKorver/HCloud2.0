<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true" CodeBehind="Agenda.aspx.cs" Inherits="HCloud.PortalContent.Agenda" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function countdaysKeyDown(e) {
            var value = $('#<%=countdays.ClientID%>').text;
            if (value != null) {
                if (value.length >= 15) {
                    $('#<%=countdays.ClientID%>').text = 15;
                }
            }
            if (e.keycode == 189 || e.charCode == 189) {
                $('#<%=countdays.ClientID%>').text = 1;
            }
        }
        function CheckAll(oCheckbox,Table) {
            var GridView2 = document.getElementById(Table);
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
        function SearchFilterInput_OnkeyUp(id, tabelid) {
            if ($('#' + id).val() != "") {
                $('#' + tabelid + ' tbody tr').hide();
            }
            $('#' + tabelid + ' tbody .TherapiesGridHeader').show();
            var data = $('#' + id).val();
            var len = data.length;
            if (len > 0) {
                $('#' + tabelid).find('tbody tr').each(function () {
                    coldata = $(this).children().eq(2);
                    console.log(coldata);
                    var temp = coldata.text().toUpperCase().indexOf(data.toUpperCase());
                    console.log(temp);
                    if (temp === 0) {

                        console.log($(this));
                        $(this).show();
                    }
                });
            } else {
                if ($('#' + id).val() != "") {
                    $('#' + tabelid + ' tbody tr').hide();
                }
            }
        }

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
    <h1>Agenda</h1>

    <DayPilot:DayPilotCalendar ID="ActivityAgenda" runat="server" />
    <div style="width: 100%; text-align: center; display: inline-block;">
        <asp:Label runat="server" Text="Aantal dagen:"></asp:Label><asp:TextBox ID="countdays" Style="width: 60px; display: inline-block;" min="1" max="15" runat="server" MaxLength="2" onkeydown="countdaysKeyDown(event);" AutoPostBack="true" OnTextChanged="countdays_TextChanged" TextMode="Number" CssClass="form-control" Text="7"></asp:TextBox>
        <asp:Button Style="display: inline-block;" runat="server" ID="BackDay" OnClick="BackDay_Click" Text="Vorige dag" CssClass="btn btn-default hcloudBtn"></asp:Button>
        <asp:Button Style="display: inline-block;" runat="server" ID="Today" OnClick="Today_Click" Text="Vandaag" CssClass="btn btn-default hcloudBtn"></asp:Button>
        <asp:Button Style="display: inline-block;" runat="server" ID="NextDay" OnClick="NextDay_Click" Text="Volgende dag" CssClass="btn btn-default hcloudBtn"></asp:Button>
    </div>
    <h2>Accepteer afspraken</h2>
    <div class="GridviewToolbar">
        <div class="GridviewToolbarItem">
            <button class="btnRefresh" onclick="window.location.reload();">
                <div>
                    <i class="fas fa-sync-alt RefreshFilter"></i>
                </div>
                Refresh
            </button>
        </div>

        <div class="GridviewToolbarItem Search">
            <div style="" class="SearchDiv">
                <input placeholder="Zoeken" type="text" class="SearchInput" id="SearchFilterInput" onkeyup="SearchFilterInput_OnkeyUp(this.id,'<%=TherapyGrid.ClientID %>');" />
                <button class="btnSearch" onclick="">
                    <i class="glyphicon glyphicon-search"></i>
                </button>
            </div>

        </div>
    </div>
    <div style="max-height: 300px; overflow: auto;">
        <asp:GridView ID="TherapyGrid" runat="server" AutoGenerateColumns="false" Width="100%" HeaderStyle-CssClass="TherapiesGridHeader">

            <Columns>
                <asp:TemplateField HeaderStyle-Width="22px">
                    <HeaderTemplate>
                        <input id="Checkbox2" type="checkbox" onchange="CheckAll(this,'<%=TherapyGrid.ClientID %>');" title="Alles Selecteren" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="ItemCheckBox" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="Description" HeaderText="Beschrijving" SortExpression="Description" />
                <asp:BoundField DataField="therapistLastName" HeaderText="Behandelaar" SortExpression="therapistLastName" />
                <asp:BoundField DataField="Date" HeaderText="Datum/Tijd" SortExpression="Date" />
                <asp:BoundField DataField="Time" HeaderText="Behandeltijd" SortExpression="Time" />
            </Columns>
        </asp:GridView>
       
    </div>

    <br />
    <asp:Button ID="AcceptButton" runat="server" Text="Accepteren" CssClass="btn btn-default hcloudBtn" OnClick="AcceptButton_Click"/>
    <asp:Label runat="server" ID="Messager" Text=""></asp:Label>
</asp:Content>
