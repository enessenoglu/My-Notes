using Notlarim102.BusinessLayer;
using Notlarim102.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Notlarim102.WepApp.Models
{
    public class CacheHelper
    {
        public static List<Category> GetCategoriesFromCache()
        {
            var result = WebCache.Get("category-cache");
            if (result==null)
            {
                CategoryManager cm = new CategoryManager();
                result = cm.List();
                WebCache.Set("category-cache",result,30,true);
            }
            return result;
        }
        public static void Remove(string key)
        {
            WebCache.Remove(key);
        }
        public static void RemoveCatFromCache()
        {
            Remove("category-cache");
        }
    }
}