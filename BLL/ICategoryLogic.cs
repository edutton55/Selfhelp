using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ICategoryLogic
    {
        List<CategorySM> GetCategories();
        void CreateCategory(CategorySM category);
        CategorySM GetCategoryById(int tempId);
        void EditCategoryById(CategorySM category);
        string GetCategory(int id);
    }
}
