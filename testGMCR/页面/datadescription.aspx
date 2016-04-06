<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="datadescription.aspx.cs" Inherits="页面_datadescription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br/><br/>
    <div class="titlename">Input data</div>
     <div class="filescrollbox "   style="vertical-align:middle">
        
            <br/><br/><br/><br/><br/><br/>

        <table style="text-align:center;margin:0 auto" >

           <tr>
                <td style="height: 29px; text-align: center">
                    Please enter the decision-makers and the corresponding option name：
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Table ID="HolderTable" runat="server"></asp:Table>
                </td>
            </tr>
            <tr>
                <td style="height: 29px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
         <br/><br/><br/><br/><br/>
   </div>
</asp:Content>

