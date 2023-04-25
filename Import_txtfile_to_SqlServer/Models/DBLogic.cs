using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Import_txtfile_to_SqlServer.Models
{
    public class DBLogic
    {
        SqlConnection con;
        SqlCommand cmd;
        clsCommon constring;
        public string CreateTable(string command)
        {
            string msg = "";
            try
            {
                constring = new clsCommon();
                using (con = new SqlConnection(constring.Con()))
                {
                    cmd = new SqlCommand(command, con);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int tblrow = cmd.ExecuteNonQuery();
                    con.Close();
                    if (tblrow > 0)
                    {
                        msg = tblrow.ToString();
                    }
                    else
                    {
                        msg = tblrow.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "0";
            }
            return msg;
        }

        internal string InsertData(string cmdData)
        {
            string msg = "";
            try
            {
                constring = new clsCommon();
                using (con = new SqlConnection(constring.Con()))
                {
                    cmd = new SqlCommand(cmdData, con);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int tblrow = cmd.ExecuteNonQuery();
                    con.Close();
                    if (tblrow > 0)
                    {
                        msg = tblrow.ToString();
                    }
                    else
                    {
                        msg = tblrow.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "0";
            }
            return msg;
        }
    }
}