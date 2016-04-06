using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections;

/// <summary>
/// manualperfence 的摘要说明
/// </summary>
public class manualperfence
{
    public manualperfence()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }


    public static int [][] calculate_martix(string[]paixu,int feasible_state)
    {

        ArrayList charperfence = new ArrayList();
        for (int i = 0; i < paixu.Length; i++)
        {
            charperfence.Add(paixu[i]);
        }
        ArrayList number_order = new ArrayList();
        ArrayList symbol_order = new ArrayList();

        for (int p = 0; p < charperfence.Count; p++)
        {
            int pointa = 0;//a的一个更新位置；
            int[] a_num = new int[feasible_state];
            int[] a_symbol = new int[feasible_state - 1];
            string[] perfence1 = Regex.Split(charperfence[p].ToString(), ">>");

            for (int i = 0; i < perfence1.Length; i++)
            {
                string[] perfence2 = perfence1[i].Split('>');
                for (int j = 0; j < perfence2.Length; j++)
                {
                    string[] perfence3 = perfence2[j].Split('=');
                    for (int k = 0; k < perfence3.Length; k++)
                    {
                        a_num[pointa] = int.Parse(perfence3[k].Trim());
                        if (k > 0)
                        {
                            a_symbol[pointa - 1] = 0;
                        }
                        if (pointa != feasible_state - 1) pointa++;  //到最后一位就停止++
                    }
                    if (j != perfence2.Length - 1)
                        a_symbol[pointa - 1] = 1;

                }
                if (i != perfence1.Length - 1)
                    a_symbol[pointa - 1] = feasible_state;

            }
            number_order.Add(a_num);
            symbol_order.Add(a_symbol);
        }

        int[][] martix = getperfencemartix(number_order, symbol_order, feasible_state);

        int[][] perfence_martixsanjiao = new int[feasible_state][];
        for (int i = 0; i < feasible_state; i++)
        {
            perfence_martixsanjiao[i] = new int[i + 1];
            for (int j = 0; j <= i; j++)
            {
                perfence_martixsanjiao[i][j] = martix[i][j];

            }
        }

        return perfence_martixsanjiao;

    }

    public static int getperfencenum(int num, int feasible_state)
    {
        if (num >= feasible_state) return 10;
        else if (num >= 1) return 1;
        else return 0;
    }

    public static int getperfence(int[] number_order, int[] symbol_order, int p, int q)//注意只接受p>q;    所给的结果是个上三角
    {

        int guanxi = 0;
        for (int j = p + 1; j <= q; j++)
        {
            guanxi += symbol_order[j - 1];
        }
        return getperfencenum(guanxi, number_order.Length);
    }


    public static int[][] getp_martix(int[] number_order, int[] symbol_order)
    {
        int feasible_state = number_order.Length;
        int[][] martix = new int[feasible_state][];
        for (int i = 0; i < feasible_state; i++)
        {
            martix[i] = new int[feasible_state];

        }
        for (int i = 0; i < feasible_state; i++)
        {

            martix[i][i] = 0;
            for (int j = i + 1; j < feasible_state; j++)
            {
                martix[number_order[i] - 1][number_order[j] - 1] = getperfence(number_order, symbol_order, i, j);
                martix[number_order[j] - 1][number_order[i] - 1] = -martix[number_order[i] - 1][number_order[j] - 1];  //-1 xu hao  cong 1 kais 
            }
        }

        return martix;
    }

    public static int[][] getperfencemartix(ArrayList number_order, ArrayList symbol_order, int feasible_state)
    {


        int[][] martix = getp_martix((int[])number_order[0], (int[])symbol_order[0]);
        for (int i = 0; i < feasible_state; i++)
        {
            for (int j = i + 1; j < feasible_state; j++)
            {
                for (int k = 1; k < number_order.Count; k++)
                {

                    int[][] othermartix = getp_martix((int[])number_order[k], (int[])symbol_order[k]);
                    if (martix[i][j] != othermartix[i][j])
                    {
                        martix[i][j] = 2;
                        martix[j][i] = 2;
                        break;
                    }
                }
            }
        }



        return martix;
    }

}