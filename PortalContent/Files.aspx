<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true" CodeBehind="Files.aspx.cs" Inherits="HCloud.PortalContent.Files" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script>

        $(document).ready(function (event) {
            $(".FilterGrid").hide();
            $("#SearchButton").click(function () {
                $(".filecard").fadeOut();
                return false;
            });
            $("#SearchFilterInput").keyup(function (e) {
                var a = String.fromCharCode(e.which);
                var myarray = $(".filecard");
                var arrayLength = myarray.length;
                for (var i = 0; i < arrayLength; i++) {
                    var str = myarray[i].innerText;
                    var checkstr = $(this).val();
                    console.log(checkstr);
                    var text = str.replace(/\s/g, '').toLowerCase();
                    console.log(text);
                    if (text.includes(checkstr)) {
                        $("#" + myarray[i].id).fadeIn();
                    }
                    else {
                        $("#" + myarray[i].id).fadeOut();
                    }
                }
            });
        });




    </script>
    <div class="FilesTopBar">
        <div class="FilesTopBarItem">
            <asp:Label runat="server" ID="FilesFrom" Text="Bestanden van ..."></asp:Label>
        </div>
        <div class="FilesTopBarItem">
            <div class="GridviewToolbarItem Search">
                <div style="" class="SearchDiv">
                    <input placeholder="Zoeken" type="text" class="SearchInput" id="SearchFilterInput" />
                    <button class="btnSearch" id="SearchButton">
                        <i class="glyphicon glyphicon-search"></i>
                    </button>
                </div>
            </div>
        </div>

        <div id="Filter" onclick="ShowFilter();" class="GridviewToolbarItem">
            <i class="glyphicon glyphicon-filter"></i>Filter 
            <div style="display: inline-block;" id="Filtername_date" runat="server"></div>
        </div>
    </div>

    <div class="FilesMain" runat="server" id="FilesMain">
    </div>

    <div>
        <asp:Label runat="server" ID="Messager"></asp:Label>
    </div>

    <div class="FilterGrid" id="FilterGrid">
        <div>
            <div id="Dragger" onmousedown="DraggermsDown();" style="font-size: 20px; padding: 10px; padding-bottom: 0px; margin-right: 0px;">
                Filter<div onclick="ShowFilter();" class="HCicon" style="float: right;"><i class="glyphicon glyphicon-remove"></i></div>
            </div>
        </div>
        <hr style="margin-top: 10px;" />
        <div style="padding: 10px; padding-top: 0px;">
            <asp:Panel runat="server" DefaultButton="SaveFilter">
            <b>Bestandsformaat:</b><br />
            <asp:TextBox ID="FilterFileFormat" runat="server" CssClass="form-control"></asp:TextBox>
            <b>Datum:</b><br />

            <asp:TextBox ID="FilterFileDate" TextMode="Date" CssClass="form-control " runat="server"></asp:TextBox>
            </asp:Panel>
        </div>
        <hr style="margin: 10px;" />
        <div>
            <asp:Button ID="SaveFilter" CausesValidation="false" runat="server" OnClick="SaveFilter_Click" class="btn hcloudBtn" Style="float: right; margin-right: 10px; margin-bottom: 10px;" Text="Opslaan"></asp:Button>
        </div>

    </div>
</asp:Content>
