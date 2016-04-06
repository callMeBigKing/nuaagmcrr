<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="manualdelet.aspx.cs" Inherits="页面_delet4aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="titlename">Delete state</div>
    
      <div class="filescrollbox "   style="vertical-align:middle">
           <br/><br/><br/><br />
          <table style="margin:0 auto">
        <tr>
            <td style="height: 29px; text-align: center">
                <asp:Label ID="Label2" runat="server" Text="Remove the check box selected state。"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Vertical">
                </asp:CheckBoxList>
            </td>
        </tr>
                <tr>
            <td style="height: 29px; text-align: center">
                
         <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
                <asp:Label ID="Label1" runat="server"  CssClass="lable"></asp:Label>
            </td>
                    
        </tr>

    </table>
<br/><br/><br/>
          </div>

</asp:Content>

