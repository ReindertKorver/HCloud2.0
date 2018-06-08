<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.master" AutoEventWireup="true" CodeBehind="NewFile.aspx.cs" Inherits="HCloud.PortalContent.NewFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function FileUploadChange(element) {
            if (document.getElementById("<%=file.ClientID%>").files.length != 0) {
                var fullPath = $("#<%=file.ClientID%>").val();
                var filename = fullPath.replace(/^.*[\\\/]/, '')
                document.getElementById("StatusLabel").innerText = "Bestand '" + filename + "' geupload";
                document.getElementById("StatusLabel").style.color = "Green";
            }
            else {
                document.getElementById("StatusLabel").innerText = "Bestand niet geupload, probeer opnieuw";
                document.getElementById("StatusLabel").style.color = "Red";
            }
        }
    </script>
    <h1>Nieuw bestand
    </h1>
    Hieronder kunt u een nieuw bestand aanmaken
    <div>
        <b>Gebruiker:</b><br />
        <asp:DropDownList ID="NewFileClient" runat="server" CssClass="form-control"></asp:DropDownList>

        <b>Beschrijving:</b>
        <asp:TextBox runat="server" ID="FileDescription" CssClass="form-control"></asp:TextBox>
        <b>Bestand:</b>
        <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:PostBackTrigger ControlID="SaveButton" />
            </Triggers>

            <ContentTemplate>
                <asp:FileUpload runat="server" name="file" ID="file" CssClass="form-control" Style="display: none;" onchange="FileUploadChange(this);" />

            </ContentTemplate>
        </asp:UpdatePanel>

        <label for="<%=file.ClientID %>" style="cursor: pointer; text-decoration: underline;">Kies een bestand</label><br />
        <label id="StatusLabel"></label>
        <br />
        <div runat="server" id="FileResult">
        </div>
        <asp:Button ID="SaveButton" Text="Opslaan" CssClass="btn hcloudBtn" runat="server" OnClick="SaveButton_Click" />
        <asp:Label runat="server" ID="Message"></asp:Label>
    </div>

</asp:Content>
