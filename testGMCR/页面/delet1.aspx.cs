using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class 页面_delet1 : System.Web.UI.Page
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
            Label2.Visible = false;
            Button1.Visible = false;
        }
    }

    void add()              //增加一个delet
    {
        for (int i = 0; i <model.get_optionnum(); i++)
        {
            TableCell cell = new TableCell();
            CheckBox check=(CheckBox)this.Table1.Rows[i].Cells[2].Controls[0];
            Label label = new Label();

            cell.Controls.Add(label);

            this.Table1.Rows[i].Cells.Add(cell);            
        }
/*
        TableCell deletcell = new TableCell();
        Button deletbutton = new Button();
        deletbutton.Text = "删除";
        if (Convert.ToInt32(ViewState["delet1"]) != 0) deletbutton.ID = ViewState["delet1"].ToString();
        else deletbutton.ID = "0";
        deletbutton.Click += new System.EventHandler(deletbutton_Click);
        deletcell.Controls.Add(deletbutton);
        this.Table1.Rows[model.get_optionnum()].Cells.Add(deletcell);*/

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
        for (int x = 0; x <model.get_optionnum(); x++)
        {
            TableRow row = new TableRow();
            
            // DM 信息
            TableCell cell1 = new TableCell();
            TextBox bt1 = new TextBox();
            bt1.Text =model.get_DM_descrip()[ flag] ;
            if (x == jugedm[flag])
            {
                cell1.Controls.Add(bt1);
                if (flag < jugedm.Length - 1) flag++;
            }
            row.Cells.Add(cell1);

            // option信息
            TableCell cell2 = new TableCell();
            TextBox bt2 = new TextBox();
            bt2.Text = model.get_option_descrip()[ x];
            cell2.Controls.Add(bt2);
            row.Cells.Add(cell2);

            // 选择信息
            TableCell cell_check = new TableCell();
            CheckBox check = new CheckBox();
            cell_check.Controls.Add(check);
            row.Cells.Add(cell_check);

            //按钮
            TableCell cell_button = new TableCell();
            Button button=new Button();
            button.Text="Add";
            button.Click += new EventHandler(button_Click);
            if(x==model.get_optionnum()/2)cell_button.Controls.Add(button);
            row.Cells.Add(cell_button);
            
            this.Table1.Rows.Add(row);
        }
     /*   TableRow lastrow = new TableRow();
        TableCell newcell = new TableCell();
        lastrow.Cells.Add(newcell); lastrow.Cells.Add(newcell); lastrow.Cells.Add(newcell); lastrow.Cells.Add(newcell);
        Table1.Rows.Add(lastrow);*/

        
    }
        void button_Click(object sender, EventArgs e)
        {
            add();
            ViewState["delet1"] = Convert.ToInt16(ViewState["delet1"]) + 1;
            for (int i = 0; i < model.get_optionnum(); i++)
            {
                Label label =(Label) this.Table1.Rows[i].Cells[Table1.Rows[i].Cells.Count - 1].Controls[0];
                CheckBox check=(CheckBox)this.Table1.Rows[i].Cells[2].Controls[0];
                if (check.Checked == true) label.Text = "1";
                else label.Text = "0";
                check.Checked = false;
            }

        }

        //Response.Write(model.get_option().Length);

        protected void Button1_Click(object sender, EventArgs e)
        { 
            int row=Table1.Rows[0].Cells.Count-4;
            int col=model.get_optionnum();        
            int [][]delet1_matrix=new int [row][];
            for(int i=0;i<row;i++)
            {
                delet1_matrix[i] = new int[col];
            }
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                { 
                    Label label=(Label)this.Table1.Rows[j].Cells[i+4].Controls[0];
                    delet1_matrix[i][j] = Convert.ToInt16(label.Text);
                
                }
            }

            model.delet_state1(delet1_matrix);
            Session["model"] = this.model;
            Response.Write("<script>alert('Deleted successfully')</script>");
            Response.Redirect("showstate.aspx");
            
        }


}