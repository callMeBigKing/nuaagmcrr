using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// SimpleStability 的摘要说明
/// </summary>
public class SimpleStability: BaseStability
{
    public Martix Nash;
    public Martix GMR;
    public Martix SMR;
    public Martix SEQ;
    public SimpleStability(GMCR model, int dm) :base(model,dm)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        Nash = new Martix(state_num, state_num);
        GMR = new Martix(state_num, state_num);
        SMR = new Martix(state_num, state_num);
        SEQ = new Martix(state_num, state_num);
    }

    public void calculate_stability()
    {
        //E 是全1矩阵
        Martix E = new Martix(state_num);//全1 矩阵
        Martix I = new Martix(state_num, true);//单位矩阵

        Martix P_increase = this.get_P(symbol[0]);//p+
        Martix P_reduce = E-I- P_increase;  //p-=
        

        //决策者i状态转移矩阵
        Martix Ji = this.get_J();//J
        Martix Ji_increase = this.get_J(symbol[0],dm);//J+

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

        Nash = Ji_increase * E;

        GMR = Ji_increase * (E - (MN_i * P_reduce.trans()).sign());

        Martix Q_pre = E - (Ji * P_increase.trans()).sign();
        Martix Q = MN_i * (P_reduce.trans() & Q_pre);
        SMR = Ji_increase * (E - Q.sign());

        Martix W = (MN_i_increase * P_reduce.trans()).sign();
        SEQ = Ji_increase * (E - W);

    }




}