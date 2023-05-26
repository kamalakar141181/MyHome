using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyHome.WebAPI.Business;
using MyHome.WebAPI.Context;

namespace MyHome.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private readonly AppDBContext appDBContext;
        private readonly ILogger<UtilityController> logger;
        private readonly IUtilityBL utilityBL;

        public UtilityController(AppDBContext appDBContext, IUtilityBL utilityBL, ILogger<UtilityController> logger)
        {
            try
            {
                logger.LogInformation("TestProductController() -> Execution Started");
                this.appDBContext = appDBContext;
                this.utilityBL = utilityBL;
                this.logger = logger;
                logger.LogInformation("TestProductController() -> Execution Completed");
            }
            catch (Exception ex)
            {
                logger.LogTrace(ex.Message);
            }
        }

        [HttpGet("GetDropDownListValues")]
        public async Task<IActionResult> GetDropDownListValues(string tableDetails)
        {
            try
            {
                string[] tableInfo = new string[1];
                tableInfo[0] = tableDetails;
                var response = await utilityBL.GetListOfValues(tableInfo);
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
    }
}
