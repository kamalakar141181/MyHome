using MyHome.WebAPI.Models;

namespace MyHome.WebAPI.Business
{
    public interface ITestProductBLUsingADODotNet
    {
        Task<int> AddTestProduct(TestProductEntity testProductEntity);
        Task<List<TestProductEntity>> GetTestProductList();
        Task<IEnumerable<TestProductEntity>> GetTestProductByID(int testProductID);
        Task<int> UpdateTestProduct(TestProductEntity testProductEntity);
        Task<int> DeleteTestProduct(int testProductID);
    }
}
