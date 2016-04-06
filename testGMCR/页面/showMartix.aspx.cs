using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;

public partial class 页面_showMartix : System.Web.UI.Page
{
    GMCR model = new GMCR();
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
        try
        {*/
            model = (GMCR)Session["model"];
            //creatSPMartix();
            //creatSJMartix();
           // creatTable();
            
           int [][]symbol=new int [2][];

            symbol[0]=new int[] {1};
            symbol[1]=new int[] {1,2};
            string []name=new string[2];
            name[0]="+";
            name[1]="+,U";
            int []dm=new int [model.get_DM_descrip().Length];
            for(int i=0;i<dm.Length;i++)
            {
                dm[i]=i;
            }

            CreatTableP(name, symbol, dm);
            CreatTableJ(name, symbol, dm);
            CreatTableMH(dm);
            
           /*
        }
        catch
        {
           Label1.Text="偏好或状态转移没有设置完全。";
           Label1.BackColor = Color.Gray;
        }
            * */
    }



    public void CreatTableP(string[] name, int[][] symbol, int[] dm)
    {//生成P矩阵
        ///@parma name:矩阵名称  symbol：需要进行运算的符号对应BaseStability 中的，dm 决策者 从0开始
        ///注意这三个矩阵维数要相同     

          //cell1 是表头 cell2是矩阵的table
        for (int people = 0; people < dm.Length; people++)
        {
            TableRow row = new TableRow(); 
            for (int i = 0; i < name.Length; i++)
            {
                TableCell cell1 = new TableCell();
                cell1.Text = "P" + name[i] + "  " + (dm[people] + 1).ToString();
                TableCell cell2 = new TableCell();
                cell2.Controls.Add(showOneMartix(BaseStability.get_P(model, symbol[i], dm[people])));
                row.Cells.Add(cell1); row.Cells.Add(cell2);
            }
            Table1.Rows.Add(row);
        }

    }

    public void CreatTableJ(string[] name, int[][] symbol, int[] dm)
    {//生成P矩阵
        ///@parma name:矩阵名称  symbol：需要进行运算的符号对应BaseStability 中的，dm 决策者 从0开始
        ///注意这三个矩阵维数要相同     
        for (int people = 0; people < dm.Length; people++)
        {
            TableRow row = new TableRow();   //cell1 是表头 cell2是矩阵的table
            for (int i = 0; i < name.Length; i++)
            {
                TableCell cell1 = new TableCell();
                cell1.Text = "J" + name[i] + "  " + (dm[people] + 1).ToString();
                TableCell cell2 = new TableCell();
                cell2.Controls.Add(showOneMartix(BaseStability.get_J(model, symbol[i], dm[people])));
                row.Cells.Add(cell1); row.Cells.Add(cell2);
            }
            Table1.Rows.Add(row);
        }
    }

