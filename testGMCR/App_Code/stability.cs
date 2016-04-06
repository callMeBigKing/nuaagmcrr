using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;

/// <summary>
/// stability 的摘要说明
/// </summary>
public class stability
{

    private GMCR model;
    public Martix Nash;
    public Martix GMR;
    public Martix SMR;
    public Martix SEQ;
    public int dm;
    private ArrayList M_it_martix;
    private ArrayList M_it_increase_martix;
    
    private  int dm_num ;
    private int state_num;

    private int thr;
    private int thr_increase;


    public stability(GMCR model,int dm_i)   //初始化要模型，和决策者，我们计算的是决策者i的稳定性。
	{
        
        this.model = model;

        this.dm_num = model.get_DM_descrip().Length;
        this.state_num = model.get_feasible_state();

        this.dm = dm_i;
        int state_num = model.get_feasible_state();
        M_it_martix = new ArrayList();
        M_it_increase_martix = new ArrayList();
        Nash = new Martix(state_num, state_num);
        GMR = new Martix(state_num, state_num);
        SMR = new Martix(state_num, state_num);
        SEQ = new Martix(state_num, state_num);
        thr=this.get_thr();
        thr_increase = this.get_thr_increase();
       for (int t = 0; t < thr; t++)   //对 M_it_martix list 进行初始化
        {
            ArrayList arr = new ArrayList();
            for (int dm = 0; dm < model.get_DM_descrip().Length; dm++)
            {
                if(t==0)arr.Add(this.get_Ji_martix(dm));
                else arr.Add(dm); 
            }
            M_it_martix.Add(arr);
        }
       for (int t = 0; t < thr_increase; t++)  
        {
            ArrayList arr = new ArrayList();
            for (int dm = 0; dm < dm_num; dm++)
            {
                if(t==0)arr.Add(this.get_Ji_increase_martix(dm));
                else arr.Add(dm);
            }
            M_it_increase_martix.Add(arr);
        }
         
	}

    private Martix get_M_it(int dm_i, int t,int []H)
    {

       ArrayList arr=(ArrayList)this.M_it_martix[t];
       try
       {
           Martix M = (Martix)arr[dm_i];
           return M;
        }
        catch
       {
               Martix M = new Martix(state_num,state_num);
               for (int dm = 0; dm < dm_num; dm++)
               {
                   if (H[dm]==0) continue;
                   if (dm == dm_i) continue;
                   Martix M_jt_1 = get_M_it(dm, t - 1,H);
                   M = M | M_jt_1;
               }
               M = (this.get_Ji_martix(dm_i) * M).sign();
               arr[dm_i] = M;
               this.M_it_martix[t] = arr;
               return M;
  
        }
       
    }

