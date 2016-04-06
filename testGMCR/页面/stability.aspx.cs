using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
public partial class 页面_stability : System.Web.UI.Page
{
    //系统给出两张表格根据目录来确定
    GMCR model = new GMCR();
    protected void Page_Load(object sender, EventArgs e)
    { //表格的数据可以通过form 类建立好，这里只需要分别设置好每个稳定性的表头就可以了
      
        
     
         
        model = (GMCR)Session["model"];



        ////
          //随意个一组偏好用来快速调试
        //强度偏好
        /*
        string[] dm1 = new string[] { "2>4>3>5>1>6>9>7>>8" };

        string[] dm2 = new string[] { "7=3>5=9>8=4>>6=2=1" };

        string[] dm3 = new string[] { "2=3=4=5=6=7=8=9>1" };

        model.SetPrefence(dm1, 0);
        model.SetPrefence(dm2, 1);
        model.SetPrefence(dm3,2);

        Session["model"] = model;
         
     */
        

        /*
        //强度偏好 
        string[] dm1 = new string[] { "1>3>>2=4" };

      //  string[] dm2 = new string[] { "3>1>>2=4" };

        string[] dm2 = new string[] { "2>4>>1=3" };

        model.SetPrefence(dm1, 0);
        model.SetPrefence(dm2, 1);
        Session["model"] = model;

        *///



        // 混合偏好 gisborn
        /*
        string[] dm1 = new string[] { "2>6>4>8>1>5>3>>7" };

        string[] dm2 = new string[] { "3>7>4>8>1>5>2>>6" ,"2>>6>1>5>4>8>3>7"};

        string[] dm3 = new string[] { "3>4>7>8>5>6>1>>2" };

        model.SetPrefence(dm1, 0);
        model.SetPrefence(dm2, 1);
        model.SetPrefence(dm3, 2);
        Session["model"] = model;
        */
        /*
        int dm_num=model.get_DM_descrip().Length;
        int op_num=model.get_option_descrip().Length;
        for (int dm = 0; dm < dm_num; dm++)
        {
            string[] initstatement = new string[op_num];
            for (int op = 0; op < op_num; op++)
            {
                initstatement[op] = ((op + dm) % op_num + 1).ToString();
               // if (dm == 0) initstatement[op] += "||" + ((op+1 + dm) % op_num + 1).ToString();
               // if (dm == op) initstatement[op] += "&M";
            }
               
                model.trans_preferencestatement(dm, initstatement);
        }
        *///////////   //// 

        try
        {

            int type = model.GetPerfenceType();
            if (type == 0) CreatSimpleForm();
            else if (type == 1) CreatStrengthForm();
            else if (type == 2) CreatUncertainForm();
            else if (type == 3) CreatHybridForm();
        }
        catch
        {
            Label1.Text = "No comprehensive data set, check, policy makers preferences and state transition is set up";
        }
        
      
    }



    void CreatForm()
    {  //传一些参数进来完全可以写成一个函数   //暂时还是那几个不做过多处理
    
    }

