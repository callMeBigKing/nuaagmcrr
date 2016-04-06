<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="loadModel.aspx.cs" Inherits="页面_loadModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="titlename">Load model</div>
      <div class="filescrollbox "   style="vertical-align:middle">
           <br/><br/><br/><br />
          <table style="margin:0 auto">
        <tr>
            <td style="height: 29px; text-align: center">
                <asp:Label ID="Label2" runat="server" Text="load the check box selected model。"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server"></asp:RadioButtonList>
            </td>
        </tr>
                <tr>
            <td style="height: 29px; text-align: center">
                
         <asp:Button ID="Button1" runat="server" Text="OK" OnClick="Button1_Click" />
                <asp:Label ID="Label1" runat="server"  CssClass="lable"></asp:Label>
            </td>
                    
        </tr>

    </table>
<br/><br/><br/>
          </div>
</asp:Content>

