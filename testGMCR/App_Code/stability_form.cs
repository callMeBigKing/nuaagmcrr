using System;
using System.Collections.Generic;

using System.Web;
using System.Collections;
/// <summary>
/// stability_form 的摘要说明
/// </summary>
public class stability_form
{
    private GMCR model;
    public int[][] form;
    ArrayList Nash;
    ArrayList GMR;
    ArrayList SMR;
    ArrayList SEQ;
    
    public stability_form(GMCR model)
	{
        this.model = model;
        Nash = new ArrayList();
        GMR = new ArrayList();
        SMR = new ArrayList();
        SEQ = new ArrayList();
        form = new int[model.get_feasible_state()][];
        for (int i = 0; i < model.get_feasible_state(); i++)
        {
            form[i] = new int[4 *( model.get_DM_descrip().Length + 1)];
        }
	}

    public void setform()
    {
        int dm_num = model.get_DM_descrip().Length;
        int state_num = model.get_feasible_state();

        for (int dm = 0; dm < dm_num; dm++)
        {

            ;
               stability dm_stability = new stability(model, dm);
               dm_stability.calculate_stability();
               Nash.Add(dm_stability.Nash);
               GMR.Add(dm_stability.GMR);
               SMR.Add(dm_stability.SMR);
               SEQ.Add(dm_stability.SEQ);
       
        }
        for (int state = 0; state < state_num; state++)
        {
            int allNash_flag = 1;  //用于判断Eq 是否为1
            int allGMR_flag = 1;  //用于判断Eq 是否为1
            int allSMR_flag = 1;  //用于判断Eq 是否为1
            int allSEQ_flag = 1;  //用于判断Eq 是否为1
            for (int dm = 0; dm < dm_num; dm++)
            {

                Martix Nash_martix = (Martix)Nash[dm];
                Martix GMR_martix = (Martix)GMR[dm];
                Martix SMR_martix = (Martix)SMR[dm];
                Martix SEQ_martix = (Martix)SEQ[dm];
                if (Nash_martix.martix[state][state] == 0)
                { form[state][dm] = 1; }
                else { form[state][dm] = 0; allNash_flag = 0; }
                if (GMR_martix.martix[state][state] == 0) form[state][dm_num + 1 + dm] = 1;
                else { form[state][dm_num + 1 + dm] = 0; allGMR_flag = 0; }
                if (SMR_martix.martix[state][state] == 0) form[state][2 * (dm_num + 1) + dm] = 1;
                else { form[state][2 * (dm_num + 1) + dm] = 0; allSMR_flag = 0; }
                if (SEQ_martix.martix[state][state] == 0) form[state][3 * (dm_num + 1) + dm] = 1;
                else { form[state][3 * (dm_num + 1) + dm] = 0; allSEQ_flag = 0; }
            }
            if (allNash_flag == 1) form[state][dm_num] = 1;
            else form[state][dm_num] = 0;
            if (allGMR_flag == 1) form[state][dm_num + 1 + dm_num] = 1;
            else form[state][dm_num + 1 + dm_num] = 0;
            if (allSMR_flag == 1) form[state][2 * (dm_num + 1) + dm_num] = 1;
            else form[state][2 * (dm_num + 1) + dm_num] = 0;
            if (allSEQ_flag == 1) form[state][3 * (dm_num + 1) + dm_num] = 1;
            else form[state][3 * (dm_num + 1) + dm_num] = 0;
        } 
    }
}