    private Martix get_M_it_increase(int dm_i, int t, int[] H)
    {

        ArrayList arr=(ArrayList)this.M_it_increase_martix[t];
        try
        {   Martix M=(Martix)arr[dm_i];
                return M;
        }
        catch
        {

            Martix M = new Martix(state_num, state_num);          //M 初始为全0矩阵
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (H[dm] == 0) continue;
                if (dm == dm_i) continue;
                Martix M_jt_1 = this.get_M_it_increase(dm, t - 1,H);
                M = M | M_jt_1;
            }
            M = (this.get_Ji_increase_martix(dm_i) * M).sign();
            arr[dm_i] = M;
            this.M_it_increase_martix[t] = arr;
            return M;
        }
    }
     

    private Martix get_MH(int []H)
    {   
        Martix M = new Martix(state_num, state_num);
        for (int t = 0; t < thr; t++)
       {
           
           for (int dm = 0; dm < dm_num; dm++)
           {
               if (H[dm] == 0) continue;
               else
               {
                   Martix M_jt_1 = this.get_M_it(dm,t,H);
                   M = M | M_jt_1;
               }
           }
       }
        for (int i = 0; i < state_num; i++)
        {  //自己不能转移到自己
            M.martix[i][i] = 0;
        }
       return M;
    }

    private Martix get_MH_increase(int[] H)
    {
        Martix M = new Martix(state_num, state_num);
        for (int t = 0; t < thr_increase; t++)
        {
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (H[dm] == 0) continue;
                else
                {
                    Martix M_jt_1 = this.get_M_it_increase(dm,t,H);
                    M = M | M_jt_1;
                }
            }
        }
        for (int i = 0; i < state_num; i++)
        {  //自己不能转移到自己
            M.martix[i][i] = 0;
        }

        return M;

    }

    private int get_thr()              // 得到&
    {

        int thr=0;
        for (int dm = 0; dm < dm_num; dm++)
        {
            Martix Ji = this.get_Ji_martix(dm);
            for (int i = 0; i < state_num; i++)
            {
                for (int j = 0; j < state_num; j++)
                {
                    if (Ji.martix[i][j] == 1) thr++;
                }
            }
        }
        return thr;
    }

    private int get_thr_increase()              // 得到&
    {

        int thr_increase = 0;
        for (int dm = 0; dm < dm_num; dm++)
        {
           // Martix P_increase = this.get_preferencemartix_increase(dm);
            //Martix Ji = this.get_Ji_martix(dm);
            Martix Ji_increase = get_Ji_increase_martix(dm);
            for (int i = 0; i < state_num; i++)
            {
                for (int j = 0; j < state_num; j++)
                {
                    if (Ji_increase.martix[i][j] == 1) thr_increase++;
                }
            }
        }
        return thr_increase;
    }
  
    private Martix get_Ji_martix(int dm_i)    //得到i的状态转移矩阵
    {
        double[][] a = new double[state_num][];
        for (int i = 0; i < state_num; i++)
        {
            a[i] = new double[state_num];
            for (int j = 0; j < state_num; j++)
            {
                a[i][j] = model.get_trans_marix(dm_i)[i][j] ;
            }
        }

        Martix M = new Martix(a);
        return M;

    }

    private Martix get_Ji_increase_martix(int dm_i)
    {
        Martix P_increase = this.get_preferencemartix_increase(dm_i);
        //决策者i状态转移矩阵
        Martix Ji = this.get_Ji_martix(dm_i);
        Martix Ji_increase = Ji & P_increase;
        return Ji_increase;
    }

    private Martix get_preferencemartix_reduce(int dm_i)   //P-=的矩阵
    {
        int [][] prefenece_martix=model.get_prefenece_martix(dm_i);
        double[][] P_ = new double[state_num][];
        for (int i = 0; i < state_num; i++)
        {
            P_[i] = new double[state_num];
            for (int j = 0; j <=i; j++)
            {
                    if (i == j) { P_[i][j] = 0; continue; }
                    if (prefenece_martix[i][j] == 1)  //  i>j
                    {
                        P_[i][j] = 1;
                        P_[j][i] = 0;
                    }
                    else if (prefenece_martix[i][j] == 0)
                    {
                        P_[i][j] = 1;
                        P_[j][i] = 1;
                    }
                    else if (prefenece_martix[i][j] == -1)//i<j
                    {
                        P_[i][j] = 0;
                        P_[j][i] = 1;
                    }
                    else
                    {
                        P_[i][j] = 0;
                        P_[j][i] = 0;
                    }
                
               
            }
        }
        Martix M = new Martix(P_);
        return M;
    }

    private Martix get_preferencemartix_increase(int dm_i)   //P+的矩阵
    {

        int state_num = this.model.get_feasible_state();
        int dm_num = model.get_transitions().Length;
        int[][] prefenece_martix = model.get_prefenece_martix(dm_i);
        double[][] P_increase = new double[state_num][];
        for (int i = 0; i < state_num; i++)
        {
            P_increase[i] = new double[state_num];
            for (int j = 0; j <= i; j++)
            {
                if (i == j)
                {
                    P_increase[j][i] = 0;
                    continue;
                }
                else
                {
                    if (prefenece_martix[i][j] == 1)//i<j
                    {
                        P_increase[i][j] = 0;
                        P_increase[j][i] = 1;
                    }
                    else if (prefenece_martix[i][j] == -1)//i<j
                    {
                        P_increase[i][j] = 1;
                        P_increase[j][i] = 0;
                    }
                    else
                    {
                        P_increase[i][j] = 0;
                        P_increase[j][i] = 0;
                    }
                }
            }
        }
        Martix M = new Martix(P_increase);
        return M;
    }

    public void calculate_stability()
    {  
        //E 是全1矩阵
        Martix E=new Martix(state_num);
        //两个偏好矩阵
        Martix P_reduce = this.get_preferencemartix_reduce(dm);     
        Martix P_increase = this.get_preferencemartix_increase(dm);
        
        //决策者i状态转移矩阵
        Martix Ji = this.get_Ji_martix(dm);
        Martix Ji_increase =this.get_Ji_increase_martix(dm);

        //决策者N-i状态转移矩阵
        int[] H = new int[dm_num];
        for (int i = 0; i < dm_num; i++)
        {
            if (dm == i) H[i] = 0;
            else H[i] = 1;
        }
        ;
        Martix MN_i = this.get_MH(H);
        Martix MN_i_increase = this.get_MH_increase(H);
        for (int i = 0; i < state_num; i++)
        {
            MN_i.martix[i][i] = 0;
            MN_i_increase.martix[i][i] = 0;
        }
        Nash = Ji_increase * E;

        GMR = Ji_increase * (E - (MN_i * P_reduce.trans()).sign());

        Martix Q_pre = E - (Ji * P_increase.trans()).sign();
        Martix Q=MN_i*(P_reduce.trans()&Q_pre);
        SMR = Ji_increase * (E - Q.sign());

        Martix W=(MN_i_increase*P_reduce.trans()).sign();
        SEQ = Ji_increase * (E - W);
        
    }
   
}
/*
   private Martix get_N_itrans_martix(int dm_i)    //得到MN-i 的状态转移矩阵
    {   
        int state_num=this.model.get_feasible_state();
        int dm_num=model.get_transitions().Length;
        double[][] pretrans_martix = new double[state_num][];
        int[][][] transitions_martix = model.get_transitions();
        for (int i = 0; i < state_num; i++)
        { 
           pretrans_martix[i]=new double [state_num];
           for (int j = 0; j < state_num; j++)
           {   

               pretrans_martix[i][j]=0;
               int flag = 0;
               for (int dm = 0; dm < dm_num; dm++)
               {
                   if (dm == dm_i) continue;
                   for (int state = 0; state < transitions_martix[dm][i].Length; state++)
                   {
                       if (transitions_martix[dm][i][state] == -1) break;
                       if (transitions_martix[dm][i][state] == j)
                       { 
                           flag = 1;
                           break;
                       }
                       
                   }
                   if (flag == 1) break;
               }
               if (flag == 1) pretrans_martix[i][j] = 1;
           }
        }

        Martix M = new Martix(pretrans_martix);
        return M;
         
    }

    private Martix get_N_itrans_increasmartix(int dm_i)    //得到N-i  +的状态转移矩阵
    {
        int state_num = this.model.get_feasible_state();
        int dm_num = model.get_transitions().Length;
        Martix MN_i_increase = new Martix(state_num,state_num);
        Martix P=new Martix(state_num,state_num);
        for (int i = 0; i < state_num; i++)
        {
            if (i == dm_i) continue;
            else
            { 
               Martix P_increase = this.get_preferencemartix_increase(dm_i);
               P = P | P_increase;
            }
        }
        MN_i_increase = this.get_N_itrans_martix(dm_i) & P;
        return MN_i_increase;
    }

 */