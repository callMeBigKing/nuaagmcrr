using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;

/// <summary>
/// DBTemp 的摘要说明
/// 进行mysql 数据库操作的类
/// </summary>
public class DBTemp
{
    protected MySqlConnection mysql;
    
    public  DBTemp()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //初始化一个connection 
        //String mysqlStr = "Database=test;Data Source=127.0.0.1;User Id=root;Password=123456;pooling=false;CharSet=utf8;port=3306";
       String mysqlStr = "Database=qdm128525373_db;Data Source=qdm128525373.my3w.com;User Id=qdm128525373;Password=xdacy123;pooling=false;CharSet=utf8;port=3306";
         mysql = new MySqlConnection(mysqlStr);
    }


    private   MySqlCommand getSqlCommand(String sql)
    {
        MySqlCommand mySqlCommand = new MySqlCommand(sql, mysql);
        return mySqlCommand;
    }
    // 非查询 无需返回数据的操作
    public Boolean NonQuery(string sql)
    {
        MySqlCommand mySqlCommand = getSqlCommand(sql);
        try
        {
            mysql.Open();
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();

        }
        catch 
        {
            return false;
        }
        return true;
    }
    
    public int  QueryId(string sql)
    {  
        //暂时不进行数据返回，
        //没有找到择返回-1
        MySqlCommand mySqlCommand = getSqlCommand(sql);
        mysql.Open();
        MySqlDataReader reader = mySqlCommand.ExecuteReader();
        int output=-1;
        if (reader.Read())
        {
            output = int.Parse(reader["id"].ToString());
        }
        reader.Close();
        mysql.Close();
        return output;
    }

    public MySqlDataReader Query(string sql)
    {
        //暂时不进行数据返回，
        //没有找到择返回-1
        MySqlCommand mySqlCommand = getSqlCommand(sql);
        mysql.Open();
        MySqlDataReader reader = mySqlCommand.ExecuteReader();
        return reader;
    }

    
}