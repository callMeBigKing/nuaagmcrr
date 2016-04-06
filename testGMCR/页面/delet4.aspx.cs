using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
public partial class 页面_delet4 : System.Web.UI.Page
{
    GMCR model = new GMCR();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CreateControl();
            if (Convert.ToInt32(ViewState["delet1"]) != 0)
            {
                for (int i = 0; i < Convert.ToInt32(ViewState["delet1"].ToString()); i++)
                {
                    add();
                }
            }
        }
        catch (Exception exp)
        {
            Label1.Text = "No input information, please enter the information input interface";
            Label2.Visible =false;
            Button1.Visible = false;
        }
    }

    void add()              //增加一个delet
    {
        TableRow row = new TableRow();
        TableCell cell = new TableCell();
        Label label = new Label();
        cell.Controls.Add(label);
        row.Cells.Add(cell);
        this.Table2.Rows.Add(row);

    }

    void CreateControl()
    {
        ///文本框
        ///  
        model = (GMCR)Session["model"];
        int flag = 0;
        int[] jugedm = new int[model.get_option().Length];
        for (int i = 0; i < model.get_option().Length; i++)
        {
            if (i == 0)
            { jugedm[i] = 0; }
            else { jugedm[i] = jugedm[i - 1] + model.get_option()[i - 1]; }

        }
        for (int x = 0; x < model.get_optionnum(); x++)
        {
            TableRow row = new TableRow();

            // DM 信息
            TableCell cell1 = new TableCell();
            TextBox bt1 = new TextBox();
            bt1.Text = model.get_DM_descrip()[flag];
            if (x == jugedm[flag])
            {
                cell1.Controls.Add(bt1);
                if (flag < jugedm.Length - 1) flag++;
            }
            row.Cells.Add(cell1);

            // option信息
            TableCell cell2 = new TableCell();
            TextBox bt2 = new TextBox();
            bt2.Text = model.get_option_descrip()[x];
            cell2.Controls.Add(bt2);
            row.Cells.Add(cell2);



            //按钮
            TableCell cell_button = new TableCell();
            Button button = new Button();
            TextBox texbox = new TextBox();
            button.Text = "Add";
            button.Click += new EventHandler(button_Click);
            if (x == (model.get_optionnum() / 2) - 1) cell_button.Controls.Add(texbox);
            if (x == model.get_optionnum() / 2) cell_button.Controls.Add(button);
            row.Cells.Add(cell_button);
            this.Table1.Rows.Add(row);
        }
    }

    void button_Click(object sender, EventArgs e)
    {
        add();
        ViewState["delet1"] = Convert.ToInt16(ViewState["delet1"]) + 1;

        Label label = (Label)this.Table2.Rows[this.Table2.Rows.Count - 1].Cells[0].Controls[0];
        TextBox text1 = (TextBox)this.Table1.Rows[(model.get_optionnum() / 2) - 1].Cells[2].Controls[0];
        label.Text = text1.Text;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string[] statement = new string[Table2.Rows.Count];
            for (int i = 0; i < statement.Length; i++)
            {
                Label label = (Label)this.Table2.Rows[i].Cells[0].Controls[0];
                statement[i] = label.Text;
            }
            model.delet_state4(statement);
            Session["model"] = this.model;
            Response.Write("<script>alert('Deleted successfully')</script>");
            Response.Redirect("showstate.aspx");
        }
        catch (Exception exception)
        {
            Label1.Text = "The input format is incorrect";
            Label1.BackColor = Color.Gray;
            

        }
    }

}