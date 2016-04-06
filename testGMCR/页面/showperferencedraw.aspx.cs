using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;

public partial class 页面_showperferencedraw : System.Web.UI.Page
{
    GMCR model = new GMCR();
    protected void Page_Load(object sender, EventArgs e)
    {
        

            Response.ClearContent();
            model = (GMCR)Session["model"];
            Label1.Visible = false;
           
                int dm = 0;
                int prefence_num = 0;
         try
            {
                if (Request.QueryString["dm"] != null && DropDownList1.Items.Count == 0) dm = int.Parse(Request.QueryString["dm"]);
                if (DropDownList1.Items.Count != 0)
                {
                    dm = int.Parse(DropDownList1.SelectedItem.Value);
                    //DropDownList1.SelectedItem.Selected = false;
                }
                if (DropDownList1.Items.Count == 0)
                {
                    for (int i = 0; i < model.get_DM_descrip().Length; i++)
                    {
                        ListItem item = new ListItem();
                        item.Text = model.get_DM_descrip()[i];
                        item.Value = i.ToString();
                        if (dm == i) item.Selected = true;
                        DropDownList1.Items.Add(item);
                    }

                }
                 prefence_num = model.get_prefence_num(dm);
            }
            catch (Exception exception)
            {
                Label1.Visible = true;
                Label1.Text = "No input information, please enter the information input interface"; 
                Label1.BackColor = Color.Gray;
            }
            try
            {
              CreateControl(dm);
              creatmartix(dm);
              
            }
            catch (Exception exception)
            {
             Label1.Visible = true;
             Label1.Text = "Decision maker" + model.get_DM_descrip()[dm] + "'preference not set";
             Label1.BackColor = Color.Gray;
            }
         
            if (((string [])model.init_statement[dm])[0] != "")
            {

                for (int preference_point = 0; preference_point < prefence_num; preference_point++)
                {
                    TableRow row = new TableRow();
                    TableCell cell = new TableCell();
                    drawing(preference_point, dm);
                    System.Web.UI.WebControls.Image photo = new System.Web.UI.WebControls.Image();
                    photo.ImageUrl = "~/image/perference" + preference_point + ".jpg";
                    cell.Controls.Add(photo);
                    row.Cells.Add(cell);
                    Table2.Rows.Add(row);
                }
                //  CreateControl();
            }
            else
            {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                cell.Text = "Option Prioritizing not set Preference tree unavailable";
                cell.CssClass = "label";
                row.Cells.Add(cell);
                Table2.Rows.Add(row);
            }

    }


    void creatmartix(int dm)
    {  

        for (int i = 0; i < model.get_feasible_state() + 1; i++)
        {
            TableRow row = new TableRow();
            for (int j = 0; j < model.get_feasible_state() + 1; j++)
            {
                TableCell cell = new TableCell();
                if (i == 0 && j == 0) cell.Text = "   ";
                else if (i == 0)
                {
                    int p = j - 1;
                    cell.Text = "S" +(p+1).ToString();
                }
                else if (j == 0)
                {
                    int p = i - 1;
                    cell.Text = "S" + (p + 1).ToString() + "   ";
                }
                else if (i >= j)
                {
                    if (model.get_prefenece_martix(dm)[i-1][j-1] == 0) cell.Text = "= ";
                    if (model.get_prefenece_martix(dm)[i-1][j-1] == 1) cell.Text = "> ";
                    if (model.get_prefenece_martix(dm)[i-1][j-1] == -1) cell.Text = "< ";
                    if (model.get_prefenece_martix(dm)[i-1][j-1] == 2) cell.Text = "U ";
                    if (model.get_prefenece_martix(dm)[i - 1][j - 1] ==10) cell.Text = ">> ";
                    if (model.get_prefenece_martix(dm)[i - 1][j - 1] == -10) cell.Text = "<< ";
                }
                else
                {
                    if (model.get_prefenece_martix(dm)[j-1][i-1] == 0) cell.Text = "= ";
                    if (model.get_prefenece_martix(dm)[j - 1][i - 1] == 1) cell.Text = "< ";
                    if (model.get_prefenece_martix(dm)[j - 1][i - 1] == -1) cell.Text = "> ";
                    if (model.get_prefenece_martix(dm)[j - 1][i - 1] == 2) cell.Text = "U ";
                    if (model.get_prefenece_martix(dm)[j - 1][i - 1] == -10) cell.Text = ">> ";
                    if (model.get_prefenece_martix(dm)[j - 1][i - 1] == 10) cell.Text = "<< ";
                }

                cell.CssClass = "cell";
                cell.Width = 40;
                row.Cells.Add(cell);
            }
           // row.BorderColor = Color.Aqua;
            //row.BorderWidth = 1;
            Table3.Rows.Add(row);
        }
    }

