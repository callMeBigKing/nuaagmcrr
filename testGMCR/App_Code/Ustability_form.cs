using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;

/// <summary>
/// Ustability_form 的摘要说明
/// </summary>
public class Ustability_form
{
	private GMCR model;
    public int[][] form;
    ArrayList Nash_a;
    ArrayList Nash_b;
    ArrayList Nash_c;
    ArrayList Nash_d;

    ArrayList GMR_a;
    ArrayList GMR_b;
    ArrayList GMR_c;
    ArrayList GMR_d;

    ArrayList SMR_a;
    ArrayList SMR_b;
    ArrayList SMR_c;
    ArrayList SMR_d;

    ArrayList SEQ_a;
    ArrayList SEQ_b;
    ArrayList SEQ_c;
    ArrayList SEQ_d;
    
    public Ustability_form(GMCR model)
	{
        this.model = model;
        Nash_a = new ArrayList(); Nash_b = new ArrayList(); Nash_c = new ArrayList(); Nash_d = new ArrayList();
        GMR_a = new ArrayList(); GMR_b = new ArrayList(); GMR_c = new ArrayList(); GMR_d= new ArrayList();
        SMR_a = new ArrayList(); SMR_b = new ArrayList(); SMR_c = new ArrayList(); SMR_d = new ArrayList();
        SEQ_a = new ArrayList(); SEQ_b = new ArrayList(); SEQ_c = new ArrayList(); SEQ_d = new ArrayList();
        form = new int[4 * model.get_feasible_state()][];
        for (int i = 0; i < 4 * model.get_feasible_state(); i++)
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
               Ustability dm_stability = new Ustability(model, dm);
               dm_stability.calculate_stability();
               Nash_a.Add(dm_stability.Nash_a); Nash_b.Add(dm_stability.Nash_b); Nash_c.Add(dm_stability.Nash_c); Nash_d.Add(dm_stability.Nash_d);
               GMR_a.Add(dm_stability.GMR_a); GMR_b.Add(dm_stability.GMR_b); GMR_c.Add(dm_stability.GMR_c); GMR_d.Add(dm_stability.GMR_d);
               SMR_a.Add(dm_stability.SMR_a); SMR_b.Add(dm_stability.SMR_b); SMR_c.Add(dm_stability.SMR_c); SMR_d.Add(dm_stability.SMR_d);
               SEQ_a.Add(dm_stability.SEQ_a); SEQ_b.Add(dm_stability.SEQ_b); SEQ_c.Add(dm_stability.SEQ_c); SEQ_d.Add(dm_stability.SEQ_d);
        }
        for (int state = 0; state < state_num; state++)
        { //用于判断Eq 是否为1
            int allNash_flag_a = 1; int allNash_flag_b = 1; int allNash_flag_c = 1; int allNash_flag_d = 1;
            int allGMR_flag_a = 1; int allGMR_flag_b = 1; int allGMR_flag_c = 1; int allGMR_flag_d = 1;
            int allSMR_flag_a = 1; int allSMR_flag_b = 1; int allSMR_flag_c = 1; int allSMR_flag_d = 1;
            int allSEQ_flag_a = 1; int allSEQ_flag_b = 1; int allSEQ_flag_c = 1; int allSEQ_flag_d = 1;
            for (int dm = 0; dm < dm_num; dm++)
            {

                Martix Nasha_martix = (Martix)Nash_a[dm]; Martix Nashb_martix = (Martix)Nash_b[dm]; Martix Nashc_martix = (Martix)Nash_c[dm]; Martix Nashd_martix = (Martix)Nash_d[dm];
                Martix GMRa_martix = (Martix)GMR_a[dm]; Martix GMRb_martix = (Martix)GMR_b[dm]; Martix GMRc_martix = (Martix)GMR_c[dm]; Martix GMRd_martix = (Martix)GMR_d[dm];
                Martix SMRa_martix = (Martix)SMR_a[dm]; Martix SMRb_martix = (Martix)SMR_b[dm]; Martix SMRc_martix = (Martix)SMR_c[dm]; Martix SMRd_martix = (Martix)SMR_d[dm];
                Martix SEQa_martix = (Martix)SEQ_a[dm]; Martix SEQb_martix = (Martix)SEQ_b[dm]; Martix SEQc_martix = (Martix)SEQ_d[dm]; Martix SEQd_martix = (Martix)SEQ_d[dm];
                if (Nasha_martix.martix[state][state] == 0)
                { form[state*4][dm] = 1; }
                else { form[state*4][dm] = 0; allNash_flag_a = 0; }

                if (Nashb_martix.martix[state][state] == 0)
                { form[state * 4+1][dm] = 1; }
                else { form[state * 4+1][dm] = 0; allNash_flag_b = 0; }

                if (Nashc_martix.martix[state][state] == 0)
                { form[state * 4+2][dm] = 1; }
                else { form[state * 4+2][dm] = 0; allNash_flag_c = 0; }

                if (Nashd_martix.martix[state][state] == 0)
                { form[state * 4+3][dm] = 1; }
                else { form[state * 4+3][dm] = 0; allNash_flag_d = 0; }


                if (GMRa_martix.martix[state][state] == 0) form[state*4][dm_num + 1 + dm] = 1;
                else { form[state*4][dm_num + 1 + dm] = 0; allGMR_flag_a = 0; }

                if (GMRb_martix.martix[state][state] == 0) form[state * 4+1][dm_num + 1 + dm] = 1;
                else { form[state * 4+1][dm_num + 1 + dm] = 0; allGMR_flag_b = 0; }

                if (GMRc_martix.martix[state][state] == 0) form[state * 4+2][dm_num + 1 + dm] = 1;
                else { form[state * 4+2][dm_num + 1 + dm] = 0; allGMR_flag_c = 0; }

                if (GMRd_martix.martix[state][state] == 0) form[state * 4+3][dm_num + 1 + dm] = 1;
                else { form[state * 4+3][dm_num + 1 + dm] = 0; allGMR_flag_d = 0; }



                if (SMRa_martix.martix[state][state] == 0) form[state*4][2 * (dm_num + 1) + dm] = 1;
                else { form[state*4][2 * (dm_num + 1) + dm] = 0; allSMR_flag_a = 0; }

                if (SMRb_martix.martix[state][state] == 0) form[state * 4+1][2 * (dm_num + 1) + dm] = 1;
                else { form[state * 4+1][2 * (dm_num + 1) + dm] = 0; allSMR_flag_b = 0; }

                if (SMRc_martix.martix[state][state] == 0) form[state * 4+2][2 * (dm_num + 1) + dm] = 1;
                else { form[state * 4+2][2 * (dm_num + 1) + dm] = 0; allSMR_flag_c = 0; }

                if (SMRd_martix.martix[state][state] == 0) form[state * 4+3][2 * (dm_num + 1) + dm] = 1;
                else { form[state * 4+3][2 * (dm_num + 1) + dm] = 0; allSMR_flag_d = 0; }


                if (SEQa_martix.martix[state][state] == 0) form[state*4][3 * (dm_num + 1) + dm] = 1;
                else { form[state*4][3 * (dm_num + 1) + dm] = 0; allSEQ_flag_a = 0; }

                if (SEQb_martix.martix[state][state] == 0) form[state * 4+1][3 * (dm_num + 1) + dm] = 1;
                else { form[state * 4+1][3 * (dm_num + 1) + dm] = 0; allSEQ_flag_b = 0; }

                if (SEQc_martix.martix[state][state] == 0) form[state * 4+2][3 * (dm_num + 1) + dm] = 1;
                else { form[state * 4+2][3 * (dm_num + 1) + dm] = 0; allSEQ_flag_c = 0; }

                if (SEQd_martix.martix[state][state] == 0) form[state * 4+3][3 * (dm_num + 1) + dm] = 1;
                else { form[state * 4+3][3 * (dm_num + 1) + dm] = 0; allSEQ_flag_d = 0; }
            }
            if (allNash_flag_a == 1) form[state*4][dm_num] = 1;
            else form[state*4][dm_num] = 0;
            if (allNash_flag_b == 1) form[state * 4+1][dm_num] = 1;
            else form[state * 4+1][dm_num] = 0;
            if (allNash_flag_c == 1) form[state * 4+2][dm_num] = 1;
            else form[state * 4+2][dm_num] = 0;
            if (allNash_flag_d == 1) form[state * 4+3][dm_num] = 1;
            else form[state * 4+3][dm_num] = 0;

            if (allGMR_flag_a == 1) form[state*4][dm_num + 1 + dm_num] = 1;
            else form[state*4][dm_num + 1 + dm_num] = 0;
            if (allGMR_flag_b == 1) form[state * 4+1][dm_num + 1 + dm_num] = 1;
            else form[state * 4+1][dm_num + 1 + dm_num] = 0;
            if (allGMR_flag_c == 1) form[state * 4+2][dm_num + 1 + dm_num] = 1;
            else form[state * 4+2][dm_num + 1 + dm_num] = 0;
            if (allGMR_flag_d == 1) form[state * 4+3][dm_num + 1 + dm_num] = 1;
            else form[state * 4+3][dm_num + 1 + dm_num] = 0;


            if (allSMR_flag_a == 1) form[state*4][2 * (dm_num + 1) + dm_num] = 1;
            else form[state*4][2 * (dm_num + 1) + dm_num] = 0;
            if (allSMR_flag_b == 1) form[state * 4+1][2 * (dm_num + 1) + dm_num] = 1;
            else form[state * 4+1][2 * (dm_num + 1) + dm_num] = 0;
            if (allSMR_flag_c == 1) form[state * 4+2][2 * (dm_num + 1) + dm_num] = 1;
            else form[state * 4+2][2 * (dm_num + 1) + dm_num] = 0;
            if (allSMR_flag_d == 1) form[state * 4+3][2 * (dm_num + 1) + dm_num] = 1;
            else form[state * 4+3][2 * (dm_num + 1) + dm_num] = 0;


            if (allSEQ_flag_a == 1) form[state*4][3 * (dm_num + 1) + dm_num] = 1;
            else form[state*4][3 * (dm_num + 1) + dm_num] = 0;
            if (allSEQ_flag_b == 1) form[state * 4+1][3 * (dm_num + 1) + dm_num] = 1;
            else form[state * 4+1][3 * (dm_num + 1) + dm_num] = 0;
            if (allSEQ_flag_c == 1) form[state * 4+2][3 * (dm_num + 1) + dm_num] = 1;
            else form[state * 4+2][3 * (dm_num + 1) + dm_num] = 0;
            if (allSEQ_flag_d == 1) form[state * 4+3][3 * (dm_num + 1) + dm_num] = 1;
            else form[state * 4+3][3 * (dm_num + 1) + dm_num] = 0;
        }
       
            
        
    }
}