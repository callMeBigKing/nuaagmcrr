using System;
using System.Collections.Generic;

using System.Web;

/// <summary>
/// martix 的摘要说明
/// </summary>
public class Martix
{
    public double[][] martix;
    public int row;
    public int col;
	public Martix(int a,int b)   //a行b列的0矩阵
    {
        row = a;
        col = b;
        martix = new double[a][];
        for (int i = 0; i < a; i++)
        {
            martix[i] = new double[b];
            for (int j = 0; j < b; j++)
            {
                martix[i][j] = 0;
            }
        }
	}

  
    public Martix(int m, bool I)   //构建m维的单位矩阵
    {   
        row = m;
        col = m;
        martix = new double[m][];
        for (int i = 0; i < row; i++)
        {
            martix[i] = new double[m];
            for (int j = 0; j < col; j++)
            {
                if(i==j)martix[i][j] = 1;
                else martix[i][j] = 0;
            }
        }
    }

    public Martix(double[][]a)
    {
        row = a.Length;
        col = a[0].Length;
        martix = new double[row][];
        for (int i = 0; i < row; i++)
        {
            martix[i] = new double[col];
            for (int j = 0; j < col; j++)
            {
                martix[i][j] = a[i][j];
            }
        }
    }

    public Martix(int m)  //m维的全1矩阵
    {
        row = m;
        col = m;
        martix = new double[m][];
        for (int i = 0; i < row; i++)
        {
            martix[i] = new double[m];
            for (int j = 0; j < col; j++)
            {
                martix[i][j] = 1;
            }
        }
    }
       //   &表示点乘  0-1矩阵& 和。的效果相同
    public static Martix operator &(Martix A, Martix B)
    {
        Exception e = new Exception();
        if (A.col != B.col || A.row != B.row) throw e;
        Martix M = new Martix(A.row, A.col);
        for (int i = 0; i < A.row; i++)
        {
            for (int j = 0; j < B.col; j++)
            {
                M.martix[i][j] = A.martix[i][j] * B.martix[i][j];
            }
        }
        return M;

    }
     //或
    public static Martix operator |(Martix A, Martix B)
    {
        Exception e = new Exception();
        if (A.col != B.col || A.row != B.row) throw e;
        Martix M = new Martix(A.row, A.col);
        for (int i = 0; i < A.row; i++)
        {
            for (int j = 0; j < B.col; j++)
            {
                if (A.martix[i][j] == 0 && B.martix[i][j] == 0) M.martix[i][j] = 0;
                else M.martix[i][j] = 1;
            }
        }
        return M;

    }

    public static Martix operator ==(Martix A, Martix B)
    { 
        //表示同或
        Exception e = new Exception();
        if (A.row != B.row|| A.col != B.col) throw e;
        Martix M = new Martix(A.row, A.col);  //0 矩阵
        for (int i = 0; i < A.row; i++)
        {
            for (int j = 0; j < A.col; j++)
            {
                if (A.martix[i][j] == B.martix[i][j])
                {
                    M.martix[i][j] = 1;
                }
            }
        }

        return M;
    }

    public static Martix operator !=(Martix A, Martix B)
    {
        //表示异或
        
        Exception e = new Exception();
        if (A.row != B.row || A.col != B.col) throw e;
        Martix M = new Martix(A.row, A.col);  //0 矩阵
        for (int i = 0; i < A.row; i++)
        {
            for (int j = 0; j < A.col; j++)
            {
                if (A.martix[i][j] != B.martix[i][j])
                {
                    M.martix[i][j] = 1;
                }
            }
        }

        return M;
    }
    //矩阵相乘
    public static Martix operator *(Martix A, Martix B)
   {  
       Exception e=new Exception();
       if(A.col!=B.row)throw e;
       Martix M=new Martix(A.row,B.col);
       for (int i = 0; i < A.row; i++)
       {
           for (int j = 0; j < B.col; j++)
           {
               M.martix[i][j] = 0;
               for (int k = 0; k < A.col; k++)
               {
                   M.martix[i][j] += A.martix[i][k] * B.martix[k][j];
               }
           }
       }

     return M;
  }

    public static Martix operator *(double h, Martix B)
    {//矩阵数乘

        Martix M = new Martix(B.row, B.col);
        for (int i = 0; i < B.row; i++)
        {
            for (int j = 0; j < B.col; j++)
            {
                M.martix[i][j] = h*B.martix[i][j];

            }
        }

        return M;
    }

    public static Martix operator *(Martix B,double h )
    {
        //矩阵数乘
        Martix M = new Martix(B.row, B.col);
        for (int i = 0; i < B.row; i++)
        {
            for (int j = 0; j < B.col; j++)
            {
                M.martix[i][j] = h * B.martix[i][j];

            }
        }

        return M;
    }

    public static Martix operator +(Martix A, Martix B)
    {
        Exception e = new Exception();
        if (A.col != B.col||A.row!=B.row) throw e;
        Martix M = new Martix(A.row, B.col);

        for (int i = 0; i < A.row; i++)
        {
            for (int j = 0; j < B.col; j++)
            {
                M.martix[i][j] = A.martix[i][j] + B.martix[i][j];
            }
        }
        return M;
    }

    public static Martix operator -(Martix A, Martix B)
    {
        Exception e = new Exception();
        if (A.col != B.col || A.row != B.row) throw e;
        Martix M = new Martix(A.row, B.col);

        for (int i = 0; i < A.row; i++)
        {
            for (int j = 0; j < B.col; j++)
            {
                M.martix[i][j] = A.martix[i][j] - B.martix[i][j];
            }
        }
        return M;
    }

    public Martix trans()
    {
        Martix M = new Martix(col, row);

        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                M.martix[i][j] = martix[j][i];
            }
        }
        return M;
    }

    

    public Martix sign(Martix A)
    {
        Martix M = new Martix(A.row, A.col);
        for (int i = 0; i < A.row; i++)
        {
            for (int j = 0; j < A.col; j++)
            {  
                if( A.martix[j][i]>0) M.martix[i][j] =1;
                else if (A.martix[j][i] == 0) M.martix[i][j] = 0;
                else if (A.martix[j][i] < 0) M.martix[i][j] = -1;
            }
        }
        return M;      
    }

    public Martix sign()
    {
        Martix M = new Martix(row, col);
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (martix[i][j] > 0) M.martix[i][j] = 1;
                else if (martix[i][j] == 0) M.martix[i][j] = 0;
                else if (martix[i][j] < 0) M.martix[i][j] = -1;
            }
        }
        return M;
    }



}