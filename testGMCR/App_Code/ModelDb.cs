using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

/// <summary>
/// ModelBd 的摘要说明
/// </summary>
public class ModelDb : DBTemp
{
    public ModelDb()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public Boolean StoreModel(Model model, int userId, string modelName)
    {
        TransToDbData trans = new TransToDbData();
        ModelBean bean = trans.Trans(model);
        bean.UserId = userId;
        bean.ModelName = modelName;
        return StoreBean(bean);
    }

    private Boolean StoreBean(ModelBean bean)
    {
        string storeSql = "INSERT INTO `model` (`modelName`, `userId`, `inputData`, `name`, `delet1`, `delet2`, `delet3`, `delet4`, `deletManual`, `coalesceState`, `reName`,  `transition1`, `transition2`, `statement`, `preference`) VALUES (";
        //都要加上单引号

        string values = add(bean.ModelName) + "," + add(bean.UserId) + "," + add(bean.InputData) + "," +add( bean.Name) + "," + add(bean.Delet1) + "," + add(bean.Delet2) + "," + add(bean.Delet3) + "," + add(bean.Delet4) + "," +add( bean.DeletManual) + "," +add(bean.CoalesceState)+","+ add(bean.Rename) + "," + add(bean.Transition1) + "," + add(bean.Transition2) + "," + add(bean.Statement) + "," + add(bean.Preference) + ")";
        storeSql += values;

        return this.NonQuery(storeSql);
    }

    private string add(string str) 
    {
        return  "'" + str + "'";
    }

    private string add(int str)
    {
        return "'" + str.ToString() + "'";
    }

    public Model GetModel(int id)
    {   //查询模型
        Model model = new Model();
        ModelBean bean = new ModelBean();
        string sql = "select * from model where id=" + id;
        MySqlDataReader reader = this.Query(sql);
        if (reader.Read())
        {
            bean.ModelName = reader["modelName"].ToString();
            bean.InputData = reader["inputData"].ToString();
            bean.Name = reader["name"].ToString();
            bean.Delet1 = reader["delet1"].ToString();
            bean.Delet2 = reader["delet2"].ToString();
            bean.Delet3 = reader["delet3"].ToString();
            bean.Delet4 = reader["delet4"].ToString();
            bean.DeletManual = reader["deletManual"].ToString();
            bean.CoalesceState = reader["coalesceState"].ToString();
            bean.Rename = reader["reName"].ToString();
            bean.Transition1 = reader["transition1"].ToString();
            bean.Transition2 = reader["transition2"].ToString();
            bean.Statement = reader["statement"].ToString();
            bean.Preference = reader["preference"].ToString();
        }
        model = DataTrans.Trans(bean);
        return model;


    }

    public ArrayList QueryModel(int userid)
    {//查询模型，返回两个一唯string数组string【0】表示 modelname  string【1】表示modelid
        string selectSql = "select id ,modelName from model where userid=5 or userid=" + userid.ToString();
        MySqlDataReader reader = this.Query(selectSql);
        ArrayList list = new ArrayList();

        while (reader.Read())
        {
            string[] str = new string[2];
            str[0] = (string)reader["modelName"].ToString();
            str[1] = (string)reader["id"].ToString();
            list.Add(str);
        }
        reader.Close();
        mysql.Close();
        return list;
    }
}