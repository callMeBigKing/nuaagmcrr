<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="showtranslatemartix.aspx.cs" Inherits="页面_showtranslatemartix" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div class="titlename">State transition matrix</div>
    <div style="margin :50px auto">
        <asp:Table ID="Table2" runat="server"></asp:Table>
        </div>
        <div style="margin :50px auto">
    <asp:Table ID="Table1" runat="server"  ></asp:Table>
            
    <asp:Label ID="Label1" runat="server"  CssClass="lable"></asp:Label>
            <br/><br/><br/><br/><br/>
        </div>
</asp:Content>

