<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html lang="en" class="no-js">

<style type="text/css">

    .submit_button{
    cursor: pointer;
    width: 110%;
    height: 44px;
    margin-top: 25px;
    padding: 0;
    background: #ef4300;
    -moz-border-radius: 6px;
    -webkit-border-radius: 6px;
    border-radius: 6px;
    border: 1px solid #ff730e;
    -moz-box-shadow:
        0 15px 30px 0 rgba(255,255,255,.25) inset,
        0 2px 7px 0 rgba(0,0,0,.2);
    -webkit-box-shadow:
        0 15px 30px 0 rgba(255,255,255,.25) inset,
        0 2px 7px 0 rgba(0,0,0,.2);
    box-shadow:
        0 15px 30px 0 rgba(255,255,255,.25) inset,
        0 2px 7px 0 rgba(0,0,0,.2);
    font-family: 'PT Sans', Helvetica, Arial, sans-serif;
    font-size: 14px;
    font-weight: 700;
    color: #fff;
    text-shadow: 0 1px 2px rgba(0,0,0,.1);
    -o-transition: all .2s;
    -moz-transition: all .2s;
    -webkit-transition: all .2s;
    -ms-transition: all .2s;
}


    </style>
    <script  lang="javascript">

        function doLogin() {
            if (!checkData()) return false;

            //checkLoginType();
            document.getElementById("theform").submit();
        }
        function checkData() {
            var acct = document.getElementById("username").value;
            var pwd = document.getElementById("password").value;
            // var auth = document.getElementById("userauthcode").value;

                if (acct.length < 1) {
                    alert("请输入用户名!");
                    document.getElementById("username").focus();
                    return false;
                } else if (pwd.length < 1) {
                    alert("请输入密码!");
                    document.getElementById("password").focus();
                    return false;
                if (acct != "nuaa") {
                    alert("用户名错误");
                    return false
                }
                else if (pwd != "123456") {
                    alert("密码错误");
                    return false;
                
                }

            } //else if(auth.length < 1) {
            //alert("请输入验证码!");
            //document.getElementById("userauthcode").focus();
            //return false;
            //}

            return true;
        }



        </script>
    <head>

        <meta charset="utf-8">
        <title>登录(Login)</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta name="description" content="">
        <meta name="author" content="">

        <!-- CSS -->
        <link rel="stylesheet" href="assets/css/reset.css">
        <link rel="stylesheet" href="assets/css/supersized.css">
        <link rel="stylesheet" href="assets/css/style.css">

        <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
        <!--[if lt IE 9]>
            <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
        <![endif]-->

    </head>

    <body  >
         <form id="form1" runat="server">
        <div class="page-container">
            <h1>登录(Login)</h1>
            <!--<form action="../页面/Default2.aspx?x=0&y=0" method="post" id="theform">-->
                <asp:TextBox ID="TextBox1" runat="server"   CssClass="username"  ></asp:TextBox>
               <!-- <input type="text" id="username" name="username" class="username" placeholder="Username！">!-->
                <asp:TextBox ID="TextBox2" runat="server" CssClass="password" TextMode="Password"></asp:TextBox>
                <!--<input type="password"  id="password" name="password" class="password" placeholder="password！">-->
                <asp:Button ID="Button1" runat="server" Text="Login"  CssClass="submit_button" OnClick="Button1_Click"   />
               <!-- <button type="submit" class="submit_button" onclick="doLogin()">Login</button> !-->
                <div class="error"><span>+</span></div>
         <!--   </form>-->
           
        </div>
		
        <!-- Javascript -->
        <script src="assets/js/jquery-1.8.2.min.js" ></script>
        <script src="assets/js/supersized.3.2.7.min.js" ></script>
        <script src="assets/js/supersized-init.js" ></script>
        <script src="assets/js/scripts.js" ></script>
             </form>
    </body>

</html>
