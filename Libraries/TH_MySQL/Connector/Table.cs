﻿using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


using TH_Configuration;
using TH_Global;

namespace TH_MySQL.Connector
{
    public static class Table
    {

        public static bool Create(SQL_Settings sql, string tableName, object[] columnDefinitions, string primaryKey)
        {

            bool Result = false;

            try
            {
                MySqlConnection conn;
                conn = new MySqlConnection();
                conn.ConnectionString = "server=" + sql.Server + ";user=" + sql.Username + ";port=" + sql.Port + ";password=" + sql.Password + ";database=" + sql.Database + ";";
                conn.Open();

                MySqlCommand Command;
                Command = new MySqlCommand();
                Command.Connection = conn;

                string coldef = "";

                //Create Column Definition string
                for (int x = 0; x <= columnDefinitions.Length - 1; x++)
                {
                    coldef += columnDefinitions[x].ToString();
                    if (x < columnDefinitions.Length - 1) coldef += ",";
                }

                string Keydef = "";
                if (primaryKey != null) Keydef = ", PRIMARY KEY (" + primaryKey.ToLower() + ")";

                Command.CommandText = "CREATE TABLE IF NOT EXISTS " + tableName + " (" + coldef + Keydef + ")";

                Command.Prepare();
                Command.ExecuteNonQuery();

                Command.Dispose();

                conn.Close();

                Command.Dispose();
                conn.Dispose();

                Result = true;
            }
            catch (MySqlException ex)
            {
                Logger.Log(ex.Message);
            }
            catch (Exception ex) { }

            return Result;

        }

        public static bool Truncate(SQL_Settings sql, string tableName)
        {

            bool Result = false;

            try
            {
                MySqlConnection conn;
                conn = new MySqlConnection();
                conn.ConnectionString = "server=" + sql.Server + ";user=" + sql.Username + ";port=" + sql.Port + ";password=" + sql.Password + ";database=" + sql.Database + ";";
                conn.Open();

                MySqlCommand Command;
                Command = new MySqlCommand();
                Command.Connection = conn;

                Command.CommandText = "TRUNCATE TABLE " + tableName;

                Command.Prepare();
                Command.ExecuteNonQuery();

                Command.Dispose();

                conn.Close();

                Command.Dispose();
                conn.Dispose();

                Result = true;
            }
            catch (MySqlException ex)
            {
                Logger.Log(ex.Message);
            }
            catch (Exception ex) { }

            return Result;

        }

        public static bool Drop(SQL_Settings sql, string tableName)
        {

            bool Result = false;

            try
            {
                MySqlConnection conn;
                conn = new MySqlConnection();
                conn.ConnectionString = "server=" + sql.Server + ";user=" + sql.Username + ";port=" + sql.Port + ";password=" + sql.Password + ";database=" + sql.Database + ";";
                conn.Open();

                MySqlCommand Command;
                Command = new MySqlCommand();
                Command.Connection = conn;

                Command.CommandText = "DROP TABLE IF EXISTS " + tableName;

                Command.Prepare();
                Command.ExecuteNonQuery();

                Command.Dispose();

                conn.Close();

                Command.Dispose();
                conn.Dispose();

                Result = true;
            }
            catch (MySqlException ex)
            {
                Logger.Log(ex.Message);
            }
            catch (Exception ex) { }

            return Result;

        }

        public static bool Drop(SQL_Settings sql, string[] tableNames)
        {

            bool Result = false;

            try
            {
                MySqlConnection conn;
                conn = new MySqlConnection();
                conn.ConnectionString = "server=" + sql.Server + ";user=" + sql.Username + ";port=" + sql.Port + ";password=" + sql.Password + ";database=" + sql.Database + ";";
                conn.Open();

                MySqlCommand Command;
                Command = new MySqlCommand();
                Command.Connection = conn;

                string tablenames = "";
                for (int x = 0; x <= tableNames.Length - 1; x++)
                {
                    tablenames += tableNames[x];
                    if (x < tableNames.Length - 1) tablenames += ", ";
                }

                Command.CommandText = "DROP TABLE IF EXISTS " + tablenames;

                Command.Prepare();
                Command.ExecuteNonQuery();

                Command.Dispose();

                conn.Close();

                Command.Dispose();
                conn.Dispose();

                Result = true;
            }
            catch (MySqlException ex)
            {
                Logger.Log(ex.Message);
            }
            catch (Exception ex) { }

            return Result;

        }


        public static string[] List(SQL_Settings sql)
        {

            List<string> Tables = new List<string>();

            try
            {
                MySql.Data.MySqlClient.MySqlConnection conn;
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = "server=" + sql.Server + ";user=" + sql.Username + ";port=" + sql.Port + ";password=" + sql.Password + ";database=" + sql.Database + ";";
                conn.Open();

                MySql.Data.MySqlClient.MySqlCommand Command;
                Command = new MySql.Data.MySqlClient.MySqlCommand();
                Command.Connection = conn;
                Command.CommandText = "SHOW TABLES";

                MySql.Data.MySqlClient.MySqlDataReader Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read()) Tables.Add(Reader[0].ToString());
                }

