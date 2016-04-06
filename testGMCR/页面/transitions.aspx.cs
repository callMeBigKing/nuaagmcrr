using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class 页面_transitions : System.Web.UI.Page
{
    GMCR model = new GMCR();
    protected void Page_Load(object sender, EventArgs e)
    {
         try
        {  
            model = (GMCR)Session["model"];
            //text daimai  测试代码
          //  int[] sort = new int[]{0,4,2,6,1,5,3,7};
           
          //  model.AlterState(sort);
           // Session["model"] = this.model;
             /////
            Createtable1();
            Createtable2();
            if (Convert.ToInt32(ViewState["pattern"]) != 0)
            {
                for (int i = 0; i < Convert.ToInt32(ViewState["pattern"].ToString()); i++)
                {
                    add();
                }
            }
        }
         catch (Exception exp)
         {
             Label1.Text = "No input information, please enter the information input interface";
             Label2.Visible = false;
             Label3.Visible = false;
             Button1.Visible = false;
         }
    }

    void Createtable1()
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

            // 选择信息
            TableCell N = new TableCell();
            N.Text = "N";
            row.Cells.Add(N);
 

            TableCell drop_cell = new TableCell();
            DropDownList droplist = new DropDownList();

            ListItem two = new ListItem();
            two.Text = "<-->";
            two.Value = "2"; ListItem zero = new ListItem();
            zero.Text = "-->";
            zero.Value = "0";
            ListItem one = new ListItem();
            one.Text = "<--";
            one.Value = "1";

            droplist.Items.Add(two);
            droplist.Items.Add(zero);
            droplist.Items.Add(one);
           
            drop_cell.Controls.Add(droplist);
            row.Cells.Add(drop_cell);

            TableCell Y = new TableCell();
            Y.Text = "Y";
            row.Cells.Add(Y);

            this.Table1.Rows.Add(row);
        }
    }

    void Createtable2()
    { 
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

            // 选择信息
            TableCell drop_cell = new TableCell();
            DropDownList droplist = new DropDownList();
            ListItem Y = new ListItem();
            Y.Text = "Y";
            Y.Value = "1";
            ListItem N = new ListItem();
            N.Text = "N";
            N.Value = "0";
            ListItem un = new ListItem();
            un.Text = "-";
            un.Value = "2";
            droplist.Items.Add(Y);
            droplist.Items.Add(N);
            droplist.Items.Add(un);
            drop_cell.Controls.Add(droplist);
            row.Cells.Add(drop_cell);

            //加图片
            TableCell photo = new TableCell();
            if (x == model.get_optionnum() / 2)
            {
                Image image = new Image();
                image.ImageUrl = "~/image/jiantou.png";
                photo.Controls.Add(image);
            }
            row.Cells.Add(photo);
            //pattern信息
            TableCell drop_cell1 = new TableCell();
            DropDownList droplist1 = new DropDownList();
            ListItem Y1 = new ListItem();
            Y1.Text = "Y";
            Y1.Value = "1";
            ListItem N1 = new ListItem();
            N1.Text = "N";
            N1.Value = "0";
            ListItem un1 = new ListItem();
            un1.Text = "-";
            un1.Value = "2";
            droplist1.Items.Add(Y1);
            droplist1.Items.Add(N1);
            droplist1.Items.Add(un1);
            drop_cell1.Controls.Add(droplist1);
            row.Cells.Add(drop_cell1);
            //添加按钮
            TableCell cell_button = new TableCell();
            if (x == model.get_optionnum() / 2)
            {
                Button button = new Button();
                button.Text = "Add";
                button.Click += new EventHandler(button_Click);
                cell_button.Controls.Add(button);
            }
            row.Cells.Add(cell_button);

            this.Table2.Rows.Add(row);
        }
    
    }

    void add()              //增加一对pattern
    {
        for (int i = 0; i < model.get_optionnum(); i++)
        {
            TableCell cell1 = new TableCell();
            Label label = new Label();
            cell1.Controls.Add(label);
            this.Table2.Rows[i].Cells.Add(cell1);

            TableCell cell2 = new TableCell();
            if (i == model.get_optionnum() / 2)
            {
                Image image = new Image();
             //   image.ImageUrl = "~/image/C3%5W[)L]}BU6D`B@%Z%FX4.png";
                cell2.Controls.Add(image);
            }
            this.Table2.Rows[i].Cells.Add(cell2);

            TableCell cell3 = new TableCell();
            Label labe3 = new Label();
            cell3.Controls.Add(labe3);
            this.Table2.Rows[i].Cells.Add(cell3);
        }
    }

    void button_Click(object sender, EventArgs e)
    {
        add();
        ViewState["pattern"] = Convert.ToInt16(ViewState["pattern"]) + 1;
        for (int i = 0; i < model.get_optionnum(); i++)
        {
            Label label1 = (Label)this.Table2.Rows[i].Cells[Table2.Rows[i].Cells.Count - 3].Controls[0];
            Label label3 = (Label)this.Table2.Rows[i].Cells[Table2.Rows[i].Cells.Count - 1].Controls[0];
            if (i == model.get_optionnum() / 2)
            {
                Image image = (Image)this.Table2.Rows[i].Cells[Table2.Rows[i].Cells.Count - 2].Controls[0];
                image.ImageUrl = "~/image/jiantou.png";
            }
            DropDownList droplist1 = (DropDownList)this.Table2.Rows[i].Cells[2].Controls[0];
            DropDownList droplist3 = (DropDownList)this.Table2.Rows[i].Cells[4].Controls[0];
            label1.Text = droplist1.SelectedItem.Text;
            label3.Text = droplist3.SelectedItem.Text;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int []single = new int[model.get_optionnum()];
        int[][] pattern1;
        int[][] pattern2;
        int count = (Table2.Rows[1].Cells.Count - 6) / 3;             //用于统计pattern的个数
        pattern1 = new int[count][];
        pattern2 = new int[count][];

            for (int i = 0; i < count; i++)
            {
                pattern1[i] = new int[model.get_optionnum()];
                pattern2[i] = new int[model.get_optionnum()];
            }

            for (int i = 0; i < model.get_optionnum(); i++)
        {    
            DropDownList droplist1 = (DropDownList)this.Table1.Rows[i].Cells[3].Controls[0];
            single[i] =int.Parse(droplist1.SelectedItem.Value);
        }
            for (int i = 0; i < pattern1.Length; i++)
            {
                for (int j = 0; j < pattern1[i].Length; j++)
                {
                    int countpoint = 3 * i + 6;
                    Label label1 = (Label)this.Table2.Rows[j].Cells[countpoint].Controls[0];
                    Label label3 = (Label)this.Table2.Rows[j].Cells[countpoint+2].Controls[0];
                    if (label1.Text == "Y") pattern1[i][j] = 1;
                    else if (label1.Text == "N") pattern1[i][j] = 0;
                    else if (label1.Text == "-") pattern1[i][j] = 2;
                    if (label3.Text == "Y") pattern2[i][j] = 1;
                    else if (label3.Text == "N") pattern2[i][j] = 0;
                    else if (label3.Text == "-") pattern2[i][j] = 2;

                }
            }

        model.set_transitions(single, pattern1, pattern2);
        Session["model"] = this.model;
        Response.Write("<script>alert('设置成功')</script>");
        Response.Redirect("showtranslatemartix.aspx");
        

        


    }
}