    void CreatUncertainForm()
    {//不确定稳定性表格
        StabilityForm form = new StabilityForm(model);
        form.SetForm();
        int dm_num = form.dm_num;
        int state_num = form.state_num;
        //表头

        //第一行的表头
        TableRow rowhead = new TableRow();
        string[] tablehead = { "state", "Nash", "GMR", "SMR", "SEQ" };
        for (int i = 0; i < tablehead.Length; i++)
        {
            TableCell cell0 = new TableCell();
            cell0.Text = tablehead[i];
            cell0.CssClass = "cell";

            if (i == 0) { cell0.ColumnSpan = 2; cell0.RowSpan = 2; }
            else cell0.ColumnSpan = (dm_num + 1);
            rowhead.Cells.Add(cell0);
        }
        Table1.Rows.Add(rowhead);
        //
        // 第二行表头
        TableRow rowrow = new TableRow();
        for (int i = 0; i < 4; i++)
        {

            for (int j = 0; j < dm_num; j++)  //决策者信息
            {
                TableCell cell = new TableCell();

                cell.Width = 50;
                cell.Text = model.get_DM_descrip()[j];
                rowrow.Cells.Add(cell);
            }
            TableCell cellep = new TableCell();     //Eq 信息
            cellep.CssClass = "cell";
            cellep.Text = "Eq";
            rowrow.Cells.Add(cellep);
        }
        Table1.Rows.Add(rowrow);


        for (int state = 0; state < state_num * 4; state++)
        {
            TableRow row = new TableRow();
            TableCell cellhead = new TableCell();
            cellhead.Text = "S" + (state / 4 + 1).ToString();
            cellhead.CssClass = "cell";
            cellhead.Width = 40;
            cellhead.RowSpan = 4;
            if (state % 4 == 0) row.Cells.Add(cellhead);

            if (state % 4 == 0)
            {
                TableCell cellheada = new TableCell();
                cellheada.Text = "a";
                cellheada.CssClass = "cell";
                row.Cells.Add(cellheada);
            }
            if (state % 4 == 1)
            {
                TableCell cellheadb = new TableCell();
                cellheadb.Text = "b";
                cellheadb.CssClass = "cell";
                row.Cells.Add(cellheadb);
            }
            if (state % 4 == 2)
            {
                TableCell cellheadc = new TableCell();
                cellheadc.Text = "c";
                cellheadc.CssClass = "cell";
                cellheadc.Width = 30;
                row.Cells.Add(cellheadc);
            }
            if (state % 4 == 3)
            {
                TableCell cellheadd = new TableCell();
                cellheadd.Text = "d";
                cellheadd.CssClass = "cell"; ;
                row.Cells.Add(cellheadd);
            }
            for (int j = 0; j < form.form[state].Length; j++)
            {
                TableCell cell = new TableCell();
                cell.CssClass = "cell";
                cell.Width = 50;
                if (form.form[state][j] == 1) cell.Text = "1  ";
                else cell.Text = "   ";

                row.Cells.Add(cell);
            }
            Table1.Rows.Add(row);

        }

    }

    void CreatHybridForm()
    {//混合偏好表格
        StabilityForm form = new StabilityForm(model);
        form.SetForm();
        int dm_num = form.dm_num;
        int state_num = form.state_num;
        //表头

        //第一行的表头
        TableRow rowhead = new TableRow();
        string[] tablehead = { "state", "Nash", "GMR", "SMR", "SEQ", "SGMR", "SSMR", "SSEQ", "WGMR", "WSMR", "WSEQ", };
        for (int i = 0; i < tablehead.Length; i++)
        {
            TableCell cell0 = new TableCell();
            cell0.Text = tablehead[i];
            cell0.CssClass = "cell";

            if (i == 0) { cell0.ColumnSpan = 2; cell0.RowSpan = 2; }
            else cell0.ColumnSpan = (dm_num + 1);
            rowhead.Cells.Add(cell0);
        }
        Table1.Rows.Add(rowhead);
        //
        // 第二行表头
        TableRow rowrow = new TableRow();
        for (int i = 0; i < 10; i++)
        {

            for (int j = 0; j < dm_num; j++)  //决策者信息
            {
                TableCell cell = new TableCell();

                cell.Width = 50;
                cell.Text = model.get_DM_descrip()[j];
                rowrow.Cells.Add(cell);
            }
            TableCell cellep = new TableCell();     //Eq 信息
            cellep.CssClass = "cell";
            cellep.Text = "Eq";
            rowrow.Cells.Add(cellep);
        }
        Table1.Rows.Add(rowrow);


        for (int state = 0; state < state_num * 4; state++)
        {
            TableRow row = new TableRow();
            TableCell cellhead = new TableCell();
            cellhead.Text = "S" + (state / 4 + 1).ToString();
            cellhead.CssClass = "cell";
            cellhead.Width = 40;
            cellhead.RowSpan = 4;
            if (state % 4 == 0) row.Cells.Add(cellhead);

            if (state % 4 == 0)
            {
                TableCell cellheada = new TableCell();
                cellheada.Text = "a";
                cellheada.CssClass = "cell";
                row.Cells.Add(cellheada);
            }
            if (state % 4 == 1)
            {
                TableCell cellheadb = new TableCell();
                cellheadb.Text = "b";
                cellheadb.CssClass = "cell";
                row.Cells.Add(cellheadb);
            }
            if (state % 4 == 2)
            {
                TableCell cellheadc = new TableCell();
                cellheadc.Text = "c";
                cellheadc.CssClass = "cell";
                cellheadc.Width = 30;
                row.Cells.Add(cellheadc);
            }
            if (state % 4 == 3)
            {
                TableCell cellheadd = new TableCell();
                cellheadd.Text = "d";
                cellheadd.CssClass = "cell"; ;
                row.Cells.Add(cellheadd);
            }
            for (int j = 0; j < form.form[state].Length; j++)
            {
                TableCell cell = new TableCell();
                cell.CssClass = "cell";
                cell.Width = 50;
                if (form.form[state][j] == 1) cell.Text = "1  ";
                else cell.Text = "   ";

                row.Cells.Add(cell);
            }
            Table1.Rows.Add(row);

        }
    }


