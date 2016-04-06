<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="transitions.aspx.cs" Inherits="页面_transitions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="titlename">Set Transitions</div>
        <table style="text-align:center;margin:100px 50px">
            
            <tr><td>
                     <asp:Label ID="Label3" runat="server" Text="Single Option Based Irreversibility:<br />设置option Y—N之间的转换"  ></asp:Label>
                </td></tr>
            <tr>
                <td>
                 <asp:Table ID="Table1" runat="server"></asp:Table>
                    <br /><br /><br />
                </td>
            </tr>

           <tr><td>
               <asp:Label ID="Label2" runat="server" Text="Multiple Option Based Irreversibility:<br />与patter1匹配的状态不能转移到于pattern2匹配的状态" ></asp:Label>
                   
               </td></tr>
            <tr>
                <td>
                 <asp:Table ID="Table2" runat="server"></asp:Table>
                </td>
            </tr>
            <tr>
                <td style="height: 29px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
                    <asp:Label ID="Label1" runat="server"  CssClass="lable"></asp:Label>

                </td>
            </tr>        
    </table>
</asp:Content>

