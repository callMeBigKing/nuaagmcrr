using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// DataTrans 的摘要说明
/// 将ModelBean 转化为Model
/// </summary>
public class DataTrans
{
    public DataTrans()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static Model Trans(ModelBean bean)
    {
        Model model = new Model();
        model.InputData = ToIntArr(bean.InputData);
        model.Name = ToStringDArr(bean.Name);
        model.Delet1 = ToIntDArr(bean.Delet1);
        model.Delet2 = ToIntDArr(bean.Delet1);
        model.Delet3Pattern1 = ToIntDArr(bean.Delet3.Split(';')[0]);
        model.Delet3Pattern2 = ToIntDArr(bean.Delet3.Split(';')[1]);
        model.delet4Statement = ToStringArr(bean.Delet4);
        //
        model.manualDelet = ToIntArr(bean.DeletManual);
        model.coalesceState = ToIntDArr(bean.CoalesceState);
        model.sort = ToIntArr(bean.Rename);
        model.single = ToIntArr(bean.Transition1);
        model.transPattern1 = transPattern(bean.Transition2.Split(';')[0]);
        model.transPattern2 = transPattern(bean.Transition2.Split(';')[1]);
        model.initStatement = ToArrList(bean.Statement);
        model.manualPerfence = ToArrList(bean.Preference);
        return model;
    }

    private static ArrayList ToArrList(string str)
    {
        ArrayList list = new ArrayList();
        string[][] temp = ToStringDArr(str);
        for (int i = 0; i < temp.Length; i++)
        {
            list.Add(temp[i]);

        }
        return list;
    }


    private static int[] ToIntArr(string str) 
	{
		//string s 中的数据是用逗号隔开
		//string to int []   
        // 适用于 mamual delet  和 inputdata  排序  trans——single 
      
        ///str 为""  时设置返回为null；
        
		string [] input=str.Split(',');
        int[] output ;
        if (str == "") 
        {
            return output = new int[0];
        }
        output = Array.ConvertAll<string, int>(input, s => int.Parse(s));


        return output;
	}



    private static string[][] ToStringDArr(string str)
    {
        //逗号分号隔开，
        //用于DM&option name  perfencestatement
        string[] outputTemp = str.Split(';');
        string[][] output = new string[outputTemp.Length][];
        for (int i = 0; i < outputTemp.Length; i++)
        {
            output[i] = outputTemp[i].Split(',');//第一个spit逗号
        }
        return output;
    }

    private static int[][] ToIntDArr(string str)
    {  //直接用分号隔开的
        //用于delet1 delet2  delet3 状态合并
        string[] input = str.Split(';');
        int length = input.Length;
        int[][] output = new int[length][];
        for (int i = 0; i < length; i++)
        {
            output[i] = Array.ConvertAll<char, int>(input[i].ToCharArray(), s => int.Parse(s.ToString()));
        }
        return output;
    }

    private static string[] ToStringArr(string str)
    {  //逗号隔开转化成string数组 
        //用于delet4
        string[] output = str.Split(',');
        return output;
    }

    private static int[][] transPattern(string str)
    {  //直接用douhao 隔开的
        //用于delet1 delet2  delet3 状态合并
        string[] input = str.Split(',');
        int length = input.Length;
        int[][] output = new int[length][];
        for (int i = 0; i < length; i++)
        {
            output[i] = Array.ConvertAll<char, int>(input[i].ToCharArray(), s => int.Parse(s.ToString()));
        }
        return output;
    }

}