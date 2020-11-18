#region Comments and History
/*
 * =======================================================================================
 *   Author   : Schalk Kotze
 *   Date     : 02 May 2010
 *   Purpose  : Static class for Global Vars
 *              No Instance constructor needed
 *              
 * =======================================================================================
*/
#endregion Comments and History

static class clsUserInfo
{
    
 #region class properties and globals

    private static  string m_UserID = "";
    private static string m_UserName = "";
    private static string m_ProfileID = "";
    private static string m_sysadmin = "";
    private static string m_SuperUser = "";
    
    public static string UserID { get { return m_UserID; } set { m_UserID = value; }}
    public static string UserName { get { return m_UserName; } set { m_UserName = value; }}
    public static string SysAdmin { get { return m_sysadmin; } set { m_sysadmin = value; } }
    public static string SuperUser { get { return m_SuperUser; } set { m_SuperUser = value; } }
    public static string ProfileID { get { return m_ProfileID; } set { m_ProfileID = value; } }


 #endregion class properties and globals
}


