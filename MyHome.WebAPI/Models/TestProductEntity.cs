using System.ComponentModel.DataAnnotations;

namespace MyHome.WebAPI.Models
{
    public class TestProductEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
    }
}
