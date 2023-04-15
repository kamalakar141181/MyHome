using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyHome.WebAPI.Business;
using MyHome.WebAPI.Helpers;
using MyHome.WebAPI.Models;
using System.Net;
using System.Xml;

namespace MyHome.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestProductUsingADODotNetController : ControllerBase
    {
        private readonly ITestProductBLUsingADODotNet testProductBLUsingADODotNet;

        public TestProductUsingADODotNetController(ITestProductBLUsingADODotNet testProductBLUsingADODotNet)
        {
            this.testProductBLUsingADODotNet = testProductBLUsingADODotNet;
        }
        [HttpPost("AddTestProduct-ADO.NET")]
        public async Task<IActionResult> AddTestProduct(TestProductEntity testProductEntity)
        {
            if (testProductEntity == null)
            {
                return BadRequest();
            }

            try
            {
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
                var response = await testProductBLUsingADODotNet.DeleteTestProduct(testProductID);
                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}