    void CreatSimpleForm()
    {//简单稳定性表格
      //基本上还是用之前的程序，建好表头然后填数据
        StabilityForm form = new StabilityForm(model);
        form.SetForm();
        int dm_num = form.dm_num;
        int state_num = form.state_num;


        TableRow rowhead = new TableRow();
        string[] tablehead = { "state", "Nash", "GMR", "SMR", "SEQ" };

        //将第一行的表头写入表格
        for (int i = 0; i < tablehead.Length; i++)
        {
            TableCell cell0 = new TableCell();
            cell0.Text = tablehead[i];
            cell0.CssClass = "cell";

            if (i == 0) { cell0.Width = 50; cell0.RowSpan = 2; }
            else cell0.ColumnSpan = (dm_num + 1);
            rowhead.Cells.Add(cell0);
        }
        Table1.Rows.Add(rowhead);


        TableRow rowrow = new TableRow();//第2行 表头 写入决策者信息
        for (int i = 0; i < 4; i++)
        {

            for (int j = 0; j < dm_num; j++)   //写入决策者
            {
                TableCell cell = new TableCell();

                cell.Width = 50;
             
                cell.Text = model.get_DM_descrip()[j];
                rowrow.Cells.Add(cell);
            }
            TableCell cellep = new TableCell();  //写入eq
            cellep.CssClass = "cell";
            cellep.Width = 50;

            cellep.Text = "Eq";
            rowrow.Cells.Add(cellep);
        }
        Table1.Rows.Add(rowrow);

        //状态s编号 和对应的稳定性信息
        for (int state = 0; state < state_num; state++)
        {
            TableRow row = new TableRow();

            TableCell cellhead = new TableCell();  //状态编号
            cellhead.Text = "S" + (state+1).ToString();
            cellhead.CssClass = "cell";
            row.Cells.Add(cellhead);

            for (int j = 0; j < form.form[state].Length; j++)  //写入稳定性
            {
                TableCell cell = new TableCell();
                cell.CssClass = "cell";
                
                if (form.form[state][j] == 1) cell.Text = "1  ";
                else cell.Text = "   ";

                row.Cells.Add(cell);
            }
            Table1.Rows.Add(row);

        }
    }

    void CreatStrengthForm()
    { //强度偏好表格
        StabilityForm form = new StabilityForm(model);
        form.SetForm();
        int dm_num = form.dm_num;
        int state_num = form.state_num;

        //第一行表头
        TableRow rowhead = new TableRow();
        string[] tablehead = { "state", "Nash", "GMR", "SMR", "SEQ", "SGMR", "SSMR", "SSEQ", "WGMR", "WSMR", "WSEQ", };

        for (int i = 0; i < tablehead.Length; i++)
        {
            TableCell cell0 = new TableCell();
            cell0.Text = tablehead[i];
            cell0.CssClass = "cell";
            if (i == 0) { cell0.Width = 50; cell0.RowSpan = 2; }
            else cell0.ColumnSpan = (dm_num + 1);
            rowhead.Cells.Add(cell0);
        }
        Table1.Rows.Add(rowhead);


        //第二行表头添加决策者
        TableRow rowrow = new TableRow();
        for (int i = 0; i < tablehead.Length - 1; i++)
        {

            for (int j = 0; j < dm_num; j++)   //添加决策者信息
            {
                TableCell cell = new TableCell();
                cell.Width = 50;
                cell.CssClass = "cell";
                cell.Text = model.get_DM_descrip()[j];
                rowrow.Cells.Add(cell);
            }
            TableCell cellep = new TableCell();  //加入Eq
            cellep.CssClass = "cell";
            cellep.Width = 50;

            cellep.Text = "Eq";
            rowrow.Cells.Add(cellep);
        }
        Table1.Rows.Add(rowrow);

        //添加状态编号和稳定性结果，一行一行的添加
        for (int state = 0; state < state_num; state++)
        {
            TableRow row = new TableRow();

            TableCell cellhead = new TableCell();
            cellhead.Text = "S" + (state+1).ToString();
            cellhead.CssClass = "cell";

            row.Cells.Add(cellhead);

            for (int j = 0; j < form.form[state].Length; j++)
            {
                TableCell cell = new TableCell();
                cell.CssClass = "cell";
                if (form.form[state][j] == 1) cell.Text = "1  ";
                row.Cells.Add(cell);
            }
            Table1.Rows.Add(row);

        }
    }

