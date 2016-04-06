<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Alter.aspx.cs" Inherits="页面_Alter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="titlename">Rename state</div>    
    <div class="filescrollbox "   style="vertical-align:middle;margin-left:20%  10%" >
           <table style="text-align:center;margin:0 auto">  
          <tr> 
              <td>
              <asp:Table ID="Table1" runat="server"></asp:Table>
                  </td>
         </tr>
               <tr> 
              <td>
        <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" />
                          </td>
         </tr>
               </table>
    </div>
    
</asp:Content>

