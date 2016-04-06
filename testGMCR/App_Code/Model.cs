using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// Molde 的摘要说明 
/// 存储GMCR类中输入的数据
/// </summary>
public class Model
{
    private int[] inputData;

    public int[] InputData
    {
        get { return inputData; }
        set { inputData = value; }
    }
    private string[][] name;

    public string[][] Name  // string[0] dm  string [1]option  
    {
        get { return name; }
        set { name = value; }
    }

    private int[][] delet1;



    public int[][] Delet1
    {
        get { return delet1; }
        set { delet1 = value; }
    }
    private int[][] delet2;

    public int[][] Delet2
    {
        get { return delet2; }
        set { delet2 = value; }
    }
    private int[][] delet3Pattern1;

    public int[][] Delet3Pattern1
    {
        get { return delet3Pattern1; }
        set { delet3Pattern1 = value; }
    }

    private int[][] delet3Pattern2;

    public int[][] Delet3Pattern2
    {
        get { return delet3Pattern2; }
        set { delet3Pattern2 = value; }
    }

    public string[] delet4Statement;

    public int[] manualDelet;
    public int[][] coalesceState;
    public int[] sort;//rename;
    public int [] single;
    public int[][] transPattern1;
    public int[][] transPattern2;
    public ArrayList initStatement;  //initStatement 里面是string[]
    public ArrayList manualPerfence;//manualPerfence 里面是string[]

    public Model()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        this.manualPerfence = new ArrayList();
        this.initStatement = new ArrayList();
    }
}