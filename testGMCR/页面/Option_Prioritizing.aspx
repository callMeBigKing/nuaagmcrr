<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Option_Prioritizing.aspx.cs" Inherits="页面_Option_Prioritizing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="titlename">Set perfence</div>
    <div class="filescrollbox "   style="vertical-align:middle">

           <br/><br/><br/><br/><br/><br/>
     <table style="text-align:center;margin:0 50px">
         
            <tr>
                <td style="height: 29px; text-align: center">
                 
                    <asp:Label ID="Label2" runat="server" Text="Please input the preference of decision makers  (Compatible with all preferences. )such as" ></asp:Label> 

                </td>
            </tr>     
          <tr>
                <td style="height: 29px; text-align: center">
                    <asp:Label ID="Label3" runat="server" Text=" 1||-1" ></asp:Label>   

                </td>
            </tr>
        <tr>
                <td style="height: 29px; text-align: center">
                    <asp:Label ID="Label4" runat="server" Text=" 2IFF3&1if1@1||3|2if-1@1" ></asp:Label>     

                </td>
            </tr>
        <tr>
                <td style="height: 29px; text-align: center">
                    <asp:Label ID="Label5" runat="server" Text="  3IF4" ></asp:Label>      

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
                   <asp:Button ID="btnCallBack" runat="server" Text="Submit" OnClick="btnCallBack_Click" />
        <asp:Button ID="btnHid" runat="server" OnClick="btnHid_Click" Width="0px" Height="0px" BorderColor="White" BorderWidth="0px" />
         <asp:HiddenField ID="hid" runat="server" />

                </td>
            </tr>       
        <tr>
                <td style="height: 29px; text-align: center">
                    <asp:Label ID="Label1" runat="server"  CssClass="lable"></asp:Label>
                   
                </td>
        </tr>  
    </table>
 <br/><br/><br/><br/><br/>
          </div>
</asp:Content>

