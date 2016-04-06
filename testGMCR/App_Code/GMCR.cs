using System;
using System.Collections.Generic;

using System.Web;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
/// <summary>
/// GMCR 的摘要说明
/// 
/// </summary>
public class GMCR
{
    public Model model;  //用于存储模型输入数据
    private int[] option;             //一个向量，向量长度表示DM的个数，数组每个元素表示该DM所控制的opinion个数
    private string[] option_descrip;           //option 的描述    每行表示该一个DM对应的option
    private string[] DM_descrip;              //DM 的描述  
    private int[][] state;
    private int feasible_state;          //可行状态的个数
    // private string[] init_prestament;                 //表示原始的偏好陈述。
    public ArrayList init_statement;
    // private int[][][] preference_Prioritizing;
    private ArrayList perfence_prioritizing;
    //private int[][][] preference_rank;                        //对于强度偏好我们默认阀值为2^m m为option个数
    private ArrayList perfence_rank;
    // private int[][] preference_score;   //同rank差不多，只是这边无需排序。第一个下标表示第几组prefence。第二个下标表示状态i 内容存score；
    private ArrayList perfence_score;

    public ArrayList trans_martix;   //状态转移矩阵

    private ArrayList perferencestatement;

    private ArrayList perfencestatement_num;
    //  private int[][] prefenece_martix;
    public ArrayList perfence_martix;
    private int[][][] transitions;                            //状态转移的一个矩阵数组；
    //public int[] perfenceqiangdu;                          //判断该位置是否有强度标志。
    public ArrayList perfenceqiangdu;

    public GMCR(Model model)
    {
        this.model = new Model();
        this.model = model;
        this.inputdata(model.InputData);

        this.set_descrip(model.Name[1], model.Name[0]);
        if (model.Delet1[0].Length != 0) this.delet_state1(model.Delet1);

        if (model.Delet2[0].Length != 0) this.delet_state2(model.Delet2);
        if (model.Delet3Pattern1[0].Length != 0) this.delet_state3(model.Delet3Pattern1, model.Delet3Pattern2);
        if (model.delet4Statement[0]!="") this.delet_state4(model.delet4Statement);
        if (model.manualDelet.Length!= 0) this.delet_manual(model.manualDelet);
        if (model.coalesceState[0].Length != 0) this.coalesce_state(model.coalesceState);
        if (model.sort.Length != 0) this.AlterState(model.sort);
        if (model.single.Length != 0) this.set_transitions(model.single, model.transPattern1, model.transPattern2);
       
        for (int i = 0; i < model.initStatement.Count; i++)
        {
            string[] statement = (string[])model.initStatement[i];
            string [] manualPerfence=(string [])model.manualPerfence[i];
            if (statement[0] != "") this.trans_preferencestatement(i, statement);
            if (manualPerfence[0] != "")
            {
                int[][] perfence_martixsanjiao = manualperfence.calculate_martix(manualPerfence, feasible_state);
                this.model.manualPerfence[i] = manualPerfence;
                this.set_perfencemanual(perfence_martixsanjiao, i);
            }
        
        }
    
    }

    public GMCR()
    {
        this.model = new Model();
    }

