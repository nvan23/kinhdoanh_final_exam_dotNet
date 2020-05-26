﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinhdoanh
{
    class clsDatabase
    {
        public static SqlConnection con;
        public static bool OpenConnection()
        {
            try
            {
                con = new SqlConnection(@"Data Source=DESKTOP-E30J54Q\SQLEXPRESS;Initial Catalog=kinhdoanh;Integrated Security=True;Pooling=False");
                con.Open();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool CloseConnection()
        {
            try
            {
                con.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

    }
}
