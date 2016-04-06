using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// StrengthStability 的摘要说明
/// </summary>
public class StrengthStability:BaseStability
{
    public Martix Nash;
    public Martix GMR;
    public Martix SMR;
    public Martix SEQ;
    public Martix SGMR;   //三种强偏好
    public Martix SSMR;
    public Martix SSEQ;
    private ArrayList M_it_increase_strength;   //  M +，++   i t
    public int thr_increase_strength;
    public StrengthStability(GMCR model, int dm_i) : base(model, dm_i)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        Nash = new Martix(state_num, state_num);
        GMR = new Martix(state_num, state_num);
        SMR = new Martix(state_num, state_num);
        SEQ = new Martix(state_num, state_num);
        SGMR = new Martix(state_num, state_num);
        SSMR = new Martix(state_num, state_num);
        SSEQ = new Martix(state_num, state_num);
        M_it_increase_strength = new ArrayList();
        ///
        for (int t = 0; t < thr_increase_strength; t++)
        {
            ArrayList arr = new ArrayList();
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (t == 0) arr.Add(this.get_J(symbol[2],dm));   
                else arr.Add(dm);
            }
            M_it_increase_strength.Add(arr);
        }
    }

    public Martix get_M_it_increase_strength(int dm_i, int t, int[] H)     //Mit +,++
    {
        ArrayList arr = (ArrayList)this.M_it_increase_strength[t];
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
            M = (this.get_J(symbol[2],dm_i) * M).sign();
            arr[dm_i] = M;
            this.M_it_increase_strength[t] = arr;
            return M;
        }
    }

    public Martix get_MH_increase_strength(int[] H)  //MH +,++
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

    public int get_thr_increase_strength()              // 得到状态转移中边的个数  & +，++
    {
        int thr_increase = 0;
        for (int dm = 0; dm < dm_num; dm++)
        {
            Martix Ji_increase = get_J(symbol[2],dm);   //统计j++，+ 每个中的边数
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


    public void calculate_stability()
    {

        Martix E = new Martix(state_num);//全1 矩阵
        Martix I = new Martix(state_num, true);//单位矩阵

        Martix Pstrength = this.get_P(symbol[1]); //P ++;
        Martix Pincrease_strength = this.get_P(symbol[2]); //p +,++
        Martix Preduce = E - I - Pincrease_strength;  //p-,=,--

        Martix PSreduce = this.get_P(new []{ -10});   //p--
        
        

        Martix Ji = this.get_J();//J
        Martix Jincrease_strength = this.get_J(symbol[2]); //J,+，++

        int[] H = new int[dm_num];
        for (int i = 0; i < dm_num; i++)   //联合
        {
            if (dm == i) H[i] = 0;
            else H[i] = 1;
        }

        Martix MN_i = this.get_MH(H);   //M  N-i
        Martix MH_increase_strength = this.get_MH_increase_strength(H);  //M+,++  N-i;


        Nash = Jincrease_strength * E;
        GMR = Jincrease_strength * (E - (MN_i * Preduce.trans()).sign());
        Martix G = Preduce.trans() & (E - (Ji * Pincrease_strength.trans()).sign());
        SMR = Jincrease_strength * (E - (MN_i * G).sign());
        SEQ = Jincrease_strength * (E - (MH_increase_strength * Preduce.trans()).sign());
        ////
        SGMR = Jincrease_strength * (E - (MN_i * PSreduce.trans()).sign());
        Martix SG = Pstrength & (E - (Ji * (E - Pstrength)).sign());
        SSMR = Jincrease_strength * (E - (MN_i * SG).sign());

        SSEQ = Jincrease_strength * (E - (MH_increase_strength * PSreduce.trans()).sign());
    }
}