using MyHome.WebAPI.Models;

namespace MyHome.WebAPI.Business
{
    public interface ITestProductBL
    {
        Task<int> AddTestProductAsync(TestProductEntity testProductEntity);
        Task<List<TestProductEntity>> GetTestProductListAsync();
        Task<IEnumerable<TestProductEntity>> GetTestProductByIdAsync(int testProductID);        
        Task<int> UpdateTestProductAsync(TestProductEntity testProductEntity);
        Task<int> DeleteTestProductAsync(int testProductID);
    }
}
