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
                return BadRequest("Inputs not received");
            }
            try
            {
                logger.LogTrace(JsonConvert.SerializeObject(testProductEntity, Formatting.Indented, setting));
                var response = await testProductBLUsingADODotNet.AddTestProduct(testProductEntity);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogTrace(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProductList-ADO.NET")]
        public async Task<IActionResult> GetProductList()
        {
            try
            {
                var response = await testProductBLUsingADODotNet.GetTestProductList();
                if (response == null)
                {
                    return BadRequest("No records found");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogTrace(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProductByID-ADO.NET")]
        public async Task<IActionResult> GetProductByID(int testProductID)
        {
            if (testProductID < 0)
            {
                return BadRequest("Enter valid Product ID");
            }
            try
            {
                logger.LogTrace("Test Product ID is : {0} ",JsonConvert.SerializeObject(testProductID, Formatting.Indented, setting));
                var response = await testProductBLUsingADODotNet.GetTestProductByID(testProductID);
                if (response == null)
                {
                    return BadRequest("No records found");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogTrace(ex.Message);
                return BadRequest(ex.Message);
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
                var response = await testProductBLUsingADODotNet.UpdateTestProduct(testProductEntity);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogTrace(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteTestProduct-ADO.NET")]
        public async Task<IActionResult> DeleteTestProduct(int testProductID)
        {
            try
            {
                logger.LogTrace("Test Product ID is : {0} ", JsonConvert.SerializeObject(testProductID, Formatting.Indented, setting));
                var response = await testProductBLUsingADODotNet.DeleteTestProduct(testProductID);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogTrace(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}

