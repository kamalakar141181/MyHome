using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyHome.WebAPI.Context;
using MyHome.WebAPI.Models;

namespace MyHome.WebAPI.Business
{
    public class TestProductBL : ITestProductBL
    {
        private readonly AppDBContext appDBContext;

        public TestProductBL(AppDBContext dbContext)
        {
            this.appDBContext = dbContext;
        }
        public async Task<int> AddTestProductAsync(TestProductEntity testProductEntity)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Name", testProductEntity.Name));
            parameter.Add(new SqlParameter("@Description", testProductEntity.Description));           

            var result = await Task.Run(() => appDBContext.Database
           .ExecuteSqlRawAsync(@"EXEC spAddTestProduct @Name, @Description", parameter.ToArray()));

            return result;
        }
        public async Task<List<TestProductEntity>> GetTestProductListAsync()
        {
            return await appDBContext.tblTestProduct
                .FromSqlRaw<TestProductEntity>("spGetTestProductList")
                .ToListAsync();
        }
        public async Task<IEnumerable<TestProductEntity>> GetTestProductByIdAsync(int testProductID)
        {
            var param = new SqlParameter("@ID", testProductID);

            var productDetails = await Task.Run(() => appDBContext.tblTestProduct
                            .FromSqlRaw(@"EXEC spGetTestProductByID @ID", param).ToListAsync());

            return productDetails;
        }                
        public async Task<int> UpdateTestProductAsync(TestProductEntity testProductEntity)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ID", testProductEntity.ID));
            parameter.Add(new SqlParameter("@Name", testProductEntity.Name));
            parameter.Add(new SqlParameter("@Description", testProductEntity.Description));
            

            var result = await Task.Run(() => appDBContext.Database
            .ExecuteSqlRawAsync(@"EXEC spUpdateTestProduct @ID, @Name, @Description", parameter.ToArray()));
            return result;
        }
        public async Task<int> DeleteTestProductAsync(int testProductID)
        {
            return await Task.Run(() => appDBContext.Database.ExecuteSqlInterpolatedAsync($"spDeleteTestProduct {testProductID}"));
        }
    }
}
