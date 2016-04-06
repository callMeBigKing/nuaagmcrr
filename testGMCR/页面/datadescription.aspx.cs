using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class 页面_datadescription : System.Web.UI.Page
{

  

    protected void Page_Load(object sender, EventArgs e)
    {


            CreateControl();

       
    }



    void CreateControl()
    {
        ///文本框
        /// 

  
        GMCR model = new GMCR();
        model = (GMCR)Session["model"];
        int flag=0;
        int []jugedm=new int[model.get_option().Length];
        for(int i=0;i<model.get_option().Length;i++)
        {
            if (i == 0)
            { jugedm[i] = 0; }
            else { jugedm[i] = jugedm[i - 1] + model.get_option()[i - 1]; }
           
        }
        for (int x = 0; x < model.get_optionnum(); x++)
        {
            TableRow row = new TableRow();

                TableCell cell1 = new TableCell();
                TextBox bt1 = new TextBox();
                bt1.Text = "DM" + (flag+1);
                if (x == jugedm[flag])
                {
                    cell1.Controls.Add(bt1);
                    if (flag < jugedm.Length - 1) flag++;
                }           
                row.Cells.Add(cell1);

                TableCell cell2 = new TableCell();
                TextBox bt2 = new TextBox();
                bt2.Text = "op" +(x+1);
                cell2.Controls.Add(bt2);
                row.Cells.Add(cell2);
            

            HolderTable.Rows.Add(row);
        }

        //Response.Write(model.get_option().Length);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GMCR model = new GMCR();
        model = (GMCR)Session["model"];
        string[] option_descrip=new string[model.get_optionnum()];
        string[] DM_descrip=new string[model.get_option().Length];
        int flag=0;
        int []jugedm=new int[model.get_option().Length];
        for(int i=0;i<model.get_option().Length;i++)
        {
            if (i == 0)
            { jugedm[i] = 0; }
            else { jugedm[i] = jugedm[i - 1] + model.get_option()[i - 1]; }
           
        }
       
        for (int x = 0; x < model.get_optionnum(); x++)
        {
            TableRow row = HolderTable.Rows[x];

            if (x == jugedm[flag])
            {
                TextBox bt1 = (TextBox)row.Cells[0].Controls[0];
                DM_descrip[flag] = bt1.Text;
                if (flag < jugedm.Length - 1) flag++;
               
            }

            TextBox bt2 = (TextBox)row.Cells[1].Controls[0];
            option_descrip[x] = bt2.Text;
        }
        model.set_descrip(option_descrip, DM_descrip);
        Session["model"] = model;
        Response.Write("<script>alert('data import successful')</script>");
       
            
    }

}