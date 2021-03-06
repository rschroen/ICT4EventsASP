﻿// <copyright file="PostDal.cs" company="ICT4EventsASP">
//     Copyright (c) ICT4EventsASP. All rights reserved.
// </copyright>
// <author>Jeroen Pullich</author>
namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Oracle.DataAccess.Client;

    /// <summary>
    /// Class used to retrieve data from to database for usage in the media sharing.
    /// </summary>
    public class PostDAL
    {
        #region Load Queries
        /// <summary>
        /// Method for getting the data of a file.
        /// </summary>
        /// <param name="fileID">Identifier of the file</param>
        /// <returns>Returns a data table containing the file data.</returns>
        public DataTable LoadFile(string fileID)
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

        /// <summary>
        /// Method for getting the data of all the files within a category.
        /// </summary>
        /// <param name="categoryID">Identifier of the target category</param>
        /// <returns>Returns a data table containing the file data of all the files.</returns>
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

        /// <summary>
        /// Method for getting the data of all the messages of a post.
        /// </summary>
        /// <param name="postID">Identifier of the target post</param>
        /// <returns>Returns a data table containing the message data of all the messages.</returns>
        public DataTable LoadPostMessages(string postID)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
            {
                conn.Open();
                string loadQuery = "SELECT bd.ID, bd.ACCOUNT_ID, bd.DATUM, br.TITEL, br.INHOUD FROM BIJDRAGE bd, BERICHT br, BIJDRAGE_BERICHT bb WHERE bd.ID = bb.BERICHT_ID AND br.BIJDRAGE_ID = bb.BERICHT_ID AND bb.BIJDRAGE_ID = :post_id";

                using (OracleCommand cmd = new OracleCommand(loadQuery, conn))
                {
                    cmd.Parameters.Add(new OracleParameter("post_ID", postID));
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

        /// <summary>
        /// Method for getting the amount of likes and flags of a post.
        /// </summary>
        /// <param name="postID">Identifier of the target post</param>
        /// <returns>Returns a list with the amounts of likes and flags.</returns>
        public List<int> GetLikeFlagCount(string postID)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
            {
                List<int> result = new List<int>();

                conn.Open();
                string loadQuery = "SELECT COALESCE(SUM(LIKES), 0) FROM ACCOUNT_BIJDRAGE WHERE BIJDRAGE_ID = :post_id";

                try
                {
                    using (OracleCommand cmd = new OracleCommand(loadQuery, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("post_ID", postID));

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(Convert.ToInt32(reader.GetValue(0)));
                            }
                        }
                    }

                    loadQuery = "SELECT COALESCE(SUM(ONGEWENST), 0) FROM ACCOUNT_BIJDRAGE WHERE BIJDRAGE_ID = :post_id";

                    using (OracleCommand cmd = new OracleCommand(loadQuery, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("post_ID", postID));

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(Convert.ToInt32(reader.GetValue(0)));
                            }
                        }
                    }
                }
                catch (OracleException ex)
                {
                    Console.WriteLine("Error: " + ex.Message.ToString());
                    result.Add(0);
                    result.Add(0);
                }

                return result;
            }
        }

        /// <summary>
        /// Method for getting all the root categories (categories without a parent).
        /// </summary>
        /// <returns>Returns a data table containing the root category data.</returns>
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

        /// <summary>
        /// Method for getting all the categories (Both parent and child).
        /// </summary>
        /// <returns>Returns a data table containing all category data</returns>
        public DataTable LoadAllCategories()
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
            {
                conn.Open();
                string loadQuery = "SELECT * FROM CATEGORIE";
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

        /// <summary>
        /// Method for getting all the child categories of a certain parent.
        /// </summary>
        /// <param name="parentID">Identifier of the parent category</param>
        /// <returns>Returns a data table containing the child category data></returns>
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

        #region Insert Queries
        /// <summary>
        /// Method for getting the user ID based on a username.
        /// </summary>
        /// <param name="userName">Target username</param>
        /// <returns>Returns an integer with the user ID.</returns>
        public int GetAccountID(string userName)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
            {
                int result = 0;

                conn.Open();
                string query = "SELECT ID FROM ACCOUNT WHERE GEBRUIKERSNAAM = :userName";

                try
                {
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("userName", userName));

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = Convert.ToInt32(reader.GetValue(0));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message.ToString());
                    result = 0;
                }

                return result;
            }
        }

        /// <summary>
        /// Method for getting the user ID based on a category name.
        /// </summary>
        /// <param name="categoryName">Target category name</param>
        /// <returns>Returns an integer with the category ID.</returns>
        public int GetCategoryID(string categoryName)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
            {
                int result = 0;

                conn.Open();
                string query = "SELECT BIJDRAGE_ID FROM CATEGORIE WHERE NAAM = :name";

                try
                {
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("name", categoryName));

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = Convert.ToInt32(reader.GetValue(0));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message.ToString());
                    result = 0;
                }

                return result;
            }
        }


        public string GetCategoryName(string categoryID)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
            {
                string result = string.Empty;

                conn.Open();
                string query = "SELECT NAAM FROM CATEGORIE WHERE BIJDRAGE_ID = :id";

                try
                {
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("id", categoryID));

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = reader.GetValue(0).ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = "Error: " + ex.Message.ToString();
                }

                return result;
            }
        }

        public int GetParentCategoryID(string childID)
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
            {
                int result = 0;

                conn.Open();
                string query = "SELECT CATEGORIE_ID FROM CATEGORIE WHERE BIJDRAGE_ID = :id";

                try
                {
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("id", childID));

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = Convert.ToInt32(reader.GetValue(0));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message.ToString());
                    result = 0;
                }

                return result;
            }
        }

        /// <summary>
        /// Method for inserting a file in the database.
        /// </summary>
        /// <param name="userName">Username of the uploader</param>
        /// <param name="categoryID">ID of the category of the file</param>
        /// <param name="location">Full path to the location of the file on the file server</param>
        /// <param name="size">Size of the file in bytes</param>
        /// <returns>Returns a "1" if the insert was successful, otherwise "0".</returns>
        public int InsertFile(string userName, string categoryID, string location, string size)
        {
            int result = 0;
            string insertQuery = string.Empty;
            try
            {
                int id = this.InsertPost(userName, "BESTAND");

                string accountID = this.GetAccountID(userName).ToString();

                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
                {
                    conn.Open();

                    insertQuery = @"INSERT INTO BESTAND (BIJDRAGE_ID, CATEGORIE_ID, BESTANDSLOCATIE, GROOTTE) 
                    VALUES (:id, :categoryID, :location, :filesize)";

                    using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("id", id));
                        cmd.Parameters.Add(new OracleParameter("categoryID", categoryID));
                        cmd.Parameters.Add(new OracleParameter("location", location));
                        cmd.Parameters.Add(new OracleParameter("filesize", size));

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (OracleException ex)
            {
                Debug.WriteLine(this.ErrorString(ex));
                result = 0;
            }

            return result;
        }

        /// <summary>
        /// Method for inserting a category in the database.
        /// </summary>
        /// <param name="userName">Username of the creator</param>
        /// <param name="parentName">Name of a potential parent category</param>
        /// <param name="name">Name of the new category</param>
        /// <returns>Returns a "1" if the insert was successful, otherwise "0".</returns>
        public int InsertCategory(string userName, string parentName, string name)
        {
            int result = 0;
            string insertQuery = string.Empty;
            try
            {
                int id = this.InsertPost(userName, "CATEGORIE");

                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
                {
                    conn.Open();

                    if (parentName == string.Empty)
                    {
                        insertQuery = @"INSERT INTO CATEGORIE (BIJDRAGE_ID, NAAM) VALUES (:id, :categoryName)";
                    }
                    else
                    {
                        insertQuery = @"INSERT INTO CATEGORIE (BIJDRAGE_ID, CATEGORIE_ID, NAAM) VALUES (:id, :parentID, :categoryName)";
                    }

                    using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                    {
                        if (parentName == string.Empty)
                        {
                            cmd.Parameters.Add(new OracleParameter("id", id));
                            cmd.Parameters.Add(new OracleParameter("categoryName", name));
                        }
                        else
                        {
                            string parentID = this.GetCategoryID(parentName).ToString();

                            cmd.Parameters.Add(new OracleParameter("id", id));
                            cmd.Parameters.Add(new OracleParameter("parentID", parentID));
                            cmd.Parameters.Add(new OracleParameter("categoryName", name));
                        }

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (OracleException ex)
            {
                Debug.WriteLine(this.ErrorString(ex));
                result = 0;
            }

            return result;
        }

        /// <summary>
        /// Method for inserting a new message in the database.
        /// </summary>
        /// <param name="userName">Username of the creator</param>
        /// <param name="title">Title of the new message</param>
        /// <param name="content">Content of the new message</param>
        /// <param name="targetID">ID of the target post</param>
        /// <returns>Returns a "1" if the insert was successful, otherwise "0".</returns>
        public int InsertMessage(string userName, string title, string content, string targetID)
        {
            int result = 0;
            string insertQuery = string.Empty;
            try
            {
                int id = this.InsertPost(userName, "BERICHT");

                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
                {
                    conn.Open();

                    insertQuery = @"INSERT INTO BERICHT (BIJDRAGE_ID, TITEL, INHOUD) 
                    VALUES (:id, :title, :content)";

                    using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("id", id));
                        cmd.Parameters.Add(new OracleParameter("title", title));
                        cmd.Parameters.Add(new OracleParameter("content", content));

                        result = cmd.ExecuteNonQuery();
                    }

                    insertQuery = @"INSERT INTO BIJDRAGE_BERICHT (BIJDRAGE_ID, BERICHT_ID) 
                    VALUES (:targetID, :id )";

                    using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("targetID", targetID));
                        cmd.Parameters.Add(new OracleParameter("id", id));
                        
                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (OracleException ex)
            {
                Debug.WriteLine(this.ErrorString(ex));
                result = 0;
            }

            return result;
        }

        /// <summary>
        /// Method for inserting a like or flag in the database
        /// </summary>
        /// <param name="userName">Username of the creator</param>
        /// <param name="postID">ID of the target post</param>
        /// <param name="like">Is "1" if it is a like, otherwise "0"</param>
        /// <param name="flag">Is "1" if it is a like, otherwise "0</param>
        /// <returns>Returns a "1" if the insert was successful, otherwise "0".</returns>
        public int InsertLikeFlag(string userName, string postID, int like, int flag)
        {
            int result = 0;
            int id = 0;

            try
            {
                string accountID = this.GetAccountID(userName).ToString();

                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
                {
                    conn.Open();

                    string query = @"SELECT ACCOUNT_BIJDRAGE_FCSEQ.NEXTVAL FROM DUAL";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id = Convert.ToInt32(reader.GetValue(0));
                            }
                        }
                    }

                    string insertQuery = @"INSERT INTO ACCOUNT_BIJDRAGE (ID, ACCOUNT_ID, BIJDRAGE_ID, LIKES, ONGEWENST) VALUES (:id, :account_ID, :bijdrage_ID, :likes, :ongewenst)";

                    using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("id", id));
                        cmd.Parameters.Add(new OracleParameter("account_ID", accountID));
                        cmd.Parameters.Add(new OracleParameter("bijdrage_ID", postID));
                        cmd.Parameters.Add(new OracleParameter("likes", like));
                        cmd.Parameters.Add(new OracleParameter("ongewenst", flag));

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (OracleException ex)
            {
                Debug.WriteLine(this.ErrorString(ex));
                result = 0;
            }

            return result;
        }
        #endregion

        #region Update Queries
        /// <summary>
        /// Method for updating the like value of Post
        /// </summary>
        /// <param name="userName">identifier of the username</param>
        /// <param name="postID">identifier of post</param>
        /// <param name="like">identifier of like</param>
        /// <returns>returns 1 on success, 0 on failure</returns>
        public int UpdateLike(string userName, string postID, int like)
        {
            int result = 0;

            try
            {
                string accountID = this.GetAccountID(userName).ToString();

                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string insertQuery = @"UPDATE ACCOUNT_BIJDRAGE SET LIKES = :likes WHERE ACCOUNT_ID = :account_ID AND BIJDRAGE_ID = :bijdrage_ID";

                    using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("likes", like));
                        cmd.Parameters.Add(new OracleParameter("account_ID", accountID));
                        cmd.Parameters.Add(new OracleParameter("bijdrage_ID", postID));
                        
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (OracleException ex)
            {
                Debug.WriteLine(this.ErrorString(ex));
                result = 0;
            }

            return result;
        }

        /// <summary>
        /// Method for updating the flag value of the Post
        /// </summary>
        /// <param name="userName">identifier of the username</param>
        /// <param name="postID">identifier of post</param>
        /// <param name="flag">identifier of flag</param>
        /// <returns>1 on success, 0 on failure</returns>
        public int UpdateFlag(string userName, string postID, int flag)
        {
            int result = 0;

            try
            {
                string accountID = this.GetAccountID(userName).ToString();

                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string insertQuery = @"UPDATE  ACCOUNT_BIJDRAGE SET ONGEWENST = :ongewenst WHERE ACCOUNT_ID = :account_ID AND BIJDRAGE_ID =:bijdrage_ID";

                    using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("ongewenst", flag));
                        cmd.Parameters.Add(new OracleParameter("account_ID", accountID));
                        cmd.Parameters.Add(new OracleParameter("bijdrage_ID", postID));

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (OracleException ex)
            {
                Debug.WriteLine(this.ErrorString(ex));
                result = 0;
            }

            return result;
        }
        #endregion
        #region Delete Queries
        /// <summary>
        /// Method for deleting a file from the database.
        /// </summary>
        /// <param name="postID">Identifier of the post</param>
        /// <returns>Returns a "1" if the insert was successful, otherwise "0".</returns>
        public int DeleteFile(string postID)
        {
            int result = 0;

            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM BESTAND WHERE BIJDRAGE_ID = :postID";
                    using (OracleCommand cmd = new OracleCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("postID", postID));

                        result = cmd.ExecuteNonQuery();
                    }
                }

                result = this.DeletePost(postID);
            }
            catch (OracleException ex)
            {
                Debug.WriteLine(this.ErrorString(ex));
                return 0;
            }

            return result;
        }

        /// <summary>
        /// Method for deleting all the likes and flags of a post
        /// </summary>
        /// <param name="postID">Identifier of the post</param>
        /// <returns>Returns a "1" if the insert was successful, otherwise "0".</returns>
        public int DeletePostLikeFlags(string postID)
        {
            int result = 0;

            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM ACCOUNT_BIJDRAGE WHERE BIJDRAGE_ID = :postID";
                    using (OracleCommand cmd = new OracleCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("postID", postID));

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (OracleException ex)
            {
                Debug.WriteLine(this.ErrorString(ex));
                return 0;
            }

            return result;
        }

        /// <summary>
        /// Method for deleting a category and all childs
        /// </summary>
        /// <param name="postID">Identifier of the post</param>
        /// <returns>Returns a "1" if the insert was successful, otherwise "0".</returns>
        public int DeleteCategory(string postID)
        {
            int result = 0;

            try
            {
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
                {
                    conn.Open();

                    // Delete child categories:
                    string deleteQuery = "DELETE FROM CATEGORIE WHERE BIJDRAGE_ID = :postID";
                    using (OracleCommand cmd = new OracleCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("postID", postID));

                        result = cmd.ExecuteNonQuery();
                    }

                    deleteQuery = "DELETE FROM CATEGORIE WHERE BIJDRAGE_ID = :postID";
                    using (OracleCommand cmd = new OracleCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("postID", postID));

                        result = cmd.ExecuteNonQuery();
                    }
                }

                result = this.DeletePost(postID);
            }
            catch (OracleException ex)
            {
                Debug.WriteLine(this.ErrorString(ex));
                return 0;
            }

            return result;
        }

        /// <summary>
        /// Method for deleting a target post from the database.
        /// </summary>
        /// <param name="postID">Identifier of the post</param>
        /// <returns>Is "1" if it is a like, otherwise "0".</returns>
        public int DeletePost(string postID)
        {
            //if (this.DeletePostLikeFlags(postID) == 0)
            //{
            //    return 0;
            //}
            //else
            //{
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string insertQuery = "DELETE FROM BIJDRAGE WHERE ID = :postID";
                    using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("postID", postID));
                        try
                        {
                            return cmd.ExecuteNonQuery();
                        }
                        catch (OracleException ex)
                        {
                            Debug.WriteLine(this.ErrorString(ex));
                            return 0;
                        }
                    }
                }
            //}
        }
        #endregion

        #region Check Methods

        /// <summary>
        /// Method for checking to see if a account has already liked or flagged the post in the past.
        /// </summary>
        /// <param name="userName">identifier of the Username</param>
        /// <param name="postID">identifier of the post</param>
        /// <returns>Returns 1 if the account has already liked or flagged the post in the past
        /// else returns 0
        /// </returns>
        public int CheckLikeFlag(string userName, string postID)
        {
            try
            {
                string accountID = this.GetAccountID(userName).ToString();
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string checkUser = "SELECT COUNT(*) FROM dual WHERE EXISTS(SELECT account_ID FROM ACCOUNT_BIJDRAGE WHERE account_id = :account_id AND bijdrage_ID = :bijdrage_ID)";
                    using (OracleCommand cmd = new OracleCommand(checkUser, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("account_id", accountID));
                        cmd.Parameters.Add(new OracleParameter("bijdrage_id", postID));
                        try
                        {
                            return Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        }
                        catch (OracleException ex)
                        {
                            Console.WriteLine(this.ErrorString(ex));
                            return 0;
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                Debug.WriteLine(this.ErrorString(ex));
                return 0;
            }
        }

        /// <summary>
        /// Method to check if the account has liked the post or not
        /// </summary>
        /// <param name="userName">identifier of the username</param>
        /// <param name="postID">identifier of the post</param>
        /// <param name="like">identifier of the like value</param>
        /// <returns>1 if it exits, 0 it it doesn't</returns>
        public int CheckLike(string userName, string postID, int like)
        {
            try
            {
                string accountID = this.GetAccountID(userName).ToString();
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string checkUser = "SELECT COUNT(*) FROM dual WHERE EXISTS(SELECT account_ID FROM ACCOUNT_BIJDRAGE WHERE account_id = :account_id AND bijdrage_ID = :bijdrage_ID and likes = :likes)";
                    using (OracleCommand cmd = new OracleCommand(checkUser, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("account_id", accountID));
                        cmd.Parameters.Add(new OracleParameter("bijdrage_id", postID));
                        cmd.Parameters.Add(new OracleParameter("likes", like));
                        try
                        {
                            return Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        }
                        catch (OracleException ex)
                        {
                            Console.WriteLine(this.ErrorString(ex));
                            return 0;
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                Debug.WriteLine(this.ErrorString(ex));
                return 0;
            }
        }

        /// <summary>
        /// Method to check if the account has flag the post or not
        /// </summary>
        /// <param name="userName">identifier of the username</param>
        /// <param name="postID">identifier of the post</param>
        /// <param name="flag">identifier of the flag value</param>
        /// <returns>1 if it exits, 0 it it doesn't</returns>
        public int CheckFlag(string userName, string postID, int flag)
        {
            try
            {
                string accountID = this.GetAccountID(userName).ToString();
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
                {
                    conn.Open();
                    string checkUser = "SELECT COUNT(*) FROM dual WHERE EXISTS(SELECT account_ID FROM ACCOUNT_BIJDRAGE WHERE account_id = :account_id AND bijdrage_ID = :bijdrage_ID and ongewenst = :ongewenst)";
                    using (OracleCommand cmd = new OracleCommand(checkUser, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("account_id", accountID));
                        cmd.Parameters.Add(new OracleParameter("bijdrage_id", postID));
                        cmd.Parameters.Add(new OracleParameter("ongewenst", flag));
                        try
                        {
                            return Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        }
                        catch (OracleException ex)
                        {
                            Console.WriteLine(this.ErrorString(ex));
                            return 0;
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                Debug.WriteLine(this.ErrorString(ex));
                return 0;
            }
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Method for returning Oracle exceptions as string
        /// </summary>
        /// <param name="ex">Oracle exception</param>
        /// <returns>Oracle exception as string</returns>
        private string ErrorString(OracleException ex)
        {
            return "Code: " + ex.ErrorCode + "\n" + "Message: " + ex.Message;
        }

        /// <summary>
        /// Method for inserting a post in the database.
        /// </summary>
        /// <param name="userName">Username of the post creator</param>
        /// <param name="type">Type of the post (file, message, category></param>
        /// <returns>Returns a "1" if the insert was successful, otherwise "0".</returns>
        private int InsertPost(string userName, string type)
        {
            int result = 0;
            int id = 0;
            string insertQuery = string.Empty;
            try
            {
                string accountID = this.GetAccountID(userName).ToString();

                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
                {
                    conn.Open();

                    string query = @"SELECT BIJDRAGE_FCSEQ.NEXTVAL FROM DUAL";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id = Convert.ToInt32(reader.GetValue(0));
                            }
                        }
                    }

                    insertQuery = @"INSERT INTO BIJDRAGE (ID, ACCOUNT_ID, DATUM, SOORT) VALUES (:id, :accountID, TO_DATE(:uploadDate, 'dd/mm/yyyy hh24:mi:ss'), :kind)";

                    using (OracleCommand cmd = new OracleCommand(insertQuery, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("id", id));
                        cmd.Parameters.Add(new OracleParameter("accountID", accountID));
                        cmd.Parameters.Add(new OracleParameter("uploadDate", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")));
                        cmd.Parameters.Add(new OracleParameter("kind", type));

                        result = cmd.ExecuteNonQuery();
                        if (result != 0)
                        {
                            result = id;
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                Debug.WriteLine(this.ErrorString(ex));
                result = 0;
            }

            return result;
        }
        #endregion

    }
}