    //一般稳定性表单
    void creat_stability_form()
    {
        stability_form form = new stability_form(model);
        form.setform();


        int state_num = model.get_feasible_state();
        int dm_num = model.get_DM_descrip().Length;

        TableRow rowhead = new TableRow();
        string[] tablehead = { "state", "Nash", "GMR", "SMR", "SEQ" };

        for (int i = 0; i < tablehead.Length; i++)
        {
            TableCell cell0 = new TableCell();
            cell0.Text = tablehead[i];
            cell0.CssClass = "cell";

            if (i == 0) cell0.Width = 50;
            else cell0.ColumnSpan = (dm_num + 1);
            rowhead.Cells.Add(cell0);
        }
        Table1.Rows.Add(rowhead);

        TableRow rowrow = new TableRow();

        TableCell cellnull = new TableCell();
        cellnull.CssClass = "cell";
        //cell.Height = 30;

        cellnull.Width = 50;

        cellnull.Text = "";
        rowrow.Cells.Add(cellnull);
        for (int i = 0; i < 4; i++)
        {

            for (int j = 0; j < dm_num; j++)
            {
                TableCell cell = new TableCell();

                cell.Width = 50;
                cellnull.CssClass = "cell";
                cell.Text = model.get_DM_descrip()[j];
                rowrow.Cells.Add(cell);
            }
            TableCell cellep = new TableCell();
            cellep.CssClass = "cell";
            cellep.Width = 50;

            cellep.Text = "Ep";
            rowrow.Cells.Add(cellep);
        }
        Table1.Rows.Add(rowrow);
        for (int state = 0; state < state_num; state++)
        {
            TableRow row = new TableRow();

            TableCell cellhead = new TableCell();
            cellhead.Text = "S" + state.ToString();

            cellhead.Width = 50;
            cellhead.CssClass = "cell";

            row.Cells.Add(cellhead);

            for (int j = 0; j < form.form[state].Length; j++)
            {
                TableCell cell = new TableCell();
                cell.CssClass = "cell";
                cell.Width = 50;
                if (form.form[state][j] == 1) cell.Text = "1  ";
                else cell.Text = "   ";

                row.Cells.Add(cell);
            }
            Table1.Rows.Add(row);

        }
    }
    //不确定稳定性表单

