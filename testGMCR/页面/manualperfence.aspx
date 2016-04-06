<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="manualperfence.aspx.cs" Inherits="页面_manualperfence" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
      <div class="titlename">Set perfence</div>
      <div class="filescrollbox "   style="align-content:center; text-align:left ">
    <table style="margin:100px auto">
      <tr ><td ><asp:Table ID="Table1" runat="server" ></asp:Table></td></tr>
   <tr><td>  <asp:Label ID="Label2" runat="server"  Text="please input"></asp:Label><asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList><asp:Label ID="Label3" runat="server"  Text="'perfence(Compatible with all preference) "> </asp:Label></td></tr>
   <tr><td >ifS1>S0 请在下面表格第二行第一列输入">" </td></tr>
         <tr><td>若S1>>S0 请在下面表格第二行第一列输入">>" </td></tr>
         <tr><td>若S1US0 请在下面表格第二行第一列输入"U" </td></tr>
   <tr><td> <asp:Table ID="Table2" runat="server" CssClass="table"></asp:Table></td></tr>
       
        <tr>
                <td style="height: 29px; text-align: center">
                    <asp:Button ID="btnCallBack" runat="server" Text="Submit" OnClick="btnCallBack_Click" />
                     <asp:Button ID="btnHid" runat="server" OnClick="btnHid_Click" Width="0px" Height="0px" BorderColor="White" BorderWidth="0px" />
                      <asp:HiddenField ID="hid" runat="server" />
                    <asp:Label ID="Label1" runat="server"  CssClass="lable"></asp:Label>
                </td>
            </tr> 

         </table>
        </div>
</asp:Content>

