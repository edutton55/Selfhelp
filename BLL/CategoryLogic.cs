using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ApplicationLogger;

namespace BLL
{
    public class CategoryLogic:ICategoryLogic
    {
        private ICategoryDAO categoryData;
        private static ILoggerIO logs;

        public CategoryLogic(ICategoryDAO categoryDAO, ISQLDAO dao, ILoggerIO log)
        {
            logs = log;
            categoryData = categoryDAO;   //injecting dependency
            categoryData.GetDataWriter(dao);//dependency injector through a infrastructure
        }
        public List<CategorySM> GetCategories()
        {
            return Map(categoryData.GetCategories());
        }
        private CategorySM Map(CategoryDM category)
        {
            CategorySM video = new CategorySM();
            video.CtgyId = Convert.ToInt32(category.CtgyId);
            video.Category = category.Category;
            return video;
        }
        private CategoryDM Map(CategorySM category)
        {
            CategoryDM video = new CategoryDM();
            video.CtgyId = category.CtgyId.ToString();
            video.Category = category.Category;
            return video;
        }
        private List<CategorySM> Map(List<CategoryDM> categories)
        {
            List<CategorySM> divs = new List<CategorySM>();
            foreach (CategoryDM category in categories)
            {
                divs.Add(Map(category));
            }
            return divs;
        }
        private List<CategoryDM> Map(List<CategorySM> categories)
        {
            List<CategoryDM> categorys = new List<CategoryDM>();
            foreach (CategorySM category in categories)
            {
                categorys.Add(Map(category));
            }
            return categorys;
        }
        public void CreateCategory(CategorySM category)
        {
            try
            {
                categoryData.CreateCategory(Map(category));
                logs.LogError("Event ", "User was able to create new item ", "Class:CategoryLogic, Method:CreateCategory");
            }
            catch (Exception P)
            {
                logs.LogError("Error ", "User was unable to create a new item ", "Class:UserLogic, Method:CreateCategory");
            }
        }
        public CategorySM GetCategoryById(int tempId) 
        {
            return Map(categoryData.GetCategoryById(tempId));
        }

        public void EditCategoryById(CategorySM category)
        {
            try
            {
                categoryData.EditCategoryById(Map(category));
                logs.LogError("Event ", "User was able to update Category", "Class:CategoryLogic, Method:UpdateCategoryById");
            }
            catch (Exception e) 
            {
                logs.LogError("Error ", "User was unable to update Category", "Class:CategoryLogic, Method:UpdateCategoryById");
            }
        }
        public string GetCategory(int id)
        {
            CategorySM category = Map(categoryData.GetCategoryById(id));
            return category.Category;
        }
    }
}
