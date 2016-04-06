<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="delet3.aspx.cs" Inherits="页面_delet3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="titlename">Delete state</div>
    
      <div class="filescrollbox "   style="vertical-align:middle">
           <br/><br/><br/><br/>
     <table style="text-align:center;margin:0 50px">
         <tr>
                <td>
                      <asp:Label ID="Label2" runat="server" Text="delet the state that match pattern1 but don't match pattern2"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
        
        <tr><td> <asp:Label ID="Label3" runat="server" Text="please chose pattern1："   ></asp:Label></td></tr>
            <tr>
                <td>
                    <asp:Table ID="Table1" runat="server" ></asp:Table>
                </td>
                
            </tr>
         <tr><td><br /><br /><asp:Label ID="Label4" runat="server" Text="please chose pattern2：" ></asp:Label></td></tr>
            <tr>
                <td >
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
        <asp:Label ID="Label1" runat="server"  CssClass="lable" ></asp:Label>
                      </td>
            </tr>    
    </table>
                 <br/><br/>
          </div>
</asp:Content>

