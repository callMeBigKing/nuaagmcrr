<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="delet2.aspx.cs" Inherits="页面_delet2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="titlename">Delete state</div>
    
      <div class="filescrollbox "   style="vertical-align:middle">
           <br/><br/><br/><br/><br/><br/>
     <table style="text-align:center;margin:0 auto">
         <tr>
                <td>
             <asp:Label ID="Label2" runat="server" Text=" Remove the mutual exclusion in option,  the option you selected at least can be choice."></asp:Label>         <br/>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Table ID="Table1" runat="server" ></asp:Table>
                </td>
            </tr>

            <tr>
                <td style="height: 29px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
                    <asp:Label ID="Label1" runat="server"  CssClass="lable" ></asp:Label>
                </td>
            </tr>        
    </table>
            <br/><br/><br/><br/><br/>
          </div>
</asp:Content>


