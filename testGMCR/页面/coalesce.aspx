<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="coalesce.aspx.cs" Inherits="页面_coalesce" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="titlename">State coalesce</div>

        <div class="filescrollbox "   style="vertical-align:middle">
           <br/><br/><br/><br/><br/><br/>
     <table style="text-align:center;margin:0 100px">
            <tr>
                <td>
                    <asp:Table ID="Table1" runat="server" ></asp:Table>
                </td>
            </tr>

            <tr>
                <td style="height: 29px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>        
    </table>
            </div>
</asp:Content>

