using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Import_txtfile_to_SqlServer.Models
{
    public class clsCommon
    {
        public string Con()
        {
            string clscon = ConfigurationManager.ConnectionStrings["conn"].ToString();
            return clscon;
        }
    }
}