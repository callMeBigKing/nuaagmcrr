using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
public partial class 页面_showstate : System.Web.UI.Page
{
    GMCR model = new GMCR();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
             CreateControl();
        }
        catch (Exception exception)
        {
            Label1.Text = "还没有输入决策者信息，请到输入界面输入";
            Label1.BackColor = Color.Gray;
        }
        
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

        for (int x = 0; x < model.get_optionnum() + 1; x++)
        {
            TableRow row = new TableRow();

            // DM 信息

            TableCell cell1 = new TableCell();
            if (x < model.get_optionnum())
            {
                TextBox bt1 = new TextBox();
                bt1.Text = model.get_DM_descrip()[flag];
                if (x == jugedm[flag])
                {
                    cell1.Controls.Add(bt1);
                    if (flag < jugedm.Length - 1) flag++;
                }
            }
            row.Cells.Add(cell1);

            // option信息
            TableCell cell2 = new TableCell();
            TextBox bt2 = new TextBox();
            if (x < model.get_optionnum())
            {
                bt2.Text = model.get_option_descrip()[x];
                cell2.Controls.Add(bt2);
            }
            row.Cells.Add(cell2);
            //空的单元格
            TableCell black_cell = new TableCell();
            row.Cells.Add(black_cell);

            for (int i = 0; i < model.get_feasible_state(); i++)
            {
                TableCell cell = new TableCell();
                if (x < model.get_optionnum())
                {
                    cell.Text = model.get_state()[i][x].ToString();
                }
                if (x == model.get_optionnum())
                {
                    cell.Text = "S" + (i + 1).ToString();
                }
                row.Cells.Add(cell);
                TableCell b_cell = new TableCell();
                row.Cells.Add(b_cell);
            }

            this.Table1.Rows.Add(row);
        }
    }
}