    void creat_Sstability_form()
    {
        SStability_form form = new SStability_form(model);
        form.setform();


        int state_num = model.get_feasible_state();
        int dm_num = model.get_DM_descrip().Length;

        TableRow rowhead = new TableRow();
        string[] tablehead = { "state", "Nash", "GMR", "SMR", "SEQ" ,  "SGMR", "SSMR", "SSEQ",  "WGMR", "WSMR", "WSEQ", };

        for (int i = 0; i < tablehead.Length; i++)
        {
            TableCell cell0 = new TableCell();
            cell0.Text = tablehead[i];
            cell0.CssClass = "cell";

            if (i == 0) cell0.Width = 50;
            else cell0.ColumnSpan = (dm_num + 1);
            rowhead.Cells.Add(cell0);
        }
        Table1.Rows.Add(rowhead);

        TableRow rowrow = new TableRow();

        TableCell cellnull = new TableCell();
        cellnull.CssClass = "cell";
        //cell.Height = 30;

        cellnull.Width = 50;

        cellnull.Text = "";
        rowrow.Cells.Add(cellnull);
        for (int i = 0; i < tablehead.Length-1; i++)
        {

            for (int j = 0; j < dm_num; j++)
            {
                TableCell cell = new TableCell();
                cell.Width = 50;
                cell.CssClass = "cell";
                cell.Text = model.get_DM_descrip()[j];
                rowrow.Cells.Add(cell);
            }
            TableCell cellep = new TableCell();
            cellep.CssClass = "cell";
            cellep.Width = 50;

            cellep.Text = "Ep";
            rowrow.Cells.Add(cellep);
        }
        Table1.Rows.Add(rowrow);
        for (int state = 0; state < state_num; state++)
        {
            TableRow row = new TableRow();

            TableCell cellhead = new TableCell();
            cellhead.Text = "S" + state.ToString();

            cellhead.Width = 50;
            cellhead.CssClass = "cell";

            row.Cells.Add(cellhead);

            for (int j = 0; j < form.form[state].Length; j++)
            {
                TableCell cell = new TableCell();
                cell.CssClass = "cell";
                cell.Width = 50;
                if (form.form[state][j] == 1) cell.Text = "1  ";
                else cell.Text = "   ";

                row.Cells.Add(cell);
            }
            Table1.Rows.Add(row);

        }

    }
    void creat_Ustability_form()
    {
        Ustability_form form = new Ustability_form(model);
        form.setform();
        int state_num = model.get_feasible_state();
        int dm_num = model.get_DM_descrip().Length;
        //表头
        TableRow rowhead = new TableRow();
        string[] tablehead = { "state", "Nash", "GMR", "SMR", "SEQ" };
        for (int i = 0; i < tablehead.Length; i++)
        {
            TableCell cell0 = new TableCell();
            cell0.Text = tablehead[i];
            cell0.CssClass = "cell";

            if (i == 0) {  cell0.ColumnSpan = 2; }
            else cell0.ColumnSpan = (dm_num + 1);
            rowhead.Cells.Add(cell0);
        }
        Table1.Rows.Add(rowhead);
        //
        // 第二行表头
        TableRow rowrow = new TableRow();
        TableCell cellnull = new TableCell();
        cellnull.CssClass = "cell";
        //cell.Height = 30;
        cellnull.ColumnSpan = 2;
        cellnull.Text = "";

        rowrow.Cells.Add(cellnull);
        for (int i = 0; i < 4; i++)
        {

            for (int j = 0; j < dm_num; j++)
            {
                TableCell cell = new TableCell();

                cell.Width = 50;
                cellnull.CssClass = "cell";
                cell.Text = model.get_DM_descrip()[j];
                rowrow.Cells.Add(cell);
            }
            TableCell cellep = new TableCell();
            cellep.CssClass = "cell";
            cellep.Width = 50;

            cellep.Text = "Ep";
            rowrow.Cells.Add(cellep);
        }
        Table1.Rows.Add(rowrow);


        for (int state = 0; state < state_num*4; state++)
        {
            TableRow row = new TableRow();
            TableCell cellhead = new TableCell();
            cellhead.Text = "S" + (state/4 +1).ToString();
            cellhead.CssClass = "cell";
            cellhead.Width = 40;
            cellhead.RowSpan = 4;
            if (state%4==0) row.Cells.Add(cellhead);

            if (state % 4 == 0)
            {
                TableCell cellheada = new TableCell();
                cellheada.Text = "a";
                cellheada.CssClass = "cell";
                row.Cells.Add(cellheada);
            }
            if (state % 4 == 1)
            {
                TableCell cellheadb = new TableCell();
                cellheadb.Text = "b";
                cellheadb.CssClass = "cell";
                row.Cells.Add(cellheadb);
            }
            if (state % 4 == 2)
            {
                TableCell cellheadc = new TableCell();
                cellheadc.Text = "c";
                cellheadc.CssClass = "cell";
                cellheadc.Width = 30;
                row.Cells.Add(cellheadc);
            }
            if (state % 4 == 3)
            {
                TableCell cellheadd = new TableCell();
                cellheadd.Text = "d";
                cellheadd.CssClass = "cell"; ;
                row.Cells.Add(cellheadd);
            }
                for (int j = 0; j < form.form[state].Length; j++)
                {
                    TableCell cell = new TableCell();
                    cell.CssClass = "cell";
                    cell.Width = 50;
                    if (form.form[state][j] == 1) cell.Text = "1  ";
                    else cell.Text = "   ";

                    row.Cells.Add(cell);
                }
            Table1.Rows.Add(row);

        }
    }
}