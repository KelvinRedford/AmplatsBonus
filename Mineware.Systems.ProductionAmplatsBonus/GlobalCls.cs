using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


public class Procedures
{
    private static string m_BookFrm = "";
    public static string BookFrm { get { return m_BookFrm; } set { m_BookFrm = value; } }

    private static string m_PropFrm1 = "";
    public static string PropFrm1 { get { return m_PropFrm1; } set { m_PropFrm1 = value; } }

    private static string m_ServicesFrm = "";
    public static string ServicesFrm { get { return m_ServicesFrm; } set { m_ServicesFrm = value; } }
    
    private static int m_Prod = 0;
    private static string m_Prod2 = "";

    public static int Prod { get { return m_Prod; } set { m_Prod = value; } }
    public static string Prod2 { get { return m_Prod2; } set { m_Prod2 = value; } }

    private static string m_MsgText = "";
    public static string MsgText { get { return m_MsgText; } set { m_MsgText = value; } }

    private static string m_MsgInfo = "";
    public static string MsgInfo { get { return m_MsgInfo; } set { m_MsgInfo = value; } }

    //Production month to be used for system calculations
    public void ProdMonthCalc(int ProdMonth1)
    {
        //int Prod;
        Decimal month = Convert.ToDecimal(ProdMonth1);
        String PMonth = month.ToString();
        PMonth.Substring(4, 2);
        if (Convert.ToInt32(PMonth.Substring(4, 2)) > 12)
        {
            int M = Convert.ToInt32(PMonth.Substring(0, 4));
            M++;
            PMonth = M.ToString();
            PMonth = PMonth + "01";
            ProdMonth1 = Convert.ToInt32(PMonth);
        }
        else
        {
            if (Convert.ToInt32(PMonth.Substring(4, 2)) < 1)
            {
                int M = Convert.ToInt32(PMonth.Substring(0, 4));
                M--;
                PMonth = M.ToString();
                PMonth = PMonth + "12";
                ProdMonth1 = Convert.ToInt32(PMonth);
            }
        }
        Procedures.Prod = ProdMonth1;
    }

   

