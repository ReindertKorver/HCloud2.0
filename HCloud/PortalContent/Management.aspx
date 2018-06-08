<%@ Page Title="Management" Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true" CodeBehind="Management.aspx.cs" Inherits="HCloud.PortalContent.Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>

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
    <h1>Management</h1>
    <div>
        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" Height="150px" ActiveTabIndex="0" Width="100%" TabStripPlacement="Top" CssClass="MyTabStyle" CssTheme="none">
            <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Gebruikers">
                <ContentTemplate>
                    <h3>Gebruikers:</h3>
                    <div class="panel-group" id="accordion">
                        <div class="panel panel-default">
                            <%-- Collapse Goedkeuring gebruikers --%>
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">
                                        <b>Gebruiker goedkeuring</b>
                                    </a>
                                </h4>
                            </div>
                            <div id="collapse1" class="panel-collapse collapse in">
                                <div class="panel-body">

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
                                                <input placeholder="Zoeken" type="text" class="SearchInput" id="SearchFilterInput" onkeyup="SearchFilterInput_OnkeyUp(this.id,'<%=UserGrid.ClientID %>');" />
                                                <button class="btnSearch" onclick="">
                                                    <i class="glyphicon glyphicon-search"></i>
                                                </button>
                                            </div>

                                        </div>
                                    </div>
                                    <div style="max-height: 300px; overflow: auto;">
                                        <asp:GridView ID="UserGrid" runat="server" AutoGenerateColumns="false" Width="100%" HeaderStyle-CssClass="TherapiesGridHeader">

                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="22px">
                                                    <HeaderTemplate>
                                                        <input id="Checkbox2" type="checkbox" onchange="CheckAll(this,'<%=UserGrid.ClientID %>');" title="Alles Selecteren" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ItemCheckBox" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FirstName" HeaderText="Naam" SortExpression="FirstName" />
                                                <asp:BoundField DataField="BsnNumber" HeaderText="Bsnnummer" SortExpression="BsnNumber" />
                                                <asp:BoundField DataField="EmailAdress" HeaderText="Emailadres" SortExpression="EmailAdress" />
                                            </Columns>
                                        </asp:GridView>
                                        <div id="Messaging" runat="server"></div>
                                    </div>

                                    <br />
                                    <asp:Button ID="Allowuserbtn" runat="server" OnClick="SaveAllowUser_Click" class="btn hcloudBtn" Text="Gebruikers toestaan" />


                                </div>
                            </div>
                        </div>
                        <%-- Collapse 2 --%>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">
                                        <b>Rollen bij gebruiker wijzigen</b>
                                    </a>
                                </h4>
                            </div>
                            <div id="collapse2" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <div style="width: 100%; display: inline-block;">
                                        <div class="CollumnOf2">
                                            <b>Gebruiker</b>
                                            <input type="text" id="SearchUser" class="form-control" placeholder="Zoeken" style="width: 100%;" onkeyup="SearchList(this.id,'<%=ListBoxUsers.ClientID%>');" /><br />
                                            <asp:ListBox ID="ListBoxUsers" runat="server" CssClass="form-control" Width="100%"></asp:ListBox>
                                        </div>
                                        <div class="CollumnOf2" style="float: right;">
                                            <b>Rol</b>
                                            <input type="text" id="SearchRole" class="form-control" placeholder="Zoeken" style="width: 100%;" onkeydown="SearchList(this.id,'<%=ListBoxRoles.ClientID%>');" /><br />
                                            <asp:ListBox ID="ListBoxRoles" runat="server" CssClass="form-control" Width="100%"></asp:ListBox>
                                        </div>
                                        <asp:Button ID="SaveRole" runat="server" Text="Opslaan" class="btn hcloudBtn" OnClick="SaveRole_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%-- Collapse 3 --%>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">
                                        <b>Arts aan gebruiker toevoegen</b>
                                    </a>
                                </h4>
                            </div>
                            <div id="collapse3" class="panel-collapse collapse">
                                <div class="panel-body" style="width: 100%;">
                                    <div style="width: 100%; display: inline-block;">
                                        <div class="CollumnOf2">
                                            <b>Gebruiker</b>
                                            <input type="text" id="SearchUser1" class="form-control" placeholder="Zoeken" style="width: 100%;" onkeyup="SearchList(this.id,'<%=ListBoxUserTherapist.ClientID%>');" /><br />
                                            <asp:ListBox ID="ListBoxUserTherapist" runat="server" CssClass="form-control" Width="100%"></asp:ListBox>
                                        </div>
                                        <div class="CollumnOf2" style="float: right;">
                                            <b>Arts</b>
                                            <input type="text" id="SearchTherapist" class="form-control" placeholder="Zoeken" style="width: 100%;" onkeydown="SearchList(this.id,'<%=ListBoxTherapist.ClientID%>');" /><br />
                                            <asp:ListBox ID="ListBoxTherapist" runat="server" CssClass="form-control" Width="100%"></asp:ListBox>
                                        </div>
                                        <asp:Button ID="SaveTherapistChange" OnClick="SaveTherapistChange_Click" runat="server" Text="Opslaan" class="btn hcloudBtn" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Functies">
                <ContentTemplate>
                    <h3>Functies:</h3>
                    <div style="width: 100%; display: inline-block;">
                        <div class="CollumnOf2">
                            <input type="text" id="SearchRoles" class="form-control" placeholder="Zoeken" style="width: 100%;" onkeyup="SearchList(this.id,'<%=ListboxFunctionRoles.ClientID%>');" /><br />
                            <asp:ListBox ID="ListboxFunctionRoles" runat="server" style="max-height:300px; height:275px;" CssClass="form-control" Width="100%"></asp:ListBox>
                        </div>
                        <div class="CollumnOf2" style="float: right;">

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
                                        <input placeholder="Zoeken" type="text" class="SearchInput" id="SearchFilterInput1" onkeyup="SearchFilterInput_OnkeyUp(this.id,'<%=RoleGrid.ClientID %>');" />
                                        <button class="btnSearch" onclick="">
                                            <i class="glyphicon glyphicon-search"></i>
                                        </button>
                                    </div>

                                </div>
                            </div>
                            <div style="max-height: 300px; overflow: auto;">
                                <asp:GridView ID="RoleGrid" runat="server" AutoGenerateColumns="false" Width="100%" HeaderStyle-CssClass="TherapiesGridHeader">

                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="22px">
                                            <HeaderTemplate>
                                                <input id="CheckBoxRole" type="checkbox" onchange="CheckAll(this,'<%=RoleGrid.ClientID %>');" title="Alles Selecteren" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ItemCheckBoxRole" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Description"  HeaderText="Recht" SortExpression="Description" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="ChangeRoleBtn" OnClick="ChangeRoleBtn_Click" runat="server" Text="Opslaan" class="btn hcloudBtn" />
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </div>

    <asp:Label ID="Messager" runat="server" Text=""></asp:Label>
</asp:Content>
