using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class 页面_ManualP : System.Web.UI.Page
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
            if (Convert.ToInt32(ViewState["delet1"]) != 0)
            {
                for (int i = 0; i < Convert.ToInt32(ViewState["delet1"].ToString()); i++)
                {
                    add();
                }
            }
        }
        catch (Exception excep)
        {
            Label1.Text = "No input information, please enter the information input interface";
            btnCallBack.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            DropDownList1.Visible = false;
        }
        
    }


    void add()              //增加一个delet
    {
        TableRow row = new TableRow();
        TableCell cell = new TableCell();
        Label label1 = new Label();

        Button btn = new Button();
        btn.ID = "bt" + Table2.Rows.Count;
        btn.Text = "-";
        btn.Width = 28;
        btn.CommandArgument = Table2.Rows.Count.ToString();
        btn.Click += new System.EventHandler(bt_Click);

        cell.Controls.Add(label1);
        cell.Controls.Add(btn);

        row.Cells.Add(cell);
        this.Table2.Rows.Add(row);

    }

    private void bt_Click(object sender, System.EventArgs e)
    {
        Button btn = (Button)sender;
        Table2.Rows[Convert.ToInt16(btn.CommandArgument)].Visible = false;
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
    

    //需要各处一个判断算法
    void addperfence()
    {
        int feasible_state = model.get_feasible_state();
        int dm = int.Parse(DropDownList1.SelectedValue);

        ArrayList paixulist = new ArrayList();
        int num = 0;
        for (int i = 0; i < Table2.Rows.Count; i++)
        {
            if (Table2.Rows[i].Visible == true)
            {
                paixulist.Add(((Label)Table2.Rows[i].Cells[0].Controls[0]).Text);
                num++;
            }
        }
        string[] paixu = new string[num];
        for (int i=0;i< num;i++)
        {
            paixu[i] = paixulist[i].ToString();
        }

        try
        {
            int[][] perfence_martixsanjiao = manualperfence.calculate_martix(paixu, feasible_state);
            model.model.manualPerfence[dm] = paixu;
            model.set_perfencemanual(perfence_martixsanjiao, dm);
            Session["model"] = model;
            Response.Write("<script>alert('Enter the success preferences')</script>");
        }
        catch
        {
            Response.Write("<script>alert('input massage error ')</script>");
        }
}



    protected void Button1_Click1(object sender, EventArgs e)
    {

        add();
        ViewState["delet1"] = Convert.ToInt16(ViewState["delet1"]) + 1;

        Label label1 = (Label)this.Table2.Rows[this.Table2.Rows.Count - 1].Cells[0].Controls[0];
        label1.Text = TextBox1.Text;
        label1.Text += "  ";
        TextBox1.Text = "";
        TextBox1.Focus();

    }
}