    //Production month that that will be displayed on the front end
    public void ProdMonthVis(int ProdMonth1)
    {        
        Procedures.Prod2 = ProdMonth1.ToString().Substring(0, 4);

        if (ProdMonth1.ToString().Substring(4, 2) == "01")
        {
            Procedures.Prod2 = "Jan-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "02")
        {
            Procedures.Prod2 = "Feb-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "03")
        {
            Procedures.Prod2 = "Mar-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "04")
        {
            Procedures.Prod2 = "Apr-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "05")
        {
            Procedures.Prod2 = "May-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "06")
        {
            Procedures.Prod2 = "Jun-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "07")
        {
            Procedures.Prod2 = "Jul-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "08")
        {
            Procedures.Prod2 = "Aug-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "09")
        {
            Procedures.Prod2 = "Sep-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "10")
        {
            Procedures.Prod2 = "Oct-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "11")
        {
            Procedures.Prod2 = "Nov-" + Procedures.Prod2;
        }

        if (ProdMonth1.ToString().Substring(4, 2) == "12")
        {
            Procedures.Prod2 = "Dec-" + Procedures.Prod2;
        }
    }

    //extracts the string value before the colon
    public string ExtractBeforeColon(string TheString)
    {
        if (TheString != "")
        {
        string BeforeColon;

        int index = TheString.IndexOf(":");

        BeforeColon = TheString.Substring(0, index); 

        return BeforeColon;
        }
        else { return "";
        }
    }

    //extracts the string value after the colon
    public string ExtractAfterColon(string TheString)
    {
        string AfterColon;

        int index = TheString.IndexOf(":"); // Kry die postion van die :

        AfterColon = TheString.Substring(index + 1); // kry alles na :

        return AfterColon;
    }
    
    public DataTable GetSections(string ProdMonth, string HierId, string SectionId)
    {
        MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
        _dbMan.ConnectionString = "";

        _dbMan.SqlStatement = " Select SECTIONid, Name,  Hierarchicalid Hier " +
                              "from Section s where s.Prodmonth = '" + ProdMonth.ToString() + "' and HierarchicalType = 'Pro' ";
                             if (HierId.ToString() != "NO")
                              {
                              _dbMan.SqlStatement = _dbMan.SqlStatement +" and Hierarchicalid = '"+ HierId.ToString() +"' " ;
                              }
                             
                              _dbMan.SqlStatement = _dbMan.SqlStatement + "and Sectionid like '" + SectionId.ToString() + "%' ";
                              
                              _dbMan.SqlStatement = _dbMan.SqlStatement +" order by SECTIONid ";
        _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
        _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
        _dbMan.ResultsTableName = "GetSections"; 
        _dbMan.ExecuteInstruction();

        DataTable dt1 = _dbMan.ResultsDataTable;
        return dt1;
    }

    public DataView Search(DataTable SearchTable, string SearchString)
    {
        DataView dv = new DataView(SearchTable);
        string SearchExpression = null;

        if (!String.IsNullOrEmpty(SearchString))//(Filtertxt.Text))
        {

            SearchExpression = string.Format("'{0}%'", SearchString);//Filtertxt.Text);
            dv.RowFilter = "Description like " + SearchExpression;
        }

        //DataTable dtResult = 
        //MessageBox.Show(SearchTable.Rows.Count.ToString());
        return dv;
    }

   
}

public class SysSettings
{
    private static int m_ProdMonth = 0;
    private static int m_MillMonth = 0;
    private static string m_Banner = "";
    private static Decimal m_StdAdv = 0;
    private static string m_CheckMeas = "";
    private static string M_PlanType = "";
    private static string M_CleanShift = "";
    private static string M_AdjBook = "";
    private static int M_BlastQual = 0;
    private static string M_DSOrg = "N";
    private static string M_CHkMeasLevel = "MO";
    private static string M_PlanNotes = "";
    private static string M_CurDir = "";  

    public static int ProdMonth { get { return m_ProdMonth; } set { m_ProdMonth = value; } }
    public static int MillMonth { get { return m_MillMonth; } set { m_MillMonth = value; } }
    public static string Banner { get { return m_Banner; } set { m_Banner = value; } }
    public static Decimal StdAdv { get { return m_StdAdv; } set { m_StdAdv = value; } }
    public static string CheckMeas { get { return m_CheckMeas; } set { m_CheckMeas = value; } }
    public static string PlanType { get { return M_PlanType; } set { M_PlanType = value; } }
    public static string CleanShift { get { return M_CleanShift; } set { M_CleanShift = value; } }
    public static string AdjBook { get { return M_AdjBook; } set { M_AdjBook = value; } }
    public static int BlastQual { get { return M_BlastQual; } set { M_BlastQual = value; } }
    public static string DSOrg { get { return M_DSOrg; } set { M_DSOrg = value; } }
    public static string CHkMeasLevel { get { return M_CHkMeasLevel; } set { M_CHkMeasLevel = value; } }
    public static string PlanNotes { get { return M_PlanNotes; } set { M_PlanNotes = value; } }
    public static string CurDir { get { return M_CurDir; } set { M_CurDir = value; } }


    //gets all the fields from the SYSSET table
    public void GetSysSettings()
    {
        MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
        _dbMan.ConnectionString = "";

        _dbMan.SqlStatement = "select * from sysset ";
        _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
        _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
        _dbMan.ExecuteInstruction();
        DataTable SubB = _dbMan.ResultsDataTable;
       

        SysSettings.ProdMonth = Convert.ToInt32(SubB.Rows[0]["currentproductionmonth"].ToString());
        SysSettings.MillMonth = Convert.ToInt32(SubB.Rows[0]["currentmillmonth"].ToString());
        SysSettings.Banner = SubB.Rows[0]["Banner"].ToString();
        SysSettings.StdAdv = Convert.ToDecimal(SubB.Rows[0]["stpadv"].ToString());
        SysSettings.CheckMeas = SubB.Rows[0]["CheckMeas"].ToString();
        SysSettings.PlanType = SubB.Rows[0]["PlanType"].ToString();
        SysSettings.CleanShift = SubB.Rows[0]["CleanShift"].ToString();
        SysSettings.AdjBook = SubB.Rows[0]["AdjBook"].ToString();
        SysSettings.BlastQual = Convert.ToInt32(Math.Round(Convert.ToDecimal(SubB.Rows[0]["percblastqualification"].ToString()),0));
        SysSettings.DSOrg = SubB.Rows[0]["dsorg"].ToString();
        SysSettings.CHkMeasLevel = SubB.Rows[0]["checkmeaslvl"].ToString();
        SysSettings.PlanNotes = SubB.Rows[0]["PlanNotes"].ToString();
    }

    //sets the logged on user information
    public void SetUserInfo()
    {
        //MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
        //_dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

        //_dbMan.SqlStatement = " select * from BMCS_Users a \r\n "+
        //                      " left outer join \r\n "+
        //                      " BMCS_Profile b on a.ProfileID = b.ProfileID \r\n " +
        //                      " where userid = '" + clsUserInfo.UserID + "'";
        //_dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
        //_dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
        //_dbMan.ExecuteInstruction();
        //DataTable dtUsers = _dbMan.ResultsDataTable;

        //if (dtUsers.Rows.Count > 0)
        //{
        //    clsUserInfo.UserName = dtUsers.Rows[0]["name"].ToString();
        //    clsUserInfo.ProfileID = dtUsers.Rows[0]["ProfileID"].ToString();
        //    clsUserInfo.SysAdmin = dtUsers.Rows[0]["SystemAdmin"].ToString();
        //    clsUserInfo.SuperUser = dtUsers.Rows[0]["SuperUser"].ToString();
        //}
    }

 
    

}

public class clsValidations
{
    public enum ValidationType
    {
        MWInteger5,
        MWCleanText,
        MWDouble5D1,
        MWDouble5D2,
        MWDouble5D3,
        MWDouble5D4,
        MWDate
    }
    public ValidationType MWValidationType;

    public clsValidations()
    {
    }

    public string _MWInput;
    public string MWInput { set { _MWInput = value; } }

    public bool Validate()
    {
        try
        {
            switch (MWValidationType)
            {
                case ValidationType.MWCleanText:

                    // Clean Text without ' and / \ only a-z A-Z and -

                    Regex ValFactor1 = new Regex(@"^\s*[a-zA-Z0-9,\s\-]+\s*$");
                    if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }

                case ValidationType.MWDate:
                    break;
                case ValidationType.MWInteger5:

                    // Limits to 5 left

                    ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}$");
                    if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }


                case ValidationType.MWDouble5D1:

                    // Limits to 5 left and only 1 decimal

                    ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}(?:\.\d{0,1})?$");
                    if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }

                case ValidationType.MWDouble5D2:

                    // Limits to 5 left and only 2 decimals

                    ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}(?:\.\d{0,2})?$");
                    if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }


                case ValidationType.MWDouble5D3:

                    // Limits to 5 left and only 1 decimals

                    ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}(?:\.\d{0,3})?$");
                    if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }

                case ValidationType.MWDouble5D4:

                    // Limits to 5 left and only 1 decimals

                    ValFactor1 = new Regex(@"^(?=.*[0-9]?.*$)\d{0,5}(?:\.\d{0,4})?$");
                    if (ValFactor1.IsMatch(_MWInput)) { return true; } else { return false; }


            }
            return true;
        }
        catch
        {
            return false;
        }
    }

}
