using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class 页面_Option_Prioritizing : System.Web.UI.Page
{
    GMCR model = new GMCR();
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Visible = false;
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
        catch (Exception excep)
        {
            Label1.Text = "No input information, please enter the information input interface";
            Label1.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            Label4.Visible = false;
            Label5.Visible = false;
            btnCallBack.Visible = false;

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
            bt1.Width = 100;
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
            bt2.Width = 100;
            bt2.Text =model.get_option_descrip()[ x] ;
            cell2.Controls.Add(bt2);
            row.Cells.Add(cell2);

            //按钮
            TableCell cell_button = new TableCell();

            if (x == (model.get_optionnum() / 2) - 1)
            {
                TextBox texbox1 = new TextBox();

                DropDownList list = new DropDownList();
                for (int i = 0; i < model.get_DM_descrip().Length; i++)
                {
                    ListItem dm = new ListItem();

                    dm.Text = model.get_DM_descrip()[i];
                    dm.Value = i.ToString();                //注意value 是string 类型。
                    list.Items.Add(dm);
                }

                TextBox texbox2 = new TextBox();


                cell_button.Controls.Add(texbox1);
                cell_button.Controls.Add(list);
                //cell_button.Controls.Add(texbox2);

            }
            if (x == model.get_optionnum() / 2)
            {
                Button button = new Button();
                button.Text = "Add";
                button.Click += new EventHandler(button_Click);
                cell_button.Controls.Add(button);                  
            }
            row.Cells.Add(cell_button);
            this.Table1.Rows.Add(row);
        }
    }

    void button_Click(object sender, EventArgs e)
    {
            add();
            ViewState["delet1"] = Convert.ToInt16(ViewState["delet1"]) + 1;

            Label label1 = (Label)this.Table2.Rows[this.Table2.Rows.Count-1].Cells[0].Controls[0];
            
            TextBox text1 = (TextBox)this.Table1.Rows[(model.get_optionnum() / 2) - 1].Cells[2].Controls[0];
            
            label1.Text = text1.Text;
            label1.Text += "  ";

            text1.Text = "";
            text1.Focus();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
     
        
        
    }

    protected void btnCallBack_Click(object sender, EventArgs e)
    {
        DropDownList listt = (DropDownList)this.Table1.Rows[(model.get_optionnum() / 2) - 1].Cells[2].Controls[1];
        int dm = int.Parse(listt.SelectedItem.Value);
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





    void addperfence()
    {
        DropDownList listt = (DropDownList)this.Table1.Rows[(model.get_optionnum() / 2) - 1].Cells[2].Controls[1];
        int dm = int.Parse(listt.SelectedItem.Value);
        try
        {
         
            int initstatement_count = 0;
            for (int i = 0; i < Table2.Rows.Count; i++)
            {
                if (Table2.Rows[i].Visible == true) initstatement_count++;
            }
            string[] initstatement = new string[initstatement_count];
            int initstatement_point = 0;   //之歌位置指针。
            for (int i = 0; i < Table2.Rows.Count; i++)
            {
                if (Table2.Rows[i].Visible == true)
                {
                    Label text1 = (Label)this.Table2.Rows[i].Cells[0].Controls[0];
                    initstatement[initstatement_point] = text1.Text.Trim();
                    initstatement_point++;
                }
            }


                model.trans_preferencestatement(dm, initstatement);

                Session["model"] = this.model;
                Response.Write("<script>alert('偏好设置完成')</script>");
                Response.Redirect("showperferencedraw.aspx?dm=" + dm);

        }
        catch (Exception exception)
        {
            Label1.Visible = true;
            Label1.Text = "The input format is incorrect";
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
}