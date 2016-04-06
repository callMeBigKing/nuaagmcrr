<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="shuru.aspx.cs" Inherits="shuru" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <br/><br/>
    <div class="titlename">Input date</div>
                 <div class="top-info-wrap" style="float:right;    font-size: 40px;
    font-weight: 100;padding:0px 100px">
                        <ul class="top-info-list clearfix">
                            
                            <li><a href="loadModel.aspx">Load Data</a></li>
                            
                        </ul>
                    </div>


     <div class=" filescrollbox "  style="text-align:center">
        
            <br/><br/><br/><br/><br/><br/><br/><br/>
          Please enter each option corresponds to the number of DM formats such as“2,4,1”<br/><br/>
        
        <asp:TextBox ID="TextBox1" runat="server"  ></asp:TextBox><br/><br/>
        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
        <br/>
           <br/> 
        <asp:Label ID="Label1" runat="server"  ></asp:Label>
            <br/><br/><br/><br/><br/><br/><br/><br/>
           
</div>
     
</asp:Content>

