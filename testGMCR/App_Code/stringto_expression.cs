using System;
using System.Collections.Generic;

using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

/// <summary>
/// stringto_expression 的摘要说明
/// </summary>
public class stringto_expression
{
	public stringto_expression()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public decimal change(string str, ArrayList arr)
    {
        string numberStr = IndexToNumber(str, arr);
        string postFix = InfixToPostfix(numberStr);
        decimal number = PostfixToResult(postFix);
        return number;
    }


    public  string InfixToPostfix(string expression)
    {
        Stack<char> operators = new Stack<char>();
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < expression.Length; i++)
        {
            char ch = expression[i];
            if (char.IsWhiteSpace(ch)) continue;
            switch (ch)
            {

                case '-':
                    while (operators.Count > 0)
                    {
                        char c = operators.Pop();   //pop Operator
                        if (c == '(')
                        {
                            operators.Push(c);      //push Operator
                            break;
                        }
                        else if (c == '|')
                        {
                            operators.Push(c);
                            break;
                        }
                        else if (c == '&')
                        {
                            operators.Push(c);
                            break;
                        }

                        else
                        {
                            result.Append(c);
                        }
                    }
                    operators.Push(ch);
                    result.Append(" ");
                    break;

                case '|':
                    while (operators.Count > 0)
                    {
                        char c = operators.Pop();   //pop Operator
                        if (c == '(')
                        {
                            operators.Push(c);      //push Operator
                            break;
                        }

                        else
                        {
                            result.Append(c);
                        }
                    }
                    operators.Push(ch);
                    result.Append(" ");
                    break;

                case '&':
                    while (operators.Count > 0)
                    {
                        char c = operators.Pop();
                        if (c == '(')
                        {
                            operators.Push(c);
                            break;
                        }
                        else if (c == '|')
                        {
                            operators.Push(c);
                            break;
                        }

                        else
                        {
                            result.Append(c);
                        }

                    }
                    operators.Push(ch);
                    result.Append(" ");
                    break;
                case '(':
                    operators.Push(ch);
                    break;
                case ')':
                    while (operators.Count > 0)
                    {
                        char c = operators.Pop();
                        if (c == '(')
                        {
                            break;
                        }
                        else
                        {
                            result.Append(c);
                        }
                    }
                    break;
                default:
                    result.Append(ch);
                    break;
            }
        }
        while (operators.Count > 0)
        {
            result.Append(operators.Pop()); //pop All Operator
        }
        return result.ToString();
    }

    /// <summary>后缀表达式求值
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public  decimal PostfixToResult(string expression)
    {
        Stack<decimal> results = new Stack<decimal>();
        decimal x, y;
        int two_flag=0;
        for (int i = 0; i < expression.Length; i++)
        {
            char ch = expression[i];
            if (char.IsWhiteSpace(ch)) continue;
           // 判断是不是有2 
            if (ch == '2') 
            {   
                two_flag = 1;
                break; 
            }
            switch (ch)
            {
                case '|':
                    y = results.Pop();
                    x = results.Pop();
                    if (x == 1 && y == 1) results.Push(x);
                    else results.Push(x + y);
                    break;

                case '&':
                    y = results.Pop();
                    x = results.Pop();
                    results.Push(x * y);
                    break;
                case '-':
                    y = results.Pop();
                    if (y == 1) results.Push(0);
                    else results.Push(1);
                    break;
                default:
                    results.Push(decimal.Parse(expression[i].ToString()));
                    break;
            }
        }
        if (two_flag == 0) return results.Peek();
        else return -1;
    }

    /// <summary>把表达式的索引改成具体的数
    /// 例如(1+3)/(2+4) {12,3,4,5} =>(12+4)/(3+5)
    /// </summary>
    /// <param name="exprssion"></param>
    /// <param name="arr"></param>
    /// <returns></returns>
    public  string IndexToNumber(string exprssion, ArrayList arr)
    {
        string rtn = string.Empty;
        arr.Insert(0, 3);
        object[] paras = arr.ToArray();
        exprssion = Regex.Replace(exprssion, "(\\d+)", "{$1}");
        rtn = string.Format(exprssion, paras);
        return rtn;
    }


}