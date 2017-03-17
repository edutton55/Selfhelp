using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ICategoryDAO
    {
        void GetDataWriter(ISQLDAO dataWriter);
        List<CategoryDM> Read(SqlParameter[] parameters, string statement);
        List<CategoryDM> GetCategories();
        void CreateCategory(CategoryDM category);
        void RemoveCategoryById(int categoryId);
        void EditCategoryById(CategoryDM category);
        CategoryDM GetCategoryById(int categoryId);
    }
}
