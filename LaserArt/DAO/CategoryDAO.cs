using LaserArt.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;

namespace LaserArt.DAO
{
    public class CategoryDAO:DAO
    {
        public static List<Category> getProducts(int? id)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetCategory", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        if (id == null)
                            command.Parameters.AddWithValue("@Id", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Id", id);
                        SqlDataReader rdr = command.ExecuteReader();
                        List<Category> newCategoryList = new List<Category>();
                        string culture = Thread.CurrentThread.CurrentCulture.Parent.Name.ToUpper();

                        while (rdr.Read())
                        {
                            Category newCategory = new Category();
                            newCategory.Id = Convert.ToInt32(rdr["Id"]);
                            newCategory.CategoryName = rdr["CategoryName_"+culture].ToString();
                            newCategory.CategoryName_AM = rdr["CategoryName_AM"].ToString();
                            newCategory.CategoryName_EN = rdr["CategoryName_EN"].ToString();
                            newCategory.CategoryName_RU = rdr["CategoryName_RU"].ToString();

                            newCategory.CategoryImage = rdr["CategoryImage"].ToString();

                            newCategoryList.Add(newCategory);
                        }
                        return newCategoryList;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }

        internal static void DeleteCategoryByID(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_DeleteCategory", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }

        public static Category saveProduct(Category newCategory)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_SaveCategory", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        if (newCategory.Id == null)
                            command.Parameters.AddWithValue("@Id", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Id", newCategory.Id);
                        command.Parameters.AddWithValue("@CategoryName_AM", newCategory.CategoryName_AM);
                        command.Parameters.AddWithValue("@CategoryName_RU", newCategory.CategoryName_RU);
                        command.Parameters.AddWithValue("@CategoryName_EN", newCategory.CategoryName_EN);

                        command.Parameters.AddWithValue("@CategoryImage", newCategory.CategoryImage);

                        command.ExecuteNonQuery();
                        
                        return newCategory;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }
    }
}