﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemRental.aspx.cs" Inherits="ICT4Events.ItemRental" EnableEventValidation = "false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        Exemplaren uitlenen/verwijderen:</p>
&nbsp;<asp:Label ID="Label2" runat="server" Text="Item ID:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label3" runat="server" Text="Barcode: "></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label5" runat="server" Text="Datum: "></asp:Label>
    <p>
        <asp:TextBox ID="TbLeenUitItemID" runat="server"></asp:TextBox>
        <asp:TextBox ID="TbLeenUitBarcode" runat="server"></asp:TextBox>
        <asp:TextBox ID="TbLeenUitDatum" runat="server"></asp:TextBox>
        DD-MM-YYYY&nbsp; HH24:MI:SS</p>
    <p>
&nbsp;<asp:Button ID="BtnVrijeArtikelen" runat="server" OnClick="BtnVrijeArtikelen_Click" Text="Vrije artikelen" />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Geleende artikelen" Width="189px" />
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnLeenUit" runat="server" OnClick="BtnLeenUit_Click" Text="Leen uit" />
&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Neem in" OnClick="BtnNeemIn_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnVerwijder" runat="server" OnClick="BtnVerwijder_Click" Text="Verwijder" />
    </p>
    <p>
        <asp:Label ID="Label14" runat="server" Text="Zoek exemplaar op barcode:"></asp:Label>
        <asp:TextBox ID="TbZoekExemplaar" runat="server"></asp:TextBox>
        <asp:Button ID="BtnZoekenExemplaar" runat="server" Text="Zoek" OnClick="BtnZoekenExemplaar_Click" />
    </p>
    <p>
        <asp:GridView ID="GvRental" runat="server" OnRowDataBound="GvRental_RowDataBound" OnSelectedIndexChanged="GvRental_SelectedIndexChanged">
        </asp:GridView>
    </p>
    <p>
        ------------------------------------------------------------------------------------------------------------------------------------------------</p>
    <p>
        Artikel toevoegen/verwijderen:</p>
    <p>
        &nbsp;<asp:Label ID="Label13" runat="server" Text="Naam:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label7" runat="server" Text="Merk:"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label8" runat="server" Text="Serie:"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="Label12" runat="server" Text="Prijs:"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label10" runat="server" Text="Aantal: "></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnArtikelVerwijder" runat="server" Text="Verwijder" OnClick="BtnArtikelVerwijder_Click" />
    </p>
    <asp:TextBox ID="TbArtikelNaam" runat="server"></asp:TextBox>
    <asp:TextBox ID="TbArtikelMerk" runat="server"></asp:TextBox>
    <asp:TextBox ID="TbArtikelSerie" runat="server"></asp:TextBox>
    <asp:TextBox ID="TbArtikelPrijs" runat="server" Width="47px"></asp:TextBox>
    <asp:TextBox ID="TbArtikelAantal" runat="server" Width="84px"></asp:TextBox>
    <br />
    <p>
        <asp:Button ID="BtnArtikelVoegToe" runat="server" Text="Voeg toe" Width="199px" OnClick="BtnArtikelVoegToe_Click"  />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnArtikelPasAan" runat="server" Text="Pas aan" Width="199px" OnClick="BtnArtikelPasAan_Click" />
        &nbsp;*Aantal verminderen bij exemplaren.<asp:GridView ID="GvArtikel" runat="server" OnRowDataBound="GvArtikel_RowDataBound" OnSelectedIndexChanged="GvArtikel_SelectedIndexChanged" AutoGenerateSelectButton="True">
        </asp:GridView>
    </p>
    <p>
        ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</p>
    <p>
        Geleende artikelen zoeken:</p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        <asp:Label ID="Label6" runat="server" Text="Zoek op persoon:"></asp:Label>
        <asp:TextBox ID="TbZoekPersoon" runat="server"></asp:TextBox>
        <asp:Button ID="BtnZoekenPersoon" runat="server" Text="Zoek" OnClick="BtnZoekenPersoon_Click" />
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:GridView ID="GvZoekPersoon" runat="server">
    </asp:GridView>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
    </asp:Content>
