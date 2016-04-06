using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class shuru : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string manualdata = this.TextBox1.Text;
        try
        {
          string [] strdata=manualdata.Split(',');
            
          int[] data = new int[strdata.Length];
          for (int i = 0; i < strdata.Length; i++)
          {
              data[i] = int.Parse(strdata[i]);
          }
          GMCR model = new GMCR();
          Session.Timeout = 70;
          model = (GMCR)Session["model"];
          model.inputdata(data);
          Session["model"] = model;
          Response.Redirect("~/页面/datadescription.aspx");
       
        }
        catch
        {
            Label1.Text = "输入格式有误";
            Label1.BackColor = Color.Gray;
            TextBox1.Text = "";
            TextBox1.Focus(); 
        }  
        
    }
}

/*int[] data = new int[manualdata.Length];
    int firstindex = 0;
    int secondindex = 0;
    int i;
        
        for (i = 0; i < manualdata.Length; i++)
        {
            secondindex = manualdata.IndexOf(",", firstindex);
            if (secondindex == -1) break;
            string temp = manualdata.Substring(firstindex, secondindex - firstindex);
            data[i] = int.Parse(temp);
            firstindex = secondindex + 1;

        }
        data[i] = int.Parse(manualdata.Substring(firstindex, manualdata.Length - firstindex));
  
       int[] datainput = new int[i+1];
       for (int j = 0; j <= i; j++)
      {
           datainput[j] = data[j];
       }
     */