    public int get_perfencestatement_num(int dm_i)
    {
        return (int)perfencestatement_num[dm_i];
    }
    public void AlterState(int[] alterState)
    {
        this.model.sort = alterState;
        int row = this.feasible_state;
        int col = this.get_optionnum();

        //nt[][] tempState = this.state; 注意这里赋值给的是地址；
        int[][] tempState = new int[row][];
        for (int i = 0; i < row; i++)
        {
            tempState[i] = new int[col];
            for (int j = 0; j < col; j++)
            {
                tempState[i][j] = state[i][j];
            }
        }

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                this.state[alterState[i]][j] = tempState[i][j];
            }
        }

    }

    void chushihua(int dm_num)
    {

        init_statement = new ArrayList();
        perfence_prioritizing = new ArrayList();
        perfence_rank = new ArrayList();
        perfence_score = new ArrayList();
        perferencestatement = new ArrayList();
        perfencestatement_num = new ArrayList();
        perfence_martix = new ArrayList();
        perfenceqiangdu = new ArrayList();
        trans_martix = new ArrayList();
        for (int i = 0; i < dm_num; i++)
        {
            perfence_rank.Add(i);
            init_statement.Add(new string []{""});
            perfence_prioritizing.Add(i);
            perfence_score.Add(i);
            perferencestatement.Add(i);
            perfencestatement_num.Add(1);
            perfence_martix.Add(i);
            perfenceqiangdu.Add(i);
            trans_martix.Add(i);
             
            this.model.manualPerfence.Add(new string[] { "" });
            this.model.initStatement.Add(new string[] { "" });
        }
    }

    public void inputdata(int[] option)        //输入原始数组
    {
        chushihua(option.Length);
        this.model.InputData = option;
        this.option = new int[option.Length];
        this.option = option;
        getall_state();
    }

    private void getall_state()                                 //初始化所有状态
    {
        int num = get_optionnum();
        state = new int[(int)(Math.Pow(2, num))][];
        feasible_state = (int)Math.Pow(2, num);
        for (int col = 0; col < Math.Pow(2, num); col++)
        {
            state[col] = new int[num];
            if (col == 0)                                   //第一层全部赋值为0；
            {
                for (int rol = 0; rol < num; rol++)
                {
                    state[col][rol] = 0;
                }
            }

            else
            {
                for (int rol = 0; rol < num; rol++)
                {
                    state[col][rol] = state[col - 1][rol];
                }
                state[col][num - 1] += 1;
                for (int rol = num - 1; rol >= 0; rol--)
                {
                    if (state[col][rol] == 2)
                    {
                        state[col][rol] = 0;
                        state[col][rol - 1] += 1;
                    }

                }
            }
        }
    }

    public void set_descrip(string[] option_descrip, string[] DM_descrip)               //输入描述
    {
        this.model.Name = new string[2][];
        this.model.Name[0] = DM_descrip;
        this.model.Name[1] = option_descrip;

        this.DM_descrip = new string[DM_descrip.Length];
        this.option_descrip = new string[option_descrip.Length];
        this.DM_descrip = DM_descrip;
        this.option_descrip = option_descrip;
    }

    public void delet_state1(int[][] at_most_one)      //int[][] at_most_one 为二维数组，每一行表示一个atmost
    {
        this.model.Delet1 = at_most_one;
        int juge = 0;
        for (int onemost = 0; onemost < at_most_one.Length; onemost++)
            for (int i = 0; i < feasible_state; i++)
            {
                for (int j = 0; j < get_optionnum(); j++)
                {
                    juge += state[i][j] * at_most_one[onemost][j];
                }
                if (juge > 1)            //超过一个   则可行状态减少一个，同时让后面的状态覆盖前面的状态
                {

                    for (int col = i; col < feasible_state - 1; col++)
                    {
                        for (int rol = 0; rol < get_optionnum(); rol++)
                        {
                            state[col][rol] = state[col + 1][rol];
                        }
                    }
                    feasible_state -= 1;
                    i--;
                }
                juge = 0;
            }
    }

    public void delet_state2(int[][] at_least_one)      //int[][] at_most_one 为二维数组，每一行表示一个atmost
    {
        this.model.Delet2 = at_least_one;
        int juge = 0;
        for (int onemost = 0; onemost < at_least_one.Length; onemost++)
            for (int i = 0; i < feasible_state; i++)
            {
                for (int j = 0; j < get_optionnum(); j++)
                {
                    juge += state[i][j] * at_least_one[onemost][j];
                }
                if (juge < 1)            //超过一个   则可行状态减少一个，同时让后面的状态覆盖前面的状态
                {

                    for (int col = i; col < feasible_state - 1; col++)
                    {
                        for (int rol = 0; rol < get_optionnum(); rol++)
                        {
                            state[col][rol] = state[col + 1][rol];
                        }
                    }
                    feasible_state -= 1;
                    i--;
                }
                juge = 0;
            }
    }

    public void delet_state3(int[][] p1, int[][] p2)      //删除第三种类型 p1,p2 的每一行为对应的文章中的p1,p2  不确定的记作2
    {
        this.model.Delet3Pattern1 = p1;
        this.model.Delet3Pattern2 = p2;

        for (int p = 0; p < p1.Length; p++)
        {
            int m0 = 0;
            int m1 = 0;
            int n0 = 0;
            int n1 = 0;
            for (int rol = 0; rol < get_optionnum(); rol++)
            {
                m0 += (int)(Math.Sign(p1[p][rol]) * Math.Pow(2, get_optionnum() - rol - 1));
                m1 += (int)((1 - Math.Abs(1 - p1[p][rol])) * Math.Pow(2, get_optionnum() - rol - 1));
                n0 += (int)(Math.Sign(p2[p][rol]) * Math.Pow(2, get_optionnum() - rol - 1));
                n1 += (int)((1 - Math.Abs(1 - p2[p][rol])) * Math.Pow(2, get_optionnum() - rol - 1));
            }
            for (int i = 0; i < feasible_state; i++)
            {
                int b = 0;
                for (int j = 0; j < get_optionnum(); j++)
                {
                    b += state[i][j] * (int)Math.Pow(2, get_optionnum() - j - 1);
                }

                if (((b | m0) == m0 && (b & m1) == m1) && (!((b | n0) == n0 && (b & n1) == n1)))    //与p1 match 但是与p2 不match
                {

                    for (int col = i; col < feasible_state - 1; col++)
                    {
                        for (int rol = 0; rol < get_optionnum(); rol++)
                        {
                            state[col][rol] = state[col + 1][rol];
                        }
                    }
                    feasible_state -= 1;
                    i--;
                }

            }
        }
    }
    public void delet_state4(string[] statements)
    {
        this.model.delet4Statement = statements;
        int[] deletvector = new int[feasible_state];
        int count = 0;
        for (int i = 0; i < feasible_state; i++)
        {
            deletvector[i] = 0;
        }
        stringto_expression strto_expression = new stringto_expression();
        for (int i = 0; i < statements.Length; i++)
        {
            for (int j = 0; j < feasible_state; j++)
            {
                ArrayList arr = new ArrayList();
                for (int setarr = 0; setarr < get_optionnum(); setarr++)
                {
                    arr.Add(state[j][setarr]);
                }
                string numberStr = strto_expression.IndexToNumber(statements[i], arr);
                string postFix = strto_expression.InfixToPostfix(numberStr);
                decimal number = strto_expression.PostfixToResult(postFix);
                if (number == 1)
                {
                    deletvector[j] = (int)number;
                    count++;
                }
            }
        }
        int[] rows = new int[count];
        int point = 0;
        for (int i = 0; i < feasible_state; i++)
        {
            if (deletvector[i] == 1)
            {
                rows[point] = i;
                point++;
            }
            if (point >= count) break;
        }

        this.delet_manual(rows);

    }
    public void delet_manual(int[] rows)            //手动删除rows为要删除的行号从0开始算,需要从小到大进行排序
    {
        this.model.manualDelet = rows;
        int[] deletrows = rows;
        int deletrows_point = 0;                     //deletrow 数组的指针。
        for (int i = 0; i < feasible_state; i++)
        {
            if (deletrows_point >= deletrows.Length) break;
            if (i == deletrows[deletrows_point])
            {
                for (int col = i; col < feasible_state - 1; col++)
                {
                    for (int rol = 0; rol < get_optionnum(); rol++)
                    {
                        state[col][rol] = state[col + 1][rol];
                    }
                }
                feasible_state--;
                i--;
                deletrows_point++;
                for (int j = deletrows_point; j < deletrows.Length; j++)                                //真等于的话后面要删的行号都减1
                {
                    deletrows[j]--;
                }

            }
        }
    }
   
    public void coalesce_state(int[][] coalesce)        //给一个int pattern数组，将满足条件的都合并起来
    {
        this.model.coalesceState = coalesce;
        int flag = 0;                                 //flag用于判断pattern是否起到作用
        for (int p = 0; p < coalesce.Length; p++)
        {

            int m0 = 0;
            int m1 = 0;
            for (int rol = 0; rol < get_optionnum(); rol++)
            {
                m0 += (int)(Math.Sign(coalesce[p][rol]) * Math.Pow(2, get_optionnum() - rol - 1));
                m1 += (int)((1 - Math.Abs(1 - coalesce[p][rol])) * Math.Pow(2, get_optionnum() - rol - 1));

            }
            for (int i = 0; i < feasible_state; i++)
            {
                int b = 0;
                for (int j = 0; j < get_optionnum(); j++)
                {
                    b += state[i][j] * (int)Math.Pow(2, get_optionnum() - j - 1);
                }

                if (((b | m0) == m0 && (b & m1) == m1))
                {

                    for (int col = i; col < feasible_state - 1; col++)
                    {
                        for (int rol = 0; rol < get_optionnum(); rol++)
                        {
                            state[col][rol] = state[col + 1][rol];
                        }
                    }
                    feasible_state -= 1;
                    i--;
                    flag = 1;
                }
            }

            if (flag == 1)                                 //对于起到作用的pattern我们要进行补充合并后的state
            {
                for (int rol = 0; rol < get_optionnum(); rol++)
                {
                    state[feasible_state][rol] = coalesce[p][rol];
                }
                feasible_state++;
            }
            flag = 0;

        }
    }


    public string[] get_initstatement(int dm)
    {
        return (string[])this.init_statement[dm];
    }

    public void trans_preferencestatement(int dm, string[] init_statements)        //dm 为0,1，2,3 表示第几个决策者。从0开始计算       //将混合偏好转换为强度偏好
    {
        this.model.initStatement[dm] = init_statements;
        this.init_statement[dm] = init_statements;

        string[][] preferencestament = new string[64][];            //最多能分成64组； 最好是等需要用的时候再进行初始化。
        ///////强度偏好
        int[] perfenceqiangdu = new int[init_statements.Length];
        for (int i = 0; i < init_statements.Length; i++)
        {
            int has_M = init_statements[i].IndexOf("&M", 0);
            if (has_M != -1)
            {
                perfenceqiangdu[i] = 1;

                for (int qiangud = 0; qiangud < i; qiangud++)
                {
                    perfenceqiangdu[qiangud]++;
                }
            }
            else perfenceqiangdu[i] = 0;
        }
        this.perfenceqiangdu[dm] = perfenceqiangdu;
        ///////////////////************
        //c初始化preferencestament；
        for (int i = 0; i < 64; i++)
        {
            preferencestament[i] = new string[init_statements.Length];
            //preferencestament[i][0]="-1";
        }
        int preferencestament_point = 0;  //表示上一层可用的偏好组数；
        for (int i = 0; i < init_statements.Length; i++)           //这里只要求得到普通的statement至于后面的分开交由另外的算法实现。
        {
            int yucount = 0;             //用来记录符号“||”的数量。
            int firstindex = 0;
            int secondindex = 0;
            if (i == 0)          //如果是第一个
            {

                while (true)
                {
                    secondindex = init_statements[i].IndexOf("||", firstindex);
                    if (secondindex != -1)
                    {
                        preferencestament[preferencestament_point][i] = init_statements[i].Substring(firstindex, secondindex - firstindex);
                        preferencestament_point++;
                    }
                    else break;
                    firstindex = secondindex + 2;
                }
                if (init_statements[i].IndexOf("&M", 0) == -1) preferencestament[preferencestament_point][i] = init_statements[i].Substring(firstindex, init_statements[i].Length - firstindex);
                else preferencestament[preferencestament_point][i] = init_statements[i].Substring(firstindex, init_statements[i].IndexOf("&M", 0) - firstindex);

            }
            else
            {

                //进行赋值
                string statement_temp = "";
                string if_left = "";            //若没有if则将statement放到if_left中
                string if_right = "";
                int layer = -1;
                while (true)
                {
                    secondindex = init_statements[i].IndexOf("||", firstindex);
                    if (secondindex != -1)
                    {
                        yucount++;
                        //temp是中间变量主要用于处理其中的if
                        statement_temp = init_statements[i].Substring(firstindex, secondindex - firstindex);

                        if (statement_temp.IndexOf("if", 0) != -1)
                        {
                            //有if则分别取出if两边的statement  
                            if_left = statement_temp.Substring(0, statement_temp.IndexOf("if", 0)).Trim();
                            if_right = statement_temp.Substring(statement_temp.IndexOf("if", 0) + 2, statement_temp.IndexOf("@", 0) - statement_temp.IndexOf("if", 0) - 2).Trim();
                            layer = int.Parse(statement_temp.Substring(statement_temp.IndexOf("@", 0) + 1, statement_temp.Length - statement_temp.IndexOf("@", 0) - 1));
                        }
                        else if_left = statement_temp.Trim();

                        if (yucount == 1)
                        {
                            for (int fuzhi = 0; fuzhi <= preferencestament_point; fuzhi++)
                            {
                                int can_fuzhi = 1;    //判断能不能够赋值
                                //判断前面是否重复；
                                for (int loop = 0; loop < i; loop++)
                                {
                                    if (preferencestament[fuzhi][loop] == if_left) can_fuzhi = 0;
                                }
                                if (statement_temp.IndexOf("if", 0) != -1 && preferencestament[fuzhi][layer - 1] != if_right) can_fuzhi = 0;
                                if (can_fuzhi == 1) preferencestament[fuzhi][i] = if_left;
                                else if (can_fuzhi == 0) preferencestament[fuzhi][i] = "no";
                            }
                        }
                        else if (yucount > 1)
                        {
                            //当大于1时必须要进行一波赋值
                            for (int fuzhi_i = (yucount - 1) * (preferencestament_point + 1); fuzhi_i < (yucount) * (preferencestament_point + 1); fuzhi_i++)
                            {

                                for (int fuzhi_j = 0; fuzhi_j < i; fuzhi_j++)
                                {
                                    preferencestament[fuzhi_i][fuzhi_j] = preferencestament[fuzhi_i - (yucount - 1) * (preferencestament_point + 1)][fuzhi_j];
                                }
                            }

                            for (int fuzhi = (yucount - 1) * (preferencestament_point + 1); fuzhi < (yucount) * (preferencestament_point + 1); fuzhi++)
                            {
                                int can_fuzhi = 1;    //判断能不能够赋值
                                //判断前面是否重复；
                                for (int loop = 0; loop < i - 1; loop++)
                                {
                                    if (preferencestament[fuzhi][loop] == if_left) can_fuzhi = 0;
                                }
                                if (statement_temp.IndexOf("if", 0) == 1 && preferencestament[fuzhi][layer - 1] != if_right) can_fuzhi = 0;
                                if (can_fuzhi == 1) preferencestament[fuzhi][i] = if_left;
                                else preferencestament[fuzhi][i] = "no";
                            }

                        }
                    }
                    else break;
                    firstindex = secondindex + 2;
                }
                if (init_statements[i].IndexOf("&M", 0) == -1) statement_temp = init_statements[i].Substring(firstindex, init_statements[i].Length - firstindex);
                else statement_temp = init_statements[i].Substring(firstindex, init_statements[i].IndexOf("&M", 0) - firstindex);
                //分两种  一种是就一个statement  另外一种是还剩最后一个statement。
                if (statement_temp.IndexOf("if", 0) != -1)
                {
                    //有if则分别取出if两边的statement  
                    if_left = statement_temp.Substring(0, statement_temp.IndexOf("if", 0)).Trim();
                    if_right = statement_temp.Substring(statement_temp.IndexOf("if", 0) + 2, statement_temp.IndexOf("@", 0) - statement_temp.IndexOf("if", 0) - 2).Trim();
                    layer = int.Parse(statement_temp.Substring(statement_temp.IndexOf("@", 0) + 1, statement_temp.Length - statement_temp.IndexOf("@", 0) - 1));
                }
                else if_left = statement_temp.Trim();
                if (yucount == 0)
                {
                    //  只有一个statement；
                    for (int fuzhi = 0; fuzhi <= preferencestament_point; fuzhi++)
                    {
                        int can_fuzhi = 1;    //判断能不能够赋值
                        //判断前面是否重复；
                        for (int loop = 0; loop < i - 1; loop++)
                        {
                            if (preferencestament[fuzhi][loop] == if_left) can_fuzhi = 0;
                        }
                        if (statement_temp.IndexOf("if", 0) != -1 && preferencestament[fuzhi][layer - 1] != if_right) can_fuzhi = 0;
                        if (can_fuzhi == 1) preferencestament[fuzhi][i] = if_left;
                        else if (can_fuzhi == 0) preferencestament[fuzhi][i] = "no";
                    }
                }
                else
                {
                    //剩下的最后一个部分；
                    for (int fuzhi_i = (yucount) * (preferencestament_point + 1); fuzhi_i < (yucount + 1) * (preferencestament_point + 1); fuzhi_i++)
                    {

                        for (int fuzhi_j = 0; fuzhi_j < i; fuzhi_j++)
                        {
                            preferencestament[fuzhi_i][fuzhi_j] = preferencestament[fuzhi_i - (yucount) * (preferencestament_point + 1)][fuzhi_j];
                        }
                    }

                    for (int fuzhi = (yucount) * (preferencestament_point + 1); fuzhi < (yucount + 1) * (preferencestament_point + 1); fuzhi++)
                    {
                        int can_fuzhi = 1;    //判断能不能够赋值
                        //判断前面是否重复；
                        for (int loop = 0; loop < i - 1; loop++)
                        {
                            if (preferencestament[fuzhi][loop] == if_left) can_fuzhi = 0;
                        }
                        if (statement_temp.IndexOf("if", 0) != -1 && preferencestament[fuzhi][layer - 1] != if_right) can_fuzhi = 0;
                        if (can_fuzhi == 1) preferencestament[fuzhi][i] = if_left;
                        else if (can_fuzhi == 0) preferencestament[fuzhi][i] = "no";
                    }
                }
                //赋值完后要进行删除  只需对非0行进行删除

                int deletnum = 0;
                for (int delet_inull = 0; delet_inull < (yucount + 1) * (preferencestament_point + 1); delet_inull++)
                {
                    if (preferencestament[delet_inull][i] == "no")
                    {
                        //是-1则进行delet后面的覆盖前面的。
                        for (int delet_i = delet_inull; delet_i < (yucount + 1) * (preferencestament_point + 1); delet_i++)
                        {
                            for (int delet_j = 0; delet_j <= i; delet_j++)
                            {
                                preferencestament[delet_i][delet_j] = preferencestament[delet_i + 1][delet_j];
                            }
                        }
                        deletnum++;
                        delet_inull--;
                    }
                }
                preferencestament_point = (yucount + 1) * (preferencestament_point + 1) - 1;
                preferencestament_point -= deletnum;


            }//else end;


        }//for end;
        int preferencestament_num = preferencestament_point + 1;
        perfencestatement_num[dm] = preferencestament_num;


        perferencestatement[dm] = preferencestament;

        set_Option_Prioritizing(dm);
        calculate_prefenece_martix(dm);
    }

    private void calculate_prefenece_martix(int dm)
    {
        int[][] prefenece_martix = new int[feasible_state][];
        //  
        int flag = 0;               //falg 用来记录关系，(i,j)  1表示i>j大于，-1表示i<j小于，0表示等于。2表示不确定 //强度偏好i>>j用10 和i<<j-10来表示  暂时不添加。
        int flag_temp = 0;         //一个临时的flag
        int preferencestament_num = (int)perfencestatement_num[dm];
        int[][] preference_score = (int[][])perfence_score[dm];

        string[] ppp = (string[])init_statement[dm];
        double yuzhi = Math.Pow(2, ppp.Length);
        for (int i = 0; i < feasible_state; i++)
        {
            //初始化为一个下三角矩阵；
            prefenece_martix[i] = new int[i + 1];
            for (int j = 0; j < i + 1; j++)
            {
                //状态 i于状态j进行偏好判断。
                if (i == j)
                {
                    flag = 0;
                    prefenece_martix[i][j] = flag;
                    continue;
                }
                //对每组进行比较
                for (int preference_point = 0; preference_point < preferencestament_num; preference_point++)
                {
                    int distance = preference_score[preference_point][i] - preference_score[preference_point][j];
                    if (preference_point == 0)
                    {
                        if (distance >= yuzhi) flag = 10;
                        else if (distance > 0) flag = 1;
                        else if (distance == 0) flag = 0;
                        else if (distance > -yuzhi) flag = -1;
                        else flag = -10;
                    }
                    else
                    {

                        if (distance >= yuzhi) flag_temp = 10;
                        else if (distance > 0) flag_temp = 1;
                        else if (distance == 0) flag_temp = 0;
                        else if (distance > -yuzhi) flag_temp = -1;
                        else flag_temp = -10;
                    }
                    if (preference_point > 0)
                    {
                        if (flag_temp != flag)
                        {
                            flag = 2;
                            break;
                        }
                    }
                }
                prefenece_martix[i][j] = flag;
            }
        }

        perfence_martix[dm] = prefenece_martix;
    }
    public int get_prefence_num(int dm)
    {
        int get_prefence_num = (int)perfencestatement_num[dm];
        return get_prefence_num;
    }

    private void set_Option_Prioritizing(int dm)            //一个statements 由三部分组成  sta1 连接词iff or IF sta2 ；
    {
        string[][] preferencestament;
        preferencestament = (string[][])this.perferencestatement[dm];
        //this.perfenceqiangdu = new int[preferencestament[0].Length];                //强度偏好的标志
        //将statement赋值给pewferencestatemwnt
        //全部改动了将偏好改成一个整体的statement
        int preferencestament_num = (int)perfencestatement_num[dm];
        string[][][] preferencestament_temp = new string[preferencestament_num][][];        //将一般的状态切割成sta1 和sta2；

        for (int i = 0; i < preferencestament_num; i++)
        {

            preferencestament_temp[i] = new string[preferencestament[0].Length][];
            for (int j = 0; j < preferencestament[0].Length; j++)
            {
                preferencestament_temp[i][j] = new string[3];
                if (preferencestament[i][j].IndexOf("IFF", 0) != -1)   //有IFF
                {
                    preferencestament_temp[i][j][0] = preferencestament[i][j].Substring(0, preferencestament[i][j].IndexOf("IFF", 0));
                    preferencestament_temp[i][j][1] = "IFF";
                    preferencestament_temp[i][j][2] = preferencestament[i][j].Substring(preferencestament[i][j].IndexOf("IFF", 0) + 3, preferencestament[i][j].Length - preferencestament[i][j].IndexOf("IFF", 0) - 3);
                }
                else if (preferencestament[i][j].IndexOf("IF", 0) != -1)
                {
                    preferencestament_temp[i][j][0] = preferencestament[i][j].Substring(0, preferencestament[i][j].IndexOf("IF", 0));
                    preferencestament_temp[i][j][1] = "IF";
                    preferencestament_temp[i][j][2] = preferencestament[i][j].Substring(preferencestament[i][j].IndexOf("IF", 0) + 2, preferencestament[i][j].Length - preferencestament[i][j].IndexOf("IF", 0) - 2);
                }
                else
                {
                    preferencestament_temp[i][j][0] = preferencestament[i][j];
                    preferencestament_temp[i][j][1] = "NONE";
                    preferencestament_temp[i][j][2] = "";
                }
            }
        }
        int[][][] preference_Prioritizing = new int[preferencestament_num][][];
        int[][][] preference_Prioritizing_temp = new int[preferencestament_num][][];////用于存储sta2的真值情况
        for (int i = 0; i < preferencestament_num; i++)
        {
            preference_Prioritizing[i] = new int[feasible_state][];
            preference_Prioritizing_temp[i] = new int[feasible_state][];
            for (int j = 0; j < feasible_state; j++)
            {
                preference_Prioritizing[i][j] = new int[preferencestament[0].Length];
                preference_Prioritizing_temp[i][j] = new int[preferencestament[0].Length];
                for (int k = 0; k < preferencestament[0].Length; k++)
                {
                    preference_Prioritizing[i][j][k] = -1;
                    preference_Prioritizing_temp[i][j][k] = -1;      //初始状态给-1；
                }
            }
        }


        for (int preference_point = 0; preference_point < preferencestament_num; preference_point++)
        {
            stringto_expression strto_expression = new stringto_expression();
            for (int i = 0; i < preferencestament[0].Length; i++)
            {

                /*  int has_M = preferencestament_temp[preference_point][i][2].IndexOf("M", 0);
                  //不是-1的则有强度  有强度标记为1；
                  preferencestament_temp[preference_point][i][2] = preferencestament_temp[preference_point][i][2].Trim();
                  if (has_M != -1)
                  {
                      perfenceqiangdu[i] = 1;
                      statements[i][2] = statements[i][2].Substring(0, statements[i][2].Length - 2);
                      for (int qiangud = 0; qiangud < i; qiangud++)
                      {
                          perfenceqiangdu[qiangud]++;
                      }

                  }
                   
                  else perfenceqiangdu[i] = 0;
                  */
                for (int j = 0; j < feasible_state; j++)
                {
                    ArrayList arr1 = new ArrayList();
                    ArrayList arr2 = new ArrayList();
                    for (int setarr = 0; setarr < get_optionnum(); setarr++)
                    {
                        arr1.Add(state[j][setarr]);
                        arr2.Add(state[j][setarr]);
                    }
                    //判断是否有强度，并且记下强度；
                    preference_Prioritizing[preference_point][j][i] = (int)strto_expression.change(preferencestament_temp[preference_point][i][0], arr1);
                    if (preferencestament_temp[preference_point][i][2].Trim() != "") preference_Prioritizing_temp[preference_point][j][i] = (int)strto_expression.change(preferencestament_temp[preference_point][i][2], arr2);
                }
            }
        }
        for (int preference_point = 0; preference_point < preferencestament_num; preference_point++)
        {
            for (int i = 0; i < feasible_state; i++)
            {
                for (int j = 0; j < preferencestament[0].Length; j++)
                {
                    //此处设置一个级别最高的flag 用于判断是不是有涉及打复合状态，若有复合状态前面会返回-1
                    if (preference_Prioritizing[preference_point][i][j] == -1)
                    {
                        preference_Prioritizing[preference_point][i][j] = 0;
                        continue;
                    }
                    if (preferencestament_temp[preference_point][j][1] == "IFF")
                    {
                        if (preference_Prioritizing[preference_point][i][j] == preference_Prioritizing_temp[preference_point][i][j]) preference_Prioritizing[preference_point][i][j] = 1;
                        else if (preference_Prioritizing[preference_point][i][j] != preference_Prioritizing_temp[preference_point][i][j]) preference_Prioritizing[preference_point][i][j] = 0;
                    }
                    if (preferencestament_temp[preference_point][j][1] == "IF")
                    {
                        if ((preference_Prioritizing[preference_point][i][j] == 0) && (preference_Prioritizing_temp[preference_point][i][j] == 1)) preference_Prioritizing[preference_point][i][j] = 0;
                        else preference_Prioritizing[preference_point][i][j] = 1;
                    }
                }
            }
        }
        int[] qiangdu = (int[])perfenceqiangdu[dm];
        int[][][] preference_rank = new int[preferencestament_num][][];      //rank的第一列为偏好值 第二列为对应的状态列；
        int[][] preference_score = new int[preferencestament_num][];
        for (int preference_point = 0; preference_point < preferencestament_num; preference_point++)
        {
            preference_rank[preference_point] = new int[feasible_state][];
            preference_score[preference_point] = new int[feasible_state];
            for (int i = 0; i < feasible_state; i++)
            {
                preference_rank[preference_point][i] = new int[2];
                preference_rank[preference_point][i][1] = i;
                for (int j = 0; j < preferencestament[0].Length; j++)
                {
                    preference_rank[preference_point][i][0] += preference_Prioritizing[preference_point][i][j] * (int)(Math.Pow(2, preference_Prioritizing[0][0].Length - j - 1)) + preference_Prioritizing[preference_point][i][j] * qiangdu[j] * (int)Math.Pow(2, preference_Prioritizing[0][0].Length);  //强度暂时不加
                }
                preference_score[preference_point][i] = preference_rank[preference_point][i][0];
            }
        }


        for (int preference_point = 0; preference_point < preferencestament_num; preference_point++)
        {
            int temp1, temp0, k;                                            //temp 用于排序
            for (int i = 0; i < preference_rank[preference_point].Length - 1; i++)                      //选择排序
            {
                k = i;
                for (int j = i + 1; j < preference_rank[preference_point].Length; j++)
                {
                    if (preference_rank[preference_point][k][0] < preference_rank[preference_point][j][0])
                    {
                        k = j;
                    }
                }
                temp0 = preference_rank[preference_point][k][0];
                preference_rank[preference_point][k][0] = preference_rank[preference_point][i][0];
                preference_rank[preference_point][i][0] = temp0;

                temp1 = preference_rank[preference_point][k][1];
                preference_rank[preference_point][k][1] = preference_rank[preference_point][i][1];
                preference_rank[preference_point][i][1] = temp1;
            }
        }
        perfence_rank[dm] = preference_rank;
        perfence_score[dm] = preference_score;
        perfence_prioritizing[dm] = preference_Prioritizing;

    }

    public string[][] get_preferencestament(int dm)
    {
        return (string[][])perferencestatement[dm];
    }
    
    public void set_perfencemanual(int[][] martix, int dm)
    {
        perfence_martix[dm] = martix;
    }

    public void setPerfenceMartix(int[][] martix, int dm)//设置偏好矩阵，不对称的，m*m
    {

    }

    public int[][][] get_perferencerank(int dm)
    {
        return (int[][][])perfence_rank[dm];
    }

    public int[][][] get_Option_Prioritizing(int dm)
    {
        return (int[][][])perfence_prioritizing[dm];
    }

    public int[][] get_prefenece_martix(int dm)
    {
        return (int[][])perfence_martix[dm];

    }
     public Martix get_Prefence_martix(int dm)    //返回martix   注意这里的p(i,j)=1  i<j  要与之前的反过来
    {   
        

        int[][] Prefence_martix = this.get_prefenece_martix(dm);
        int state_num = this.get_feasible_state();
        double[][] martix = new double[state_num][];
        if(Prefence_martix[0].Length != state_num)
        {
            for (int i = 0; i < state_num; i++)
            {
                martix[i] = new double[state_num];
                for (int j = 0; j <= i; j++)
                {
                    if (Prefence_martix[i][j] != 2)
                    {
                        martix[i][j] = -Prefence_martix[i][j];
                        if (i != j) martix[j][i] = -martix[i][j];
                    }
                    else
                    {
                        martix[i][j] = 2;    //2不可能i=j
                        martix[j][i] = 2;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < state_num; i++)
            {
                martix[i] = new double[state_num];
                for (int j = 0; j < state_num; j++)
                {
                    if (Prefence_martix[i][j] != 2)
                    {
                        martix[i][j] = -Prefence_martix[i][j];
                        
                    }
                    else
                    {
                        martix[i][j] = 2;    //2不可能i=j
                        martix[j][i] = 2;
                    }
                }
            }
        }
        Martix Prefence = new Martix(martix);
        return Prefence;
    }
   
    public string[] get_option_descrip()
    {
        return option_descrip;
    }

    public string[] get_DM_descrip()
    {
        return DM_descrip;
    }

    public int[] get_option()
    {
        return option;
    }

    public int get_feasible_state()
    {
        return feasible_state;
    }

    public int[][] get_state()
    {
        return state;
    }
    public int get_optionnum()
    {
        int sum = 0;
        for (int i = 0; i < option.Length; i++)
        {
            sum += option[i];
        }
        return sum;

    }

    public void set_transitions(int[] single, int[][] multiple_pattern1, int[][] multiple_pattern2)                          //设置状态转移
    {
        this.model.single = single;
        this.model.transPattern1 = multiple_pattern1;
        this.model.transPattern2 = multiple_pattern2;
        //transitions  第一层下标表示DM 第二层下标表示
        this.transitions = new int[this.option.Length][][];
        int dm_i_op = 0;                    //dm_i_op=0 表示dm_i控制的第一个option位置；
        for (int i = 0; i < this.option.Length; i++)  //dm_num
        {
            if (i > 0) dm_i_op += this.option[i - 1];
            this.transitions[i] = new int[this.feasible_state][];
            for (int j = 0; j < this.feasible_state; j++)
            {
                //每个Si(u) 默认长度是Math.Pow(2,this.option[i])初始时对每个赋值为-1；其中存的内容是state的序号
                this.transitions[i][j] = new int[(int)Math.Pow(2, this.option[i])];
                for (int num = 0; num < transitions[i][j].Length; num++) transitions[i][j][num] = -1;
                //point 用于指向transitions[i][j]的位置
                int point = 0;
                for (int k = 0; k < this.feasible_state; k++)
                {
                    if (k == j) continue;
                    int juge1 = 1;
                    //第一层过滤器；
                    for (int juge_J_K = 0; juge_J_K < this.get_optionnum(); juge_J_K++)
                    {
                        if (dm_i_op <= juge_J_K && juge_J_K < dm_i_op + this.option[i]) continue;
                        if (this.state[j][juge_J_K] + this.state[k][juge_J_K] == 1)    //考虑复合状态这边只有1-0 和0-1不能够通过，2-0 2-1  0-2 1-2 都是应该能够通过的
                        {
                            juge1 = 0;                       //只要不是break出来的juge1都为1 说明通过第一层过滤器；
                            break;
                        }
                    }
                    if (juge1 == 0) continue;
                    //第二层过滤器 判断是否满足int []single;
                    for (int juge_single = 0; juge_single < this.get_optionnum(); juge_single++)  //single =0   only0->1  single=1 only 1->0   single=2 都ok
                    {
                        if (single[juge_single] == 0)
                        {
                            if (this.state[j][juge_single] == 1 && this.state[k][juge_single] == 0)   //  1->0 1->2 是不被允许的。
                            {
                                juge1 = 0;
                                break;
                            }
                        }
                        if (single[juge_single] == 1)      //0->1   0->2 
                        {
                            if (this.state[j][juge_single] == 0 && this.state[k][juge_single] == 1)
                            {
                                juge1 = 0;
                                break;
                            }
                        }
                    }
                    if (juge1 == 0) continue;
                    //第三层过滤器
                    if (multiple_pattern1 == null) goto jump3;
                    for (int juge_pattern = 0; juge_pattern < multiple_pattern1.Length; juge_pattern++)
                    {
                        int m0 = 0;          //m,n 用来判断是否与pattern match  bj  bk  分别为状态 j k 的数值
                        int m1 = 0;
                        int n0 = 0;
                        int n1 = 0;
                        int bj = 0;
                        int bk = 0;
                        for (int rol = 0; rol < get_optionnum(); rol++)
                        {
                            // m 表示pattern1  n表示pattern2；
                            m0 += (int)(Math.Sign(multiple_pattern1[juge_pattern][rol]) * Math.Pow(2, get_optionnum() - rol - 1));
                            m1 += (int)((1 - Math.Abs(1 - multiple_pattern1[juge_pattern][rol])) * Math.Pow(2, get_optionnum() - rol - 1));
                            n0 += (int)(Math.Sign(multiple_pattern2[juge_pattern][rol]) * Math.Pow(2, get_optionnum() - rol - 1));
                            n1 += (int)((1 - Math.Abs(1 - multiple_pattern2[juge_pattern][rol])) * Math.Pow(2, get_optionnum() - rol - 1));
                            bj += state[j][rol] * (int)Math.Pow(2, get_optionnum() - rol - 1);
                            bk += state[k][rol] * (int)Math.Pow(2, get_optionnum() - rol - 1);
                        }

                        if (((bj | m0) == m0 && (bj & m1) == m1) && ((bk | n0) == n0 && (bk & n1) == n1))   //如果bj与pattern1 match  bk与pattern2match 则不状态k不满足；
                        {
                            juge1 = 0;
                            break;
                        }
                    }
                    if (juge1 == 0) continue;
                //若能走到这里必然能通过全部的三重过滤器，那么从状态j能够到达状态k；
                jump3: this.transitions[i][j][point] = k;
                    point++;

                }
            }
        }
        calculate_trans_martix();
    }

    private void calculate_trans_martix()
    {
        int dm_num = this.option.Length; ;
        int state_num = this.feasible_state;

        for (int dm = 0; dm < dm_num; dm++)
        {
            int[][] trans_martix = new int[state_num][];
            for (int state_i = 0; state_i < state_num; state_i++)
            {
                trans_martix[state_i] = new int[state_num];
                for (int state_j = 0; state_j < state_num; state_j++)
                {
                    trans_martix[state_i][state_j] = 0;
                    for (int search = 0; search < this.transitions[dm][state_i].Length; search++)
                    {
                        if (this.transitions[dm][state_i][search] == -1) break;
                        else if (this.transitions[dm][state_i][search] == state_j)
                        {
                            trans_martix[state_i][state_j] = 1;
                            break;
                        }
                    }

                }

            }
            this.trans_martix[dm] = trans_martix;
        }
    }

    public int[][][] get_transitions()
    {
        return this.transitions;
    }

    public int[][] get_trans_marix(int dm)
    {
        return (int[][])this.trans_martix[dm];
    }

    public int GetPerfenceType()
    { //返回偏好类型 简单偏好0，强度偏好1；不确定偏好2；混合偏好3；
        //遍历所有偏好矩阵去判断类型
        int type = 0;
        int strength = 0;
        int uncertain = 0;
        int dm_num = this.get_DM_descrip().Length;
        int state_num = this.get_feasible_state();
        for (int dm = 0; dm < dm_num; dm++)
        {
            Martix martix = this.get_Prefence_martix(dm);
            for (int i = 0; i < state_num; i++)
            {
                for (int j = 0; j < state_num; j++)
                {
                    if (martix.martix[i][j] == 2) uncertain = 1;
                    if (martix.martix[i][j] == 10) strength = 1;
                }
            }
        }

        type = 2 * uncertain + strength;   //刚好满足这个式子
        return type;
    }

    public void SetPrefence(string[] paixu, int dm)
    {
        //排序是一个字符串数组，每一个string是一组强度偏好，多组强度偏好来构成一组混合偏好
        int[][] perfence_martix = manualperfence.calculate_martix(paixu, this.feasible_state);
        int[][] perfence_martixsanjiao = new int[feasible_state][];

        for (int i = 0; i < feasible_state; i++)
        {
            perfence_martixsanjiao[i] = new int[i + 1];
            for (int j = 0; j <= i; j++)
            {
                perfence_martixsanjiao[i][j] = perfence_martix[i][j];

            }
        }
        this.set_perfencemanual(perfence_martixsanjiao, dm);
    }
}

