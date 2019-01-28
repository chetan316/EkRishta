using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

    public class Logger
    {
        public static void Logging(string ErrMessage, string ErrStackTrace, int UserId, DateTime dtTime)
        {
            try
            {
                DataSet dsResponse = new DataSet();
                string conStr = ConfigurationManager.ConnectionStrings["DBEntity"].ConnectionString;
                SqlConnection connString = new SqlConnection(conStr);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@ErrMessage", ErrMessage);
                sqlCmd.Parameters.AddWithValue("@ErrStackTrace", ErrStackTrace);
                sqlCmd.Parameters.AddWithValue("@UserId", UserId);
                sqlCmd.Parameters.AddWithValue("@DateTime", dtTime);
                sqlCmd.CommandText = "InsertLogs";
                sqlCmd.Connection = connString;
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                sda.Fill(dsResponse);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
