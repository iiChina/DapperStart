using CursoDapper.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CursoDapper 
{
    class Program 
    {
        static void Main(string[] args) 
        {        
            const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$;Encrypt=false";

            using (var connection = new SqlConnection(connectionString))
            {
                // GetCategory(connection);
                // UpdateCategory(connection);
                // ListCategories(connection);
                // CreateManyCategories(connection);
                // ListCategories(connection);
                // CreateCategory(connection);
                ExecuteProcedure(connection);
            }
        }

        static void GetCategory(SqlConnection connection) 
        {
            var category = connection.Query<Category>("SELECT [Id], [Title] FROM [Category] WHERE Id = @Id", new { 
                Id = "af3407aa-11ae-4621-a2ef-2028b85507c4"
            });

            foreach(var item in category)
            {
                Console.WriteLine($"{item.Id} - {item.Title}.");
            }
        }

        static void ListCategories(SqlConnection connection)
        {
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

            foreach(var item in categories) 
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }

        static void CreateCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var insertSql = @"INSERT INTO [Category] VALUES ( 
                    @Id, @Title, @Url, @Summary, @Order, @Description, @Featured
                );";

            var rows = connection.Execute(insertSql, new 
            {
                category.Id, 
                category.Title,
                category.Url,
                category.Summary, 
                category.Order,
                category.Description,
                category.Featured 
            });

            Console.WriteLine($"{rows} linhas inseridas");
        }
    
        static void CreateManyCategories(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Categoria Nova";
            category2.Url = "categoria-nova";
            category2.Description = "Categoria nova";
            category2.Order = 9;
            category2.Summary = "Categoria";
            category2.Featured = true;

            var insertSql = @"INSERT INTO [Category] VALUES ( 
                    @Id, @Title, @Url, @Summary, @Order, @Description, @Featured
                );";

            var rows = connection.Execute(insertSql, new [] {
                 new
                 {
                    category.Id, 
                    category.Title,
                    category.Url,
                    category.Summary, 
                    category.Order,
                    category.Description,
                    category.Featured 
                 },
                  new 
                  {
                    category2.Id, 
                    category2.Title,
                    category2.Url,
                    category2.Summary, 
                    category2.Order,
                    category2.Description,
                    category2.Featured 
                  }
            });

            Console.WriteLine($"{rows} linhas inseridas");
        }

        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = "UPDATE [Category] SET [Title] = @title WHERE [Id] = @Id;";
            var rows = connection.Execute(updateQuery, new 
            {
                Id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
                Title = "Frontend 2021"
            });

            Console.WriteLine($"{rows} registros atualizados.");
        }

        static void ExecuteProcedure(SqlConnection connection) 
        {
            var sql = "[spDeleteStudent]";
            var pars = new { StudentId = "35afa626-15e5-4b60-98dc-a2a91e744887" };

            var affectedRows = connection.Execute(sql, pars, commandType: System.Data.CommandType.StoredProcedure);

            Console.WriteLine($"{affectedRows} linhas afetadas.");
        }

        static void ExecuteReadProcedure(SqlConnection connection) 
        {
            var procedure = "[spGetCoursesByCategory]";
            var pars = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };

            var courses = connection.Query(procedure, pars, commandType: System.Data.CommandType.StoredProcedure); 

            foreach(var course in courses) 
            {
                Console.WriteLine(course.Id);
            }
        }

        static void DeleteCategory(SqlConnection connection) 
        {

        }
    }
}