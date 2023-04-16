using Microsoft.AspNetCore.Mvc;
using MyHome.WebAPI.Business;
using MyHome.WebAPI.Models;
using Newtonsoft.Json;

namespace MyHome.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestProductUsingADODotNetController : ControllerBase
    {
        private readonly ITestProductBLUsingADODotNet testProductBLUsingADODotNet;
        private readonly ILogger<TestProductController> logger;

        #region JSON Serialization Settings
        JsonSerializerSettings setting = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };
        #endregion

        #region Constructor
        public TestProductUsingADODotNetController(ITestProductBLUsingADODotNet testProductBLUsingADODotNet, ILogger<TestProductController> logger)
        {
            this.testProductBLUsingADODotNet = testProductBLUsingADODotNet;
            this.logger = logger;

            logger.LogDebug("LogDebug -> TestProductUsingADODotNetController");
            logger.LogInformation("LogInformation -> TestProductUsingADODotNetController");
            logger.LogWarning("LogWarning -> TestProductUsingADODotNetController");
            logger.LogError("LogError -> TestProductUsingADODotNetController");
        }
        #endregion

        #region Action Methods
        [HttpPost("AddTestProduct-ADO.NET")]
        public async Task<IActionResult> AddTestProduct(TestProductEntity testProductEntity)
        {
            if (testProductEntity == null)
            {
                return BadRequest();
            }

            try
            {
                logger.LogTrace(JsonConvert.SerializeObject(testProductEntity, Formatting.Indented, setting));
                var response = await testProductBLUsingADODotNet.AddTestProduct(testProductEntity);
                return Ok(response);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("GetProductList-ADO.NET")]
        public async Task<List<TestProductEntity>> GetProductList()
        {
            try
            {
                return await testProductBLUsingADODotNet.GetTestProductList();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("GetProductByID-ADO.NET")]
        public async Task<IEnumerable<TestProductEntity>> GetProductByID(int testProductID)
        {
            try
            {
                logger.LogTrace("Test Product ID is : {0} ",JsonConvert.SerializeObject(testProductID, Formatting.Indented, setting));
                var response = await testProductBLUsingADODotNet.GetTestProductByID(testProductID);

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

        [HttpPut("UpdateTestProduct-ADO.NET")]
        public async Task<IActionResult> UpdateTestProduct(TestProductEntity testProductEntity)
        {
            if (testProductEntity == null)
            {
                return BadRequest();
            }

            try
            {
                logger.LogTrace(JsonConvert.SerializeObject(testProductEntity, Formatting.Indented, setting));
                var result = await testProductBLUsingADODotNet.UpdateTestProduct(testProductEntity);
                return Ok(result);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("DeleteTestProduct-ADO.NET")]
        public async Task<int> DeleteTestProduct(int testProductID)
        {
            try
            {
                logger.LogTrace("Test Product ID is : {0} ", JsonConvert.SerializeObject(testProductID, Formatting.Indented, setting));
                var response = await testProductBLUsingADODotNet.DeleteTestProduct(testProductID);
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

