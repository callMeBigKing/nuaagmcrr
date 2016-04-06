using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Class1 的摘要说明
/// </summary>
public class BaseStability
{
    //计算稳定性只用到了偏好和状态转移矩阵所以用着
    protected GMCR model;   //模型
    protected int dm;   //当前决策者
    protected int dm_num;  //决策者个数
    protected int state_num;//状态个数
    //者两个是可以作用于所有的dm 可以设置为更高一级别的常量
    protected int thr;   // 总的边数
    protected int thr_increase;//  + 强偏好的边数
    //用来存储Mit 增加递归效率
    protected ArrayList M_it;   //mit
    protected ArrayList M_it_increase;// m + it
    public int[][] symbol;

    public BaseStability(GMCR model, int dm_i)
    {   //初始化symbol
        symbol = new int[5][];
        symbol[0] = new int[] { 1}; //+
        symbol[1] = new int[] {  10 }; //++
        symbol[2] = new int[] { 1 ,10}; //+,++
        symbol[3] = new int[] { 1, 2 };//u,+
        symbol[4] = new int[] { 1, 2, 10 };//+,++,U
        ////
        this.model = model;
        this.dm = dm_i;
        this.dm_num = model.get_DM_descrip().Length;
        this.state_num = model.get_feasible_state();
        thr = this.get_thr();
        thr_increase = this.get_thr_increase();


        M_it= new ArrayList();
        M_it_increase= new ArrayList();
        for (int t = 0; t < thr; t++)   //对 M_it_martix list 进行初始化
        {
            ArrayList arr = new ArrayList();
            for (int dm = 0; dm < model.get_DM_descrip().Length; dm++)
            {
                if (t == 0) arr.Add(this.get_J(dm));
                else arr.Add(dm);
            }
            M_it.Add(arr);
        }
        ///
        
        for (int t = 0; t < thr_increase; t++)
        {
            ArrayList arr = new ArrayList();
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (t == 0) arr.Add(this.get_J(symbol[0], dm));
                else arr.Add(dm);
            }
            M_it_increase.Add(arr);
        }
    }

    protected Martix get_M_it(int dm_i, int t, int[] H)
    {

        ArrayList arr = (ArrayList)this.M_it[t];
        try
        {
            Martix M = (Martix)arr[dm_i];
            return M;
        }
        catch
        {
            Martix M = new Martix(state_num, state_num);
            for (int dm = 0; dm < dm_num; dm++)
            {
                if (H[dm] == 0) continue;
                if (dm == dm_i) continue;
                Martix M_jt_1 = get_M_it(dm, t - 1, H);
                M = M | M_jt_1;
            }
            M = (this.get_J(dm_i) * M).sign();
            arr[dm_i] = M;
            this.M_it[t] = arr;
            return M;
        }

    }

    protected Martix get_M_it_increase(int dm_i, int t, int[] H)
    {

        ArrayList arr = (ArrayList)this.M_it_increase[t];
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
                Martix M_jt_1 = this.get_M_it_increase(dm, t - 1, H);
                M = M | M_jt_1;
            }
            M = (this.get_J(symbol[0],dm_i) * M).sign();
            arr[dm_i] = M;
            this.M_it_increase[t] = arr;
            return M;
            
        }
    }


    public Martix get_MH(int[] H)
    {
        Martix M = new Martix(state_num, state_num);
        for (int t = 0; t < thr; t++)
        {

            for (int dm = 0; dm < dm_num; dm++)
            {
                if (H[dm] == 0) continue;
                else
                {
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


    protected int get_thr()              // 得到&
    {

        int thr = 0;
        for (int dm = 0; dm < dm_num; dm++)
        {
            Martix J = this.get_J(dm);
            for (int i = 0; i < state_num; i++)
            {
                for (int j = 0; j < state_num; j++)
                {
                    if (J.martix[i][j] == 1) thr++;
                }
            }
        }
        return thr;
    }

    protected int get_thr_increase()              // 得到&
    {

        int thr_increase = 0;
        for (int dm = 0; dm < dm_num; dm++)
        {
            // Martix P_increase = this.get_preferencemartix_increase(dm);
            //Martix Ji = this.get_Ji_martix(dm);
            Martix J = get_J(symbol[0], dm);
            for (int i = 0; i < state_num; i++)
            {
                for (int j = 0; j < state_num; j++)
                {
                    if (J.martix[i][j] == 1) thr_increase++;
                }
            }
        }
        return thr_increase;
    }

    public Martix get_P(int [] symbol)
    {
        Martix prefence= model.get_Prefence_martix(dm);
        Martix P = new Martix(state_num, state_num);
        for(int i=0;i< symbol.Length;i++)
        {
            Martix martix = new Martix(state_num); //全一矩阵
            martix = symbol[i] * martix;
            P = P+(prefence == martix);   //  tonghuo 
        }

        return P.sign();
    }

    public Martix get_P(int[] symbol,int dm)
    {
        Martix prefence = model.get_Prefence_martix(dm);
        Martix P = new Martix(state_num, state_num);
        for (int i = 0; i < symbol.Length; i++)
        {
            Martix martix = new Martix(state_num); //全一矩阵
            martix = symbol[i] * martix;
            P =P+(prefence == martix);   //  tonghuo 
        }

        return P.sign();
    }
    public Martix get_P()
    {
        return model.get_Prefence_martix(dm);
    }

    public Martix get_P(int dm)
    {
        return model.get_Prefence_martix(dm);
    }

    public Martix get_J(int [] symbol)
    {    //带符号的j
        Martix P = this.get_P(symbol);
        Martix J = this.get_J();
        return P & J;
    }

    public Martix get_J(int[] symbol,int dm)
    {    //dm带符号的j
        Martix P = this.get_P(symbol,dm);
        Martix J = this.get_J(dm);
        return P & J;
    }

    public Martix get_J(int dm)    
    {  //不带符号的j
        double[][] a = new double[state_num][];
        int [][]trans=model.get_trans_marix(dm);
        for (int i = 0; i < state_num; i++)
        {
            a[i] = new double[state_num];
            for (int j = 0; j < state_num; j++)
            {
                a[i][j] = trans[i][j];
            }
        }

        Martix M = new Martix(a);
        return M;

    }
    public Martix get_J()
    {  //不带符号的j
        double[][] a = new double[state_num][];
        int[][] trans = model.get_trans_marix(dm);
        for (int i = 0; i < state_num; i++)
        {
            a[i] = new double[state_num];
            for (int j = 0; j < state_num; j++)
            {
                a[i][j] = trans[i][j];
            }
        }

        Martix M = new Martix(a);
        return M;

    }

    public static Martix get_P(GMCR model,int [] symbol,int dm)
    {   
        int state_num=model.get_feasible_state();
        Martix prefence = model.get_Prefence_martix(dm);

        Martix P = new Martix(state_num, state_num);
        for (int i = 0; i < symbol.Length; i++)
        {
            Martix martix = new Martix(state_num); //全一矩阵
            martix = symbol[i] * martix;
            P =P+(prefence == martix);   //  tonghuo 
        }

        return P.sign();

    }

    public static Martix get_J(GMCR model, int[] symbol, int dm)
    {
        Martix P = BaseStability.get_P(model, symbol, dm);
        Martix J = BaseStability.get_J(model,dm);
        return P & J;
    }

    public static Martix get_J(GMCR model,int dm)
    {
        int state_num = model.get_feasible_state();
        double[][] a = new double[state_num][];
        int[][] trans = model.get_trans_marix(dm);
        for (int i = 0; i < state_num; i++)
        {
            a[i] = new double[state_num];
            for (int j = 0; j < state_num; j++)
            {
                a[i][j] = trans[i][j];
            }
        }

        Martix M = new Martix(a);
        return M;
    }
}