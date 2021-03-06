﻿using LaserArt.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaserArt.Models
{
    public class Category
    {
        public int? Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryName_AM { get; set; }
        public string CategoryName_RU { get; set; }
        public string CategoryName_EN { get; set; }

        public string CategoryImage { get; set; }

        public static List<Category> GetCategories(int? id)
        {
            return CategoryDAO.getProducts(id);
        }

        public Category SaveCategory()
        {
            return CategoryDAO.saveProduct(this);
        }

        public static void DeleteCategory(int id)
        {
            CategoryDAO.DeleteCategoryByID(id);
        }
    }
}