    public void CreatTableMH( int[] dm)
    {
        for (int people = 0; people < dm.Length; people++)
        {
            UncertainStability stability = new UncertainStability(model,people);
            TableRow row = new TableRow();   //cell1 是表头 cell2是矩阵的table


            int[] H = new int[dm.Length];
            for (int i = 0; i < dm.Length; i++)
            {  
                if(i!=people)H[i]=1;
                else H[i]=0;

            }
              
                TableCell cell1 = new TableCell();
                cell1.Text = "M n-" + (dm[people] + 1).ToString()  + "  ";
                TableCell cell2 = new TableCell();
                cell2.Controls.Add(showOneMartix(stability.get_MH(H)));
                row.Cells.Add(cell1); row.Cells.Add(cell2);

                TableCell cellA1 = new TableCell();
                cellA1.Text = "M n-" + (dm[people] + 1).ToString() + "  ";
                TableCell cellA2 = new TableCell();
                cellA2.Controls.Add(showOneMartix(stability.get_MH_increase(H)));
                row.Cells.Add(cellA1); row.Cells.Add(cellA2);


                TableCell cellC1 = new TableCell();
                cellC1.Text = "M n-" + (dm[people] + 1).ToString() + "  ";
                TableCell cellC2 = new TableCell();
                cellC2.Controls.Add(showOneMartix(stability.get_MH_Uincrease(H)));
                row.Cells.Add(cellC1); row.Cells.Add(cellC2);

           
            Table1.Rows.Add(row);
        }
    
    }



    
    /// <summary>
    ///  分割线，下面的暂时不用后面记得删除
    /// </summary>
    private void creatTable()
    { 
      int dmNum=model.get_DM_descrip().Length;
        

      for (int i = 0; i < dmNum; i++)
      {   
          TableRow row = new TableRow();
          TableRow rowhead = new TableRow();
         int[] H = new int[dmNum];
        for (int j = 0; j < dmNum; j++)   //联合
        {
            if (j == i) H[j] = 0;
            else H[j] = 1;
        }
          Ustability dm_stability = new Ustability(model, i);
          TableCell cellhead1 = new TableCell();
          TableCell cellhead2 = new TableCell();
          TableCell cellhead3 = new TableCell();
          cellhead1.Text = dm_stability.thr.ToString();
          cellhead2.Text = dm_stability.thr_increase.ToString();
          cellhead3.Text = dm_stability.thr_Uincrease.ToString();
          cellhead1.ColumnSpan = 2;
          cellhead2.ColumnSpan = 2;
          cellhead3.ColumnSpan = 2;
          rowhead.Cells.Add(cellhead1); rowhead.Cells.Add(cellhead2); rowhead.Cells.Add(cellhead3);
          Table1.Rows.Add(rowhead);

          TableCell cell1 = new TableCell();
          cell1.Text = "M（N-_" + (i + 1).ToString() + "）";
          TableCell cell2 = new TableCell();
          cell2.Controls.Add(showOneMartix( dm_stability.get_MH(H)));
          row.Cells.Add(cell1); row.Cells.Add(cell2);


          TableCell cell3 = new TableCell();
          cell3.Text = "M +（N-_" + (i + 1).ToString() + "）";
          TableCell cell4 = new TableCell();
          cell4.Controls.Add(showOneMartix(dm_stability.get_MH_increase(H)));
          row.Cells.Add(cell3); row.Cells.Add(cell4);

          TableCell cell5 = new TableCell();
          cell5.Text = "M +,U（N-_" + (i+1).ToString() + "）";
          TableCell cell6 = new TableCell();
          cell6.Controls.Add(showOneMartix(dm_stability.get_MH_Uincrease(H)));
          row.Cells.Add(cell5); row.Cells.Add(cell6);

          Table1.Rows.Add(row);
      }


    }


    //各个决策者的状态转移矩阵；
    private void creatJMartix()
    {//J 矩阵
        int dmNum = model.get_DM_descrip().Length;

        for (int i = 0; i < dmNum; i++)
        {
            TableRow row = new TableRow();   //cell1 是表头 cell2是矩阵的table
            
            Ustability dm_stability = new Ustability(model, i);    
            TableCell cell1 = new TableCell();
            cell1.Text = "J（" + (i + 1).ToString() + "）";
            TableCell cell2 = new TableCell();
            cell2.Controls.Add(showOneMartix(dm_stability.get_Ji_martix(i)));
            row.Cells.Add(cell1); row.Cells.Add(cell2);


            TableCell cell3 = new TableCell();
            cell3.Text = "J +（" + (i + 1).ToString() + "）";
            TableCell cell4 = new TableCell();
            cell4.Controls.Add(showOneMartix(dm_stability.get_Ji_increase_martix(i)));
            row.Cells.Add(cell3); row.Cells.Add(cell4);

            TableCell cell5 = new TableCell();
            cell5.Text = "J +,U（" + (i + 1).ToString() + "）";
            TableCell cell6 = new TableCell();
            cell6.Controls.Add(showOneMartix(dm_stability.get_Ji_Uincrease_martix(i)));
            row.Cells.Add(cell5); row.Cells.Add(cell6);

            Table1.Rows.Add(row);
        }
    }


    private void creatSJMartix()
    {  // 强度偏好 矩阵
        int dmNum = model.get_DM_descrip().Length;

        for (int i = 0; i < dmNum; i++)
        {
            TableRow row = new TableRow();

            SStability dm_stability = new SStability(model, i);
            TableCell cell1 = new TableCell();
            cell1.Text = "J（" + (i + 1).ToString() + "）";
            TableCell cell2 = new TableCell();
            cell2.Controls.Add(showOneMartix(dm_stability.get_Ji_martix(i)));
            row.Cells.Add(cell1); row.Cells.Add(cell2);

            /*
            TableCell cell3 = new TableCell();
            cell3.Text = "J +（" + (i + 1).ToString() + "）";
            TableCell cell4 = new TableCell();
            cell4.Controls.Add(showOneMartix(dm_stability.get_Ji_increase_martix(i)));
            row.Cells.Add(cell3); row.Cells.Add(cell4);

            TableCell cell5 = new TableCell();
            cell5.Text = "J +,U（" + (i + 1).ToString() + "）";
            TableCell cell6 = new TableCell();
            cell6.Controls.Add(showOneMartix(dm_stability.get_Ji_Uincrease_martix(i)));
            row.Cells.Add(cell5); row.Cells.Add(cell6);
            */
            Table1.Rows.Add(row);
        }
    }

