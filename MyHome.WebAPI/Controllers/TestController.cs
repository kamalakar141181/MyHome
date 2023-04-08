using Microsoft.AspNetCore.Mvc;
using MyHome.WebAPI.Context;
using MyHome.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MyHome.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly AppDBContext appDBContext;
        public TestController(AppDBContext _appDBContext)
        {
            this.appDBContext = _appDBContext;
        }

        [HttpPost("AddTest")]
        public async Task<IActionResult> AddTest(TestEntity testEntity)
        {
            await appDBContext.tblTest.AddAsync(testEntity);
            await appDBContext.SaveChangesAsync();
            return Ok(testEntity);
        }

        [HttpGet("GetTest")]
        public async Task<IActionResult> GetTest()
        {
            return Ok(await appDBContext.tblTest.ToListAsync());
        }

        [HttpGet("GetTestByID")]
        public async Task<IActionResult> GetTestByID(int id)
        {
            var testEntity = await appDBContext.tblTest.FindAsync(id);
            if (testEntity == null)
            {
                return NotFound();
            }
            return Ok(testEntity);
        }

        [HttpPut("UpdateTest")]
        public async Task<IActionResult> UpdateTest(TestEntity updateTestEntity)
        {
            var testEntity = await appDBContext.tblTest.FindAsync(updateTestEntity.TestColumnID);
            if (testEntity != null)
            {
                testEntity.TestColumnName = updateTestEntity.TestColumnName;
                testEntity.TestColumnDescription = updateTestEntity.TestColumnDescription;
                await appDBContext.SaveChangesAsync();
                return Ok(testEntity);
            }
            return NotFound();
        }

        [HttpDelete("DeleteTest")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            var testEntity = await appDBContext.tblTest.FindAsync(id);
            if (testEntity != null)
            {
                appDBContext.Remove(testEntity);
                appDBContext.SaveChanges();
                return Ok(testEntity);
            }
            return NotFound();
        }

    }
}
