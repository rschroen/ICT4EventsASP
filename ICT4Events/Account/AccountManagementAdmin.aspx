﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountManagementAdmin.aspx.cs" Inherits="ICT4Events.Account.AccountManagementAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div>
        <h1>Zoek account:</h1>
        <asp:TextBox ID="tbSearchUserName" runat="server" Height="25px" Width="250px"></asp:TextBox>
        <hr />
        <br />
        <h2>E-mailadres:</h2>
        <asp:TextBox ID="tbEmailAdress" TextMode="Email" ReadOnly="true" runat="server" Height="25px" Width="250px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbEmailAdress"
            ErrorMessage="Dit veld is verplicht." />
        <br />
        <h2>Gebruikersnaam:</h2>
        <asp:TextBox ID="tbUserName" ReadOnly="true" runat="server" Height="25px" Width="250px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbUserName"
            ErrorMessage="Dit veld is verplicht." />
        <a href="ChangePassword.aspx">Klik hier om uw wachtwoord veranderen</a>
        <br />
        <h2>Voornaam:</h2>
        <br />
        <asp:TextBox ID="tbFirstName" runat="server" Height="25px" Width="250px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbFirstName"
            ErrorMessage="Dit veld is verplicht." />
        <h2>Achternaam:</h2>
        <br />
        <asp:TextBox ID="tbLastName" runat="server" Height="25px" Width="250px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbLastName"
            ErrorMessage="Dit veld is verplicht." />
        <h2>Adres:</h2>
        <br />
        <asp:TextBox ID="tbAddress" runat="server" Height="25px" Width="250px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbAddress"
            ErrorMessage="Dit veld is verplicht." />
        <h2>Stad:</h2>
        <br />
        <asp:TextBox ID="tbCity" runat="server" Height="25px" Width="250px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbCity"
            ErrorMessage="Dit veld is verplicht." />
        <h2>Postcode:</h2>
        <br />
        <asp:TextBox ID="tbZipCode" runat="server" Height="25px" Width="250px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbZipCode"
            ErrorMessage="Dit veld is verplicht." />
        <h2>TelefoonNummer:</h2>
        <br />
        <asp:TextBox ID="tbPhoneNumber" TextMode="Phone" runat="server" Height="25px" Width="250px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPhoneNumber"
            ErrorMessage="Dit veld is verplicht." />
        <br />
        <br />
        <asp:Button ID="btnSave" runat="server" Text="Sla gegevens op" />
        <asp:Button ID="btnCreate" runat="server" Text="Maak account aan" />
        <asp:Button ID="btnDelete" runat="server" Text="Verwijder account" />
    </div>
</asp:Content>