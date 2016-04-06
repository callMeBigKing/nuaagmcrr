<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="storeModel.aspx.cs" Inherits="页面_storeModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="titlename">Store Model</div>
    
      <div class="filescrollbox "   style="vertical-align:middle">
           <br/><br/><br/><br/><br/><br/>
     <table style="text-align:center;margin:0 auto">

            <tr>
                <td>
                    模型名称：
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="height: 29px; text-align: center" colspan="2">
                    <asp:Button ID="Button1" runat="server" Text="Store" OnClick="Button1_Click" />
                    <asp:Label ID="Label1" runat="server"  CssClass="lable" ></asp:Label>
                </td>
            </tr>        
    </table>
          <br/><br/><br/><br/><br/>
          </div>
</asp:Content>