    private void creatPMartix()
    { 
           int dmNum = model.get_DM_descrip().Length;

           for (int i = 0; i < dmNum; i++)
           {
               TableRow row = new TableRow();

               Ustability dm_stability = new Ustability(model, i);
               TableCell cell1 = new TableCell();
               cell1.Text = "P +（" + (i + 1).ToString() + "）";
               TableCell cell2 = new TableCell();
               cell2.Controls.Add(showOneMartix(dm_stability.get_preferencemartix_increase(i)));
               row.Cells.Add(cell1); row.Cells.Add(cell2);


               TableCell cell3 = new TableCell();
               cell3.Text = "P +U（" + (i + 1).ToString() + "）";
               TableCell cell4 = new TableCell();
               cell4.Controls.Add(showOneMartix(dm_stability.get_preferencemartix_Uincrease(i)));
               row.Cells.Add(cell3); row.Cells.Add(cell4);

               TableCell cell5 = new TableCell();
               cell5.Text = "P -=（" + (i + 1).ToString() + "）";
               TableCell cell6 = new TableCell();
               cell6.Controls.Add(showOneMartix(dm_stability.get_preferencemartix_reduce(i)));
               row.Cells.Add(cell5); row.Cells.Add(cell6);

               TableCell cell7 = new TableCell();
               cell7.Text = "P -=U（" + (i + 1).ToString() + "）";
               TableCell cell8 = new TableCell();
               cell8.Controls.Add(showOneMartix(dm_stability.get_preferencemartix_Ureduce(i)));
               row.Cells.Add(cell7); row.Cells.Add(cell8);

               Table1.Rows.Add(row);
           }
    }

    private void creatSPMartix()
    {   
        //强度偏好时的P矩阵
        int dmNum = model.get_DM_descrip().Length;

        for (int i = 0; i < dmNum; i++)
        {
            TableRow row = new TableRow();

            SStability dm_stability = new SStability(model, i);
            TableCell cell1 = new TableCell();
            cell1.Text = "P +,++（" + (i + 1).ToString() + "）";
            TableCell cell2 = new TableCell();
            cell2.Controls.Add(showOneMartix(dm_stability.get_Pincrease_strength(i)));
            row.Cells.Add(cell1); row.Cells.Add(cell2);


            TableCell cell3 = new TableCell();
            cell3.Text = "P ++（" + (i + 1).ToString() + "）";
            TableCell cell4 = new TableCell();
            cell4.Controls.Add(showOneMartix(dm_stability.get_Pstrength(i)));
            row.Cells.Add(cell3); row.Cells.Add(cell4);
            /*
            TableCell cell5 = new TableCell();
            cell5.Text = "P -=（" + (i + 1).ToString() + "）";
            TableCell cell6 = new TableCell();
            cell6.Controls.Add(showOneMartix(dm_stability.get_preferencemartix_reduce(i)));
            row.Cells.Add(cell5); row.Cells.Add(cell6);

            TableCell cell7 = new TableCell();
            cell7.Text = "P -=U（" + (i + 1).ToString() + "）";
            TableCell cell8 = new TableCell();
            cell8.Controls.Add(showOneMartix(dm_stability.get_preferencemartix_Ureduce(i)));
            row.Cells.Add(cell7); row.Cells.Add(cell8);
            */
            Table1.Rows.Add(row);
        }
    }

    private Table showOneMartix(Martix m )
    {
        Table table = new Table();
        table.CssClass = "table";
        int length = m.row;
        for (int i = 0; i < length + 1; i++)
        {
            TableRow row = new TableRow();
            for (int j = 0; j < length + 1; j++)
            {
                TableCell cell = new TableCell();
                cell.CssClass = "cell";
                if (i == 0 && j == 0) cell.Text = "   ";
                else if (i == 0)
                {
                    int p = j - 1;
                    cell.Text = "S" + (p + 1).ToString();
                }
                else if (j == 0)
                {
                    int p = i - 1;
                    cell.Text = "S" + (p + 1).ToString() + "   ";
                }
                else
                {
                    cell.Text = m.martix[i - 1][j - 1].ToString();
                }
                cell.CssClass = "cell";
                cell.Width = 40;
                row.Cells.Add(cell);
            }
            table.Rows.Add(row);
        }
        return table;

    }
}