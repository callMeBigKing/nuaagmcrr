using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// HybridStability 的摘要说明
/// 计算某个决策者的混合偏好
/// </summary>
public class HybridStability: BaseStability
{
    /*
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
    
    /// /强稳定性
    /// </summary>


    public Martix SGMR_a;
    public Martix SGMR_b;
    public Martix SGMR_c;
    public Martix SGMR_d;

    public Martix SSMR_a;
    public Martix SSMR_b;
    public Martix SSMR_c;
    public Martix SSMR_d;

    public Martix SSEQ_a;
    public Martix SSEQ_b;
    public Martix SSEQ_c;
    public Martix SSEQ_d;
    */

    // 太多了就不一个个用变量名了，变量名太多会导致后面数据处理代码及其冗长，统一放到一个list中去
    public ArrayList martix;
    public int thr_USincrease;   //+，++，u
    public ArrayList M_it_USincrease_martix;   // Mit +,++，u



    public HybridStability(GMCR model, int dm_i) : base(model, dm_i)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        /*
        Nash_a = new Martix(state_num, state_num); Nash_b = new Martix(state_num, state_num); Nash_c = new Martix(state_num, state_num); Nash_d = new Martix(state_num, state_num);
        GMR_a = new Martix(state_num, state_num); GMR_b = new Martix(state_num, state_num); GMR_c = new Martix(state_num, state_num); GMR_d = new Martix(state_num, state_num);
        SMR_a = new Martix(state_num, state_num); SMR_b = new Martix(state_num, state_num); SMR_c = new Martix(state_num, state_num); SMR_d = new Martix(state_num, state_num);
        SEQ_a = new Martix(state_num, state_num); SEQ_b = new Martix(state_num, state_num); SEQ_c = new Martix(state_num, state_num); SEQ_d = new Martix(state_num, state_num);
        //
        SGMR_a = new Martix(state_num, state_num); SGMR_b = new Martix(state_num, state_num); SGMR_c = new Martix(state_num, state_num); SGMR_d = new Martix(state_num, state_num);
        SSMR_a = new Martix(state_num, state_num); SSMR_b = new Martix(state_num, state_num); SSMR_c = new Martix(state_num, state_num); SSMR_d = new Martix(state_num, state_num);
        SSEQ_a = new Martix(state_num, state_num); SSEQ_b = new Martix(state_num, state_num); SSEQ_c = new Martix(state_num, state_num); SSEQ_d = new Martix(state_num, state_num);
        //
         * */
        M_it_USincrease_martix = new ArrayList();
        martix = new ArrayList();
        thr_USincrease = this.get_thr_USincrease();

