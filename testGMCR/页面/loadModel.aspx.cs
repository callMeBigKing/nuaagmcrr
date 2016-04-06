using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class 页面_loadModel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ModelDb modelDb = new ModelDb();
        int userid = (int)Session["id"];
        ArrayList list=modelDb.QueryModel(userid);
        for(int i=0;i<list.Count;i++)
        {
            string []str=(string [])list[i];
            //0 shi name  1  是id   0 text 1 value
            ListItem item=new ListItem();
            item.Text = str[0];
            item.Value = str[1];
            this.RadioButtonList1.Items.Add(item);
        }
        if (list.Count == 0)
        {
            this.Label1.Text = "暂无可使用的模型";
            this.Button1.Visible = false;
        }

       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ModelDb modelDb = new ModelDb();
        int modelID = int.Parse(this.RadioButtonList1.SelectedValue);
        GMCR model = new GMCR(modelDb.GetModel(modelID));
        Session["model"] = model;
        Response.Write("<script>alert('Load successfully')</script>");
        Response.Redirect("showstate.aspx");
    }
}