    void CreateControl(int dm)
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
        
        for (int x = 0; x < model.get_optionnum()+1; x++)
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
                    if (x ==model.get_optionnum())
                    {
                        cell.Text = "S"+(i+1).ToString();
                    }
                    row.Cells.Add(cell);
                    TableCell b_cell = new TableCell();
                    row.Cells.Add(b_cell);
                }
            
            this.Table1.Rows.Add(row);
        }
    }
     
    void drawing(int preference_point,int dm)
    {
        int[] qiangdu = (int[])model.perfenceqiangdu[dm];
        int m = model.get_preferencestament(dm)[preference_point].Length;                  //m表示stament的个数
        int eaqual_statenum = 2;
        int eaqual_statenumtemp = 2;
        for (int i = 0; i < model.get_perferencerank(dm)[preference_point].Length - 1; i++)
        {
            if (model.get_perferencerank(dm)[preference_point][i][0] == model.get_perferencerank(dm)[preference_point][i + 1][0])
            {
                eaqual_statenumtemp++;
            }
            else
            {
                if (eaqual_statenumtemp > eaqual_statenum)
                {
                    eaqual_statenum = eaqual_statenumtemp;
                }
                eaqual_statenumtemp = 1;
            }
        }
        int photoweight;
        if ((eaqual_statenum * 20 * Math.Pow(2, m) + 200) > 1300) photoweight = (int)(eaqual_statenum * 20 * Math.Pow(2, m) + 200);
        else photoweight = 1300;
        int weight;

        if (model.get_preferencestament(dm)[preference_point].Length < 3) weight = 200;
        else if (model.get_preferencestament(dm)[preference_point].Length < 3)
        { weight = 400; }
        else if (model.get_preferencestament(dm)[preference_point].Length < 4)
        {
            weight = 600;
        }
        else weight = 1000;
        if ((eaqual_statenum * 20 * Math.Pow(2, m))+200 > weight) weight = eaqual_statenum * 20 * (int)Math.Pow(2, m)+200;

        int initdistance = (int)((weight-200) / Math.Pow(2, m) );                   //x,y分别表示水平方向和垂直方向。
        if (eaqual_statenum * 20 > initdistance) initdistance = eaqual_statenum * 20;
        int distancex = initdistance;
        int distancey = initdistance;
        int inity=80+20*model.get_optionnum()+20*m;                        //将下面这么多地方空出来写state的信息
        int pohtolength = distancex;
        int lengthtemp = distancex;
        for (int i = 0; i < m-1; i++)
        {
            lengthtemp = (int)(lengthtemp * 1.2);
            pohtolength +=lengthtemp ;
        }
        pohtolength =pohtolength+50+ inity;

       // if (pohtolength < 700) pohtolength = 700;
        Bitmap bitmap = new Bitmap(photoweight,pohtolength);
        Graphics g = Graphics.FromImage(bitmap);
        Brush brush = new SolidBrush(Color.Blue);  //设置画刷颜
        Font font = new Font("宋体", 10);   //设置字体类型和大小
        SolidBrush sb = new SolidBrush(Color.Black);
       // g.FillEllipse(sb, 800, 300, 100, 100);
        Pen p = new Pen(Color.Black);
        //g.DrawLine(p, 850, 350, 250, 350);
        Font fonthead = new Font("宋体", 15);   //设置字体类型和大小
        g.DrawString("The " + (preference_point + 1) + " preference tree", fonthead, brush, 5, 20);
        int [][]pointx=new int [m+1][];                       //point 数组用于存储二叉的节点信息；  纬度表示层
        int  [][]pointy=new int [m+1][];

        for (int i = 0; i <m + 1; i++)                       
        {
            pointx[i] = new int[(int)Math.Pow(2, m-i)];
            pointy[i] = new int[(int)Math.Pow(2, m-i)];
        }

       
        for (int i = 0; i<m+1; i++)
        {
            for (int j = 0; j < pointx[i].Length; j++)
            {
                if (i == 0)
                {
                    pointy[i][j] = pohtolength - inity;
                    if (j == 0)
                    {
                        pointx[i][j] = 200;
                    }
                    else pointx[i][j] = pointx[i][j - 1] + distancex;
                }
                else if (i == 1)
                {
                    pointy[i][j] = pointy[i - 1][0] - distancey;
                    if (j == 0) pointx[i][j] = (pointx[i - 1][0] + pointx[i - 1][1]) / 2;
                    else pointx[i][j] = pointx[i][j - 1] + (pointx[i - 1][1] - pointx[i - 1][0])*2;
                }
                else
                {
                    pointy[i][j] = pointy[i-1][0]-(int)((pointy[i - 2][0] - pointy[i - 1][0]) * 1.2);
                    if (j == 0) pointx[i][j] = (pointx[i - 1][0] + pointx[i - 1][1]) / 2;
                    else pointx[i][j] = pointx[i][j - 1] + (pointx[i - 1][1] - pointx[i - 1][0])*2;
                }

                g.FillEllipse(sb, pointx[i][j], pointy[i][j], 10, 10);                 //画圆点

                if (i > 0)               //只要》0 就要画线
                {   //画线
                    g.DrawLine(p, pointx[i][j] + 5, pointy[i][j] + 10, pointx[i - 1][j*2]+5, pointy[i - 1][j*2]);
                    g.DrawLine(p, pointx[i][j] + 5, pointy[i][j] + 10, pointx[i - 1][j*2+1]+5, pointy[i - 1][j*2+1]);
                    //写t f
                    g.DrawString("T", font, brush, (pointx[i][j] + pointx[i - 1][j*2]) / 2-5, (pointy[i][j] + pointy[i - 1][j*2]) / 2-5);
                    g.DrawString("F", font, brush, (pointx[i][j] + pointx[i - 1][j*2+1]) / 2+5, (pointy[i][j] + pointy[i - 1][j*2+1]) / 2-5);
                    //写statement            
                }
           
            }
            //每层都写上stament
            if (i >=1)
            {
                string statement = model.get_preferencestament(dm)[preference_point][m - i];
                string[] has_M = model.get_initstatement(dm);
                if (has_M[m - i].IndexOf("&M", 0) != -1) statement += " &M";
                g.DrawString(statement, font, brush, 5, (pointy[i][0] + pointy[i - 1][0]) / 2);
            }
        }

        int opy = pohtolength - inity + 20;
        for(int i=0;i<model.get_optionnum();i++)
        {
            g.DrawString(model.get_option_descrip()[i], font, brush, 5, opy);
            opy+=20;
        }
        g.DrawString("states：", font, brush, 5, opy);
        opy += 20;
        
        p.DashStyle = DashStyle.Dot;
        g.DrawLine(p, 0, opy, photoweight, opy);
        g.DrawString("statements", font, brush, 5, opy+3);
        g.DrawString("weight", font, brush, 100, opy+3);
        g.DrawLine(p, 100, opy, 100, opy+(m+1)*20);
        opy += 20;
        for (int i = 0; i < model.get_preferencestament(dm)[preference_point].Length; i++)
        {
            g.DrawString(model.get_preferencestament(dm)[preference_point][i], font, brush, 5, opy);
            int pppg=m-1-i;
            string quanzhi = "2^" + pppg;
            if (qiangdu[i] != 0)
            {
                quanzhi += "+" + qiangdu[i] + "*2^" + m;
            }
            g.DrawLine(p, 0, opy-3, photoweight, opy-3);
            g.DrawString(quanzhi, font, brush, 100, opy);
            opy += 20;
        }
       // g.DrawString("阈值=2^"+m, font, brush, 5, opy+3);
        g.DrawString("scores=", font, brush, 100, opy+3);
        int[] flag = new int[(int)Math.Pow(2, m)];  //用于判断是否有=号
        for (int i = 0; i < model.get_feasible_state(); i++)
        {   
            int optionpoint=0;
            for (int j = 0; j < m; j++)
            {
                optionpoint += model.get_Option_Prioritizing(dm)[preference_point][i][j] * (int)Math.Pow(2, model.get_Option_Prioritizing(dm)[preference_point][0].Length - j - 1);
            }
            int optionpointx = pointx[0][(int)Math.Pow(2, m) - 1 - optionpoint]+ flag[(int)Math.Pow(2, m) - 1 - optionpoint]*20;
            int optionpointy = pointy[0][(int)Math.Pow(2, m) - 1 - optionpoint]+20;
            flag[(int)Math.Pow(2, m) - 1 - optionpoint]++;
            for (int j = 0; j < model.get_optionnum(); j++)
            {
                g.DrawString(model.get_state()[i][j].ToString(), font, brush, optionpointx, optionpointy);
                optionpointy += 20;
            }
            p.DashStyle = DashStyle.Solid;
            g.DrawEllipse(p, optionpointx, optionpointy, 17, 17);
            g.DrawString((i+1).ToString(), font, brush, optionpointx+1, optionpointy+2);
             optionpointy += 20;
             optionpointy += 20;
             int score = 0;
            for (int j = 0; j < m; j++)
            {
                int num = model.get_Option_Prioritizing(dm)[preference_point][i][j] * (int)(Math.Pow(2, m - j - 1) +model.get_Option_Prioritizing(dm)[preference_point][i][j]* qiangdu[j] * Math.Pow(2, m));
                score += num;
                g.DrawString(num.ToString(), font, brush, optionpointx , optionpointy );
                 optionpointy += 20;
                 if (i == 0)
                 {
                     p.DashStyle = DashStyle.Dot;
                     if (j == m - 1) p.DashStyle = DashStyle.Solid;
                     g.DrawLine(p, 0, opy, photoweight, opy);
                 }        
            }

          g.DrawString(score.ToString(), font, brush, optionpointx, optionpointy+3);
        }
        Response.ClearContent();
        bitmap.Save(get_relpath() + "perference" + preference_point.ToString() + ".jpg");    
    }

    string get_relpath()
    {
        string str = Request.MapPath("~/image/perference.jpg");
        int index = 0;
        int point = 0;
        while (true)
        {
            point = index;
            index = str.IndexOf("\\", index + 1);
            if (index == -1) break;
        }

        return str.Substring(0, point) + "\\";
    }
}
/*
        Bitmap bitmap = new Bitmap(1200, 700);
        Graphics g = Graphics.FromImage(bitmap);
        //g.Clear(Color.YellowGreen);
        Font font1 = new Font("宋体", 10);   //设置字体类型和大小
        Brush brush = new SolidBrush(Color.Blue);  //设置画刷颜色
        Pen myPen = new Pen(Color.Blue, 5);  //创建画笔对象
       // g.DrawString("GDI+绘制绘制阴影画笔", font1, brush, 5, 5);
        //g.DrawString("GDI+绘制绘制阴影画笔", font1, brush, 5, 20);
        HatchBrush myhatchBrush = new HatchBrush(HatchStyle.Weave, Color.Red);
        g.FillRectangle(myhatchBrush, 200, 300, 5, 5);
        SolidBrush sb = new SolidBrush(Color.Black);
        g.FillEllipse(sb, 800, 300, 10, 10);
        Pen p = new Pen(Color.Black);
        g.DrawLine(p, 850, 350, 250, 350);
        Response.ClearContent();
        bitmap.Save("C:\\Users\\xx\\Desktop\\web 项目\\GMCR2\\image\\perference.jpg");
*/