using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// StrengthStability 的摘要说明
/// </summary>
/// 计算强度偏好
public class SStability
{

    private GMCR model;  //将整个模型传过来
    public Martix Nash;
    public Martix GMR;
    public Martix SMR;
    public Martix SEQ;
    public Martix SGMR;   //三种强偏好
    public Martix SSMR;
    public Martix SSEQ;
    private int dm_num;  //决策者的个数
    private int state_num; //可行状态个数
    public int dm;  //当前决策者
    private ArrayList M_it_martix;    //  M(N-i)   it 
    private ArrayList M_it_increase_strength_martix;   //  M(N-i) +，++   i t
    public int thr;
    public int thr_increase_strength;

    public SStability(GMCR model, int dm_i)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        this.model = model;
        this.dm = dm_i;
        this.state_num = model.get_feasible_state();
        dm_num = model.get_DM_descrip().Length;
        M_it_martix = new ArrayList();
        M_it_increase_strength_martix = new ArrayList();
        Nash= new Martix(state_num, state_num);
        GMR = new Martix(state_num, state_num);
        SMR = new Martix(state_num, state_num);
        SEQ = new Martix(state_num, state_num);
        SGMR = new Martix(state_num, state_num);
        SSMR = new Martix(state_num, state_num);
        SSEQ = new Martix(state_num, state_num);
        thr = this.get_thr();
        thr_increase_strength = this.get_thr_increase_strength();
        for (int t = 0; t < thr; t++)   //对 M_it_martix list 进行初始化
        {
            ArrayList arr = new ArrayList();
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (t == 0) arr.Add(this.get_Ji_martix(dm));
                else arr.Add(dm);


            }
            M_it_martix.Add(arr);
        }

        for (int t = 0; t < thr_increase_strength; t++)
        {
            ArrayList arr = new ArrayList();
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (t == 0) arr.Add(this.get_Jincrease_strength(dm));
                else arr.Add(dm);
            }
            M_it_increase_strength_martix.Add(arr);
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


    public Martix get_M_it_increase_strength(int dm_i, int t, int[] H)
    {
        ArrayList arr = (ArrayList)this.M_it_increase_strength_martix[t];
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
                Martix M_jt_1 = this.get_M_it_increase_strength(dm, t - 1, H);
                M = M | M_jt_1;
            }
            M = (this.get_Jincrease_strength(dm_i) * M).sign();
            arr[dm_i] = M;
            this.M_it_increase_strength_martix[t] = arr;
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


    public Martix get_MH_increase_strength(int[] H)
    {
        Martix M = new Martix(state_num, state_num);

        for (int t = 0; t < thr_increase_strength; t++)
        {
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (H[dm] == 0) continue;
                else
                {
                    // ArrayList mm = (ArrayList)this.M_it_increase_martix[t];
                    Martix M_jt_1 = this.get_M_it_increase_strength(dm, t, H);
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


    public int get_thr_increase_strength()              // 得到状态转移中边的个数  & +，++
    {
        int thr_increase = 0;
        for (int dm = 0; dm < dm_num; dm++)
        {
            Martix Ji_increase = get_Jincrease_strength(dm);   //统计j++，+ 每个中的边数
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


    public Martix get_Pincrease_strength(int dm_i)   //P+,++的矩阵
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
                    if (prefenece_martix[i][j] == 1|| prefenece_martix[i][j] == 10)//i>j
                    {
                        P_increase[i][j] = 0;
                        P_increase[j][i] = 1;
                    }
                    else if (prefenece_martix[i][j] == -1|| prefenece_martix[i][j] == -10)//i<j
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


    public Martix get_Pstrength(int dm_i)   //P++的矩阵
    {
        int state_num = this.model.get_feasible_state();
        int dm_num = model.get_transitions().Length;
        int[][] prefenece_martix = model.get_prefenece_martix(dm_i);
        double[][] martix = new double[state_num][];
        for (int i = 0; i < state_num; i++)
        {
            martix[i] = new double[state_num];
            for (int j = 0; j <= i; j++)
            {
                if (i == j)
                {
                    martix[j][i] = 0;
                    continue;
                }
                else
                {
                    if (prefenece_martix[i][j] == 10)//i>j
                    {
                        martix[i][j] = 0;
                        martix[j][i] = 1;
                    }
                    else if ( prefenece_martix[i][j] == -10)//i<j
                    {
                        martix[i][j] = 1;
                        martix[j][i] = 0;
                    }
                    else
                    {
                        martix[i][j] = 0;
                        martix[j][i] = 0;
                    }
                }
            }
        }
        Martix M = new Martix(martix);
        return M;
    }
    public Martix get_Pstrengthreduce(int dm_i)   //P--的矩阵
    {
        int state_num = this.model.get_feasible_state();
        int dm_num = model.get_transitions().Length;
        int[][] prefenece_martix = model.get_prefenece_martix(dm_i);
        double[][] martix = new double[state_num][];
        for (int i = 0; i < state_num; i++)
        {
            martix[i] = new double[state_num];
            for (int j = 0; j <= i; j++)
            {
                if (i == j)
                {
                    martix[j][i] = 0;
                    continue;
                }
                else
                {
                    if ( prefenece_martix[i][j] == 10)//i>j
                    {
                        martix[i][j] = 1;
                        martix[j][i] = 0;
                    }
                    else if (prefenece_martix[i][j] == -10)//i<j
                    {
                        martix[i][j] = 0;
                        martix[j][i] = 1;
                    }
                    else
                    {
                        martix[i][j] = 0;
                        martix[j][i] = 0;
                    }
                }
            }
        }
        Martix M = new Martix(martix);
        return M;
    }

    public Martix get_Preduce(int dm_i)  //p-,=,-- 矩阵
    {
        Martix E = new Martix(state_num); //全1矩阵
        Martix I = new Martix(state_num, true);//单位矩阵
        return E - I - this.get_Pincrease_strength(dm_i);   //在这里之前的定义是有问题的，要保证对角线全为0； 而按照定义对角线相等为1；
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


    public Martix get_Jincrease_strength(int dm_i)    //J +,++  矩阵
    {
        Martix P_increase = this.get_Pincrease_strength(dm_i);
        //J+ ，++决策者i状态转移矩阵
        Martix Ji = this.get_Ji_martix(dm_i);
        Martix Ji_increase = Ji & P_increase;
        return Ji_increase;
    }


    public void calculate_stability()
    {

        Martix E = new Martix(state_num);   //全1矩阵
        Martix Preduce = this.get_Preduce(dm);  //p-,=,--
        Martix PSreduce = this.get_Pstrengthreduce(dm);   //p--
        Martix Pstrength = this.get_Pstrength(dm); //P ++;
        Martix Pincrease_strength = this.get_Pincrease_strength(dm); //p +,++

        Martix Ji = this.get_Ji_martix(dm);
        Martix Jincrease_strength = this.get_Jincrease_strength(dm); //J,+，++

        int[] H = new int[dm_num];
        for (int i = 0; i < dm_num; i++)   //联合
        {
            if (dm == i) H[i] = 0;
            else H[i] = 1;
        }

        Martix MN_i = this.get_MH(H);   //M  N-i
        Martix MH_increase_strength = this.get_MH_increase_strength(H);  //M+,++  N-i;



        Nash = Jincrease_strength * E;
        GMR = Jincrease_strength * (E - (Ji * Preduce.trans()).sign());
        Martix G = Preduce.trans() & (E - (Ji * Pincrease_strength.trans()).sign());
        SMR = Jincrease_strength * (E - (Ji * G).sign());
        SEQ = Jincrease_strength * (E - (Jincrease_strength * Preduce.trans()).sign());
        SGMR= Jincrease_strength * (E - (Ji * PSreduce.trans()).sign());

        Martix SG = Pstrength & (E - (Ji * (E - Pstrength)).sign());
        SSMR = Jincrease_strength * (E - (Ji * SG).sign());
        SSEQ= Jincrease_strength * (E - (Jincrease_strength * PSreduce.trans()).sign());
    }
}