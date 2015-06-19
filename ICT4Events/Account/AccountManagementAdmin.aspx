﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountManagementAdmin.aspx.cs" Inherits="ICT4Events.Account.AccountManagementAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type = "text/javascript">
         function Confirm() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("Weet u zeker dat u door wilt gaan?")) {
                 confirm_value.value = "Ja";
             } else {
                 confirm_value.value = "Nee";
             }
             document.forms[0].appendChild(confirm_value);
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div>
        <h1>Zoek account:</h1>
        <asp:TextBox ID="tbSearchUserName" runat="server" Height="25px" Width="250px"></asp:TextBox>
        <br />
        <asp:Button ID="btSearchAccount" runat="server" Text="Zoeken" OnClick="BtSearchAccount_Click" />
        <br />
        <asp:DropDownList ID="ddlAllAcounts" runat="server"></asp:DropDownList>
        <asp:Button ID="btnLoadUser" runat="server" Text="Persoon laden" OnClick="BtnLoadUser_Click" />
        <hr />
        <asp:Button ID="btnSave" ValidationGroup="save" runat="server" Text="Sla gegevens op" OnClick="BtnSave_Click" OnClientClick="Confirm()"/>
        <asp:Button ID="btnDelete" runat="server" Text="Verwijder account" OnClick="BtnDelete_Click" OnClientClick="Confirm()" />
        <asp:Button ID="btnCreate" runat="server" Text="Nieuw account aanmaken" OnClick="BtnCreate_Click" />
        <br />
        <br />
        <h2>AccountID:</h2>
        <asp:TextBox ID="tbAccountID" ReadOnly="true" runat="server" Height="25px" Width="250px"></asp:TextBox>
        <br />
        <h2>Is geactiveerd:</h2>
        <asp:DropDownList ID="ddlActivated" runat="server">
            <asp:ListItem Value = "0" >NIET GEACTIVEERD</asp:ListItem>
            <asp:ListItem Value = "1" >GEACTIVEERD</asp:ListItem>
        </asp:DropDownList>
        <br />
        <h2>E-mailadres:</h2>
        <asp:TextBox ID="tbEmailAdress" runat="server" Height="25px" Width="250px"></asp:TextBox>
        <asp:RegularExpressionValidator Display="Dynamic" ValidationGroup="save" ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="tbEmailAdress" ErrorMessage="Vul een geldig E-Mailadres in"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ValidationGroup="save"  Display="Dynamic" ID="reqEmail" runat="server" ControlToValidate="tbEmailAdress" ErrorMessage="Dit veld is verplicht"></asp:RequiredFieldValidator>
        <br />
        <h2>Rol:</h2>
        <br />
        <asp:DropDownList ID="ddlRol" runat="server">
            <asp:ListItem Value = "GEBRUIKER" >GEBRUIKER</asp:ListItem>
            <asp:ListItem Value = "ADMIN" >ADMIN</asp:ListItem>
        </asp:DropDownList>

        <h2>Gebruikersnaam:</h2>
        <asp:TextBox ID="tbUserName" runat="server" Height="25px" Width="250px"></asp:TextBox>
        <asp:RequiredFieldValidator ValidationGroup="save"  Display="Dynamic" ID="reqUserName" runat="server" ControlToValidate="tbUserName" ErrorMessage="Dit veld is verplicht"></asp:RequiredFieldValidator>
        <h2>Wachtwoord:</h2>
        <br />
        <asp:TextBox ID="tbPassword" runat="server" Height="25px" Width="250px"></asp:TextBox>
        <asp:RequiredFieldValidator ValidationGroup="save" Display="Dynamic" ID="reqPassWord" runat="server" ControlToValidate="tbPassWord" ErrorMessage="Dit veld is verplicht"></asp:RequiredFieldValidator>
        <br />
        <br />
        </div>
</asp:Content>
