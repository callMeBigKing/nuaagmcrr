using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;

/// <summary>
/// Ustability 的摘要说明
/// </summary>
public class Ustability
{
    public GMCR model;
    public Martix Nash_a;
    public Martix Nash_b;
    public Martix Nash_c;
    public Martix Nash_d;

    public Martix GMR_a;
    public Martix GMR_b;
    public Martix GMR_c;
    public Martix GMR_d;

    public Martix SMR_a;
    public Martix SMR_b;
    public Martix SMR_c;
    public Martix SMR_d;

    public Martix SEQ_a;
    public Martix SEQ_b;
    public Martix SEQ_c;
    public Martix SEQ_d;

    public ArrayList M_it_martix;
    public ArrayList M_it_increase_martix;
    public ArrayList M_it_Uincrease_martix;

    public int thr;
    public int thr_increase;
    public int thr_Uincrease;

    public int dm;     //决策者
    public int state_num;     //可行状态个数
    private int dm_num;        //决策者的个数

    public Ustability(GMCR model, int dm_i)
	{
        this.model = model;
        this.dm = dm_i;
        this.state_num = model.get_feasible_state();
        dm_num=model.get_DM_descrip().Length;
        M_it_martix = new ArrayList();
        M_it_increase_martix = new ArrayList();
        M_it_Uincrease_martix = new ArrayList();
        Nash_a = new Martix(state_num, state_num); Nash_b = new Martix(state_num, state_num); Nash_c = new Martix(state_num, state_num); Nash_d = new Martix(state_num, state_num);
        GMR_a = new Martix(state_num, state_num); GMR_b = new Martix(state_num, state_num); GMR_c = new Martix(state_num, state_num); GMR_d = new Martix(state_num, state_num);
        SMR_a = new Martix(state_num, state_num); SMR_b = new Martix(state_num, state_num); SMR_c = new Martix(state_num, state_num); SMR_d = new Martix(state_num, state_num);
        SEQ_a = new Martix(state_num, state_num); SEQ_b = new Martix(state_num, state_num); SEQ_c = new Martix(state_num, state_num); SEQ_d = new Martix(state_num, state_num);
        thr = this.get_thr();
        thr_increase = this.get_thr_increase();
        thr_Uincrease = this.get_thr_Uincrease();
        for (int t = 0; t < thr; t++)   //对 M_it_martix list 进行初始化
        {
            ArrayList arr = new ArrayList();
            for (int dm = 0; dm <dm_num; dm++)
            {
                if (t == 0) arr.Add(this.get_Ji_martix(dm));
                else arr.Add(dm);


            }
            M_it_martix.Add(arr);
        }
        for (int t = 0; t < thr_increase; t++)
        {
            ArrayList arr = new ArrayList();
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (t == 0) arr.Add(this.get_Ji_increase_martix(dm));
                else arr.Add(dm);
            }
            M_it_increase_martix.Add(arr);
        }

