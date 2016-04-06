using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    GMCR model = new GMCR();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["model"] = model;

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string name = this.TextBox1.Text.Trim();
        string password = this.TextBox2.Text.Trim();
        DBTemp db = new DBTemp();
        string sql = "select * from users where name='" + name + "'"+"and password='" + password+"'";
        int Result=db.QueryId(sql);
        if (Result != -1)
        {  //成功匹配到
            Session["id"] = Result;
            Response.Redirect("~/页面/shuru.aspx");
        } 
        else
        {
            Response.Write("<script>alert('用户名或者密码错误')</script>");
        }
    }
}