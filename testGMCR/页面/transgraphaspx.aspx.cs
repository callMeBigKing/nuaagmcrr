using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
public partial class 页面_transgraphaspx : System.Web.UI.Page
{
    GMCR model = new GMCR();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            draw();
            System.Web.UI.WebControls.Image photo = new System.Web.UI.WebControls.Image();
            photo.ImageUrl = "~/image/transphoto.jpg";
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.Controls.Add(photo);
            row.Cells.Add(cell);
            Table1.Rows.Add(row);
        }
        catch (Exception exception)
        {
            Label1.Text = "State transition has not been set";
            Label1.BackColor = Color.Gray;
        }
    }
    void draw()
    {
        GraphicsPath hPath = new GraphicsPath();//定义一个GraphicsPath对象实例,以便线帽中使用
        hPath.AddLine(new Point(0, 0), new Point(0, 10));//初始线帽轮廓
        hPath.AddLine(new Point(0, 10), new Point(4, 0));//特别注意：坐标系是以所画直线端点为原点,直线方向为y轴,垂直方向为x轴(有点费解)
        hPath.AddLine(new Point(0, 10), new Point(-4, 0));//三点连线画出了一个箭头
        CustomLineCap HookCap = new CustomLineCap(null, hPath);//初始自定义线帽对象实例
        HookCap.WidthScale = 0.8f;//设置线帽和直线的比例
        HookCap.SetStrokeCaps(LineCap.Round, LineCap.Round);//设置用于构成此自定义线帽的起始直线和结束直线的线帽(似乎不能是LineCap.ArrowAnchor)
        model = (GMCR)Session["model"];
        int photolength = (model.get_feasible_state() / 4 + 3) * 200;            //每一行隔150的距离。没一列隔200的距离。
        Bitmap bitmap = new Bitmap(1200, photolength);
        Graphics g = Graphics.FromImage(bitmap);
        Pen ppp = new Pen(Color.Black,3);
        Pen[] p = new Pen[10];
        p[0] = new Pen(Color.Tomato, 1.5f); p[0].CustomEndCap = HookCap;
        p[1] = new Pen(Color.Gold, 1.5f); p[1].CustomEndCap = HookCap;
        p[2] = new Pen(Color.DarkGreen, 1.5f); p[2].CustomEndCap = HookCap;
        p[3] = new Pen(Color.Red, 1.5f); p[3].CustomEndCap = HookCap;
        p[4] = new Pen(Color.SaddleBrown, 1.5f); p[4].CustomEndCap = HookCap;
        p[5] = new Pen(Color.Snow, 1.5f); p[5].CustomEndCap = HookCap;
        p[6] = new Pen(Color.WhiteSmoke, 1.5f); p[6].CustomEndCap = HookCap;
        p[7] = new Pen(Color.YellowGreen, 1.5f); p[7].CustomEndCap = HookCap;
        p[8] = new Pen(Color.Silver, 1.5f); p[8].CustomEndCap = HookCap;
        p[9] = new Pen(Color.Salmon, 1.5f); p[9].CustomEndCap = HookCap;
        Brush brush = new SolidBrush(Color.Blue);  //设置画刷颜
        Font font = new Font("宋体", 15);   //设置字体类型和大小
        
        SolidBrush sb = new SolidBrush(Color.Black);
        int initx = 300;
        int inity = 200;
        int juge = 0;
        //g.DrawEllipse(p, optionpointx, optionpointy, 17, 17);
        for (int i = 0; i < model.get_feasible_state(); i++)
        {
            g.DrawEllipse(ppp, initx, inity, 40, 40);
            if(i<10) g.DrawString("s"+(i+1).ToString(), font, brush, initx+7,inity+7);
            else g.DrawString("s" + (i + 1).ToString(), font, brush, initx + 2, inity + 8);
            for (int dm = 0; dm < model.get_transitions().Length; dm++)                                 //对每个决策者在这里进行画弧线
            {
                for (int dmsu = 0; dmsu < model.get_transitions()[dm][i].Length; dmsu++)
                {
                    if (model.get_transitions()[dm][i][dmsu] == -1) break;
                    //算出该点的坐标
                    int pointy = (int)(model.get_transitions()[dm][i][dmsu] / 4) * 150 + 200;
                    int pointx = (model.get_transitions()[dm][i][dmsu] % 4) * 150 + 300;
                    if (initx == pointx)//在同一列
                    {
                        if (inity < pointy)
                        {
                            int sweepangle = (int)(180 - 20 / (2 * 3.14 * ((pointy - inity) * 0.66 - 40)) * 360);
                            g.DrawArc(p[dm], initx - (pointy - inity) / 150 * 75 / 3 + 20, inity + 40, 2 * ((pointy - inity) / 150 * 75) / 3, pointy - inity - 40, -90, sweepangle);
                            continue;              

                        }
                        if (inity > pointy)
                        {   

                            int sweepangle = (int)(180 + 20 / (2 * 3.14 * ((pointy - inity) * 0.66 - 40)) * 360);
                            g.DrawArc(p[dm], initx + (pointy - inity) / 150 * 75 / 3 + 20, pointy + 40, 2 * ((inity - pointy) / 150 * 75) / 3, inity - pointy - 40, 90, sweepangle);
                            continue;
                        }
                    }
                    else if (inity == pointy)  //在同一行
                    {
                        if (initx < pointx)
                        {
                            int sweepangle=(int)(180 - 20 / (2 * 3.14 * ((pointx - initx)*0.66-40))*360);

                            g.DrawArc(p[dm], initx + 40, inity - (pointx - initx) / 150 * 75 / 3 + 20, pointx - initx - 40, 2 * ((pointx - initx) / 150 * 75) / 3, -180, sweepangle);
                            continue;
                        }
                        if (initx > pointx)
                        {
                            int sweepangle = (int)(180 + 25 / (2 * 3.14 * ((pointx - initx) * 0.66 - 40)) * 360);
                            g.DrawArc(p[dm], pointx + 40, inity + (pointx - initx) / 150 * 75 / 3 + 20, -(pointx - initx) - 40, 2 * (-(pointx - initx) / 150 * 75) / 3, 0, sweepangle);
                            continue;
                        }
                    }
                    else
                    {   
                         //分4种情况
                        if (initx < pointx && inity < pointy) g.DrawLine(p[dm], initx + 20, inity+40, pointx + 20-10, pointy-10);
                        if (initx < pointx && inity > pointy) g.DrawLine(p[dm], initx + 20, inity, pointx + 20 - 10, pointy+40 + 10);
                        if (initx > pointx && inity > pointy) g.DrawLine(p[dm], initx + 20, inity, pointx + 20 + 10, pointy+40 + 10);
                        if (initx > pointx && inity < pointy) g.DrawLine(p[dm], initx + 20, inity+40, pointx + 20 + 10, pointy - 10);
                    }
                }
            }
            juge++;
            if (juge == 4)
            {
                inity += 150;
                initx = 300;
                juge = 0;
            }
            else if (juge > 0 && juge < 4)
            {
                initx += 150;
            }
        }
        inity = 10;
        for (int i = 0; i < model.get_option().Length; i++)
        {   
            Brush dmcolor= new SolidBrush(Color.Black);  //设置画刷颜
            Font font1 = new Font("宋体", 10);   //设置字体类型和大
            g.DrawString(model.get_DM_descrip()[i], font1, dmcolor, 30, inity + 100);
            g.DrawLine(p[i], 70, inity + 100+10, 100, inity + 100+10);
            inity += 20;
        }
        Response.ClearContent();
        bitmap.Save(get_relpath()+"transphoto.jpg"); 
        
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
        
        return str.Substring(0, point)+"\\";
    }

}