using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace MyHome.WebAPI.Models
{
    public class TestEntity
    {
        [Key]
        public int TestColumnID { get; set; }
        public string TestColumnName { get; set; }
        public string TestColumnDescription { get; set; }
        
    }
}
