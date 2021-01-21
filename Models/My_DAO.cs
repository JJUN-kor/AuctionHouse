using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Net;

namespace WebApplication1.Models
{
    public class My_DAO
    {
        #region 연결 객체
        private static MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["AHDB_DBConnection"].ToString());
        #endregion

        #region 객체 가져오기
        public static MySqlConnection Get_connection()
        {
            return con;
        }
        #endregion

    }
}