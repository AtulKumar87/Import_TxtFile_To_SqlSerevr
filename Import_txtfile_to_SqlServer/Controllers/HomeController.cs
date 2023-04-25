using Import_txtfile_to_SqlServer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Import_txtfile_to_SqlServer.Controllers
{
    public class HomeController : Controller
    {
        DBLogic DL_db;
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase txtFile)
        {
            string msg = string.Empty; string path = string.Empty; string FPath = string.Empty;
            try
            {
                if (txtFile != null && txtFile.ContentLength > 0 && txtFile.FileName.Contains(".txt"))
                {
                    path = Server.MapPath("~/Content/TxtFile"); string FName = txtFile.FileName;
                    string[] TblName = FName.Split('.', ' ');
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FPath = path + "/" + FName;
                    txtFile.SaveAs(FPath);

                    using (StreamReader readFile = new StreamReader(FPath))
                    {
                        string line = ""; Int32 counter = 0; string vl = "";

                        while ((line = readFile.ReadLine()) != null)
                        {
                            string[] abc = line.Split('|', ' ');
                            if (counter == 0)
                            {
                                CreateTable(abc, TblName[0]);
                            }
                            if (counter > 0)
                            {
                                InsertData(abc, TblName[0]);
                            }
                            counter++;
                        }
                    }
                }
                else
                {
                    msg = "Please Select txt File.";
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            finally
            {
                if (FPath != string.Empty)
                {
                    FileInfo file = new FileInfo(FPath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
            }
            return View();
        }

        public string CreateTable(string[] Datatbl, string v)
        {
            string msg = "";
            try
            {
                int i = 0; string tblcolnm = "";

                for (i = 0; i < Datatbl.Length; i++)
                {
                    if (i == Datatbl.Length - 1)
                    {
                        tblcolnm += Datatbl[i] + " " + "varchar(100)";
                    }
                    else
                    {
                        tblcolnm += Datatbl[i] + " " + "varchar(100),";
                    }
                }
                string command = "create table " + v + " (" + tblcolnm + ")";
                DL_db = new DBLogic();
                msg = DL_db.CreateTable(command);
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            return msg;
        }
        public string InsertData(string[] TblData, string v)
        {
            string msg = string.Empty; string vl = string.Empty;
            try
            {
                int i = 0; string data = "";

                for (i = 0; i < TblData.Length; i++)
                {
                    if (i == TblData.Length - 1)
                    {
                        data += "'" + TblData[i] + "'";
                    }
                    else
                    {
                        data += "'" + TblData[i] + "',";
                    }
                }
                //vl += "(" + data + "),";
                string cmdData = "INSERT INTO " + v + " VALUES(" + data + ")";
                DBLogic db = new DBLogic();
                msg = db.InsertData(cmdData);
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            return msg;
        }
    }
}