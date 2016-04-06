using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class 页面_delet4aspx : System.Web.UI.Page
{

    GMCR model = new GMCR();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            creat();
        }

        catch (Exception excep)
        {
            Label1.Text = "No input information, please enter the information input interface";
            Label2.Visible = false;
            Button1.Visible = false;
        }
    }

    void creat()
    {
        
         model= (GMCR)Session["model"];
        for (int i = 0; i < model.get_feasible_state(); i++)
        {
            string item_text=string.Empty;
            for(int j=0;j<model.get_optionnum();j++)
            {
               item_text+=model.get_state()[i][j].ToString()+"  ";
            }
            ListItem item = new ListItem();
            item.Text = item_text;
            item.Value = i.ToString();
            this.CheckBoxList1.Items.Add(item);
        }
      
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        
        int count = 0;            //count 记录要删除的个数
        for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
        {
            if (this.CheckBoxList1.Items[i].Selected)
            {
                count++;
            }       
        }
        int[] delet = new int[count];
        int  pointer=0;
        for (int i = 0; i < this.CheckBoxList1.Items.Count; i++)
        {
            if (this.CheckBoxList1.Items[i].Selected)
            {
                delet[pointer] = i;
                pointer++;        
            }
            if (pointer >= count) break;
        }

        model.delet_manual(delet);
        Session["model"] = model;
        Response.Redirect("showstate.aspx");
       
    }
}