using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class 页面_storeModel : System.Web.UI.Page
{
    GMCR model = new GMCR();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        model = (GMCR)Session["model"];
        string name = this.TextBox1.Text.ToString();
        int id = (int)Session["id"];//userid
        ModelDb modelDb = new ModelDb();
        //////
         Boolean result= modelDb.StoreModel(model.model,id,name);
        if (result) Label1.Text = "保存成功";
        else Label1.Text = "保存失败";
    }
}