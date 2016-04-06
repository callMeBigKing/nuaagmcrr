<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="delet4.aspx.cs" Inherits="页面_delet4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="titlename">Delete state</div>
    
      <div class="filescrollbox "   style="vertical-align:middle">
           
     <table style="text-align:center;margin:100px 50px">
         <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="delet the state which make statement is true such as -1|3&5。"  ></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td> 
                 <asp:Table ID="Table1" runat="server"></asp:Table>
                </td>

                <td>
                 <asp:Table ID="Table2" runat="server"></asp:Table>
                </td>
            </tr>

            <tr>
                <td style="height: 29px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />

                </td>
            </tr>        
        <tr>
                <td style="height: 29px; text-align: center">
                    <asp:Label ID="Label1" runat="server"   CssClass="lable"></asp:Label>

                </td>
            </tr> 
    </table>
                      <br/><br/><br/><br/><br/>
          </div>
</asp:Content>

