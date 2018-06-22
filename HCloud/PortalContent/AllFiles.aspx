<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true" CodeBehind="AllFiles.aspx.cs" Inherits="HCloud.PortalContent.AllFiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Alle bestanden</h1>
    Hieronder kunt u alle bestanden van de gekozen gebruiker vinden.
    <div>
         <script>

        $(document).ready(function (event) {
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
    <div class="FilesTopBar" style="padding-top: 7px;">
        <div class="FilesTopBarItem">
            Bestanden van <asp:DropDownList ID="NewDeseaseClient" runat="server" style="display: initial; width:auto;" CssClass="form-control" OnSelectedIndexChanged="NewDeseaseClient_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
        <div class="FilesTopBarItem">
            <asp:DropDownList runat="server" ID="SortBy" OnSelectedIndexChanged="SortBy_SelectedIndexChanged" CssClass="SortbyDropDown" AutoPostBack="true"></asp:DropDownList>
        </div>
    </div>
    <div class="FilesMain" runat="server" id="FilesMain">
        
    </div>
    </div>
</asp:Content>
