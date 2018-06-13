<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true" CodeBehind="ClientData.aspx.cs" Inherits="HCloud.PortalContent.ClientData" %>
<%@ Register Src="~/UserDataControl.ascx" TagName="UserDataControl" TagPrefix="uc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="UserDataDiv" class="UserDataControl" style="padding-left: 50px;padding-right: 50px;" runat="server">
                    <uc1:UserDataControl id="ListPicker1"  Runat="server" />
                </div>
</asp:Content>
