﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace DAL
{
    public class PostDAL
    {

        #region Post Queries

        public DataTable loadFile(string fileID)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
            {
                conn.Open();
                string loadQuery = "SELECT bd.ID, bd.ACCOUNT_ID, bd.DATUM, bs.CATEGORIE_ID, bs.BESTANDSLOCATIE, bs.GROOTTE FROM BIJDRAGE bd, BESTAND bs WHERE bd.ID = bs.BIJDRAGE_ID AND bs.BIJDRAGE_ID = :file_id";

                using (OracleCommand cmd = new OracleCommand(loadQuery, conn))
                {
                    cmd.Parameters.Add(new OracleParameter("file_id", fileID));
                    OracleDataAdapter a = new OracleDataAdapter(cmd);
                    DataTable t = new DataTable();
                    try
                    {
                        a.Fill(t);
                        return t;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message.ToString());
                        return t;
                    }
                }
            }
        }

        public DataTable LoadCategoryFiles(string categoryID)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
            {
                conn.Open();
                string loadQuery = "SELECT bd.ID, bd.ACCOUNT_ID, bd.DATUM, bs.BESTANDSLOCATIE FROM BIJDRAGE bd, BESTAND bs WHERE bd.ID = bs.BIJDRAGE_ID AND bs.CATEGORIE_ID = :category_id";

                using (OracleCommand cmd = new OracleCommand(loadQuery, conn))
                {
                    cmd.Parameters.Add(new OracleParameter("category_id", categoryID));
                    OracleDataAdapter a = new OracleDataAdapter(cmd);
                    DataTable t = new DataTable();
                    try
                    {
                        a.Fill(t);
                        return t;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message.ToString());
                        return t;
                    }
                }
            }
        }

        #endregion

        #region Category Queries
        public DataTable LoadRootCategories()
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
            {
                conn.Open();
                string loadQuery = "SELECT * FROM CATEGORIE WHERE CATEGORIE_ID IS NULL";
                using (OracleCommand cmd = new OracleCommand(loadQuery, conn))
                {
                    OracleDataAdapter a = new OracleDataAdapter(cmd);
                    DataTable t = new DataTable();
                    try
                    {
                        a.Fill(t);
                        return t;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message.ToString());
                        return t;
                    }
                }
            }
        }

        public DataTable LoadChildCategories(string parentID)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
            {
                conn.Open();
                string loadQuery = "SELECT * FROM Categorie WHERE categorie_id = :categorie_id";
                using (OracleCommand cmd = new OracleCommand(loadQuery, conn))
                {
                    cmd.Parameters.Add(new OracleParameter("category_id", parentID));
                    OracleDataAdapter a = new OracleDataAdapter(cmd);
                    DataTable t = new DataTable();
                    try
                    {
                        a.Fill(t);
                        return t;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message.ToString());
                        return t;
                    }
                }
            }
        }

        #endregion
    }
}
