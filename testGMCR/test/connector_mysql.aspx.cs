using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;

public partial class test_connector_mysql : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //点击button显示数据库内容

        String mysqlcon = "database=qdm128525373_db;Password=xdacy123;User ID=qdm128525373;server=qdm128525373.my3w.com:3306";//Data Source=MySQL;;charset=utf8";
        MySqlConnection conn = new MySqlConnection(mysqlcon);
        MySqlDataAdapter mdap = new MySqlDataAdapter("select * from people", conn);
        DataSet dsall = new DataSet();
        
        int b = mdap.Fill(dsall, "xiaodong");

        int i = 1;


    }

}