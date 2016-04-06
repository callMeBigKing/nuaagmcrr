using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class 页面_show_preference : System.Web.UI.Page
{
    GMCR model = new GMCR();
    protected void Page_Load(object sender, EventArgs e)
    {
       // CreateControl();
    }

/*
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
            //空的单元格
            TableCell black_cell = new TableCell();
            row.Cells.Add(black_cell);

            for (int i = 0; i <model.get_feasible_state(); i++)
            {
                TableCell cell = new TableCell();
                cell.Text = model.get_state()[model.get_perferencerank()[i][1]][x].ToString();
                row.Cells.Add(cell);
                TableCell b_cell = new TableCell();
                if (x == model.get_optionnum() / 2)             // 添加大于号或者等与号
                {  
                    if (i == model.get_feasible_state() - 1)
                    { 
                       goto stop;
                    }

                    int sign = model.get_perferencerank()[i][0] - model.get_perferencerank()[i + 1][0];
                   if (sign>=Math.Pow(2,model.get_preferencestament().Length))
                        b_cell.Text = ">>";
                    else if (sign==0)
                        b_cell.Text = "=";
                   else b_cell.Text = ">";
                }

                stop:;
                row.Cells.Add(b_cell);
            }

           this.Table1.Rows.Add(row);
        }
    }

    */
}