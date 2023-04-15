using Microsoft.AspNetCore.Mvc;
using MyHome.WebAPI.Context;
using MyHome.WebAPI.Models;
using MyHome.WebAPI.Business;

namespace MyHome.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestProductController : ControllerBase
    {
        private readonly AppDBContext appDBContext;
        private readonly ITestProductBL testProductBL;
        private readonly ILogger<TestProductController> logger;
        public TestProductController(AppDBContext appDBContext, ITestProductBL testProductBL, ILogger<TestProductController> logger)
        {
            this.appDBContext = appDBContext;
            this.testProductBL = testProductBL;
            this.logger = logger;

            logger.LogDebug("LogDebug -> TestProductController");
            logger.LogInformation("LogInformation -> TestProductController");
            logger.LogWarning("LogWarning -> TestProductController");
            logger.LogError("LogError -> TestProductController");
        }

        [HttpPost("AddTestProductAsync")]
        public async Task<IActionResult> AddTestProductAsync(TestProductEntity testProductEntity)
        {
            if (testProductEntity == null)
            {
                return BadRequest();
            }

            try
            {
                var response = await testProductBL.AddTestProductAsync(testProductEntity);

                return Ok(response);
            }
            catch
            {
                throw;
            }
        }
        [HttpGet("getproductlist")]
        public async Task<List<TestProductEntity>> GetProductListAsync()
        {
            try
            {
                return await testProductBL.GetTestProductListAsync();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("GetProductByIdAsync")]
        public async Task<IEnumerable<TestProductEntity>> GetProductByIdAsync(int testProductID)
        {
            try
            {
                var response = await testProductBL.GetTestProductByIdAsync(testProductID);

                if (response == null)
                {
                    return null;
                }

                return response;
            }
            catch
            {
                throw;
            }
        }
        
        [HttpPut("UpdateTestProductAsync")]
        public async Task<IActionResult> UpdateTestProductAsync(TestProductEntity testProductEntity)
        {
            if (testProductEntity == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await testProductBL.UpdateTestProductAsync(testProductEntity);
                return Ok(result);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("DeleteTestProductAsync")]
        public async Task<int> DeleteTestProductAsync(int testProductID)
        {
            try
            {
                var response = await testProductBL.DeleteTestProductAsync(testProductID);
                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
