using Microsoft.AspNetCore.Mvc;
using MyHome.WebAPI.Context;
using MyHome.WebAPI.Models;
using MyHome.WebAPI.Business;
using Newtonsoft.Json;

namespace MyHome.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestProductController : ControllerBase
    {
        private readonly AppDBContext appDBContext;
        private readonly ITestProductBL testProductBL;
        private readonly ILogger<TestProductController> logger;

        #region JSON Serialization Settings
        JsonSerializerSettings setting = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };
        #endregion

        #region Constructor
        public TestProductController(AppDBContext appDBContext, ITestProductBL testProductBL, ILogger<TestProductController> logger)
        {
            try
            {
                logger.LogInformation("TestProductController() -> Execution Started");
                this.appDBContext = appDBContext;
                this.testProductBL = testProductBL;
                this.logger = logger;
                logger.LogInformation("TestProductController() -> Execution Completed");
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Action Methods
        [HttpPost("AddTestProductAsync")]
        public async Task<IActionResult> AddTestProductAsync(TestProductEntity testProductEntity)
        {
            if (testProductEntity == null)
            {
                return BadRequest();
            }

            try
            {
                logger.LogTrace(JsonConvert.SerializeObject(testProductEntity, Formatting.Indented, setting));
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
                logger.LogTrace("Test Product ID is : {0} ", JsonConvert.SerializeObject(testProductID, Formatting.Indented, setting));
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
                logger.LogTrace(JsonConvert.SerializeObject(testProductEntity, Formatting.Indented, setting));
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
                logger.LogTrace("Test Product ID is : {0} ", JsonConvert.SerializeObject(testProductID, Formatting.Indented, setting));
                var response = await testProductBL.DeleteTestProductAsync(testProductID);
                return response;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
