<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="showperferencedraw.aspx.cs" Inherits="页面_showperferencedraw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="titlename">Perfence information</div>
    
     <div > ps:For the IE browser, preference tree may not refresh, press F5 to manually refresh</div>
            <table style="text-align:center;margin:40px 50px">
         <tr>
          <td>
                <asp:Label ID="Label1" runat="server"  CssClass="lable"></asp:Label>
          </td>
         </tr> 
             <tr>
          <td>
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"></asp:DropDownList>'Perfence information
          </td>
         </tr> 
       <tr>
          <td>
    <asp:Table ID="Table1" runat="server"></asp:Table>
          </td>
         </tr>     
         <tr>      
            <td>
    <asp:Table ID="Table3" runat="server"    CssClass="table"></asp:Table>
              </td>
          </tr>
       </table>
    <asp:Table ID="Table2" runat="server"></asp:Table>
     
</asp:Content>

