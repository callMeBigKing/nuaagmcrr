using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// StabilityForm 的摘要说明
/// 生成完整的稳定性表格
/// 最大的一张表，之后所有的小型表格都从这张表格里面取出来
/// </summary>
public class StabilityForm
{
    private GMCR model;
    public int[][] form;
    public ArrayList list;
    public int dm_num ;
    public int  state_num;
    public int[][] miniForm;//小表只显示Ep不显示每个决策者的稳定性

    public StabilityForm(GMCR model)
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
        list = new ArrayList();
        this.model = model;
        dm_num = model.get_DM_descrip().Length;
        state_num = model.get_feasible_state();
	}

    private void SimpleInitial()
    {
        //简单偏好初始化
        //思路：将偏好将所有的矩阵分别按照行和列放到一个地方list 的行的 NAsh(dm1) NAsh(dm2) GMR 。。SGMR。。list的列是a ,b, c ,d
       //这里将对list进行初始化

        //form 初始化
        form = new int[state_num][];
        for (int i = 0; i < model.get_feasible_state(); i++)
        {
            form[i] = new int[4 * (dm_num+ 1)];
        }
        //list 初始化大小
        ArrayList list = new ArrayList();
        for (int i = 0; i < dm_num * 4; i++)
        {
            list.Add(i);
        }
            //list 赋值
        for (int dm = 0; dm < dm_num; dm++)
        {
            SimpleStability stability = new SimpleStability(model, dm);
            stability.calculate_stability();
            list[dm] = stability.Nash;
            list[dm + dm_num] = stability.GMR;
            list[dm + 2 * dm_num] = stability.SMR;
            list[dm + 3 * dm_num] = stability.SEQ;
        }
        this.list.Add(list);

    }

    private void StrengthInitial()
    {
        //强度偏好初始化
       

        //form 初始化
        form = new int[state_num][];
        for (int i = 0; i < model.get_feasible_state(); i++)
        {
            form[i] = new int[10 * (dm_num + 1)];
        }

        //list 初始化大小
        ArrayList list = new ArrayList();
        for (int i = 0; i < dm_num * 10; i++)
        {
            list.Add(i);
        }

        for (int dm = 0; dm < dm_num; dm++)
        {
            StrengthStability stability = new StrengthStability(model, dm);
            stability.calculate_stability();
            //一般稳定性
            list[dm] = stability.Nash;
            list[dm + dm_num] = stability.GMR;
            list[dm + 2 * dm_num] = stability.SMR;
            list[dm + 3 * dm_num] = stability.SEQ;
            //强稳定性
            list[dm + 4 * dm_num] = stability.SGMR;
            list[dm + 5 * dm_num] = stability.SSMR;
            list[dm + 6 * dm_num] = stability.SSEQ;
            //弱稳定性
            list[dm + 7 * dm_num] = stability.GMR == stability.SGMR;  //逻辑上是一中同或结构  0   0    1
            list[dm + 8 * dm_num] = stability.SMR == stability.SSMR;  //                      0   1    0
            list[dm + 9 * dm_num] = stability.SEQ == stability.SSEQ; //                       1    1   1
                                                                       //                     1   0    不存在这种情况
        }

        this.list.Add(list);


    }

    private void UncertainInitial()
    {  
        //form 初始化
        form = new int[4 * state_num][];
        for (int i = 0; i < 4 * state_num; i++)
        {
            form[i] = new int[4 * (dm_num + 1)];
        }

        //list 初始化大小
        ArrayList list_a = new ArrayList();
        ArrayList list_b = new ArrayList();
        ArrayList list_c = new ArrayList();
        ArrayList list_d = new ArrayList();
        for (int i = 0; i < dm_num * 4; i++)
        {
            list_a.Add(i);
            list_b.Add(i);
            list_c.Add(i);
            list_d.Add(i);
        }
        
        //list_a
        for (int dm = 0; dm < dm_num; dm++)
        {
            UncertainStability stability = new UncertainStability(model, dm);
            stability.calculate_stability();
            list_a[dm] = stability.Nash_a;
            list_a[dm + dm_num] = stability.GMR_a;
            list_a[dm + 2 * dm_num] = stability.SMR_a;
            list_a[dm + 3 * dm_num] = stability.SEQ_a;
            //
            list_b[dm] = stability.Nash_b;
            list_b[dm + dm_num] = stability.GMR_b;
            list_b[dm + 2 * dm_num] = stability.SMR_b;
            list_b[dm + 3 * dm_num] = stability.SEQ_b;
            //
            //
            list_c[dm] = stability.Nash_c;
            list_c[dm + dm_num] = stability.GMR_c;
            list_c[dm + 2 * dm_num] = stability.SMR_c;
            list_c[dm + 3 * dm_num] = stability.SEQ_c;
            //
            list_d[dm] = stability.Nash_d;
            list_d[dm + dm_num] = stability.GMR_d;
            list_d[dm + 2 * dm_num] = stability.SMR_d;
            list_d[dm + 3 * dm_num] = stability.SEQ_d;
        }
        this.list.Add(list_a);
        this.list.Add(list_b);
        this.list.Add(list_c);
        this.list.Add(list_d);
    }

    private void HybridStability()
    {
        //form 初始化
        form = new int[4 * state_num][];
        for (int i = 0; i < 4 * state_num; i++)
        {
            form[i] = new int[10 * (dm_num + 1)];
        }

        //

        ArrayList list_a = new ArrayList();
        ArrayList list_b = new ArrayList();
        ArrayList list_c = new ArrayList();
        ArrayList list_d = new ArrayList();
        for (int i = 0; i < dm_num * 10; i++)
        {
            list_a.Add(i);
            list_b.Add(i);
            list_c.Add(i);
            list_d.Add(i);
        }

        for (int dm = 0; dm < dm_num; dm++)
        {
            HybridStability stability = new HybridStability(model, dm);
            stability.calculate_stability();
            for (int k = 0; k < 7; k++)
            { // 把带a 下标的都加到
                list_a[dm + k * dm_num] = stability.martix[k * dm_num];
                list_b[dm + k * dm_num] = stability.martix[1+k * dm_num];
                list_c[dm + k * dm_num] = stability.martix[2+k * dm_num];
                list_d[dm + k * dm_num] = stability.martix[3 + k * dm_num];
            }
            //弱稳定性计算
            for (int k = 7; k < 10; k++)
            {
                list_a[dm + k * dm_num] = (Martix)list_a[dm + (k - 6) * dm_num] == (Martix)list_a[dm + (k - 3) * dm_num];  //逻辑同或关系
                list_b[dm + k * dm_num] = (Martix)list_a[dm + (k - 6) * dm_num] == (Martix)list_a[dm + (k - 3) * dm_num];
                list_c[dm + k * dm_num] = (Martix)list_a[dm + (k - 6) * dm_num] == (Martix)list_a[dm + (k - 3) * dm_num];
                list_d[dm + k * dm_num] = (Martix)list_a[dm + (k - 6) * dm_num] == (Martix)list_a[dm + (k - 3) * dm_num];
            
            }
        }

        this.list.Add(list_a);
        this.list.Add(list_b);
        this.list.Add(list_c);
        this.list.Add(list_d);
    }

    public void SetForm()
    {//从list 中提取出表格
        //为了将不同的稳定性统一起来处理，改动了数据的结构使得逻辑变得极其复杂，但是大大缩短了代码
       
        int type = model.GetPerfenceType();
        if (type == 0) this.SimpleInitial();
        else if (type == 1) this.StrengthInitial();
        else if (type == 2) this.UncertainInitial();
        else if (type == 3) this.HybridStability();
        
        form=new int[list.Count*state_num][];
        for (int i = 0; i < form.Length; i++)
        {
            int num = ((ArrayList)list[0]).Count / dm_num;
            form[i] = new int[num * (dm_num + 1)];
        }
            for (int i = 0; i < this.list.Count; i++)
            {
                ArrayList subList = (ArrayList)list[i];
                int num = subList.Count / dm_num;
                

                for (int j = 0; j < num; j++)
                {
                    int[] allFlag = new int[state_num];
                    for (int fuzhi = 0; fuzhi < state_num; fuzhi++) { allFlag[fuzhi] = 1; }  //ep 初始化为1
                    for (int dm = 0; dm < dm_num; dm++)
                    {
                        Martix martix = (Martix)subList[j * dm_num + dm];
                        for (int state = 0; state < state_num; state++)
                        {

                            if ((int)martix.martix[state][state] == 0) form[i + list.Count * state][j * (dm_num + 1) + dm] = 1;
                            else { allFlag[state] = 0; form[i + list.Count * state][j * (dm_num + 1) + dm] = 0; }
                             //有1 则改动为0  
                        }

                    }

                    for (int fuzhi = 0; fuzhi < state_num; fuzhi++)
                    {
                        if (allFlag[fuzhi] == 1) form[i + list.Count * fuzhi][j * (dm_num + 1) + dm_num] = 1;

                    }


                }
            }
    }

    public void SetMiniForm()
    { 
       //从大表中提取出小表小表中只有eq 数据，每取dm列
        miniForm=new int [this.form.Length][];
        int miniFormWidth = form[0].Length / (dm_num + 1);
        for (int i = 0; i < miniForm.Length; i++)
        {
            miniForm[i] = new int[miniFormWidth];

            for (int j = 0; j < miniFormWidth; j++)
            {
               miniForm[i][j] =this.form[i][j * (dm_num + 1) + dm_num];
                     
            }
        }
    
    }

}