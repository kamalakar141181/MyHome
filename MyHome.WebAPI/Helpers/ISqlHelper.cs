using Dapper;
using System.Data;

namespace MyHome.WebAPI.Helpers
{
    public interface ISqlHelper
    {
        void OpenConnection();
        void CloseConnection();
        void CommitTransaction();
        void RollbackTransaction();
        void StartTransaction();        
        DynamicParameters CreateParameter(string name, object value);
        DynamicParameters CreateParameter(string name, object value, DbType? dbType);
        DynamicParameters CreateParameter(string name, object value, DbType? dbType, ParameterDirection? direction);
        DynamicParameters CreateParameter(string name, object value, DbType? dbType, ParameterDirection? direction, int? size);
        DynamicParameters CreateParameter(string name, object value = null, DbType? dbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null);
        List<T> GetData<T>(string query);
        List<T> GetData<T>(string query, DynamicParameters dynamicParameters);
        List<T> GetData<T>(string storedProcedure, DynamicParameters dynamicParameters, CommandType commandType = CommandType.StoredProcedure);
        T GetScalar<T>(string storedProcedure, DynamicParameters dynamicParameters, CommandType commandType = CommandType.StoredProcedure);
        void SaveData(string storedProcedure, DynamicParameters dynamicParameters);
        void SaveData<T>(string storedProcedure, DynamicParameters dynamicParameters, T model);
        void SaveDataInTransaction<T>(string storedProcedure, DynamicParameters dynamicParameters);
        void SaveDataInTransaction<T>(string storedProcedure, T model);
        TResult DoTransaction<TResult>(Func<IDbTransaction, TResult> task, IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        DynamicParameters GenerateParametersFromModel(object model, bool generateId);        
    }
}
