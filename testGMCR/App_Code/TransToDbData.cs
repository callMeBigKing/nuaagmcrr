using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// TransToDbData 的摘要说明
/// </summary>
public class TransToDbData
{
    private ModelBean modelBean;
    //传一个model类，return modelBean类
    public TransToDbData()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        
    }
    public ModelBean Trans(Model model)
    {
        this.modelBean = new ModelBean();
        modelBean.InputData = this.intToString(model.InputData);
        modelBean.Name = stringDTo(model.Name);
        modelBean.Delet1 = intDToString(model.Delet1, ";");
        modelBean.Delet2 = intDToString(model.Delet2, ";");
        modelBean.Delet3 = intDToString(model.Delet3Pattern1, ",") + ";" + intDToString(model.Delet3Pattern2, ",");
        modelBean.Delet4 = stringTo(model.delet4Statement);
        modelBean.DeletManual = intToString(model.manualDelet);
        modelBean.CoalesceState = intDToString(model.coalesceState, ";");
        modelBean.Rename = intToString(model.sort);
        modelBean.Transition1 = intToString(model.single);
        modelBean.Transition2 = intDToString(model.transPattern1, ",") + ";" + intDToString(model.transPattern2, ",");
        //对于偏好应为有两种方式设置要单独写。
        modelBean.Statement = arrListTO(model.initStatement);
        modelBean.Preference = arrListTO(model.manualPerfence);
        return modelBean;
    }

    private string intDToString(int[][] data, string separator)
    {
        /*public static int[][] ToIntDArr(string str)
    {  //直接用分号隔开的
       //用于delet1 delet2  状态合并*/
        if (data == null) return "";

        string[] output = new string[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            string str = "";
            for (int j = 0; j < data[i].Length; j++)
            {
                str += data[i][j].ToString();

            }
            output[i] = str;
        }

        return string.Join(separator, output);
    }

    private string intToString(int[] data)
    {
        /*public static int [] ToIntArr(string s) 
    //string s 中的数据是用逗号隔开
    //string to int []   
    // 适用于 mamual delet  和 inputdata  排序  trans——single*/
        if (data == null) return "";
        string ouput = string.Join(",", data);
        return ouput;

    }

    private string stringDTo(string[][] data)
    {
        if (data == null) return "";
        /*public static string [][] ToStringDArr(string s)
        //逗号分号隔开，
        //用于DM&option name  perfencestatement*/
        string output = "";
        for (int i = 0; i < data.Length; i++)
        {
            output += string.Join(",", data[i]);
            if (i != data.Length - 1) output += ";";
        }
        return output;
    }

    private string stringTo(string[] data)
    {
        if (data == null) return "";
        string output = string.Join(",", data);
        return output;
    }

    private string arrListTO(ArrayList data)
    {//用于偏好
        
        string output="";
        for (int i = 0; i < data.Count; i++)
        {
            try
            {
                if(i== (int)data[i]&&i!=data.Count-1)
                {
                    output += ";";
                }
               
            }
            catch
            {
                string[] str = (string[])data[i];
                output += string.Join(",", str);
                output += ";";
            }

        }
        return output;
    }

}