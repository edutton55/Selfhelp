using ApplicationLogger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoryDAO:ICategoryDAO
    {
         private ILoggerIO logs = new LoggerIO();
        private ISQLDAO dataWriter = new SQLDAO();
        private string connectionString = @"Server=.\SQLEXPRESS;Database=SelfHelp;Trusted_Connection=true;";
        public CategoryDAO(ILoggerIO log)
        {
            logs = log;
        }
        public void GetDataWriter(ISQLDAO dataWriter)
        {
            this.dataWriter = dataWriter;
        }
        public List<CategoryDM> Read(SqlParameter[] parameters, string statement)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);

                    }
                    connection.Open();
                    SqlDataReader data = command.ExecuteReader();
                    List<CategoryDM> categories = new List<CategoryDM>();
                    while (data.Read())
                    {
                        CategoryDM category = new CategoryDM();
                        category.CtgyId = data["CtgyId"].ToString();
                        category.Category = data["Category"].ToString();
                        categories.Add(category);
                    }
                    return categories;
                }
            }
        }
        public List<CategoryDM> GetCategories()
        {
            return Read(null, "GetCategories");
        }
        public void CreateCategory(CategoryDM category)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Category", category.Category)
            };
            dataWriter.Write(parameters, "CreateCategory");
            logs.LogError("Event", "A category has been created", "Class:CategoryDAO, Method: CreateCategory");
        }
        public void RemoveCategoryById(int categoryId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@CtgyId", categoryId)
            };
            dataWriter.Write(parameters, "RemoveCategory");
            logs.LogError("Event", "An category has been removed from the library", "Class:CategoryDAO, Method: RemoveCategory");
        }
        public void EditCategoryById(CategoryDM category)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Category", category.Category)
                ,new SqlParameter("@Id", category.CtgyId)
            };
            dataWriter.Write(parameters, "UpdateCategory");
            logs.LogError("Event", "An category has been updated", "Class: CategoryDAO, Method: UpdateCategory");
        }
        public CategoryDM GetCategoryById(int categoryId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@CtgyId", categoryId)
            };
            return Read(parameters, "GetCategoryById").SingleOrDefault();
        }
    }
}
