using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Stablity_form 的摘要说明
/// </summary>
public class SStability_form
{

    private GMCR model;
    public int[][] form;
    ArrayList Nash;
    ArrayList GMR;
    ArrayList SMR;
    ArrayList SEQ;
    ///strong stabilit
    ArrayList SGMR;
    ArrayList SSMR;
    ArrayList SSEQ;
 
    public SStability_form(GMCR model)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        this.model = model;
        Nash = new ArrayList();
        GMR = new ArrayList();
        SMR = new ArrayList();
        SEQ = new ArrayList();
        ///strong stabilit
        SGMR = new ArrayList();
        SSMR = new ArrayList();
        SSEQ = new ArrayList();
        form = new int[ model.get_feasible_state()][];
        for (int i = 0; i <  model.get_feasible_state(); i++)
        {
            form[i] = new int[10 * (model.get_DM_descrip().Length + 1)];
                           //一般的四个，强稳定3个，弱稳定三个
        }

    }

    public void setform()
    {

        int dm_num = model.get_DM_descrip().Length;
        int state_num = model.get_feasible_state();
        for (int dm = 0; dm < dm_num; dm++)
        {

            ;
            SStability dm_stability = new SStability(model, dm);
            dm_stability.calculate_stability();
            Nash.Add(dm_stability.Nash);
            GMR.Add(dm_stability.GMR);
            SMR.Add(dm_stability.SMR);
            SEQ.Add(dm_stability.SEQ);
            ;
            SGMR.Add(dm_stability.SGMR);
            SSMR.Add(dm_stability.SSMR);
            SSEQ.Add(dm_stability.SSEQ);

        }

        for (int state = 0; state < state_num; state++)
        {
            int allNash_flag = 1;  //用于判断Eq 是否为1
            int allGMR_flag = 1;  //用于判断Eq 是否为1
            int allSMR_flag = 1;  //用于判断Eq 是否为1
            int allSEQ_flag = 1;  //用于判断Eq 是否为1
            ///strong stabilit
            int allSGMR_flag = 1;  //用于判断Eq 是否为1
            int allSSMR_flag = 1;  //用于判断Eq 是否为1
            int allSSEQ_flag = 1;  //用于判断Eq 是否为1
            //weak stability
            int allWGMR_flag = 1;  //用于判断Eq 是否为1
            int allWSMR_flag = 1;  //用于判断Eq 是否为1
            int allWSEQ_flag = 1;  //用于判断Eq 是否为1
            for (int dm = 0; dm < dm_num; dm++)
            {

                Martix Nash_martix = (Martix)Nash[dm];
                Martix GMR_martix = (Martix)GMR[dm];
                Martix SMR_martix = (Martix)SMR[dm];
                Martix SEQ_martix = (Martix)SEQ[dm];
                ///strong stabilit
                Martix SGMR_martix = (Martix)SGMR[dm];
                Martix SSMR_martix = (Martix)SSMR[dm];
                Martix SSEQ_martix = (Martix)SSEQ[dm];


                if (Nash_martix.martix[state][state] == 0)
                { form[state][dm] = 1; }
                else { form[state][dm] = 0; allNash_flag = 0; }

                if (GMR_martix.martix[state][state] == 0) form[state][dm_num + 1 + dm] = 1;
                else { form[state][dm_num + 1 + dm] = 0; allGMR_flag = 0; }

                if (SMR_martix.martix[state][state] == 0) form[state][2 * (dm_num + 1) + dm] = 1;
                else { form[state][2 * (dm_num + 1) + dm] = 0; allSMR_flag = 0; }

                if (SEQ_martix.martix[state][state] == 0) form[state][3 * (dm_num + 1) + dm] = 1;
                else { form[state][3 * (dm_num + 1) + dm] = 0; allSEQ_flag = 0; }

                ///strong stabilit
                if (SGMR_martix.martix[state][state] == 0) form[state][4 * (dm_num + 1) + dm] = 1;
                else { form[state][4 * (dm_num + 1) + dm] = 0; allSGMR_flag = 0; }

                if (SMR_martix.martix[state][state] == 0) form[state][5 * (dm_num + 1) + dm] = 1;
                else { form[state][5 * (dm_num + 1) + dm] = 0; allSSMR_flag = 0; }

                if (SEQ_martix.martix[state][state] == 0) form[state][6 * (dm_num + 1) + dm] = 1;
                else { form[state][6 * (dm_num + 1) + dm] = 0; allSSEQ_flag = 0; }

                // 弱稳定性通过一般的稳定性和强稳定性来计算
                form[state][7 * (dm_num + 1) + dm] = form[state][dm_num + 1 + dm]- form[state][4 * (dm_num + 1) + dm];
                form[state][8 * (dm_num + 1) + dm] = form[state][2*(dm_num + 1) + dm] - form[state][5 * (dm_num + 1) + dm];
                form[state][9 * (dm_num + 1) + dm] = form[state][3 * (dm_num + 1) + dm] - form[state][6 * (dm_num + 1) + dm];
                if (form[state][7 * (dm_num + 1) + dm] == 0) allWGMR_flag = 0;
                if (form[state][8 * (dm_num + 1) + dm] == 0) allWSMR_flag = 0;
                if (form[state][9 * (dm_num + 1) + dm] == 0) allWSEQ_flag = 0;
            }
            if (allNash_flag == 1) form[state][dm_num] = 1;
            else form[state][dm_num] = 0;
            if (allGMR_flag == 1) form[state][dm_num + 1 + dm_num] = 1;
            else form[state][dm_num + 1 + dm_num] = 0;
            if (allSMR_flag == 1) form[state][2 * (dm_num + 1) + dm_num] = 1;
            else form[state][2 * (dm_num + 1) + dm_num] = 0;
            if (allSEQ_flag == 1) form[state][3 * (dm_num + 1) + dm_num] = 1;
            else form[state][3 * (dm_num + 1) + dm_num] = 0;

            // strong stability
            if (allSGMR_flag == 1) form[state][4 * (dm_num + 1) + dm_num] = 1;
            else form[state][4 * (dm_num + 1) + dm_num] = 0;
            if (allSSMR_flag == 1) form[state][5 * (dm_num + 1) + dm_num] = 1;
            else form[state][5 * (dm_num + 1) + dm_num] = 0;
            if (allSSEQ_flag == 1) form[state][6 * (dm_num + 1) + dm_num] = 1;
            else form[state][6 * (dm_num + 1) + dm_num] = 0;

            // weak stability
            if (allWGMR_flag == 1) form[state][7 * (dm_num + 1) + dm_num] = 1;
            else form[state][7 * (dm_num + 1) + dm_num] = 0;
            if (allWSMR_flag == 1) form[state][8 * (dm_num + 1) + dm_num] = 1;
            else form[state][8 * (dm_num + 1) + dm_num] = 0;
            if (allWSEQ_flag == 1) form[state][9 * (dm_num + 1) + dm_num] = 1;
            else form[state][9 * (dm_num + 1) + dm_num] = 0;
        }


        //通过一般稳定和强稳定计算弱稳定
       
        

    }


}