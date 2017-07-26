<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ProjectCode.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %><span style="font-size: large">Task </span>
        <asp:TextBox ID="TextBox1" runat="server" Height="20px" Width="249px"></asp:TextBox>
    </h2>
    <h2><%: Title %><span style="font-size: large">Task Description<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </span></h2>
    <h3>Level</h3>
    <address>
        Plan start time</address>
    <address>
        Plan End Time</address>

    <address>
        &nbsp;</address>
</asp:Content>
