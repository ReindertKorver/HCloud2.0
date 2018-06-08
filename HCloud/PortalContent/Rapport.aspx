<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true" CodeBehind="Rapport.aspx.cs" Inherits="HCloud.PortalContent.NewRapport" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $(".FilterGrid").hide();
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            $(".FilterGrid").hide();
        });
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=TherapiesGrid.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }

        function SearchFilterInput_OnkeyUp() {
            if ($('#SearchFilterInput').val() != "") {
                $('#<%=TherapiesGrid.ClientID%> tbody tr').hide();
            }
            $('#<%=TherapiesGrid.ClientID%> tbody .TherapiesGridHeader').show();
            var data = $('#SearchFilterInput').val();
            var len = data.length;
            if (len > 0) {
                $('#<%=TherapiesGrid.ClientID%>').find('tbody tr').each(function () {
                    coldata = $(this).children().eq(1);
                    console.log(coldata);
                    var temp = coldata.text().toUpperCase().indexOf(data.toUpperCase());
                    console.log(temp);
                    if (temp === 0) {

                        console.log($(this));
                        $(this).show();
                    }
                });
            } else {
                if ($('#SearchFilterInput').val() != "") {
                    $('#<%=TherapiesGrid.ClientID%> tbody tr').hide();
                }
            }
        }
    </script>
    <script>
</script>
    <h1>Rapportages</h1>
    Hier onder kunt u rapportages van behandelingen bekijken:
     
    <br />
    <b>Behandelingen:</b><br />
    <div class="GridviewToolbar">
        <div class="GridviewToolbarItem">
            <button class="btnRefresh" onclick="window.location.reload();">
                <div>
                    <i class="fas fa-sync-alt" id="RefreshFilter"></i>
                </div>
                Refresh
            </button>
        </div>
        <div id="Filter" onclick="ShowFilter();" class="GridviewToolbarItem">
            <i class="glyphicon glyphicon-filter"></i>Filter 
            <div style="display: inline-block;" id="Filtername_date" runat="server"></div>
        </div>
        <div class="GridviewToolbarItem Search">
            <div style="" class="SearchDiv">
                <input placeholder="Zoeken" type="text" class="SearchInput" id="SearchFilterInput" onkeyup="SearchFilterInput_OnkeyUp();" />
                <button class="btnSearch" onclick="">
                    <i class="glyphicon glyphicon-search"></i>
                </button>
            </div>

        </div>
    </div>
    <div style="max-height: 300px; overflow: auto;">
        <asp:GridView ID="TherapiesGrid" runat="server" CssClass="TherapiesGrid" AutoGenerateColumns="false" Width="100%" HeaderStyle-CssClass="TherapiesGridHeader">

            <Columns>
                <asp:TemplateField HeaderStyle-Width="22px">
                    <HeaderTemplate>
                        <input id="Checkbox2" type="checkbox" onchange="CheckAll(this);" title="Alles Selecteren" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="ItemCheckBox" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Description" HeaderText="Beschrijving" SortExpression="Description" />
                <asp:BoundField DataField="date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Datum" SortExpression="date" />
                <asp:BoundField DataField="therapistLastName" HeaderText="Behandelaar" SortExpression="therapistLastName" />
                <asp:BoundField DataField="CostsInEuro" HeaderText="Kosten" SortExpression="CostsInEuro" />
                <asp:BoundField DataField="Location" HeaderText="Locatie" SortExpression="Location" />
            </Columns>
        </asp:GridView>
        <div id="Messaging" runat="server"></div>
    </div>
    <div class="FilterGrid" id="FilterGrid">
        <div>
            <div id="Dragger" onmousedown="DraggermsDown();" style="font-size: 20px; padding: 10px; padding-bottom: 0px; margin-right: 0px;">
                Filter<div onclick="ShowFilter();" class="HCicon" style="float: right;"><i class="glyphicon glyphicon-remove"></i></div>
            </div>
        </div>
        <hr style="margin-top: 10px;" />
        <div style="padding: 10px; padding-top: 0px;">
            <b>Cliënt:</b><br />
            <asp:DropDownList ID="FilterTherapyClient" runat="server" CssClass="form-control"></asp:DropDownList>
            <b>Datum:</b><br />

            <asp:TextBox ID="FilterTherapyDate" TextMode="Date" CssClass="form-control " runat="server"></asp:TextBox>

        </div>
        <hr style="margin: 10px;" />
        <div>
            <asp:Button ID="SaveFilter" CausesValidation="false" runat="server" OnClick="SaveFilter_Click" class="btn hcloudBtn" Style="float: right; margin-right: 10px; margin-bottom: 10px;" Text="Opslaan"></asp:Button>
        </div>

    </div>
    <br />


    <asp:Label runat="server" ID="lbl"></asp:Label>
    <div>
        <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
        <script type="text/javascript">
            google.charts.load('current', { 'packages': ['corechart'] });
            google.charts.setOnLoadCallback(drawChart);

            function drawChart() {

                var data = google.visualization.arrayToDataTable([
                    ['Task', 'Hours per Day'],
                    ['Work', 11],
                    ['Eat', 2],
                    ['Commute', 2],
                    ['Watch TV', 2],
                    ['Sleep', 7]
                ]);

                var options = {
                    title: 'My Daily Activities'
                };

                var chart = new google.visualization.PieChart(document.getElementById('piechart'));

                chart.draw(data, options);
            }
        </script>
        <div id="piechart" style="width: 900px; height: 500px;"></div>
    </div>
</asp:Content>
