using LaserArt.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;

namespace LaserArt.DAO
{
    public class ProductDAO : DAO
    {
        public static Product saveProduct(Product newProduct)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_SaveProduct", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;

                        if (newProduct.Id == null)
                            command.Parameters.AddWithValue("@Id", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Id", newProduct.Id);

                        command.Parameters.AddWithValue("@ProductTitle_AM", newProduct.ProductTitle_AM);
                        command.Parameters.AddWithValue("@ProductTitle_RU", newProduct.ProductTitle_RU);
                        command.Parameters.AddWithValue("@ProductTitle_EN", newProduct.ProductTitle_EN);

                        command.Parameters.AddWithValue("@ProductDescription_AM", newProduct.ProductDescription_AM);
                        command.Parameters.AddWithValue("@ProductDescription_RU", newProduct.ProductDescription_RU);
                        command.Parameters.AddWithValue("@ProductDescription_EN", newProduct.ProductDescription_EN);

                        command.Parameters.AddWithValue("@ImageSource", newProduct.ImageSource);
                        command.Parameters.AddWithValue("@ImageSource1", newProduct.ImageSource1);
                        command.Parameters.AddWithValue("@ImageSource2", newProduct.ImageSource2);
                        command.Parameters.AddWithValue("@ImageSource3", newProduct.ImageSource3);
                        command.Parameters.AddWithValue("@CategoryId", newProduct.CategoryId);
                        command.Parameters.AddWithValue("@Price", newProduct.Price);
                        command.Parameters.AddWithValue("@PriceDiscounted", newProduct.PriceDiscounted);

                        command.ExecuteNonQuery();
                        return newProduct;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
        }
        public static List<Product> getProducts(int? id)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetProducts", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        if (id == null)
                            command.Parameters.AddWithValue("@Id", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Id",id);
                        SqlDataReader rdr = command.ExecuteReader();
                        List<Product> newProductList = new List<Product>();
                        string culture=Thread.CurrentThread.CurrentCulture.Parent.Name.ToUpper();
                        while (rdr.Read())
                        {
                            Product newProduct = new Product();
                            newProduct.Id = Convert.ToInt32(rdr["Id"]);
                            newProduct.ProductTitle = rdr["ProductTitle_"+culture].ToString();
                            newProduct.ProductTitle_AM = rdr["ProductTitle_AM"].ToString();
                            newProduct.ProductTitle_EN = rdr["ProductTitle_EN"].ToString();
                            newProduct.ProductTitle_RU = rdr["ProductTitle_RU"].ToString();
                            newProduct.ProductDescription = rdr["ProductDescription_"+culture].ToString();
                            newProduct.ProductDescription_AM = rdr["ProductDescription_AM"].ToString();
                            newProduct.ProductDescription_RU = rdr["ProductDescription_RU"].ToString();
                            newProduct.ProductDescription_EN = rdr["ProductDescription_EN"].ToString();
                            newProduct.ImageSource = rdr["ImageSource"].ToString();
                            newProduct.ImageSource1 = rdr["ImageSource1"].ToString();
                            newProduct.ImageSource2 = rdr["ImageSource2"].ToString();
                            newProduct.ImageSource3 = rdr["ImageSource3"].ToString();
                            newProduct.Price =Convert.ToDecimal(rdr["Price"]);
                            newProduct.PriceDiscounted = rdr["PriceDiscounted"]==DBNull.Value? 0:Convert.ToDecimal(rdr["PriceDiscounted"]);
                            newProduct.CategoryId = Convert.ToInt32(rdr["CategoryId"]);

                            newProductList.Add(newProduct);
                        }
                        return newProductList;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }

