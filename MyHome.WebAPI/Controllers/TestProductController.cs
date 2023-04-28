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
            catch (Exception ex)
            {
                logger.LogTrace(ex.Message);               
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
            catch (Exception ex)
            {
                logger.LogTrace(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getproductlist")]
        public async Task<IActionResult> GetProductListAsync()
        {
            try
            {
                var response = await testProductBL.GetTestProductListAsync();

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

        [HttpGet("GetProductByIdAsync")]
        public async Task<IActionResult> GetProductByIdAsync(int testProductID)
        {
            try
            {
                logger.LogTrace("Test Product ID is : {0} ", JsonConvert.SerializeObject(testProductID, Formatting.Indented, setting));
                var response = await testProductBL.GetTestProductByIdAsync(testProductID);

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
        
        [HttpPut("UpdateTestProductAsync")]
        public async Task<IActionResult> UpdateTestProductAsync(TestProductEntity testProductEntity)
        {
            if (testProductEntity == null)
            {
                return BadRequest("Inputs not received");
            }
            try
            {
                logger.LogTrace(JsonConvert.SerializeObject(testProductEntity, Formatting.Indented, setting));
                var response = await testProductBL.UpdateTestProductAsync(testProductEntity);
                return Ok(response);
            }
            catch( Exception ex )
            {
                logger.LogTrace(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteTestProductAsync")]
        public async Task<IActionResult> DeleteTestProductAsync(int testProductID)
        {
            if (testProductID < 0)
            {
                return BadRequest("Enter valid Product ID");
            }
            try
            {
                logger.LogTrace("Test Product ID is : {0} ", JsonConvert.SerializeObject(testProductID, Formatting.Indented, setting));
                var response = await testProductBL.DeleteTestProductAsync(testProductID);
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
