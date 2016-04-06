using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
public partial class 页面_manualperfence : System.Web.UI.Page
{
    GMCR model = new GMCR();
    protected void Page_Load(object sender, EventArgs e)
    {
        model = (GMCR)Session["model"];
        try
        {
            if (DropDownList1.Items.Count == 0)
            {
                for (int i = 0; i < model.get_DM_descrip().Length; i++)
                {
                    ListItem item = new ListItem();
                    item.Text = model.get_DM_descrip()[i];
                    item.Value = i.ToString();

                    DropDownList1.Items.Add(item);
                }
            }
      

        CreateControl();
        }
        catch (Exception excep)
        {
            Label1.Text = "No input information, please enter the information input interface";
            btnCallBack.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            DropDownList1.Visible = false;
        }
        creatmartix();
    }

    void creatmartix()
    {
        TableRow rowhead = new TableRow();
        for (int i = 0; i < model.get_feasible_state()+1; i++)
        {
            TableCell cell = new TableCell();
            if (i > 0) cell.Text = "S" + (i).ToString();
            
            cell.CssClass = "cell";
            rowhead.Cells.Add(cell);
        }
        Table2.Rows.Add(rowhead);
            for (int i = 0; i < model.get_feasible_state(); i++)
            {
                TableRow row = new TableRow();
                for (int j = 0; j <= i + 1; j++)
                {
                    TableCell cell = new TableCell();

                    if (j == 0)
                    {
                        int p = i+1;
                        cell.Text = "S" + p;
                        cell.Width = 30;
                    }
                    else
                    {
                        if (i + 1 != j)
                        {
                            TextBox perfence = new TextBox();
                            perfence.Width = 30;
                            cell.Controls.Add(perfence);
                        }
                        else
                        {
                            Label lable = new Label();
                            lable.Text = "=";
                            lable.Width = 30;
                            cell.Controls.Add(lable);
                        }
                    }


                    cell.CssClass = "cell";
                    row.Cells.Add(cell);
                }

                Table2.Rows.Add(row);
            }
    }
    void CreateControl()
    {
        ///文本框
        ///        
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







    protected void btnCallBack_Click(object sender, EventArgs e)
    {
        int dm = int.Parse(DropDownList1.SelectedValue);
        string juge = model.perfence_martix[dm].ToString();
        if (juge != dm.ToString())
        {
            string js = string.Format("document.getElementById('{0}').value=confirm('Do you want to overwrite the existing preference?');document.getElementById('{1}').click();", hid.ClientID, btnHid.ClientID);
            ClientScript.RegisterStartupScript(GetType(), "confirm", js, true);
        }
        else
        {
            addperfence();
        }

    }

    protected void btnHid_Click(object sender, EventArgs e)
    {
        string result = hid.Value.ToLower() == "true" ? "是" : "否";
        
        if (result == "是")
        {
            addperfence();
        }
        else
        {

            return;
        }
    }

   void addperfence()
    {
        int state_num = model.get_feasible_state();
        int dm = int.Parse(DropDownList1.SelectedValue);

        int[][] perfence_martix = new int[state_num][];
        int flage = 0;
        int errori = -1;
        int errorj = -1;
        for (int i = 0; i < state_num; i++)
        {
            perfence_martix[i] = new int[i + 1];
            for (int j = 0; j <= i; j++)
            {
                if (i != j)
                {
                    TextBox text = (TextBox)Table2.Rows[i + 1].Cells[j + 1].Controls[0];
                    if (text.Text.Trim() == ">") perfence_martix[i][j] = 1;
                    else if (text.Text.Trim() == "<") perfence_martix[i][j] = -1;
                    else if (text.Text.Trim() == "=") perfence_martix[i][j] = 0;
                    else if (text.Text.Trim() == ">>") perfence_martix[i][j] = 10;
                    else if (text.Text.Trim() == "<<") perfence_martix[i][j] = -10;
                    else if (text.Text.Trim() == "U") perfence_martix[i][j] = 2;
                    else
                    {
                        flage = 1; errori = i+1; errorj = j+1;
                        break;
                    }
                }
                else
                {
                    perfence_martix[i][j] = 0;
                }

            }
        }
        if (flage == 0)
        {
            model.set_perfencemanual(perfence_martix, dm);
            Session["model"] = model;
            Response.Write("<script>alert('Enter the success preferences')</script>");
        }
        else Response.Write("<script>alert('state S" + errori + "state S" + errorj + "'perfence error" + "')</script>");
    }
}