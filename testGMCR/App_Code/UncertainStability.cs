using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// UncertainStability 的摘要说明
/// </summary>
public class UncertainStability: BaseStability
{

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

    public int thr_Uincrease;
    public ArrayList M_it_Uincrease_martix;   // Mit +,u
    public UncertainStability(GMCR model, int dm_i) : base(model, dm_i)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        Nash_a = new Martix(state_num, state_num); Nash_b = new Martix(state_num, state_num); Nash_c = new Martix(state_num, state_num); Nash_d = new Martix(state_num, state_num);
        GMR_a = new Martix(state_num, state_num); GMR_b = new Martix(state_num, state_num); GMR_c = new Martix(state_num, state_num); GMR_d = new Martix(state_num, state_num);
        SMR_a = new Martix(state_num, state_num); SMR_b = new Martix(state_num, state_num); SMR_c = new Martix(state_num, state_num); SMR_d = new Martix(state_num, state_num);
        SEQ_a = new Martix(state_num, state_num); SEQ_b = new Martix(state_num, state_num); SEQ_c = new Martix(state_num, state_num); SEQ_d = new Martix(state_num, state_num);

        thr_Uincrease = this.get_thr_Uincrease();

        M_it_Uincrease_martix = new ArrayList();
        for (int t = 0; t < thr_Uincrease; t++)
        {
            ArrayList arr = new ArrayList();
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (t == 0) arr.Add(this.get_J(symbol[3],dm));
                else arr.Add(dm);
            }
            M_it_Uincrease_martix.Add(arr);
        }
    }

    public int get_thr_Uincrease()              // 得到&+U
    {
        int thr_Uincrease = 0;
        for (int dm = 0; dm < dm_num; dm++)
        {
            // Martix P_increase = this.get_preferencemartix_increase(dm);
            //Martix Ji = this.get_Ji_martix(dm);
            Martix Ji_Uincrease = this.get_J(symbol[3],dm);

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
            M = (this.get_J(symbol[3],dm_i) * M).sign();
            arr[dm_i] = M;
            this.M_it_Uincrease_martix[t] = arr;
            return M;
        }
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


    public void calculate_stability()
    {

        Martix E = new Martix(state_num);   //全1矩阵
        Martix I = new Martix(state_num, true); //单位矩阵
        //偏好矩阵
        Martix P_increase = this.get_P(symbol[0]);//p+
        Martix P_Uincrease = this.get_P(symbol[3]);//p+u
        Martix P_reduce = E-I- P_Uincrease; //p-=
        Martix P_Ureduce = E-I- P_increase;//p-=u
        
        
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
        Martix Ji = this.get_J(); //J
        Martix Ji_increase = this.get_J(symbol[0]); //J+
        Martix Ji_Uincrease = Ji& P_Uincrease;//J+,U
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
        SMR_a = Ji_Uincrease * (E - (MN_i * W_a).sign());
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