                Reader.Close();
                conn.Close();

                Reader.Dispose();
                Command.Dispose();
                conn.Dispose();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Logger.Log(ex.Message);
            }

            string[] Result;
            Result = Tables.ToArray();

            return Result;

        }

        public static Int64 RowCount(SQL_Settings sql, string tableName)
        {

            Int64 Result = -1;

            try
            {
                MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();

                conn.ConnectionString = "server=" + sql.Server + ";user=" + sql.Username + ";port=" + sql.Port + ";password=" + sql.Password + ";database=" + sql.Database + ";";
                conn.Open();

                string query = "SELECT COUNT(*) FROM " + tableName;
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);

                DataTable t1 = new DataTable();
                using (MySql.Data.MySqlClient.MySqlDataAdapter a = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
                {
                    if (a != null) a.Fill(t1);
                }

                conn.Close();

                cmd.Dispose();
                conn.Dispose();

                if (t1.Rows.Count > 0)
                {
                    Int64 rowCount = -1;
                    Int64.TryParse(t1.Rows[0][0].ToString(), out rowCount);
                    if (rowCount >= 0) Result = rowCount;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Logger.Log(ex.Message);
            }

            return Result;

        }

        public static Int64 Size(SQL_Settings sql, string tableName)
        {

            Int64 Result = -1;

            try
            {
                MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();

                conn.ConnectionString = "server=" + sql.Server + ";user=" + sql.Username + ";port=" + sql.Port + ";password=" + sql.Password + ";database=" + sql.Database + ";";
                conn.Open();

                string query = "SELECT data_length + index_length 'Total Size bytes' FROM information_schema.TABLES WHERE table_schema = '" + sql.Database + "' AND table_name = '" + tableName + "'";

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);

                DataTable t1 = new DataTable();
                using (MySql.Data.MySqlClient.MySqlDataAdapter a = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
                {
                    if (a != null) a.Fill(t1);
                }

                conn.Close();

                cmd.Dispose();
                conn.Dispose();

                if (t1.Rows.Count > 0)
                {
                    Int64 size = -1;
                    Int64.TryParse(t1.Rows[0][0].ToString(), out size);
                    if (size >= 0) Result = size;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Logger.Log(ex.Message);
            }

            return Result;

        }


        public static DataTable Get(SQL_Settings sql, string tableName)
        {

            DataTable Result = null;

            try
            {
                MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();

                conn.ConnectionString = "server=" + sql.Server + ";user=" + sql.Username + ";port=" + sql.Port + ";password=" + sql.Password + ";database=" + sql.Database + ";";
                conn.Open();

                string query = "SELECT * FROM " + tableName;
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);

                DataTable t1 = new DataTable();
                using (MySql.Data.MySqlClient.MySqlDataAdapter a = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
                {
                    if (a != null) a.Fill(t1);
                }

                conn.Close();

                cmd.Dispose();
                conn.Dispose();

                t1.TableName = tableName;

                Result = t1.Copy();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Logger.Log(ex.Message);
            }

            return Result;

        }

        public static DataTable Get(SQL_Settings sql, string tableName, string filterExpression)
        {

            DataTable Result = null;

            try
            {
                MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();

                conn.ConnectionString = "server=" + sql.Server + ";user=" + sql.Username + ";port=" + sql.Port + ";password=" + sql.Password + ";database=" + sql.Database + ";";
                conn.Open();

                string query = "SELECT * FROM " + tableName + " " + filterExpression;

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);

                DataTable t1 = new DataTable();
                using (MySql.Data.MySqlClient.MySqlDataAdapter a = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
                {
                    a.Fill(t1);
                }

                conn.Close();

                cmd.Dispose();
                conn.Dispose();

                t1.TableName = tableName;

                Result = t1.Copy();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Logger.Log(ex.Message);
            }

            return Result;

        }

        public static DataTable Get(SQL_Settings sql, string tableName, string filterExpression, string columns)
        {

            DataTable Result = null;

            try
            {
                MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();

                conn.ConnectionString = "server=" + sql.Server + ";user=" + sql.Username + ";port=" + sql.Port + ";password=" + sql.Password + ";database=" + sql.Database + ";";
                conn.Open();

                string query = "SELECT " + columns + " FROM " + tableName + " " + filterExpression;

                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);

                DataTable t1 = new DataTable();
                using (MySql.Data.MySqlClient.MySqlDataAdapter a = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
                {
                    a.Fill(t1);
                }

                conn.Close();

                cmd.Dispose();
                conn.Dispose();

                t1.TableName = tableName;

                Result = t1.Copy();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Logger.Log(ex.Message);
            }

            return Result;

        }

    }
}