        for (int t = 0; t < thr_USincrease; t++)
        {
            ArrayList arr = new ArrayList();
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (t == 0) arr.Add(this.get_J(symbol[4], dm));
                else arr.Add(dm);
            }
            M_it_USincrease_martix.Add(arr);
        }
    }


    public int get_thr_USincrease()              // 得到&+U
    {
        int thr = 0;
        for (int dm = 0; dm < dm_num; dm++)
        {
            // Martix P_increase = this.get_preferencemartix_increase(dm);
            //Martix Ji = this.get_Ji_martix(dm);
            Martix Ji_Uincrease = this.get_J(symbol[4], dm);

            for (int i = 0; i < state_num; i++)
            {
                for (int j = 0; j < state_num; j++)
                {
                    if (Ji_Uincrease.martix[i][j] == 1) thr++;
                }
            }
        }
        return thr;
    }

    public Martix get_M_it_USincrease(int dm_i, int t, int[] H)
    {
        //   if (t == 0) return this.get_Ji_increase_martix(dm_i);
        ArrayList arr = (ArrayList)this.M_it_USincrease_martix[t];
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
                Martix M_jt_1 = this.get_M_it_USincrease(dm, t - 1, H);
                M = M | M_jt_1;
            }
            M = (this.get_J(symbol[4], dm_i) * M).sign();
            arr[dm_i] = M;
            this.M_it_USincrease_martix[t] = arr;
            return M;
        }
    }

    public Martix get_MH_USincrease(int[] H)
    {
        Martix M = new Martix(state_num, state_num);
        for (int t = 0; t < thr_USincrease; t++)
        {
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (H[dm] == 0) continue;

                else
                {
                    // ArrayList mm = (ArrayList)this.M_it_increase_martix[t];
                    Martix M_jt_1 = this.get_M_it_USincrease(dm, t, H);
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
        Martix P_Sincrease = this.get_P(symbol[2]);//p+,++
        Martix P_USincrease = this.get_P(symbol[4]);//p+,++,u
        Martix P_reduce = E - I - P_USincrease; //p-,--,=
        Martix P_Ureduce = E - I - P_Sincrease;//p-,--,=,u

        Martix P_Sinc=this.get_P(new []{10});//p++,
        Martix P_USinc=this.get_P(new []{10,2});//p++,u
        Martix P_Sred = this.get_P(new []{-10});//p--,
        Martix P_USred = this.get_P(new []{-10,2});//p--,U
        //状态转移矩阵
         Martix J = this.get_J(); //J
        Martix J_Sincrease = this.get_J(symbol[2]); //J +,++
        Martix J_USincrease = this.get_J(symbol[4]); //J +,++,U

        int[] H = new int[dm_num];
        for (int i = 0; i < dm_num; i++)   //联合
        {
            if (dm == i) H[i] = 0;
            else H[i] = 1;
        }

        Martix MN_i = this.get_MH(H);
        Martix MN_i_increase = this.get_MH_increase(H);
        Martix MN_i_Uincrease = this.get_MH_USincrease(H);
        ;
        /*
        //nash
        this.martix.Add(J_USincrease * E);//Nash_a
        this.martix.Add(J_Sincrease * E);//Nash_b
        this.martix.Add(J_USincrease * E);//Nash_c
        this.martix.Add(J_Sincrease * E);//Nash_d
        //GMR
        this.martix.Add(J_USincrease * (E - (MN_i * P_reduce.trans()).sign()));//GMR_a
        this.martix.Add(J_Sincrease * (E - (MN_i * P_reduce.trans()).sign()));//GMR_b
        this.martix.Add(J_USincrease * (E - (MN_i * P_reduce.trans()).sign()));//GMR_c
        this.martix.Add(J_Sincrease * (E - (MN_i * P_Ureduce.trans()).sign()));//GMR_d

         //SMR
        this.martix.Add(J_USincrease * (E - (MN_i * (P_USincrease & (E - (J * P_USincrease.trans()).sign()))).sign()));//SMR_a
        this.martix.Add(J_Sincrease * (E - (MN_i * (P_USincrease & (E - (J * P_Sincrease.trans()).sign()))).sign()));//SMR_b
        this.martix.Add(J_USincrease * (E - (MN_i * (P_Sincrease & (E - (J * P_Sincrease.trans()).sign()))).sign()));//SMR_c
        this.martix.Add(J_Sincrease * (E - (MN_i * (P_USincrease.trans() & (E - (J * P_Sincrease.trans()).sign()))).sign()));//SMR_d
        //SEQ
        this.martix.Add(J_USincrease * (E - (MN_i_Uincrease * P_reduce.trans()).sign()));//SEQ_a
        this.martix.Add(J_Sincrease * (E - (MN_i_Uincrease * P_reduce.trans()).sign()));//SEQ_b
        this.martix.Add(J_USincrease * (E - (MN_i_Uincrease * P_Ureduce.trans()).sign()));//SEQ_c
        this.martix.Add(J_Sincrease * (E - (MN_i_Uincrease * P_Ureduce.trans()).sign()));//SEQ_d
        
        //SGMR
        this.martix.Add(J_USincrease * (E - (MN_i * P_Sred.trans()).sign()));//SGMR_a
        this.martix.Add(J_Sincrease * (E - (MN_i * P_Sred.trans()).sign()));//SGMR_b
        this.martix.Add(J_USincrease * (E - (MN_i * P_USred.trans()).sign()));//SGMR_c
        this.martix.Add(J_Sincrease * (E - (MN_i * P_USred.trans()).sign()));//SGMR_d
        //
         * 
         */



        Martix Nash_a = J_USincrease * E;
        Martix Nash_b = J_Sincrease * E;
        Martix Nash_c = Nash_a;
        Martix Nash_d = Nash_b;
        this.martix.Add(Nash_a);
        this.martix.Add(Nash_b);
        this.martix.Add(Nash_c);
        this.martix.Add(Nash_d); 


        Martix GMR_a = J_USincrease * (E - (MN_i * P_reduce.trans()).sign());
        Martix GMR_b = J_Sincrease * (E - (MN_i * P_reduce.trans()).sign());
        Martix GMR_c = J_USincrease * (E - (MN_i * P_Ureduce.trans()).sign());
        Martix GMR_d = J_Sincrease * (E - (MN_i * P_Ureduce.trans()).sign());
        this.martix.Add(GMR_a);
        this.martix.Add(GMR_b);
        this.martix.Add(GMR_c);
        this.martix.Add(GMR_d); 


        Martix SMR_a = J_USincrease * (E - (MN_i * (P_USincrease & (E - (J * P_USincrease.trans()).sign()))).sign());
        Martix SMR_b = J_Sincrease * (E - (MN_i * (P_USincrease & (E - (J * P_Sincrease.trans()).sign()))).sign());
        Martix SMR_c = J_USincrease * (E - (MN_i * (P_Sincrease & (E - (J * P_Sincrease.trans()).sign()))).sign());
        Martix SMR_d = J_Sincrease * (E - (MN_i * (P_USincrease.trans() & (E - (J * P_Sincrease.trans()).sign()))).sign());
        this.martix.Add(SMR_a);
        this.martix.Add(SMR_b);
        this.martix.Add(SMR_c);
        this.martix.Add(SMR_d);


        Martix SEQ_a = J_USincrease * (E - (MN_i_Uincrease * P_reduce.trans()).sign());
        Martix SEQ_b = J_Sincrease * (E - (MN_i_Uincrease * P_reduce.trans()).sign());
        Martix SEQ_c = J_USincrease * (E - (MN_i_Uincrease * P_Ureduce.trans()).sign());
        Martix SEQ_d = J_Sincrease * (E - (MN_i_Uincrease * P_Ureduce.trans()).sign());
        this.martix.Add(SEQ_a);
        this.martix.Add(SEQ_b);
        this.martix.Add(SEQ_c);
        this.martix.Add(SEQ_d);


        Martix SGMR_a = J_USincrease * (E - (MN_i * P_Sred.trans()).sign());
        Martix SGMR_b = J_Sincrease * (E - (MN_i * P_Sred.trans()).sign());
        Martix SGMR_c = J_USincrease * (E - (MN_i * P_USred.trans()).sign());
        Martix SGMR_d = J_Sincrease * (E - (MN_i * P_USred.trans()).sign());
        this.martix.Add(SGMR_a);
        this.martix.Add(SGMR_b);
        this.martix.Add(SGMR_c);
        this.martix.Add(SEQ_d);

        Martix SSMR_a = J_USincrease * (E - (MN_i * (P_USred & (E - (J * P_Sred.trans()).sign()))).sign());
        Martix SSMR_b = J_Sincrease * (E - (MN_i * (P_USred.trans() & (E - (J * P_Sred.trans()).sign()))).sign());
        Martix SSMR_c = J_USincrease * (E - (MN_i * (P_Sred & (E - (J * P_Sred.trans()).sign()))).sign());
        Martix SSMR_d = J_Sincrease * (E - (MN_i * (P_Sred & (E - (J * P_Sred.trans()).sign()))).sign());
        this.martix.Add(SSMR_a);
        this.martix.Add(SSMR_b);
        this.martix.Add(SSMR_c);
        this.martix.Add(SSMR_d);


        Martix SSEQ_a = J_USincrease * (E - (MN_i_Uincrease * P_Sred.trans()).sign());
        Martix SSEQ_b = J_Sincrease * (E - (MN_i_Uincrease * P_Sred.trans()).sign());
        Martix SSEQ_c = J_USincrease * (E - (MN_i_Uincrease * P_USred.trans()).sign());
        Martix SSEQ_d = J_Sincrease * (E - (MN_i_Uincrease * P_USred.trans()).sign());
        this.martix.Add(SSEQ_a);
        this.martix.Add(SSEQ_b);
        this.martix.Add(SSEQ_c);
        this.martix.Add(SSEQ_d);

         
    }


}