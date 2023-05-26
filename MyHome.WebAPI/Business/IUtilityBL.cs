using MyHome.WebAPI.Models;

namespace MyHome.WebAPI.Business
{
    public interface IUtilityBL
    {
        Task<IEnumerable<KeyValuePairEntity>> GetListOfValues(string[] tableInfo);
    }
}