        public static List<Product> getProductsByCategoryId(int id)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetProductsByCategory", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        string culture=Thread.CurrentThread.CurrentCulture.Parent.Name.ToUpper();
                        command.Parameters.AddWithValue("@CategoryId",id);
                        SqlDataReader rdr = command.ExecuteReader();
                        List<Product> newProductList = new List<Product>();
                        while (rdr.Read())
                        {
                            Product newProduct = new Product();
                            newProduct.Id = Convert.ToInt32(rdr["Id"]);
                            newProduct.ProductTitle = rdr["ProductTitle_"+culture].ToString();
                            newProduct.ProductDescription = rdr["ProductDescription_"+culture].ToString();
                            newProduct.ImageSource = rdr["ImageSource"].ToString();

                            newProduct.ImageSource1 = rdr["ImageSource1"].ToString();
                            newProduct.ImageSource2 = rdr["ImageSource2"].ToString();
                            newProduct.ImageSource3 = rdr["ImageSource3"].ToString();
                            newProduct.Price = Convert.ToDecimal(rdr["Price"]);
                            newProduct.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                            newProduct.PriceDiscounted = rdr["PriceDiscounted"]==DBNull.Value? 0:Convert.ToDecimal(rdr["PriceDiscounted"]);

                            newProductList.Add(newProduct);
                        }
                        return newProductList;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }
        public static List<CardModel> getProductsByOrderId(int id)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetProductsByOrderid", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OrderId", id);
                        SqlDataReader rdr = command.ExecuteReader();
                        string culture = Thread.CurrentThread.CurrentCulture.Parent.Name.ToUpper();

                        List<CardModel> newProductList = new List<CardModel>();
                        while (rdr.Read())
                        {
                            Product newProduct = new Product();
                            newProduct.Id = Convert.ToInt32(rdr["Id"]);
                            newProduct.ProductTitle = rdr["ProductTitle_"+culture].ToString();
                            newProduct.ProductDescription = rdr["ProductDescription_"+culture].ToString();
                            newProduct.ImageSource = rdr["ImageSource"].ToString();
                            newProduct.ImageSource1 = rdr["ImageSource1"].ToString();
                            newProduct.ImageSource2 = rdr["ImageSource2"].ToString();
                            newProduct.ImageSource3 = rdr["ImageSource3"].ToString();
                            newProduct.Price = Convert.ToDecimal(rdr["Price"]);
                            newProduct.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                            newProduct.PriceDiscounted = rdr["PriceDiscounted"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PriceDiscounted"]);
                            CardModel model = new CardModel();
                            model.ProductQuantity = Convert.ToInt32(rdr["Quantity"]);
                            model.product = newProduct;
                            newProductList.Add(model);
                        }
                        return newProductList;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }
        public static List<Product> getProductsByQuery(string query)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_SearchProduct", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Query", string.Format("%{0}%",query));
                        SqlDataReader rdr = command.ExecuteReader();
                        string culture = Thread.CurrentThread.CurrentCulture.Parent.Name.ToUpper();

                        List<Product> newProductList = new List<Product>();
                        while (rdr.Read())
                        {
                            Product newProduct = new Product();
                            newProduct.Id = Convert.ToInt32(rdr["Id"]);
                            newProduct.ProductTitle = rdr["ProductTitle_"+culture].ToString();
                            newProduct.ProductDescription = rdr["ProductDescription_"+culture].ToString();

                            newProduct.ImageSource1 = rdr["ImageSource1"].ToString();
                            newProduct.ImageSource2 = rdr["ImageSource2"].ToString();
                            newProduct.ImageSource3 = rdr["ImageSource3"].ToString();
                            newProduct.ImageSource = rdr["ImageSource"].ToString();
                            newProduct.Price = Convert.ToDecimal(rdr["Price"]);
                            newProduct.PriceDiscounted = rdr["PriceDiscounted"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PriceDiscounted"]);
                            newProduct.CategoryId = Convert.ToInt32(rdr["CategoryId"]);

                            newProductList.Add(newProduct);
                        }
                        return newProductList;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }

        public static void deleteProduct(int id)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_DeleteProduct", sqlConnection))
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
    }
}
