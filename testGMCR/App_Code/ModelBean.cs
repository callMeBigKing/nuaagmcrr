using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ModelBean 的摘要说明
/// </summary>
public class ModelBean
{
    private int id;
    private string modelName;
    private int userId;
    private string inputData;
    private string name;//名字 [0]dm [1]option
    private string delet1;
    private string delet2;
    private string delet3;
    private string delet4;
    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    

    public string ModelName
    {
        get { return modelName; }
        set { modelName = value; }
    }
    

    public int UserId
    {
        get { return userId; }
        set { userId = value; }
    }
    

    public string InputData
    {
        get { return inputData; }
        set { inputData = value; }
    }
    

    public string Name
    {
        get { return name; }
        set { name = value; }
    }


    public string Delet1
    {
        get { return delet1; }
        set { delet1 = value; }
    }


    public string Delet2
    {
        get { return delet2; }
        set { delet2 = value; }
    }
   

    public string Delet3
    {
        get { return delet3; }
        set { delet3 = value; }
    }


    public string Delet4
    {
        get { return delet4; }
        set { delet4 = value; }
    }
    private string deletManual;

    public string DeletManual
    {
        get { return deletManual; }
        set { deletManual = value; }
    }
    private string coalesceState;

    public string CoalesceState
    {
        get { return coalesceState; }
        set { coalesceState = value; }
    }
    private string rename;

    public string Rename
    {
        get { return rename; }
        set { rename = value; }
    }
    private string transition1;

    public string Transition1
    {
        get { return transition1; }
        set { transition1 = value; }
    }
    private string transition2;

    public string Transition2
    {
        get { return transition2; }
        set { transition2 = value; }
    }
    private string statement;

    public string Statement
    {
        get { return statement; }
        set { statement = value; }
    }
    private string preference;

    public string Preference
    {
        get { return preference; }
        set { preference = value; }
    }


	public ModelBean()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
/*
        this.modelName = "";
        this.inputData = "";
        this.name = "";
        this.delet1 = "";
        this.delet2 = "";
        this.delet3 = "";
        this.delet4 = "";
        this.deletManual = "";
        this.coalesceState = "";
        this.rename = "";
        this.transition1 = "";
        this.transition2 = "";
        this.statement = "";
        this.preference = "";
        */
        
	}
}