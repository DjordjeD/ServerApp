﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ServerApp
{
    public class DBAccess
    {
        private static SqlConnection connection = new SqlConnection();
        private static SqlCommand command = new SqlCommand();
        private static SqlDataReader DbReader;
        private static SqlDataAdapter adapter = new SqlDataAdapter();
        public SqlTransaction DbTran;

        private static string strConnString = "Data Source=HPZ9C68EA-NOTE\\SQLEXPRESS;Initial Catalog=Algotech;Integrated Security=True";



        public void createConn()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void closeConn()
        {
            connection.Close();
        }


        public int executeDataAdapter(DataTable tblName, string strSelectSql)
        {
            try
            {
                if (connection.State == 0)
                {
                    createConn();
                }

                adapter.SelectCommand.CommandText = strSelectSql;
                adapter.SelectCommand.CommandType = CommandType.Text;
                SqlCommandBuilder DbCommandBuilder = new SqlCommandBuilder(adapter);


                string insert = DbCommandBuilder.GetInsertCommand().CommandText.ToString();
                string update = DbCommandBuilder.GetUpdateCommand().CommandText.ToString();
                string delete = DbCommandBuilder.GetDeleteCommand().CommandText.ToString();


                return adapter.Update(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void readDatathroughAdapter(string query, DataTable tblName)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                adapter = new SqlDataAdapter(command);
                adapter.Fill(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public SqlDataReader readDatathroughReader(string query)
        {
            //DataReader used to sequentially read data from a data source
            SqlDataReader reader;

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }

                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                reader = command.ExecuteReader();
                var myString = reader.GetString(0);
                    
                return reader;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public int executeQuery(SqlCommand dbCommand)
        {
            try
            {
                if (connection.State == 0)
                {
                    createConn();
                }

                dbCommand.Connection = connection;
                dbCommand.CommandType = CommandType.Text;


                return dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}