        for (int t = 0; t < thr_Uincrease; t++)
        {
            ArrayList arr = new ArrayList();
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (t == 0) arr.Add(this.get_Ji_Uincrease_martix(dm));
                else arr.Add(dm);
            }
            M_it_Uincrease_martix.Add(arr);
        }
	}

    public Martix get_M_it(int dm_i, int t, int[] H)
    {
        //  if (t == 0) return this.get_Ji_martix(dm_i);

        ArrayList arr = (ArrayList)this.M_it_martix[t];
        try
        {
            Martix M = (Martix)arr[dm_i];
            return M;
        }
        catch
        {
            Martix M = new Martix(state_num, state_num);  //M 是全0矩阵
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (H[dm] == 0) continue;
                if (dm == dm_i) continue;
                Martix M_jt_1 = get_M_it(dm, t - 1, H);
                M = M | M_jt_1;
            }
            M = (this.get_Ji_martix(dm_i) * M).sign();
            arr[dm_i] = M;
            this.M_it_martix[t] = arr;
            return M;

        }

    }

    public Martix get_M_it_increase(int dm_i, int t, int[] H)
    {
        ArrayList arr = (ArrayList)this.M_it_increase_martix[t];
        try
        {
            Martix M = (Martix)arr[dm_i];
            return M;
        }
        catch
        {
            int dm_num = model.get_DM_descrip().Length;
            int state_num = model.get_feasible_state();
            Martix M = new Martix(state_num, state_num);          //M 初始为全0矩阵
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (H[dm] == 0) continue;
                if (dm == dm_i) continue;
                Martix M_jt_1 = this.get_M_it_increase(dm, t - 1, H);
                M = M | M_jt_1;
            }
            M = (this.get_Ji_increase_martix(dm_i) * M).sign();
            arr[dm_i] = M;
            this.M_it_increase_martix[t] = arr;
            return M;
        }
    }

    public Martix get_M_it_Uincrease(int dm_i, int t, int[] H)
    {
        //   if (t == 0) return this.get_Ji_increase_martix(dm_i);
        ArrayList arr = (ArrayList)this.M_it_Uincrease_martix[t];
        try
        {
            Martix M = (Martix)arr[dm_i];
            return M;
        }
        catch
        {
            Martix M = new Martix(state_num, state_num);          //M 初始为全0矩阵
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (H[dm] == 0) continue;
                if (dm == dm_i) continue;
                Martix M_jt_1 = this.get_M_it_Uincrease(dm, t - 1, H);
                M = M | M_jt_1;
            }
            M = (this.get_Ji_Uincrease_martix(dm_i) * M).sign();
            arr[dm_i] = M;
            this.M_it_Uincrease_martix[t] = arr;
            return M;
        }
    }

    public Martix get_MH(int[] H)
    {   //∨暂时记为H 中为1的个数
        //H 为dm_num维的向量，被选中则为1否则为0
        Martix M = new Martix(state_num, state_num);
        for (int t = 0; t < thr; t++)
        {
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (H[dm] == 0) continue;

                else
                {
                    //ArrayList mm = (ArrayList)this.M_it_martix[t];
                    Martix M_jt_1 = this.get_M_it(dm, t, H);
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

    public Martix get_MH_increase(int[] H)
    {
        Martix M = new Martix(state_num, state_num);

        for (int t = 0; t < thr_increase; t++)
        {
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (H[dm] == 0) continue;
                else
                {
                    // ArrayList mm = (ArrayList)this.M_it_increase_martix[t];
                    Martix M_jt_1 = this.get_M_it_increase(dm, t, H);
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

    public Martix get_MH_Uincrease(int[] H)
    {
        Martix M = new Martix(state_num, state_num);
        for (int t = 0; t < thr_Uincrease; t++)
        {
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (H[dm] == 0) continue;

                else
                {
                    // ArrayList mm = (ArrayList)this.M_it_increase_martix[t];
                    Martix M_jt_1 = this.get_M_it_Uincrease(dm, t, H);
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

    public int get_thr()              // 得到&    即状态转移图中边的总数
    {
        int thr = 0;
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

    public int get_thr_increase()              // 得到&+
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

    public int get_thr_Uincrease()              // 得到&+U
    {
        int thr_Uincrease = 0;
        for (int dm = 0; dm < dm_num; dm++)
        {
            // Martix P_increase = this.get_preferencemartix_increase(dm);
            //Martix Ji = this.get_Ji_martix(dm);
            Martix Ji_Uincrease = this.get_Ji_Uincrease_martix(dm);
            
            for (int i = 0; i < state_num; i++)
            {
                for (int j = 0; j < state_num; j++)
                {
                    if (Ji_Uincrease.martix[i][j] == 1) thr_Uincrease++;
                }
            }
        }
        return thr_Uincrease;
    }


    public Martix get_Ji_martix(int dm_i)    //得到i的状态转移矩阵
    {
        int state_num = model.get_feasible_state();
        double[][] a = new double[state_num][];
        for (int i = 0; i < state_num; i++)
        {
            a[i] = new double[state_num];
            for (int j = 0; j < state_num; j++)
            {

                a[i][j] = model.get_trans_marix(dm_i)[i][j];
            }
        }

        Martix M = new Martix(a);
        return M;
    }

    public Martix get_Ji_increase_martix(int dm_i)
    {
        Martix P_increase = this.get_preferencemartix_increase(dm_i);

        //J+ 决策者i状态转移矩阵
        Martix Ji = this.get_Ji_martix(dm_i);
        Martix Ji_increase = Ji & P_increase;
        return Ji_increase;
    }

    public Martix get_Ji_Uincrease_martix(int dm_i)
    {   
        Martix P_increase = this.get_preferencemartix_increase(dm_i);
        Martix P_Uincrease= this.get_preferencemartix_Uincrease(dm_i);
        //决策者i  J(U +) 状态转移矩阵
        Martix Ji = this.get_Ji_martix(dm_i);
        Martix Ji_Uincrease = Ji & (P_increase | P_Uincrease);
        return Ji_Uincrease;
    }

    public Martix get_preferencemartix_reduce(int dm_i)   //P-=的矩阵
    {
        int[][] prefenece_martix = model.get_prefenece_martix(dm_i);
        double[][] P_ = new double[state_num][];
        for (int i = 0; i < state_num; i++)
        {
            P_[i] = new double[state_num];
            for (int j = 0; j <= i; j++)
            {
                if (i == j) { P_[i][j] = 0; continue; }
                if (prefenece_martix[i][j] ==1 )  //  i>j
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

    public Martix get_preferencemartix_increase(int dm_i)   //P+的矩阵
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

    public Martix get_preferencemartix_Uincrease(int dm_i)  //p+U 矩阵
    {
        Martix E = new Martix(state_num); //全1矩阵
        Martix I = new Martix(state_num, true);//单位矩阵

        return E  -I- this.get_preferencemartix_reduce(dm_i);    //错误表达式和正确表达式的结果相同
    }

   public Martix get_preferencemartix_Ureduce(int dm_i)  //P-=U的矩阵
    {
        Martix E = new Martix(state_num); //全1矩阵
        Martix I = new Martix(state_num, true);//单位矩阵
        return E -I-this.get_preferencemartix_increase(dm_i);
    }

    public void calculate_stability()
    {

        Martix E = new Martix(state_num);   //全1矩阵
        //偏好矩阵
        Martix P_reduce = this.get_preferencemartix_reduce(dm); //p-=
        Martix P_Ureduce = this.get_preferencemartix_Ureduce(dm);//p-=u
        Martix P_increase = this.get_preferencemartix_increase(dm);//p+
        Martix P_Uincrease = this.get_preferencemartix_Uincrease(dm);//p+u
        /*
        for (int i = 0; i < state_num; i++)
        { 
          P_reduce.martix[i][i]=0;
          P_Ureduce.martix[i][i] = 0;
          P_increase.martix[i][i] = 0;
          P_Uincrease.martix[i][i] = 0;
        }
        */
        //决策者i状态转移矩阵
        Martix Ji = this.get_Ji_martix(dm);
        Martix Ji_increase = this.get_Ji_increase_martix(dm);
        Martix Ji_Uincrease = this.get_Ji_Uincrease_martix(dm);
        //决策者N-i状态转移矩阵
        int[] H = new int[dm_num];
        for (int i = 0; i < dm_num; i++)   //联合
        {
            if (dm == i) H[i] = 0;
            else H[i] = 1;
        }
        
        Martix MN_i = this.get_MH(H);
        Martix MN_i_increase = this.get_MH_increase(H);
        Martix MN_i_Uincrease = this.get_MH_Uincrease(H);
        ;

        Nash_a = Ji_Uincrease * E;
        Nash_b = Ji_increase * E;
        Nash_c = Nash_a;
        Nash_d = Nash_b;

        GMR_a = Ji_Uincrease * (E - (MN_i * P_reduce.trans()).sign());
        GMR_b = Ji_increase * (E - (MN_i * P_reduce.trans()).sign());
        GMR_c = Ji_Uincrease * (E - (MN_i * P_Ureduce.trans()).sign());
        GMR_d = Ji_increase * (E - (MN_i * P_Ureduce.trans()).sign());
        
        Martix W_a = P_reduce.trans() & (E - (Ji * P_Uincrease.trans()).sign());
        SMR_a = Ji_Uincrease * (E - (MN_i*W_a).sign());
        Martix W_b = P_reduce.trans() & (E - (Ji * P_Uincrease.trans()).sign());
        SMR_b = Ji_increase * (E - (MN_i * W_b).sign());
        Martix W_c = P_Ureduce.trans() & (E - (Ji * P_increase.trans()).sign());
        SMR_c = Ji_Uincrease * (E - (MN_i * W_c).sign());
        Martix W_d = P_Ureduce.trans() & (E - (Ji * P_increase.trans()).sign());
        SMR_d = Ji_increase * (E - (MN_i * W_d).sign());
        
        SEQ_a = Ji_Uincrease * (E - (MN_i_Uincrease * P_reduce.trans()).sign());
        SEQ_b = Ji_increase * (E - (MN_i_Uincrease * P_reduce.trans()).sign());
        SEQ_c = Ji_Uincrease * (E - (MN_i_Uincrease * P_Ureduce.trans()).sign());
        SEQ_d = Ji_increase * (E - (MN_i_Uincrease * P_Ureduce.trans()).sign());

    }
}