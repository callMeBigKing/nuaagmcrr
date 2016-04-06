using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
public partial class 页面_showtranslatemartix : System.Web.UI.Page
{
    GMCR model = new GMCR();
    protected void Page_Load(object sender,EventArgs e)
    {
        
        model = (GMCR)Session["model"];
         try
            {
            CreateControl();
            }
         catch (Exception exception)
         {
             Label1.Text = "还没有输入决策者信息，请到输入界面输入";
             
         }
            try
            {
             creat_tans_martix();
/*
            for (int i = 0; i < model.get_feasible_state(); i++)
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                cell.Text = "状态" + i + ":  ";
                for (int j = 0; j < model.get_optionnum(); j++)
                {
                    cell.Text += model.get_state()[i][j] + "  ";
                }
                row.Cells.Add(cell);
                Table1.Rows.Add(row);
            }
            TableRow rowheater = new TableRow();
            TableCell cellheater = new TableCell();
            cellheater.Text = "设置状态转移后:";
            rowheater.Cells.Add(cellheater);
            Table1.Rows.Add(rowheater);

            int[][][] transitions = model.get_transitions();
            for (int i = 0; i < transitions.Length; i++)
            {
                for (int j = 0; j < transitions[i].Length; j++)
                {
                    TableRow row = new TableRow();
                    TableCell cell = new TableCell();
                    cell.Text = "DM_" + i + " 的状态" + j + "可以转移到：";
                    for (int k = 0; k < transitions[i][j].Length; k++)
                    {
                        if (transitions[i][j][k] == -1) break;
                        cell.Text += transitions[i][j][k] + "  ";
                    }
                    row.Cells.Add(cell);
                    Table1.Rows.Add(row);
                }
            }
 */
        }
        catch (Exception exception)
        {
            Label1.Text = "State transition has not been set";
            Label1.BackColor = Color.Gray;
        }
    }

    void creat_tans_martix()
    {   
        int dm_num=model.get_option().Length;
        for (int dm = 0; dm < dm_num; dm++)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            TableCell cell1 = new TableCell();
            cell1.Text = model.get_DM_descrip()[dm] + "'s transition martix";
            Table table=new Table();
            table.CssClass = "table";
            creat_dmtans_martix(dm, table);
            cell.Controls.Add(table);
            row.Cells.Add(cell1);
            row.Cells.Add(cell);
            Table1.Rows.Add(row);
        }

        
    }
    //算出dm给出状态转移矩阵。
   void  creat_dmtans_martix(int dm ,Table table)
    {
        int[][] dmtrans_martix = model.get_trans_marix(dm);
        for (int i = 0; i < model.get_feasible_state() + 1; i++)
        {
            TableRow row = new TableRow();
            for (int j = 0; j < model.get_feasible_state() + 1; j++)
            {
                TableCell cell = new TableCell();
                cell.CssClass = "cell";
                if (i == 0 && j == 0) cell.Text = "   ";
                else if (i == 0)
                {
                    int p = j - 1;
                    cell.Text = "S" + (p+1).ToString();
                }
                else if (j == 0)
                {
                    int p = i - 1;
                    cell.Text = "S" + (p + 1).ToString() + "   ";
                }
             
                else
                {
                    cell.Text = dmtrans_martix[i - 1][j - 1].ToString();
                }

                cell.CssClass = "cell";
                cell.Width = 40;
                row.Cells.Add(cell);
            }
            // row.BorderColor = Color.Aqua;
            //row.BorderWidth = 1;
            table.Rows.Add(row);
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

           this.Table2.Rows.Add(row);
       }
   }
}