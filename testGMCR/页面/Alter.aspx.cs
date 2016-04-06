using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class 页面_Alter : System.Web.UI.Page
{
    GMCR model = new GMCR();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.model = (GMCR)Session["model"];
        creat();
    }
   private void  creat()
    {
        
        for (int i = 0; i < model.get_feasible_state(); i++)
        {
            TableRow row = new TableRow();
            TableCell cell1 = new TableCell();
            TableCell cell2 = new TableCell();
            
            string cell1_text = string.Empty;
            for (int j = 0; j < model.get_optionnum(); j++)
            {
                cell1_text += model.get_state()[i][j].ToString() + "  ";
            }

            TextBox text = new TextBox();
            text.Width=30;
            cell1.Text = cell1_text;
            cell2.Controls.Add(text);
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            this.Table1.Rows.Add(row);
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int[] sort = new int[model.get_feasible_state()];
        for (int i = 0; i < this.Table1.Rows.Count; i++)
        {
            TextBox text = (TextBox)Table1.Rows[i].Cells[1].Controls[0];
            sort[i] = int.Parse(text.Text.Trim())-1;
        }
        model.AlterState(sort);
        
        Session["model"] = model;
        Response.Write("<script>alert('state alter successful')</script>");

    }
}