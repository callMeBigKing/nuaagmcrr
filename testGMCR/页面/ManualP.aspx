<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ManualP.aspx.cs" Inherits="页面_ManualP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="titlename">Set perfence</div>
      <div class="filescrollbox "   style="align-content:center; text-align:left ">
    <table style="margin:100px auto">
      <tr ><td ><asp:Table ID="Table1" runat="server" ></asp:Table></td></tr>
   <tr><td>  <asp:Label ID="Label2" runat="server"  Text="please input"></asp:Label><asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList><asp:Label ID="Label3" runat="server"  Text="'perfence(Compatible with all preference) "> </asp:Label></td></tr>
       <tr><td style="text-align:center"> ">"  present priority</td></tr>
         <tr><td style="text-align:center"> ">>" present strength  priority</td></tr>
        
   <tr><td><asp:TextBox ID="TextBox1" runat="server" style="float:left;width:180px"></asp:TextBox>  <asp:Table ID="Table2" runat="server" style="padding-left:100px"></asp:Table> </td></tr>
         <tr><td style="">  <asp:Button ID="Button1" runat="server" Text="add" OnClick="Button1_Click1" /> </td></tr>
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

